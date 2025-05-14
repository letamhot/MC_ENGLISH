using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AxWMPLib;
using MC_Project.Model;

namespace MC_Project
{
    public partial class ucToaSang : UserControl
    {
        private Socket _socket;
        private int _doiid = 0;
        private int _cauhoiid = 0;
        private int _goicauhoiid = 0;
        private int[] _ttgoi;
        private bool _isVideoStart = false;
        private bool _tt = false;
        private bool _x2 = false;
        private string currentPath = Directory.GetCurrentDirectory();
        QuaMienDiSanEntities _entities = new QuaMienDiSanEntities();
        public ucToaSang()
        {
            InitializeComponent();
        }

        public ucToaSang(Socket sock, int doiid, int cauhoiid, bool isStart, bool x2, bool da, bool trangthai, bool resetgoi)
        {
            InitializeComponent();
            _socket = sock;
            _doiid = doiid;
            _cauhoiid = cauhoiid;
            _isVideoStart = isStart;
            _x2 = x2;
            loadUC();
        }

        private void loadUC()
        {
            ds_doi doiDangChoi = _entities.ds_doi.Find(_doiid);

            if (_cauhoiid == 0)
            {
                VisibleGui();
                lblThele.Text = "Four Candidates will answer five questions";
                // Reset all question states when starting new round
                ResetAllQuestionStates();
            }
            else
            {
                EnabledGui();
                lblThele.Visible = true;
                if (_cauhoiid > 0)
                {
                    ds_goicauhoishining vd = _entities.ds_goicauhoishining.Find(_cauhoiid);
                    _entities.Entry(vd).Reload(); // ⚠️ Nạp lại từ DB



                    // Then display the current question
                    disPlayVeDich(_cauhoiid, (int)vd.vitri, _x2);
                    loadNutDangChon(_cauhoiid, _x2);
                    loadNutDaChon(_cauhoiid);
                    // First update all question states based on database
                    UpdateAllQuestionStates();
                    lblThele.Text = "Question " + vd.vitri + ": (" + vd.sodiem + " points)";

                    if ((bool)!vd.isvideo)
                    {
                        if (vd.noidungcauhoi.Length > 200)
                        {
                            lblNoiDungCauHoiVD.Font = new Font("Arial", 20);
                        }
                        else if (vd.noidungcauhoi.Length >= 1 && vd.noidungcauhoi.Length < 30)
                        {
                            lblNoiDungCauHoiVD.Font = new Font("Arial", 28);

                        }
                        else
                        {
                            lblNoiDungCauHoiVD.Font = new Font("Arial", 22);

                        }
                        lblNoiDungCauHoiVD.Text = vd.noidungcauhoi;
                        lblDA.Visible = true;
                        labelDA.Visible = true;
                        lblDA.Text = vd.dapan;
                        if (vd.dapan.Length > 0 && vd.dapan.Length < 10)
                        {
                            lblDA.Font = new Font("Arial", 28, FontStyle.Bold);

                        }
                        else if (vd.dapan.Length > 130)
                        {
                            lblDA.Font = new Font("Arial", 16, FontStyle.Bold);

                        }
                        else
                        {
                            lblDA.Font = new Font("Arial", 20, FontStyle.Bold);

                        }
                        pbImage.Visible = false;
                        axWinCauHoiHinhAnh.Visible = false;

                    }
                    else
                    {
                        lblNoiDungCauHoiVD.Visible = true;
                        if (vd.noidungcauhoi.Length > 200)
                        {
                            lblNoiDungCauHoiVD.Font = new Font("Arial", 18);
                        }
                        else if (vd.noidungcauhoi.Length >= 1 && vd.noidungcauhoi.Length < 30)
                        {
                            lblNoiDungCauHoiVD.Font = new Font("Arial", 28);

                        }
                        else
                        {
                            lblNoiDungCauHoiVD.Font = new Font("Arial", 20);

                        }
                        lblNoiDungCauHoiVD.Text = vd.noidungcauhoi;
                        if (_tt)
                        {
                            if (vd.urlhinhanh != null && vd.urlhinhanh != "")
                            {
                                var url = vd.urlhinhanh.Split('.');
                                if (url.Length > 0)
                                {
                                    if (url[1] == "png" || url[1] == "jpg")
                                    {
                                        pbImage.Visible = true;
                                        axWinCauHoiHinhAnh.Visible = false;

                                        pbImage.BackgroundImage = Image.FromFile(currentPath + "\\Resources\\pic\\" + vd.urlhinhanh);
                                        pbImage.BackgroundImageLayout = ImageLayout.Stretch;

                                        lblDA.Visible = true;
                                        labelDA.Visible = true;
                                        lblDA.Text = vd.dapan;

                                    }
                                    else
                                    {
                                        axWinCauHoiHinhAnh.uiMode = "none";
                                        axWinCauHoiHinhAnh.Visible = true;
                                        axWinCauHoiHinhAnh.URL = currentPath + "\\Resources\\Video\\" + vd.urlhinhanh;
                                        axWinCauHoiHinhAnh.Ctlcontrols.play();
                                        lblDA.Visible = true;
                                        labelDA.Visible = true;
                                        lblDA.Text = vd.dapan;
                                        if (vd.dapan.Length > 0 && vd.dapan.Length < 10)
                                        {
                                            lblDA.Font = new Font("Arial", 28, FontStyle.Bold);

                                        }
                                        else if (vd.dapan.Length > 130)
                                        {
                                            lblDA.Font = new Font("Arial", 16, FontStyle.Bold);

                                        }
                                        else
                                        {
                                            lblDA.Font = new Font("Arial", 20, FontStyle.Bold);

                                        }
                                    }
                                }
                            }

                        }

                        if (vd.urlhinhanh != null && vd.urlhinhanh != "")
                        {
                            var url = vd.urlhinhanh.Split('.');
                            if (url.Length > 0)
                            {
                                if (url[1] == "png" || url[1] == "jpg")
                                {
                                    pbImage.Visible = true;
                                    axWinCauHoiHinhAnh.Visible = false;

                                    pbImage.BackgroundImage = Image.FromFile(currentPath + "\\Resources\\pic\\" + vd.urlhinhanh);
                                    pbImage.BackgroundImageLayout = ImageLayout.Stretch;

                                    lblDA.Visible = true;
                                    labelDA.Visible = true;
                                    lblDA.Text = vd.dapan;
                                    if (vd.dapan.Length > 0 && vd.dapan.Length < 10)
                                    {
                                        lblDA.Font = new Font("Arial", 28, FontStyle.Bold);

                                    }
                                    else if (vd.dapan.Length > 130)
                                    {
                                        lblDA.Font = new Font("Arial", 16, FontStyle.Bold);

                                    }
                                    else
                                    {
                                        lblDA.Font = new Font("Arial", 20, FontStyle.Bold);

                                    }

                                }
                                else
                                {
                                    pbImage.Visible = false;


                                    if (_isVideoStart)
                                    {
                                        axWinCauHoiHinhAnh.uiMode = "none";

                                        axWinCauHoiHinhAnh.Visible = true;
                                        axWinCauHoiHinhAnh.URL = currentPath + "\\Resources\\Video\\" + vd.urlhinhanh;
                                        axWinCauHoiHinhAnh.Ctlcontrols.play();
                                        lblDA.Visible = true;
                                        labelDA.Visible = true;
                                        lblDA.Text = vd.dapan;
                                        if (vd.dapan.Length > 0 && vd.dapan.Length < 10)
                                        {
                                            lblDA.Font = new Font("Arial", 28, FontStyle.Bold);

                                        }
                                        else if (vd.dapan.Length > 130)
                                        {
                                            lblDA.Font = new Font("Arial", 16, FontStyle.Bold);

                                        }
                                        else
                                        {
                                            lblDA.Font = new Font("Arial", 20, FontStyle.Bold);

                                        }

                                    }
                                    else
                                    {
                                        axWinCauHoiHinhAnh.uiMode = "none";

                                        axWinCauHoiHinhAnh.Visible = false;
                                        axWinCauHoiHinhAnh.URL = currentPath + "\\Resources\\Video\\" + vd.urlhinhanh;
                                        axWinCauHoiHinhAnh.Ctlcontrols.stop();
                                        lblDA.Visible = true;
                                        labelDA.Visible = true;
                                        lblDA.Text = vd.dapan;
                                        if (vd.dapan.Length > 0 && vd.dapan.Length < 10)
                                        {
                                            lblDA.Font = new Font("Arial", 28, FontStyle.Bold);

                                        }
                                        else if (vd.dapan.Length > 130)
                                        {
                                            lblDA.Font = new Font("Arial", 16, FontStyle.Bold);

                                        }
                                        else
                                        {
                                            lblDA.Font = new Font("Arial", 20, FontStyle.Bold);

                                        }

                                    }


                                }
                            }

                        }
                    }
                }
                else
                {
                    EnabledGui1();
                    labelDA.Visible = false;
                    lblDA.Visible = false;
                }
            }
        }
        // Danh sách các ID câu hỏi đã hiển thị trước đó
        private HashSet<int> dsCauHoiDaHienThi = new HashSet<int>();
        private void ResetAllQuestionStates()
        {
            // Reset all picture boxes to default state
            pbGoi1.BackgroundImage = System.Drawing.Image.FromFile(currentPath + "\\Resources\\group4\\1-ac.png");
            pbGoi2.BackgroundImage = System.Drawing.Image.FromFile(currentPath + "\\Resources\\group4\\2-ac.png");
            pbGoi3.BackgroundImage = System.Drawing.Image.FromFile(currentPath + "\\Resources\\group4\\3-ac.png");
            pbGoi4.BackgroundImage = System.Drawing.Image.FromFile(currentPath + "\\Resources\\group4\\4-ac.png");
            pbGoi5.BackgroundImage = System.Drawing.Image.FromFile(currentPath + "\\Resources\\group4\\5-ac.png");

            // Clear the displayed questions cache
            dsCauHoiDaHienThi.Clear();
        }

        private void UpdateAllQuestionStates()
        {
            // Get all questions from database
            var allQuestions = _entities.ds_goicauhoishining.ToList();

            foreach (var question in allQuestions)
            {
                _entities.Entry(question).Reload(); // Nạp lại từng dòng mới nhất từ DB

                switch (question.trangThai)
                {
                    case 0: // Not selected
                        SetQuestionImage((int)question.vitri, "ac");
                        break;
                    case 1: // Currently selected
                        SetQuestionImage((int)question.vitri, _x2 ? "star" : "in");
                        break;
                    case 2: // Already answered
                        SetQuestionImage((int)question.vitri, "dis");
                        break;
                }
            }
        }

        private void SetQuestionImage(int vitri, string state)
        {
            string imagePath = $"{currentPath}\\Resources\\group4\\{vitri}-{state}.png";
            switch (vitri)
            {
                case 1:
                    pbGoi1.BackgroundImageLayout = ImageLayout.Stretch;
                    pbGoi1.BackgroundImage = System.Drawing.Image.FromFile(imagePath);
                    break;
                case 2:
                    pbGoi2.BackgroundImageLayout = ImageLayout.Stretch;
                    pbGoi2.BackgroundImage = System.Drawing.Image.FromFile(imagePath);
                    break;
                case 3:
                    pbGoi3.BackgroundImageLayout = ImageLayout.Stretch;
                    pbGoi3.BackgroundImage = System.Drawing.Image.FromFile(imagePath);
                    break;
                case 4:
                    pbGoi4.BackgroundImageLayout = ImageLayout.Stretch;
                    pbGoi4.BackgroundImage = System.Drawing.Image.FromFile(imagePath);
                    break;
                case 5:
                    pbGoi5.BackgroundImageLayout = ImageLayout.Stretch;
                    pbGoi5.BackgroundImage = System.Drawing.Image.FromFile(imagePath);
                    break;
            }
        }

        void disPlayVeDich(int cauhoiid, int vitri, bool isX2)
        {
            var cauhoiTS = _entities.ds_goicauhoishining.Find(cauhoiid);
            if (cauhoiTS != null)
            {
                // Always update the display based on current state
                switch (cauhoiTS.trangThai)
                {
                    case 1: // Currently selected
                        SetQuestionImage(vitri, isX2 ? "star" : "in");
                        break;
                    case 2: // Already answered
                        SetQuestionImage(vitri, "dis");
                        break;
                    default: // Not selected
                        SetQuestionImage(vitri, "ac");
                        break;
                }
            }
        }

        private void loadNutDangChon(int cauhoiid, bool isX2)
        {
            var dsCauChon = _entities.ds_goicauhoishining
                .Where(x => x.cauhoiid == cauhoiid && x.trangThai == 1)
                .ToList();

            foreach (var cauHoi in dsCauChon)
            {
                SetQuestionImage((int)cauHoi.vitri, isX2 ? "star" : "in");
            }
        }

        private void loadNutDaChon(int cauhoiid)
        {
            var dsCauDaChon = _entities.ds_goicauhoishining
                .Where(x => x.cauhoiid == cauhoiid && x.trangThai == 2)
                .ToList();

            foreach (var cauHoi in dsCauDaChon)
            {
                SetQuestionImage((int)cauHoi.vitri, _x2 ? "star" : "dis");
            }
        }
        public void VisibleGui()
        {
            //lblGioiThieu.Visible = true;
            lblThele.Visible = true;
            lblNoiDungCauHoiVD.Visible = false;
            labelDA.Visible = false;
            lblDA.Visible = false;
            pbGoi1.Visible = true;
            pbGoi2.Visible = true;
            pbGoi3.Visible = true;
            pbGoi4.Visible = true;
            pbGoi5.Visible = true;
            /*pbGoi6.Visible = true;*/

        }
        public void EnabledGui()
        {
            //lblGioiThieu.Visible = false;
            lblThele.Visible = true;
            lblNoiDungCauHoiVD.Visible = true;
            pbGoi1.Visible = true;
            pbGoi2.Visible = true;
            pbGoi3.Visible = true;
            pbGoi4.Visible = true;
            pbGoi5.Visible = true;
            /*pbGoi6.Visible = true;*/

        }
        public void EnabledGui1()
        {
            //lblGioiThieu.Visible = false;
            lblThele.Visible = true;
            lblNoiDungCauHoiVD.Visible = false;
            pbGoi1.Visible = true;
            pbGoi2.Visible = true;
            pbGoi3.Visible = true;
            pbGoi4.Visible = true;
            pbGoi5.Visible = true;
            /*pbGoi6.Visible = true;*/

        }

        
    }
}
