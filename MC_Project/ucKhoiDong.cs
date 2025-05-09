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

        private void loadUC()
        {
            
        }

        /* private void loadDanhSachCauHoi()
         {
             List<ds_goicauhoikhoidong> ds_Goicauhoikhoidongs = _entities.ds_goicauhoikhoidong.Where(x => x.goicauhoiid.Equals(_goicauhoiid)).ToList();
             if (ds_Goicauhoikhoidongs != null && ds_Goicauhoikhoidongs.Count > 0)
             {
                 int stt = 0;
                 for (int i = 0; i < ds_Goicauhoikhoidongs.Count; i++)
                 {
                     stt++;
                     ds_goicauhoikhoidong item = ds_Goicauhoikhoidongs[i];
                     string[] row = { stt.ToString(), item.noidungcauhoi, item.dapan };
                     ListViewItem lvi = new ListViewItem(row);
                     lvCauHoiKhoiDong.Items.Add(lvi);
                 }
             }
             disableButton();
         }*/

        private void disableButton(int[] _ttgoi)
        {
            if (_ttgoi[0] == 1)
            {
                pbGoi1.BackgroundImage = Image.FromFile(currentPath + "\\Resources\\group4\\1-in.png");
                pbGoi1.BackgroundImageLayout = ImageLayout.Stretch;

            }
            if (_ttgoi[1] == 1)
            {
                pbGoi2.BackgroundImage = Image.FromFile(currentPath + "\\Resources\\group4\\2-in.png");
                pbGoi2.BackgroundImageLayout = ImageLayout.Stretch;

            }
            if (_ttgoi[2] == 1)
            {
                pbGoi3.BackgroundImage = Image.FromFile(currentPath + "\\Resources\\group4\\3-in.png");
                pbGoi3.BackgroundImageLayout = ImageLayout.Stretch;

            }
            if (_ttgoi[3] == 1)
            {
                pbGoi4.BackgroundImage = Image.FromFile(currentPath + "\\Resources\\group4\\4-in.png");
                pbGoi4.BackgroundImageLayout = ImageLayout.Stretch;

            }
            if (_ttgoi[4] == 1)
            {
                pbGoi5.BackgroundImage = Image.FromFile(currentPath + "\\Resources\\group4\\5-in.png");
                pbGoi5.BackgroundImageLayout = ImageLayout.Stretch;

            }
            if (_ttgoi[5] == 1)
            {
                pbGoi6.BackgroundImage = Image.FromFile(currentPath + "\\Resources\\group4\\6-in.png");
                pbGoi6.BackgroundImageLayout = ImageLayout.Stretch;

            }
        }
        private void ucKhoiDong_Load(object sender, EventArgs e)
        {
            ds_doi teamplaying = _entities.ds_doi.Find(_doiid);
            if (_goicauhoiid == 0)
            {
                invisibleGui();
                lblthele.Text = "Question packages!";                
            }
            else
            {
                
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
                disableButton(_ttgoi);
                selectedButton(_ttgoi);


            }
        }
        private void selectedButton(int[] _ttgoi)
        {
            if (_ttgoi[0] == 2)
            {
                pbGoi1.BackgroundImage = Image.FromFile(currentPath + "\\Resources\\group4\\1-dis.png");
                this.BackgroundImageLayout = ImageLayout.Stretch;

            }
            if (_ttgoi[1] == 2)
            {
                pbGoi2.BackgroundImage = Image.FromFile(currentPath + "\\Resources\\group4\\2-dis.png");
                this.BackgroundImageLayout = ImageLayout.Stretch;

            }
            if (_ttgoi[2] == 2)
            {
                pbGoi3.BackgroundImage = Image.FromFile(currentPath + "\\Resources\\group4\\3-dis.png");
                this.BackgroundImageLayout = ImageLayout.Stretch;

            }
            if (_ttgoi[3] == 2)
            {
                pbGoi4.BackgroundImage = Image.FromFile(currentPath + "\\Resources\\group4\\4-dis.png");
                this.BackgroundImageLayout = ImageLayout.Stretch;

            }
            if (_ttgoi[4] == 2)
            {
                pbGoi5.BackgroundImage = Image.FromFile(currentPath + "\\Resources\\group4\\5-dis.png");
                this.BackgroundImageLayout = ImageLayout.Stretch;

            }
            if (_ttgoi[5] == 2)
            {
                pbGoi6.BackgroundImage = Image.FromFile(currentPath + "\\Resources\\group4\\6-dis.png");
                this.BackgroundImageLayout = ImageLayout.Stretch;

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
