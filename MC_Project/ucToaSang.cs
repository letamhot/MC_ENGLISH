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
            }
            else
            {
                EnabledGui();
                lblThele.Visible = true;
                if (_cauhoiid > 0)
                {
             
                    ds_goicauhoishining vd = _entities.ds_goicauhoishining.Find(_cauhoiid);
                    disPlayVeDich(_goicauhoiid, (int)vd.vitri, _x2);
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
                        if(vd.dapan.Length >0 && vd.dapan.Length < 10)
                        {
                            lblDA.Font = new Font("Arial", 28, FontStyle.Bold);

                        }
                        else if(vd.dapan.Length > 130)
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
                    //lblGioiThieu.Text = "Mỗi thí sinh sẽ chọn 01 gói câu hỏi trong bộ 06 gói câu hỏi của BTC. Nội dung câu hỏi liên quan đến các kiến thức chung.\nMỗi bộ câu hỏi gồm 04 câu, trong đó: Câu 1: 10 điểm, câu 2: 20 điểm, câu 3: 20 điểm, câu 4: 30 điểm.\nThời gian suy nghĩ và trả lời cho mỗi câu hỏi là 10 giây.\nNếu thí sinh chọn gói câu hỏi trả lời đúng thì dành điểm tuyệt đối, trả lời sai các thí sinh còn lại dành quyền trả lời. Nếu thí sinh dành quyền trả lời mà trả lời đúng thì sẽ dành được điểm tuyệt đối, thí sinh chọn gói câu hỏi sẽ bị trừ số điểm tương ứng của câu hỏi đó.\nNếu thí sinh dành câu hỏi trả lời mà trả lời sai thì bị trừ 1 nửa số điểm của câu hỏi. Thí sinh chọn gói câu hỏi không bị trừ điểm.\nThí sinh có quyền 01 lần đặt cược để nhân đôi số điểm của mình. Khi đặt cược mà trả lời sai sẽ bị trừ số điểm đã nhân cược.Điểm tối đa cho mỗi thí sinh ở phần thi này: 100 điểm(chưa kể điểm cược).";
                    //flpNoiDung.Visible = true;
                    labelDA.Visible = false;
                    lblDA.Visible = false;

                }
                loadNutDaChon(_cauhoiid);
                //disableGoiCauHoiKhoiDong(_goicauhoiid);
            }
        }
        // Danh sách các ID câu hỏi đã hiển thị trước đó
        private HashSet<int> dsCauHoiDaHienThi = new HashSet<int>();
        void disPlayVeDich(int cauhoiid, int vitri, bool isX2)
        {
            var cauhoiTS = _entities.ds_goicauhoishining.Find(cauhoiid);
            if (cauhoiTS != null)
            {
                if (cauhoiTS.vitri == vitri)
                {
                   
                    if (cauhoiTS.trangThai == 1)
                    {
                        if (isX2)
                        {

                            switch (cauhoiTS.vitri)
                            {
                                case 1:
                                    pbGoi1.SizeMode = PictureBoxSizeMode.StretchImage;
                                    pbGoi1.Image = System.Drawing.Image.FromFile(currentPath + "\\Resources\\group4\\ts1_ns.png");
                                    break;
                                case 2:
                                    pbGoi2.SizeMode = PictureBoxSizeMode.StretchImage;
                                    pbGoi2.Image = System.Drawing.Image.FromFile(currentPath + "\\Resources\\group4\\ts2_ns.png");
                                    break;
                                case 3:
                                    pbGoi3.SizeMode = PictureBoxSizeMode.StretchImage;
                                    pbGoi3.Image = System.Drawing.Image.FromFile(currentPath + "\\Resources\\group4\\ts3_ns.png"); ;
                                    break;
                                case 4:
                                    pbGoi4.SizeMode = PictureBoxSizeMode.StretchImage;
                                    pbGoi4.Image = System.Drawing.Image.FromFile(currentPath + "\\Resources\\group4\\ts4_ns.png"); ;
                                    break;
                                case 5:
                                    pbGoi5.SizeMode = PictureBoxSizeMode.StretchImage;
                                    pbGoi5.Image = System.Drawing.Image.FromFile(currentPath + "\\Resources\\group4\\ts5_ns.png"); ;
                                    break;

                            }
                        }
                        else
                        {
                            switch (cauhoiTS.vitri)
                            {
                                case 1:
                                    pbGoi1.SizeMode = PictureBoxSizeMode.StretchImage;
                                    pbGoi1.Image = System.Drawing.Image.FromFile(currentPath + "\\Resources\\group4\\ts1_in.png");
                                    break;
                                case 2:
                                    pbGoi2.SizeMode = PictureBoxSizeMode.StretchImage;
                                    pbGoi2.Image = System.Drawing.Image.FromFile(currentPath + "\\Resources\\group4\\ts2_in.png");
                                    break;
                                case 3:
                                    pbGoi3.SizeMode = PictureBoxSizeMode.StretchImage;
                                    pbGoi3.Image = System.Drawing.Image.FromFile(currentPath + "\\Resources\\group4\\ts3_in.png"); ;
                                    break;
                                case 4:
                                    pbGoi4.SizeMode = PictureBoxSizeMode.StretchImage;
                                    pbGoi4.Image = System.Drawing.Image.FromFile(currentPath + "\\Resources\\group4\\ts4_in.png"); ;
                                    break;
                                case 5:
                                    pbGoi5.SizeMode = PictureBoxSizeMode.StretchImage;
                                    pbGoi5.Image = System.Drawing.Image.FromFile(currentPath + "\\Resources\\group4\\ts5_in.png"); ;
                                    break;

                            }
                        }
                    }
                    else if (cauhoiTS.trangThai == 0)
                    {
                        switch (cauhoiTS.vitri)
                        {
                            case 1:
                                pbGoi1.SizeMode = PictureBoxSizeMode.StretchImage;
                                pbGoi1.Image = System.Drawing.Image.FromFile(currentPath + "\\Resources\\group4\\ts1.png");
                                break;
                            case 2:
                                pbGoi2.SizeMode = PictureBoxSizeMode.StretchImage;
                                pbGoi2.Image = System.Drawing.Image.FromFile(currentPath + "\\Resources\\group4\\ts2.png");
                                break;
                            case 3:
                                pbGoi3.SizeMode = PictureBoxSizeMode.StretchImage;
                                pbGoi3.Image = System.Drawing.Image.FromFile(currentPath + "\\Resources\\group4\\ts3.png"); ;
                                break;
                            case 4:
                                pbGoi4.SizeMode = PictureBoxSizeMode.StretchImage;
                                pbGoi4.Image = System.Drawing.Image.FromFile(currentPath + "\\Resources\\group4\\ts4.png"); ;
                                break;
                            case 5:
                                pbGoi5.SizeMode = PictureBoxSizeMode.StretchImage;
                                pbGoi5.Image = System.Drawing.Image.FromFile(currentPath + "\\Resources\\group4\\ts5.png"); ;
                                break;

                        }
                    }
                    else
                    {
                        switch (cauhoiTS.vitri)
                        {
                            case 1:
                                pbGoi1.SizeMode = PictureBoxSizeMode.StretchImage;
                                pbGoi1.Image = System.Drawing.Image.FromFile(currentPath + "\\Resources\\group4\\ts1_un.png");
                                break;
                            case 2:
                                pbGoi2.SizeMode = PictureBoxSizeMode.StretchImage;
                                pbGoi2.Image = System.Drawing.Image.FromFile(currentPath + "\\Resources\\group4\\ts2_un.png");
                                break;
                            case 3:
                                pbGoi3.SizeMode = PictureBoxSizeMode.StretchImage;
                                pbGoi3.Image = System.Drawing.Image.FromFile(currentPath + "\\Resources\\group4\\ts3_un.png"); ;
                                break;
                            case 4:
                                pbGoi4.SizeMode = PictureBoxSizeMode.StretchImage;
                                pbGoi4.Image = System.Drawing.Image.FromFile(currentPath + "\\Resources\\group4\\ts4_un.png"); ;
                                break;
                            case 5:
                                pbGoi5.SizeMode = PictureBoxSizeMode.StretchImage;
                                pbGoi5.Image = System.Drawing.Image.FromFile(currentPath + "\\Resources\\group4\\ts5_un.png"); ;
                                break;

                        }
                    }
                    // Thêm câu hỏi này vào danh sách đã hiển thị
                    dsCauHoiDaHienThi.Add(cauhoiid);

                }
            }

        }
        private void loadNutDaChon(int cauhoiid)
        {
            var dsCauDaChon = _entities.ds_goicauhoishining
                .Where(x => x.cauhoiid == cauhoiid && x.trangThai == 2)
                .ToList();
            foreach (var cauHoi in dsCauDaChon)
            {
                switch (cauHoi.vitri)
                {
                    case 1:
                        pbGoi1.SizeMode = PictureBoxSizeMode.StretchImage;
                        pbGoi1.Image = System.Drawing.Image.FromFile(currentPath + "\\Resources\\group4\\ts1_un.png");
                        break;
                    case 2:
                        pbGoi2.SizeMode = PictureBoxSizeMode.StretchImage;
                        pbGoi2.Image = System.Drawing.Image.FromFile(currentPath + "\\Resources\\group4\\ts2_un.png");
                        break;
                    case 3:
                        pbGoi3.SizeMode = PictureBoxSizeMode.StretchImage;
                        pbGoi3.Image = System.Drawing.Image.FromFile(currentPath + "\\Resources\\group4\\ts3_un.png"); ;
                        break;
                    case 4:
                        pbGoi4.SizeMode = PictureBoxSizeMode.StretchImage;
                        pbGoi4.Image = System.Drawing.Image.FromFile(currentPath + "\\Resources\\group4\\ts4_un.png"); ;
                        break;
                    case 5:
                        pbGoi5.SizeMode = PictureBoxSizeMode.StretchImage;
                        pbGoi5.Image = System.Drawing.Image.FromFile(currentPath + "\\Resources\\group4\\ts5_un.png"); ;
                        break;

                }
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
