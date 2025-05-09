using MC_Project.Model;
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

namespace MC_Project
{
    public partial class ucKhoiDong : UserControl
    {
        private Socket _socket;
        private int _doiid = 0;
        private int _cauhoiid = 0;
        private int _goicauhoiid = 0;
        private bool _isStart;
        private int[] _ttgoi;
        private string currentPath = Directory.GetCurrentDirectory();
        QuaMienDiSanEntities _entities = new QuaMienDiSanEntities();
        public ucKhoiDong()
        {
            InitializeComponent();
        }
        public ucKhoiDong(Socket sock, int doiid, int cauhoiid, int goicauhoiid, int[] ttgoi, bool isStart)
        {
            InitializeComponent();
            _socket = sock;
            _doiid = doiid;
            _cauhoiid = cauhoiid;
            _goicauhoiid = goicauhoiid;
            _ttgoi = ttgoi;
            _isStart = isStart;
            //loadUC();
        }

 
        private void ucKhoiDong_Load(object sender, EventArgs e)
        {
            ds_doi teamplaying = _entities.ds_doi.Find(_doiid);
            if (_goicauhoiid == 0)
            {
                invisibleGui();
                lblthele.Text = "Question packages!";
                // Reset all packages to available state when starting new selection
                ResetAllPackageStates();
            }
            else
            {

                // Update all package states first
                UpdateAllPackageStates(_ttgoi);

                if (_cauhoiid > 0)
                {
                    visibleGui();
                    if (teamplaying != null)
                    {
                        lblthele.Text = "Candidate " + teamplaying.tennguoichoi.ToUpper() + " is doing the section";
                    }
                    ds_goicauhoikhoidong cauhoi = _entities.ds_goicauhoikhoidong.Find(_cauhoiid);

                    _entities.Entry(cauhoi).Reload(); // ⚠️ Nạp lại từ DB
                    labelNoiDungCauHoi.Text = "Question " + cauhoi.vitri + ":"; 
                    lblcauhoi.Text = cauhoi.noidungcauhoi;
                    lblDapan.Text = cauhoi.dapan;

                }
                else
                {
                    visibleGui1();

                    if (teamplaying != null)
                    {
                        lblthele.Text = "Candidate " + teamplaying.tennguoichoi.ToString().ToUpper() + " chooses question package number " + _goicauhoiid + "\n"; 
                        lblDA.Visible = false;
                        lblcauhoi.Visible = false;
                        labelNoiDungCauHoi.Visible = false;
                    }
                    if (_isStart)
                    {
                        ds_doi teamnext = _entities.ds_doi.Where(x => x.vitridoi == teamplaying.vitridoi + 1).FirstOrDefault();
                        if(teamnext != null)
                        {
                            lblcauhoi.Text = "Congratulations to candidate " + teamplaying.tennguoichoi.ToString().ToUpper() + " completed the Warm-up section\nCandidate " + teamnext.tennguoichoi.ToString().ToUpper() + " preparing for the section";
                        }
                        else
                        {
                            lblcauhoi.Text = "Congratulations to candidate " + teamplaying.tennguoichoi.ToString().ToUpper() + " has completed the Warm-up section";

                        }
                        lblcauhoi.Visible = true;
                    }
                    else
                    {
                        lblcauhoi.Visible = false;

                    }
                }


            }
        }
       
        private HashSet<int> dsCauHoiDaHienThi = new HashSet<int>();
        private void ResetAllPackageStates()
        {
            // Reset all packages to available state (0)
            for (int i = 0; i < 6; i++)
            {
                SetPackageImage(i + 1, "ac");
            }
            dsCauHoiDaHienThi.Clear();

        }

        private void UpdateAllPackageStates(int[] states)
        {
            for (int i = 0; i < states.Length; i++)
            {
                int packageNumber = i + 1;
                switch (states[i])
                {
                    case 0: // Available
                        SetPackageImage(packageNumber, "ac"); // Available (normal)
                        break;
                    case 1: // Selected/In-progress
                        SetPackageImage(packageNumber, "in"); // Highlighted/selected
                        break;
                    case 2: // Completed
                        SetPackageImage(packageNumber, "dis"); // Disabled/grayed out
                        break;
                }
            }
        }
        private void SetPackageImage(int packageNumber, string state)
        {
            string imagePath = $"{currentPath}\\Resources\\group4\\";

            switch (state)
            {
                case "ac": // Available
                    imagePath += $"{packageNumber}-ac.png";
                    break;
                case "in": // Disabled
                    imagePath += $"{packageNumber}-in.png";
                    break;
                case "dis": // Selected
                    imagePath += $"{packageNumber}-dis.png"; // Or use a different selected image if available
                    break;
            }

            var pictureBox = GetPictureBoxByPackageNumber(packageNumber);
            if (pictureBox != null && File.Exists(imagePath))
            {
                pictureBox.BackgroundImage = Image.FromFile(imagePath);
                pictureBox.BackgroundImageLayout = ImageLayout.Stretch;
            }
        }
        private PictureBox GetPictureBoxByPackageNumber(int packageNumber)
        {
            switch (packageNumber)
            {
                case 1: return pbGoi1;
                case 2: return pbGoi2;
                case 3: return pbGoi3;
                case 4: return pbGoi4;
                case 5: return pbGoi5;
                case 6: return pbGoi6;
                default: return null;
            }
        }

        private void invisibleGui()
        {
            lblthele.Visible = true;
            lblcauhoi.Visible = false;
            labelNoiDungCauHoi.Visible = false;
            lblDapan.Visible = false;
            lblDA.Visible = false;
        }
        private void visibleGui1()
        {
            lblthele.Visible = true;
            lblcauhoi.Visible = false;
            labelNoiDungCauHoi.Visible = false;
        }
        private void visibleGui()
        {
            lblthele.Visible = true;
            lblcauhoi.Visible = true;
            labelNoiDungCauHoi.Visible = true;
            lblDapan.Visible = true;
            lblDA.Visible = true;
        }
        private void lblcauhoi_Click(object sender, EventArgs e)
        {

        }
    }
}
