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
    public partial class ucThuThach : UserControl
    {
        private Socket _socket;
        private int _doiid = 0;
        private int _cauhoiid = 0;
        private bool _isStart;
        private int _cuocthiid = 0;
/*        private int thoiGianTraLoi = 0;
*/        private string currentPath = Directory.GetCurrentDirectory();
        QuaMienDiSanEntities _entities = new QuaMienDiSanEntities();
        public ucThuThach()
        {
            InitializeComponent();
        }
        public ucThuThach(Socket sock, int doiid, int cauhoiid, bool start, int cuocthiid)
        {
            InitializeComponent();
            _socket = sock;
            _doiid = doiid;
            _cauhoiid = cauhoiid;
            _isStart = start;
            _cuocthiid = cuocthiid;
            loadUC();
        }

        private void loadUC()
        {
            if (_cauhoiid == 0)
            {
                VisibleGui();
            }
            else
            {
                //lblGioiThieu.Visible = false;
                lblThele.Visible = true;
                displayUCKhamPha(_cauhoiid);

            }
        }

        private void displayUCKhamPha(int cauhoiid)
        {
            ds_cauhoithuthach khamPha = _entities.ds_cauhoithuthach.Find(cauhoiid);
            if (khamPha != null)
            {
                if (khamPha.vitri == 1 || khamPha.vitri == 2)
                {
                    lblThele.Text = "Question " + khamPha.vitri + ": Rearrange the following words or phrases to make a complete sentence";

                }
                else if (khamPha.vitri == 3 || khamPha.vitri == 4)
                {
                    lblThele.Text = "Question " + khamPha.vitri + ": Rearrange the following sentences to make a meaningful conversation";

                }
                else
                {
                    lblThele.Text = "Question " + khamPha.vitri + ": Rearrange the following sentences to make a meaningful paragraph";

                }
                //lblThele.Text = "Question number " + khamPha.vitri + ":";
                lblDapAn.Text = khamPha.dapantext + "\n" + khamPha.dapanABC;
                //lblNoiDungCauHoiKP.Text = khamPha.noidung;
                // Xóa các Button cũ
                flowPanelSentences.Controls.Clear();

                // Đặt FlowLayoutPanel hiển thị theo cột dọc
                flowPanelSentences.FlowDirection = FlowDirection.TopDown;
                flowPanelSentences.WrapContents = false;
                flowPanelSentences.AutoScroll = true; // Bật scroll khi cần thiết

                // Tách nội dung thành các câu
                string[] sentences = khamPha.noidung.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
                string[] answerLabels = { "A", "B", "C", "D", "E" };

                // Sử dụng TextRenderer để đo kích thước text chính xác hơn
                TextFormatFlags flags = TextFormatFlags.WordBreak | TextFormatFlags.TextBoxControl | TextFormatFlags.Left;

                // Font sử dụng cho Button
                Font btnFont = new Font("Arial", 10, FontStyle.Bold);

                // Chiều rộng của Button (trừ đi padding và border)
                int buttonWidth = flowPanelSentences.ClientSize.Width - SystemInformation.VerticalScrollBarWidth - 10;

                foreach (int i in Enumerable.Range(0, sentences.Length))
                {
                    string buttonText = answerLabels[i] + ". " + sentences[i].Trim();

                    Button btn = new Button
                    {
                        Text = buttonText,
                        Font = btnFont,
                        TextAlign = ContentAlignment.MiddleLeft,
                        Padding = new Padding(10),
                        BackColor = _isStart ? Color.LightBlue : Color.LightGray,
                        ForeColor = Color.Black,
                        FlatStyle = FlatStyle.Flat,
                        Width = buttonWidth,
                        Enabled = false
                    };

                    btn.FlatAppearance.BorderColor = Color.DarkBlue;
                    btn.FlatAppearance.BorderSize = 1;

                    // Tính toán chiều cao cần thiết
                    Size proposedSize = new Size(buttonWidth - btn.Padding.Horizontal, int.MaxValue);
                    Size textSize = TextRenderer.MeasureText(btn.Text, btn.Font, proposedSize, flags);

                    // Chiều cao tối thiểu là 50, hoặc cao hơn nếu text nhiều dòng
                    btn.Height = Math.Max(60, textSize.Height + btn.Padding.Vertical + 10);

                    flowPanelSentences.Controls.Add(btn);
                }

            }
        }

        private void VisibleGui()
        {
            lblThele.Visible = true;
           //lblGioiThieu.Visible = true;
            //lblGioiThieu.Text = "Có 04 câu hỏi về địa danh hay nhân vật xưa và nay nổi tiếng trong các lĩnh vực, những sự kiện mang ý nghĩa chính trị, kinh tế, xã hội của quê hương Quảng Bình. Các thí sinh hãy phát huy tối đa khả năng phán đoán, nhanh tay, nhanh mắt trả lời mỗi câu hỏi trong 30’’.\nTrả lời từ 1-10’’ được 30 điểm, từ 11-20’’ được 20 điểm và từ 21-30’’ được 10 điểm.\nNếu câu trả lời sai chính tả, thí sinh sẽ không được tính điểm.\nĐiểm tối đa cho mỗi thí sinh ở phần thi này là 120 điểm.";
            labelDapAn.Visible = false;
            lblDapAn.Visible = false;
        }
    }
}
