using MC_Project.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MC_Project
{
    public partial class fmHienThiChiTiet : Form
    {
        private Socket _socket;
        private int _doiid = 0;
        private int _cauhoiid = 0;
        private bool _diem = false;
        private string doiChoi = "";
        private int cuocthiId = 0;
        QuaMienDiSanEntities _entities = new QuaMienDiSanEntities();
        SqlDataAccess sqlObject = new SqlDataAccess();
        public fmHienThiChiTiet()
        {
            InitializeComponent();
        }
        public fmHienThiChiTiet(Socket sock, int cauhoiid)
        {
            InitializeComponent();
            _socket = sock;
            _cauhoiid = cauhoiid;
            loadUC(_cauhoiid);
        }
        public void loadUC(int cauhoiid) {
            int cuocthiId = _entities.ds_cuocthi.FirstOrDefault(x => x.trangthai == true).cuocthiid;

            ds_cauhoithuthach ds = _entities.ds_cauhoithuthach.FirstOrDefault(x => x.cauhoiid == cauhoiid && x.cuocthiid == cuocthiId);
            if (ds != null)
            {
                //labelDapAnCT.Text = "HIỂN THỊ ĐÁP ÁN CHI TIẾT CÂU "+ ds.vitri;
                //labelDapAnCT.ForeColor = Color.Black;
                lblDapAnCT.ForeColor = Color.Black;
                lblDapAnCT.Font = new Font("Arial", ds.dapantext.Length > 600 ? 22 : ds.dapantext.Length < 100 ? 28 : 26, FontStyle.Bold);
                lblDapAnCT.Text =  ds.dapantext;
            }
        }

        private void pbMini_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void pbClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void fmHienThiChiTiet_Load(object sender, EventArgs e)
        {

        }
    }
}
