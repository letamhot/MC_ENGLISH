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
    public partial class frmTongDiem : Form
    {
        private Socket _socket;
        private int _doiid = 0;
        private int _cauhoiid = 0;
        private int _goicauhoiid = 0;
        private int[] _ttgoi;
        private bool _x2 = false;
        private bool _da = false;
        private bool _isStart = false;
        private string currentPath = Directory.GetCurrentDirectory();
        QuaMienDiSanEntities _entities = new QuaMienDiSanEntities();
        SqlDataAccess sqlObject = new SqlDataAccess();
        public frmTongDiem()
        {
            InitializeComponent();
            loadUC();

        }      

       
        public void loadUC()
        {
            string spl = "Select doiid , phanthiid, sum(sodiem) as tongdiem from ds_diem GROUP BY phanthiid, doiid";
            DataTable dt = sqlObject.getDataFromSql(spl, "").Tables[0];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        var lbl = this.Controls.Find("lbl" + (i + 1) + (j + 1), true).FirstOrDefault() as Label;
                        var dr = dt.Select("doiid =" + (i + 1) + " AND phanthiid =" + (j + 1));
                        var dr1 = dt.Select("doiid =" + (i + 1) +"");
                        if (dr.Count() > 0)
                        {
                            lbl.Text = dt.Select("doiid =" + (i + 1) + " AND phanthiid =" + (j + 1))[0]["tongdiem"].ToString();
                            
                        }
                        else
                        {
                            lbl.Text = "0";
                        }
                       
                       
                    }
                }

            }
            string spl1 = "Select doiid , sum(sodiem) as tongdiem from ds_diem GROUP BY doiid";
            DataTable dt1 = sqlObject.getDataFromSql(spl1, "").Tables[0];
            if (dt1.Rows.Count > 0)
            {
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        var lblTD = this.Controls.Find("lblTongDiem" + (i + 1), true).FirstOrDefault() as Label;
                        var dr1 = dt1.Select("doiid =" + (i + 1) + "");
                        if (dr1.Count() > 0)
                        {
                            lblTD.Text = dt1.Select("doiid =" + (i + 1) + "")[0]["tongdiem"].ToString();

                        }
                        else
                        {
                            lblTD.Text = "0";
                        }

                    }
                }

            }

            ds_cuocthi dscuocthi = _entities.ds_cuocthi.FirstOrDefault(x => x.trangthai == true);
            var ten = "";
            var dsTen = _entities.ds_doi.Where(x => x.vaitro == "TS" && x.cuocthiid == dscuocthi.cuocthiid).ToList();
            if (dsTen != null && dsTen.Count > 0)
            {
                for (int i = 0; i < dsTen.Count; i++)
                {
                    var lbl = this.Controls.Find("lblTS" + (i + 1), true).FirstOrDefault() as Label;
                    var tachten = dsTen[i].tennguoichoi.Split(' ');
                    for (int j = 1; j < tachten.Length; j++)
                    {
                        ten = tachten[j - 1] + " " + tachten[j];
                    }
                    lbl.Text = ten;
                    /*for (int vt = 0; vt < 4; vt++)
                    {
                        if (dsTen[i].vitridoi == vt)
                        {
                            lbl.Text = ten;
                        }
                    }*/


                }
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
    }
}
