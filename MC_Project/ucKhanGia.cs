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
    public partial class ucKhanGia : UserControl
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
        public ucKhanGia()
        {
            InitializeComponent();
        }
        public ucKhanGia(Socket sock, int cauhoiid, int cuocthiid, bool da)
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
                lblNoiDung.Visible = true;
                lblCauHoi.Visible = true;
                labelDA.Visible = false;
                lblDA.Visible = false;
                lblCauHoi.Text = "- Phần thi này sẽ có 4 câu hỏi dành cho các cổ động viên. Câu hỏi có nội dung liên quan đến nhà tài trợ chương trình hoặc những nội dung khác.";
            }
            else
            {
                ds_cuocthi cuocThiHienTai = _entities.ds_cuocthi.FirstOrDefault(x => x.trangthai == true);

                int idCuocThiHienTai = cuocThiHienTai.cuocthiid;
                ds_phanthikhangia dskg = _entities.ds_phanthikhangia.Find(_cauhoiid);
                lblNoiDung.Text = "Câu hỏi số " + dskg.vitri + ":";
                lblCauHoi.Text = dskg.noidungcauhoi.ToString();
                lblCauHoi.Visible = true;
                lblNoiDung.Visible = true;
                lblDA.Text = dskg.dapan.ToString();
                lblDA.Visible = true;
                labelDA.Visible = true;
                
            }



        }

    }
}
