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
    public partial class frmKhanGia : Form
    {
        private Socket _socket;
        private int _cauhoiid = 0;
        private int _cuocthiid = 0;
        private int[] _ttgoi;
        private bool _da = false;
        private string doiChoi = "";
        private int cuocthiId = 0;
        private string currentPath = Directory.GetCurrentDirectory();
        QuaMienDiSanEntities _entities = new QuaMienDiSanEntities();
        SqlDataAccess sqlObject = new SqlDataAccess();
        private int time = 0;
        public frmKhanGia()
        {
            InitializeComponent();
        }
        public frmKhanGia(Socket sock, int cauhoiid, int cuocthiid, bool da)
        {
            InitializeComponent();
            _socket = sock;
            _cauhoiid = cauhoiid;
            _cuocthiid = cuocthiid;
            _da = da;
            loadUC();
        }
        private void loadUC()
        {
            if (_cauhoiid == 0)
            {
                labelCauHoi.Visible = true;
                labelCauHoi.Text = "Contest rules: ";
                lblCauHoi.Visible = true;
                lblDapAn.Visible = false;
                pbDA.Visible = false;
                lblCauHoi.Text = "This section will have 4 questions for audiences.\nQuestions will be related to the program sponsor or other content.";
            }
            else
            {
                ds_cuocthi cuocThiHienTai = _entities.ds_cuocthi.FirstOrDefault(x => x.trangthai == true);

                int idCuocThiHienTai = cuocThiHienTai.cuocthiid;
                ds_phanthikhangia dskg = _entities.ds_phanthikhangia.Find(_cauhoiid);
                labelCauHoi.Text = "Questions number " + dskg.vitri + ":";
                lblCauHoi.Text = dskg.noidungcauhoi.ToString();
                lblCauHoi.Visible = true;
                labelCauHoi.Visible = true;
                lblDapAn.Text = dskg.dapan.ToString();
                lblDapAn.Visible = true;
                pbDA.Visible = true;
                
            }



        }

        private void pbClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
