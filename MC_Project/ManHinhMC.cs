using MC_Project.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using MC_Project;
using System.Configuration;
using System.Security.Cryptography;

delegate void AddMessage(string sNewMessage);
namespace MC_Project
{
    public partial class ManHinhMC : Form
    {
        QuaMienDiSanEntities _entities = new QuaMienDiSanEntities();
        private Socket sock;
        static string message;
        private int thoiGianConLai = 0;
        private int cuocthiId = 0;
        private byte[] byBuff = new byte[256];
        private string currentPath = Directory.GetCurrentDirectory();
        int[] ttGoiKD = { 0, 0, 0, 0, 0, 0 };
        int[] ttGoiVD = { 0, 0, 0, 0, 0, 0 };
        private event AddMessage addMessage;
        int id = 0;
        ds_doi ds_Doi = new ds_doi();
        SqlDataAccess sqlObject = new SqlDataAccess();

        public ManHinhMC()
        {
            InitializeComponent();
        }

        public ManHinhMC(int doiId)
        {
            id = doiId;
            ds_Doi = _entities.ds_doi.Find(id);
            InitializeComponent();
            connecServer();
            addMessage = new AddMessage(OnAddMessage);
        }

        private void connecServer()
        {
            Cursor cursor = Cursor.Current;
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                if (sock != null && sock.Connected)
                {
                    sock.Shutdown(SocketShutdown.Both);
                    System.Threading.Thread.Sleep(10);
                    sock.Close();
                }
                string server_ip = ConfigurationManager.AppSettings["IPServer"];
                sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPEndPoint epServer = new IPEndPoint(IPAddress.Parse(server_ip), 399); //192.168.2.117
                sock.Blocking = false;
                AsyncCallback onconnect = new AsyncCallback(OnConnect);
                sock.BeginConnect(epServer, onconnect, sock);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Server Connect failed!");
            }
            Cursor.Current = cursor;
        }
        public void OnConnect(IAsyncResult ar)
        {

            Socket sock = (Socket)ar.AsyncState;
            try
            {
                if (sock.Connected)
                {
                    SetupRecieveCallback(sock);
                    SendEvent(id.ToString() + ",cli,connected,on");
                }
                else
                    MessageBox.Show(this, "khong cho phep connect den may o xa", "loi ket noi!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "loi khi thuc hien connect!");
            }
        }
        public void SetupRecieveCallback(Socket sock)
        {
            try
            {
                AsyncCallback recieveData = new AsyncCallback(OnRecievedData);
                sock.BeginReceive(byBuff, 0, byBuff.Length, SocketFlags.None, recieveData, sock);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Setup Recieve Callback failed!");
            }
        }

        public void OnRecievedData(IAsyncResult ar)
        {
            Socket socks = (Socket)ar.AsyncState;
            try
            {
                int nBytesRec = socks.EndReceive(ar);
                if (nBytesRec > 0)
                {
                    string sRecieved = Encoding.ASCII.GetString(byBuff, 0, nBytesRec);

                    // Kiểm tra xem form đã được khởi tạo và có handle chưa
                    if (this.IsHandleCreated)
                    {
                        this.BeginInvoke(addMessage, new string[] { sRecieved });
                    }
                    else
                    {
                        Console.WriteLine("Handle chưa được tạo.");
                    }

                    SetupRecieveCallback(socks);
                }
                else
                {
                    Console.WriteLine("Client {0}, disconnected", socks.RemoteEndPoint);
                    socks.Shutdown(SocketShutdown.Both);
                    socks.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Lỗi xảy ra khi nhận kết quả trả về!");
            }
        }
        public void OnAddMessage(string sMessage)
        {
            layCuocThiHienTai();
            message = sMessage;
            string[] spl = message.Split(',');
            string src = spl[1];
            bool dapan = false;
            bool da = false;
            if (src.Equals("ser"))
            {
                if (spl[0] == "0")
                {
                    lblThoiGian.Visible = true;
                    pnlDiemSo.Visible = true;

                    if (spl[2] == "playgioithieu")
                    {
                        onoffKhanGia(true);
                        this.BackgroundImage = Image.FromFile(currentPath + "\\Resources\\group6\\gt_qmds.png");
                        this.BackgroundImageLayout = ImageLayout.Stretch;
                        pnlNoiDung.Controls.Clear();
                        //lblCauHoiChinhMain.Visible = false;
                        pnlDiemSo.Visible = false;
                        lblThoiGian.Visible = false;
                        //lblThoiGiankg.Visible = false;
                    }
                    frmTongDiem frmTongDiem;

                    if (spl[2] == "playkhoidong")
                    {
                        lblThoiGian.ForeColor = Color.White;

                        layCuocThiHienTai();
                        // Giả sử frmTongDiem là form con, kiểm tra và đóng form nếu đang mở
                        if (Application.OpenForms["frmTongDiem"] != null)
                        {
                            Application.OpenForms["frmTongDiem"].Close();
                        }

                        // Khởi tạo lại frmTongDiem sau khi đã đóng
                        frmTongDiem = new frmTongDiem();
                        onoffKhanGia(true);
                        //lblCauHoiChinhMain.Visible = false;
                        this.BackgroundImage = Image.FromFile(currentPath + "\\Resources\\group6\\kd_tl.png");
                        this.BackgroundImageLayout = ImageLayout.Stretch;
                        onOffInfo(false);
                        this.Show();
                        /*thoiGianConLai = 60;
                        lblThoiGian.Text = thoiGianConLai.ToString();
                        pnlNoiDung.Controls.Clear();
                        pnlNoiDung.Controls.Add(new ucKhoiDong(sock, id, 0, 0, ttGoiKD,false));*/
                    }
                    if (spl[2] == "playthuthach")
                    {
                        lblThoiGian.ForeColor = Color.White;
                        layCuocThiHienTai();
                        onoffKhanGia(true);
                        //lblCauHoiChinhMain.Visible = false;
                        //lblThoiGiankg.Visible = false;
                        onOffInfo(false);
                        thoiGianConLai = 30;
                        lblThoiGian.Text = thoiGianConLai.ToString();
                        frmDapAnKP frmDapAnKP;
                        fmHienThiChiTiet fmhienthichitiet;
                        // Giả sử frmTongDiem là form con, kiểm tra và đóng form nếu đang mở
                        if (Application.OpenForms["frmTongDiem"] != null)
                        {
                            Application.OpenForms["frmTongDiem"].Close();
                        }

                        if (spl[3] == "0")
                        {
                            // Giả sử frmTongDiem là form con, kiểm tra và đóng form nếu đang mở
                            if (Application.OpenForms["frmTongDiem"] != null)
                            {
                                Application.OpenForms["frmTongDiem"].Close();
                            }
                            this.BackgroundImage = Image.FromFile(currentPath + "\\Resources\\group6\\kp_tl.png");
                            this.BackgroundImageLayout = ImageLayout.Stretch;
                            pnlNoiDung.Controls.Clear();
                            //pnlNoiDung.Controls.Add(new ucThuThach(sock, id, 0, false, 0));
                            //this.Show();

                        }
                        else
                        {
                            layCuocThiHienTai();
                            onOffInfo(true);
                            lblThoiGian.Location = new Point(87, 278);
                            pnlDiemSo.Location = new Point(29, 474);

                            if (spl[4] == "ready")
                            {
                                // Giả sử frmTongDiem là form con, kiểm tra và đóng form nếu đang mở
                                if (Application.OpenForms["frmDapAnKP"] != null)
                                {
                                    Application.OpenForms["frmDapAnKP"].Close();
                                }
                                // Giả sử frmTongDiem là form con, kiểm tra và đóng form nếu đang mở
                                if (Application.OpenForms["frmDapAnKP"] != null)
                                {
                                    Application.OpenForms["frmDapAnKP"].Close();
                                }
                                timeMC.Enabled = false;

                                /*frmTongDiem = new frmTongDiem();
                                this.Hide();*/
                                this.BackgroundImage = Image.FromFile(currentPath + "\\Resources\\group6\\tc_mc_kp.png");
                                this.BackgroundImageLayout = ImageLayout.Stretch;

                                pnlNoiDung.Controls.Clear();
                                pnlNoiDung.Controls.Add(new ucThuThach(sock, id, int.Parse(spl[3]), false, 0));
                                this.Show();

                            }
                            if (spl[4] == "start")
                            {
                                timeMC.Enabled = true;
                                pnlNoiDung.Controls.Clear();
                                pnlNoiDung.Controls.Add(new ucThuThach(sock, id, int.Parse(spl[3]), true, int.Parse(spl[5])));
                                //Thread.Sleep(1000);
                            }
                            if (spl[4] == "stop")
                            {
                                // Giả sử frmTongDiem là form con, kiểm tra và đóng form nếu đang mở
                                if (Application.OpenForms["fmHienThiChiTiet"] != null)
                                {
                                    Application.OpenForms["fmHienThiChiTiet"].Close();
                                }
                                /* this.BackgroundImage = Image.FromFile(currentPath + "\\Resources\\group6\\tc_mc_kp_diem.png");
                                 pnlNoiDung.Controls.Clear();*/
                                frmDapAnKP = new frmDapAnKP(sock, int.Parse(spl[3]), false);
                                frmDapAnKP.Show();
                                timeMC.Enabled = false;

                            }
                            if (spl[4] == "hienthidiemKP")
                            {
                                // Giả sử frmTongDiem là form con, kiểm tra và đóng form nếu đang mở
                                if (Application.OpenForms["fmHienThiChiTiet"] != null)
                                {
                                    Application.OpenForms["fmHienThiChiTiet"].Close();
                                }
                                frmDapAnKP = new frmDapAnKP(sock, int.Parse(spl[3]), true);
                                frmDapAnKP.Show();
                                timeMC.Enabled = false;


                            }
                            if (spl[4] == "hienthidapanCT")
                            {
                                fmhienthichitiet = new fmHienThiChiTiet(sock, int.Parse(spl[3]));
                                fmhienthichitiet.Show();
                                timeMC.Enabled = false;
                            }
                            if (spl[4] == "capNhatDienManHinhTT")
                            {
                                layCuocThiHienTai();
                                pnlNoiDung.Controls.Clear();
                                pnlNoiDung.Controls.Add(new ucThuThach(sock, id, int.Parse(spl[3]), false, 0));
                            }


                        }
                        lblThoiGian.Text = thoiGianConLai.ToString();

                    }
                    if (spl[2] == "playkhamphachiase")
                    {
                        lblThoiGian.ForeColor = Color.Red;

                        layCuocThiHienTai();
                        onoffKhanGia(true);
                        //lblThoiGiankg.Visible = false;
                        
                        
                        onOffInfo(false);
                        // Giả sử frmTongDiem là form con, kiểm tra và đóng form nếu đang mở
                        if (Application.OpenForms["frmTongDiem"] != null)
                        {
                            Application.OpenForms["frmTongDiem"].Close();
                        }
                        // Giả sử frmTongDiem là form con, kiểm tra và đóng form nếu đang mở
                        if (Application.OpenForms["frmDapAnKP"] != null)
                        {
                            Application.OpenForms["frmDapAnKP"].Close();
                        }
                        // Giả sử frmTongDiem là form con, kiểm tra và đóng form nếu đang mở
                        if (Application.OpenForms["frmDapAnKP"] != null)
                        {
                            Application.OpenForms["frmDapAnKP"].Close();
                        }
                        if (spl[3] == "0")
                        {
                            // Giả sử frmTongDiem là form con, kiểm tra và đóng form nếu đang mở
                            if (Application.OpenForms["frmTongDiem"] != null)
                            {
                                Application.OpenForms["frmTongDiem"].Close();
                            }
                            this.BackgroundImage = Image.FromFile(currentPath + "\\Resources\\group6\\cp_tl.png");
                            this.BackgroundImageLayout = ImageLayout.Stretch;
                            this.Show();

                        }
                        else
                        {
                            layCuocThiHienTai();
                            lblThoiGian.Location = new Point(87, 278);
                            pnlDiemSo.Location = new Point(29, 474);
                            onOffInfo(true);
                            
                            ds_goicaudiscovery cauHoiChinhCP = _entities.ds_goicaudiscovery.Find(int.Parse(spl[3]));
                            if (spl[5] == "ready")
                            {
                                thoiGianConLai = 180;
                                lblThoiGian.Text = thoiGianConLai.ToString();
                                timeMC.Enabled = false;
                                this.BackgroundImage = Image.FromFile(currentPath + "\\Resources\\group6\\tc_mc_cp.png");
                                this.BackgroundImageLayout = ImageLayout.Stretch;
                                pnlNoiDung.Controls.Clear();
                                pnlNoiDung.Controls.Add(new ucKhamPhaCS(sock, id, int.Parse(spl[3]), int.Parse(spl[4]), false, false, false));  // Hình ảnh
                            }
                            if (spl[5] == "start")
                            {
                                int cauhoiId = Convert.ToInt32(spl[3]);
                                var khamPha = _entities.ds_goicaudiscovery.FirstOrDefault(x => x.cauhoiid == cauhoiId && x.trangthai == true);
                                if (!string.IsNullOrWhiteSpace(khamPha.noidungthisinh))
                                {
                                    pnlNoiDung.Controls.Clear();
                                    pnlNoiDung.Controls.Add(new ucKhamPhaCS(sock, id, int.Parse(spl[3]), int.Parse(spl[4]), true, true, true)); // Thêm tham số true để phát video
                                    timeMC.Enabled = true;
                                }
                                else
                                {
                                    pnlNoiDung.Controls.Clear();
                                    pnlNoiDung.Controls.Add(new ucKhamPhaCS(sock, id, int.Parse(spl[3]), int.Parse(spl[4]), false, false, true));
                                    timeMC.Enabled = true;
                                }

                            }
                            if (spl[5] == "stopTime")
                            {
                                int cauhoiId = Convert.ToInt32(spl[3]);
                                var khamPha = _entities.ds_goicaudiscovery.FirstOrDefault(x => x.cauhoiid == cauhoiId && x.trangthai == true);
                                if (!string.IsNullOrWhiteSpace(khamPha.noidungthisinh))
                                {
                                    pnlNoiDung.Controls.Clear();
                                    pnlNoiDung.Controls.Add(new ucKhamPhaCS(sock, id, int.Parse(spl[3]), int.Parse(spl[4]), false, true, false)); // Dừng video
                                    timeMC.Enabled = false;
                                }
                                else
                                {
                                    pnlNoiDung.Controls.Clear();
                                    pnlNoiDung.Controls.Add(new ucKhamPhaCS(sock, id, int.Parse(spl[3]), int.Parse(spl[4]), false, false, false)); // Hình ảnh
                                    timeMC.Enabled = false;
                                }

                            }
                            if (spl[5] == "hienthianhthisinh")
                            {
                                this.BackgroundImage = Image.FromFile(currentPath + "\\Resources\\group6\\tc_mc_cp.png");
                                this.BackgroundImageLayout = ImageLayout.Stretch;
                                timeMC.Enabled = false;
                                pnlNoiDung.Controls.Clear();
                                pnlNoiDung.Controls.Add(new ucKhamPhaCS(sock, id, int.Parse(spl[3]), int.Parse(spl[4]), false, true, false));
                            }
                            if (spl[5] == "hienthimanh")
                            {
                                pnlNoiDung.Controls.Clear();
                                pnlNoiDung.Controls.Add(new ucKhamPhaCS(sock, id, int.Parse(spl[3]), int.Parse(spl[4]), true, false, false));
                                //tmClient.Enabled = true;
                            }
                            if (spl[5] == "load6nut")
                            {
                                pnlNoiDung.Controls.Clear();
                                pnlNoiDung.Controls.Add(new ucKhamPhaCS(sock, id, int.Parse(spl[3]), int.Parse(spl[4]), true, false, false));
                                //tmClient.Enabled = true;
                            }
                            if (spl[5] == "capnhatTongDiem")
                            {
                                int cauhoiId = Convert.ToInt32(spl[3]);
                                var khamPha = _entities.ds_goicaudiscovery.FirstOrDefault(x => x.cauhoiid == cauhoiId && x.trangthai == true);
                                if (!string.IsNullOrWhiteSpace(khamPha.noidungthisinh))
                                {
                                    pnlNoiDung.Controls.Clear();
                                    pnlNoiDung.Controls.Add(new ucKhamPhaCS(sock, id, int.Parse(spl[3]), int.Parse(spl[4]), false, true, false));
                                    pnlDiemSo.Visible = true;
                                    layCuocThiHienTai();
                                }
                                else
                                {
                                    pnlNoiDung.Controls.Clear();
                                    pnlNoiDung.Controls.Add(new ucKhamPhaCS(sock, id, int.Parse(spl[3]), int.Parse(spl[4]), false, false, false));
                                    pnlDiemSo.Visible = true;
                                    layCuocThiHienTai();
                                }

                            }
                            
                        }

                    }
                    if (spl[2] == "playtoasang")
                    {
                        
                        lblThoiGian.ForeColor = Color.White;
                        layCuocThiHienTai();
                        // Giả sử frmTongDiem là form con, kiểm tra và đóng form nếu đang mở
                        if (Application.OpenForms["frmDapAnKP"] != null)
                        {
                            Application.OpenForms["frmDapAnKP"].Close();
                        }
                        // Giả sử frmTongDiem là form con, kiểm tra và đóng form nếu đang mở
                        if (Application.OpenForms["frmDapAnKP"] != null)
                        {
                            Application.OpenForms["frmDapAnKP"].Close();
                        }
                        if (spl[5] == "0")
                        {
                            frmKhanGia frmKhanGia;
                            onoffKhanGia(true);
                            //lblThoiGiankg.Visible = false;
                            //lblCauHoiChinhMain.Visible = false;
                            thoiGianConLai = 20;
                            lblThoiGian.Text = thoiGianConLai.ToString();
                            // Giả sử frmTongDiem là form con, kiểm tra và đóng form nếu đang mở
                            if (Application.OpenForms["frmTongDiem"] != null)
                            {
                                Application.OpenForms["frmTongDiem"].Close();
                            }
                            // Giả sử frmTongDiem là form con, kiểm tra và đóng form nếu đang mở
                            if (Application.OpenForms["frmKhanGia"] != null)
                            {
                                Application.OpenForms["frmKhanGia"].Close();
                            }
                            // Giả sử frmTongDiem là form con, kiểm tra và đóng form nếu đang mở
                            if (Application.OpenForms["frmKhanGia"] != null)
                            {
                                Application.OpenForms["frmKhanGia"].Close();
                            }
                            // Giả sử frmTongDiem là form con, kiểm tra và đóng form nếu đang mở
                            if (Application.OpenForms["frmKhanGia"] != null)
                            {
                                Application.OpenForms["frmKhanGia"].Close();
                            }
                            // Giả sử frmTongDiem là form con, kiểm tra và đóng form nếu đang mở
                            if (Application.OpenForms["frmKhanGia"] != null)
                            {
                                Application.OpenForms["frmKhanGia"].Close();
                            }
                            // Giả sử frmTongDiem là form con, kiểm tra và đóng form nếu đang mở
                            if (Application.OpenForms["frmKhanGia"] != null)
                            {
                                Application.OpenForms["frmKhanGia"].Close();
                            }
                            // Giả sử frmTongDiem là form con, kiểm tra và đóng form nếu đang mở
                            if (Application.OpenForms["frmKhanGia"] != null)
                            {
                                Application.OpenForms["frmKhanGia"].Close();
                            }
                            // Giả sử frmTongDiem là form con, kiểm tra và đóng form nếu đang mở
                            if (Application.OpenForms["frmKhanGia"] != null)
                            {
                                Application.OpenForms["frmKhanGia"].Close();
                            }
                            // Giả sử frmTongDiem là form con, kiểm tra và đóng form nếu đang mở
                            if (Application.OpenForms["frmKhanGia"] != null)
                            {
                                Application.OpenForms["frmKhanGia"].Close();
                            }
                            // Giả sử frmTongDiem là form con, kiểm tra và đóng form nếu đang mở
                            if (Application.OpenForms["frmKhanGia"] != null)
                            {
                                Application.OpenForms["frmKhanGia"].Close();
                            }


                            this.BackgroundImage = Image.FromFile(currentPath + "\\Resources\\group6\\vd_tl.png");
                            this.BackgroundImageLayout = ImageLayout.Stretch;
                            onOffInfo(false);
                            this.Show();
                        }
                        else
                        {
                            layCuocThiHienTai();
                            onoffKhanGia(true);
                            onOffInfo(true);
                            //lblCauHoiChinhMain.Visible = false;
                            thoiGianConLai = 20;

                            //lblThoiGiankg.Visible = false;
                            bool x2 = false;
                            bool tt = false;
                            if (spl[5] == "hienthi5NutCauHoi")
                            {
                                this.BackgroundImage = Image.FromFile(currentPath + "\\Resources\\group6\\tc_mc_vd.png");
                                this.BackgroundImageLayout = ImageLayout.Stretch;
                                timeMC.Enabled = false;
                                pnlNoiDung.Controls.Clear();
                                pnlNoiDung.Controls.Add(new ucToaSang(sock, id, int.Parse(spl[4]), tt, false, false, false, false));
                            }

                            if (spl[5] == "ready")
                            {

                                this.BackgroundImage = Image.FromFile(currentPath + "\\Resources\\group6\\tc_mc_vd.png");
                                this.BackgroundImageLayout = ImageLayout.Stretch;
                                timeMC.Enabled = false;
                                tt = true;
                                if (int.Parse(spl[4]) != 0)
                                {
                                    var cauhoiVD = _entities.ds_goicauhoishining.Find(int.Parse(spl[4]));

                                    thoiGianConLai = 20;
                                    lblThoiGian.Text = thoiGianConLai.ToString();
                                    pnlNoiDung.Controls.Clear();
                                    pnlNoiDung.Controls.Add(new ucToaSang(sock, id, int.Parse(spl[4]), tt, false, false, true, false));
                                }
                                else
                                {
                                    pnlNoiDung.Controls.Clear();
                                    pnlNoiDung.Controls.Add(new ucToaSang(sock, id, int.Parse(spl[4]), tt, false, false, true, false));
                                }



                            }
                            if (spl[5] == "start")
                            {

                                timeMC.Enabled = true;
                                tt = false;
                                if (int.Parse(spl[4]) != 0)
                                {
                                    var cauhoiVD = _entities.ds_goicauhoishining.Find(int.Parse(spl[4]));

                                    thoiGianConLai = 20;
                                    lblThoiGian.Text = thoiGianConLai.ToString();
                                    pnlNoiDung.Controls.Clear();
                                    pnlNoiDung.Controls.Add(new ucToaSang(sock, id, int.Parse(spl[4]), tt, false, false, false, false));
                                }
                                else
                                {
                                    pnlNoiDung.Controls.Clear();
                                    pnlNoiDung.Controls.Add(new ucToaSang(sock, id, int.Parse(spl[4]), tt, false, false, false, false));
                                }

                            }
                            if (spl[5] == "forceanswer")
                            {
                                // Hiển thị đáp án khi thí sinh 1 trả lời đúng hoặc thí sinh 2 dành quyền
                                layCuocThiHienTai();
                                da = true;
                                pnlNoiDung.Controls.Clear();

                                pnlNoiDung.Controls.Add(new ucToaSang(sock, id, int.Parse(spl[4]), tt, x2, da, false, false));
                            }


                            layCuocThiHienTai();

                            lblThoiGian.Text = thoiGianConLai.ToString();
                        }
                    }
                    if (spl[2] == "playkhangia")
                    {
                        frmKhanGia frmKhanGia;
                        // Giả sử frmTongDiem là form con, kiểm tra và đóng form nếu đang mở
                        if (Application.OpenForms["frmDapAnKP"] != null)
                        {
                            Application.OpenForms["frmDapAnKP"].Close();
                        }
                        // Giả sử frmTongDiem là form con, kiểm tra và đóng form nếu đang mở
                        if (Application.OpenForms["frmDapAnKP"] != null)
                        {
                            Application.OpenForms["frmDapAnKP"].Close();
                        }
                        // Giả sử frmTongDiem là form con, kiểm tra và đóng form nếu đang mở
                        if (Application.OpenForms["frmTongDiem"] != null)
                        {
                            Application.OpenForms["frmTongDiem"].Close();
                        }
                        /*this.BackgroundImage = Image.FromFile(currentPath + "\\Resources\\group6\\tc_kg.png");
                        this.BackgroundImageLayout = ImageLayout.Stretch;*/
                        onoffKhanGia(false);
                        //thoiGianConLai = 10;
                       /* lblThoiGiankg.Visible = true;
                        lblThoiGiankg.Text = thoiGianConLai.ToString();*/
                        if (spl[3] == "0")
                        {
                            frmKhanGia = new frmKhanGia(sock, 0, 0, false);
                            frmKhanGia.Show();
                        }
                        else
                        {
                            dapan = true;
                            frmKhanGia = new frmKhanGia(sock, int.Parse(spl[3]), int.Parse(spl[4]), dapan);
                            frmKhanGia.Show();
                           

                        }


                    }

                    if (spl[2] == "tongdiem")
                    {
                        // Giả sử frmTongDiem là form con, kiểm tra và đóng form nếu đang mở
                        if (Application.OpenForms["frmDapAnKP"] != null)
                        {
                            Application.OpenForms["frmDapAnKP"].Close();
                        }
                        // Giả sử frmTongDiem là form con, kiểm tra và đóng form nếu đang mở
                        if (Application.OpenForms["frmDapAnKP"] != null)
                        {
                            Application.OpenForms["frmDapAnKP"].Close();
                        }
                        frmTongDiem = new frmTongDiem();
                        frmTongDiem.Show();

                    }
                }
                else
                {
                    frmTongDiem frmTongDiem;

                    if (spl[2] == "playkhoidong")
                    {
                        layCuocThiHienTai();
                        onoffKhanGia(true);
                        onOffInfo(true);
                        //lblCauHoiChinhMain.Visible = false;
                        
                        //lblThoiGiankg.Visible = false;
                        
                        //lblThoiGian.Text = thoiGianConLai.ToString();
                        lblThoiGian.Location = new Point(87, 278);
                        pnlDiemSo.Location = new Point(29, 474);
                        if (spl[5] == "start")
                        {
                            timeMC.Enabled = true;
                            pnlNoiDung.Controls.Clear();
                            pnlNoiDung.Controls.Add(new ucKhoiDong(sock, int.Parse(spl[0]), int.Parse(spl[3]), int.Parse(spl[4]), ttGoiKD, false));
                            lblThoiGian.Text = thoiGianConLai.ToString();

                        }
                        else if (spl[5] == "stop")
                        {
                            ttGoiKD[int.Parse(spl[4]) - 1] = 2;

                            timeMC.Enabled = false;
                            layCuocThiHienTai();
                            thoiGianConLai = 60;
                            lblThoiGian.Text = thoiGianConLai.ToString();
                            pnlNoiDung.Controls.Clear();
                            pnlNoiDung.Controls.Add(new ucKhoiDong(sock, int.Parse(spl[0]), int.Parse(spl[3]), int.Parse(spl[4]), ttGoiKD, true));
                            lblThoiGian.Text = thoiGianConLai.ToString();

                        }
                        else if (spl[5] == "ready")
                        {
                            if(int.Parse(spl[4]) > 0)
                            {
                                ttGoiKD[int.Parse(spl[4]) - 1] = 1;

                            }
                            else
                            {
                                this.BackgroundImage = Image.FromFile(currentPath + "\\Resources\\group6\\tc_mc_kd.png");
                                this.BackgroundImageLayout = ImageLayout.Stretch;

                            }
                            thoiGianConLai = 60;
                            timeMC.Enabled = false;

                            pnlNoiDung.Controls.Clear();
                            pnlNoiDung.Controls.Add(new ucKhoiDong(sock, int.Parse(spl[0]), int.Parse(spl[3]), int.Parse(spl[4]), ttGoiKD, false));
                            lblThoiGian.Text = thoiGianConLai.ToString();

                        }
                        else if(spl[5] == "next")
                        {
                            //thoiGianConLai = 60;
                            pnlNoiDung.Controls.Clear();
                            pnlNoiDung.Controls.Add(new ucKhoiDong(sock, int.Parse(spl[0]), int.Parse(spl[3]), int.Parse(spl[4]), ttGoiKD, false));
                            lblThoiGian.Text = thoiGianConLai.ToString();

                        }
                        else if (spl[5] == "capNhatDiemManHinh")
                        {
                            layCuocThiHienTai();
                            pnlNoiDung.Controls.Clear();
                            pnlNoiDung.Controls.Add(new ucKhoiDong(sock, int.Parse(spl[0]), int.Parse(spl[3]), int.Parse(spl[4]), ttGoiKD, true));
                        }



                    }
                    if (spl[2] == "playtoasang")
                    {
                        layCuocThiHienTai();
                        onoffKhanGia(true);
                        onOffInfo(true);
                        //lblCauHoiChinhMain.Visible = false;
                        thoiGianConLai = 20;

                        //lblThoiGiankg.Visible = false;
                        bool x2 = false;
                        bool tt = false;

                        if (spl[5] == "showanswer")
                        {
                            // Hiển thị đáp án khi thí sinh 1 trả lời đúng hoặc thí sinh 2 dành quyền
                            layCuocThiHienTai();
                            da = true;
                            pnlNoiDung.Controls.Clear();

                            pnlNoiDung.Controls.Add(new ucToaSang(sock, int.Parse(spl[0]), int.Parse(spl[4]), tt, x2, da, false, false));
                        }
                        else if (spl[5] == "noanswer")
                        {
                            // Không hiển thị đáp án khi thí sinh 1 trả lời sai
                            layCuocThiHienTai();
                            da = true;
                            pnlNoiDung.Controls.Clear();

                            pnlNoiDung.Controls.Add(new ucToaSang(sock, int.Parse(spl[0]), int.Parse(spl[4]), tt, x2, da, false, false));
                        }
                        else if (spl[5] == "capNhatDiemManHinhTS")
                        {
                            da = false;
                            layCuocThiHienTai();
                            pnlNoiDung.Controls.Clear();
                            pnlNoiDung.Controls.Add(new ucToaSang(sock, int.Parse(spl[0]), int.Parse(spl[4]), tt, x2, da, false, false));
                        }
                        else if (spl[5] == "start_ngoisaohivong")
                        {
                            x2 = true;
                            pnlNoiDung.Controls.Clear();

                            pnlNoiDung.Controls.Add(new ucToaSang(sock, int.Parse(spl[0]), int.Parse(spl[4]), false, x2, da, false, false));
                        }
                        else if (spl[5] == "start_Nongoisaohivong")
                        {
                            x2 = false;
                            pnlNoiDung.Controls.Clear();

                            pnlNoiDung.Controls.Add(new ucToaSang(sock, int.Parse(spl[0]), int.Parse(spl[4]), false, x2, da, false, false));
                        }


                        lblThoiGian.Text = thoiGianConLai.ToString();

                    }


                }
            }
        }

        private void onOffInfo(bool v)
        {
            pnlNoiDung.Visible = v;
            lblThoiGian.Visible = v;
            pnlDiemSo.Visible = v;
        }

        private void layCuocThiHienTai()
        {
            lblTongDiem1.Text = lblTongDiem2.Text = lblTongDiem3.Text =lblTongDiem4.Text = "0";

            ds_cuocthi cuocThi = _entities.ds_cuocthi.FirstOrDefault(x => x.trangthai == true);
            if (cuocThi != null)
            {
                cuocthiId = cuocThi.cuocthiid;
                string sql = "SELECT doiid, sum(sodiem) as tongdiem from ds_diem WHERE cuocthiid = " + cuocthiId + " GROUP BY cuocthiid, doiid";
                DataTable dt = sqlObject.getDataFromSql(sql, "").Tables[0];
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataRow dr = dt.Rows[i];
                        ds_doi doiChoi = _entities.ds_doi.Find(int.Parse(dr["doiid"].ToString()));
                        if (doiChoi != null)
                        {
                            if (doiChoi.vitridoi == 1)
                            {
                                lblTongDiem1.Text = dr["tongdiem"].ToString();
                            }
                            if (doiChoi.vitridoi == 2)
                            {
                                lblTongDiem2.Text = dr["tongdiem"].ToString();
                            }
                            if (doiChoi.vitridoi == 3)
                            {
                                lblTongDiem3.Text = dr["tongdiem"].ToString();
                            }
                            if (doiChoi.vitridoi == 4)
                            {
                                lblTongDiem4.Text = dr["tongdiem"].ToString();
                            }
                        }
                    }
                }

            }
            _entities.SaveChanges();

        }
        private void onoffKhanGia(bool onoff)
        {
            lblThoiGian.Visible = onoff;
            lblThoiGian.Enabled = onoff;
            label1.Visible = onoff;
            label1.Enabled = onoff;
            label2.Visible = onoff;
            label2.Enabled = onoff;
            label3.Visible = onoff;
            label3.Enabled = onoff;
            label4.Visible = onoff;
            label4.Enabled = onoff;
            lblTongDiem1.Visible = onoff;
            lblTongDiem1.Enabled = onoff;
            lblTongDiem2.Visible = onoff;
            lblTongDiem2.Enabled = onoff;
            lblTongDiem3.Visible = onoff;
            lblTongDiem3.Enabled = onoff;
            lblTongDiem4.Visible = onoff;
            lblTongDiem4.Enabled = onoff;
            var ten = "";
            var dsTen = _entities.ds_doi.Where(x => x.vaitro == "TS").ToList();
            if (dsTen != null && dsTen.Count > 0)
            {
                for (int i = 0; i < dsTen.Count; i++)
                {
                    var lbl = this.Controls.Find("label" + (i + 1), true).FirstOrDefault() as Label;
                    var tachten = dsTen[i].tennguoichoi.Split(' ');
                    for (int j = 1; j < tachten.Length; j++)
                    {
                        ten = tachten[j - 1] + " " + tachten[j];
                    }
                    lbl.Text = ten;
                }
            }

        }
        private void SendEvent(string str)
        {
            // Check we are connected
            if (sock == null || !sock.Connected)
            {
                MessageBox.Show(this, "Must be connected to Send a message");
                return;
            }
            // Read the message from the text box and send it
            try
            {
                // Convert to byte array and send.
                Byte[] byteDateLine = Encoding.ASCII.GetBytes(str.ToCharArray());
                sock.Send(byteDateLine, byteDateLine.Length, 0);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Send lenh dieu khien loi!");
            }

        }

        private void pbClose_Click(object sender, EventArgs e)
        {
            try
            {
                // Gửi thông điệp đóng nếu cần
                SendEvent(id.ToString() + ",cli,connected,off");

                // Đảm bảo socket đã khởi tạo và chưa bị dispose
                if (sock != null && sock.Connected)
                {
                    try
                    {
                        sock.Shutdown(SocketShutdown.Both);
                    }
                    catch (SocketException ex)
                    {
                        Console.WriteLine("Socket shutdown error: " + ex.Message);
                    }

                    try
                    {
                        sock.Close();
                    }
                    catch (SocketException ex)
                    {
                        Console.WriteLine("Socket close error: " + ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error on pbClose: " + ex.Message);
            }
            finally
            {
                // Thoát ứng dụng sau khi đã xử lý mọi thứ
                Application.Exit();
            }
        }

        private void ManHinhMC_Load(object sender, EventArgs e)
        {
            pnlDiemSo.Visible = false;

            this.BackgroundImage = Image.FromFile(currentPath + "\\Resources\\group6\\gt_qmds.png");
            this.BackgroundImageLayout = ImageLayout.Stretch;
            pbClose.BackgroundImage = Image.FromFile(currentPath + "\\Resources\\close11.png");
            pbClose.BackgroundImageLayout = ImageLayout.Stretch;
            pbMini.BackgroundImage = Image.FromFile(currentPath + "\\Resources\\mini1.png");
            pbMini.BackgroundImageLayout = ImageLayout.Stretch;

        }

        private void timeMC_Tick(object sender, EventArgs e)
        {
            if (thoiGianConLai > 1)
            {
                thoiGianConLai = thoiGianConLai - 1;
                lblThoiGian.Text = thoiGianConLai.ToString();
                //lblThoiGiankg.Text = thoiGianConLai.ToString();
            }
            else
            {
                if (!string.IsNullOrEmpty(message))
                {
                    string[] spl = message.Split(',');

                    // Đảm bảo mảng có ít nhất 3 phần tử trước khi truy cập spl[2]
                    if (spl.Length > 2 && spl[2] == "playkhamphachiase")
                    {
                        thoiGianConLai--; // Giảm thêm 1 giây nếu điều kiện đúng
                        lblThoiGian.Text = thoiGianConLai.ToString();
                    }
                    else
                    {
                        timeMC.Enabled = false;
                        lblThoiGian.Text = "END";
                        lblThoiGian.ForeColor = Color.Red;
                    }
                }

                //lblThoiGiankg.Text = "HẾT";
            }
        }

        private void pbMini_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
    }
}
