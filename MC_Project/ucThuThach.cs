using MC_Project.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.InteropServices;
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
            if (khamPha == null) return;
            _entities.Entry(khamPha).Reload(); // ⚠️ Nạp lại từ DB


            //Hiển thị loại câu hỏi theo vị trí
            if (khamPha.vitri == 1 || khamPha.vitri == 2)
            {
                lblThele.Text = "Question " + khamPha.vitri + ": Rearrange the following words or phrases to make a complete sentence.";

            }
            else if (khamPha.vitri == 3 || khamPha.vitri == 4)
            {
                lblThele.Text = "Question " + khamPha.vitri + ": Rearrange the following utterances to make a meaningful conversation.";

            }
            else if (khamPha.vitri == 5)
            {
                lblThele.Text = "Question " + khamPha.vitri + ": Rearrange the following sentences to make a meaningful paragraph.";

            }
            else
            {
                lblThele.Text = "Question " + khamPha.vitri + ":";

            }
            lblDapAn.Text = khamPha.dapanABC;
            // Tối ưu hiển thị, tránh nháy bằng cách bật DoubleBuffered
            EnableDoubleBuffering(flowPanelSentences);

            flowPanelSentences.SuspendLayout();
            flowPanelSentences.Controls.Clear();

            flowPanelSentences.FlowDirection = FlowDirection.TopDown;
            flowPanelSentences.WrapContents = false;
            flowPanelSentences.AutoScroll = true;
            flowPanelSentences.Padding = new Padding(10);

            string[] sentences = khamPha.noidung.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            string[] answerLabels = { "A", "B", "C", "D", "E" };

            Color primaryColor = Color.FromArgb(52, 152, 219);
            Color hoverColor = Color.FromArgb(41, 128, 185);
            Font btnFont = new Font("Arial", 12, FontStyle.Bold);
            Color textColor = Color.White;
            int buttonWidth = flowPanelSentences.ClientSize.Width - SystemInformation.VerticalScrollBarWidth - 10;

            for (int i = 0; i < sentences.Length && i < answerLabels.Length; i++)
            {
                string text = answerLabels[i] + ". " + sentences[i].Trim();

                Button btn = new Button
                {
                    Text = text,
                    Font = btnFont,
                    ForeColor = textColor,
                    BackColor = primaryColor,
                    FlatStyle = FlatStyle.Flat,
                    Width = buttonWidth,
                    Height = 60, // Base height
                    Margin = new Padding(0, 0, 0, 10),
                    TextAlign = ContentAlignment.MiddleLeft,
                    Padding = new Padding(15, 10, 10, 10),
                    Cursor = Cursors.Hand,
                    UseVisualStyleBackColor = false,
                    AutoSize = false, // QUAN TRỌNG: Không dùng AutoSize để tự điều chỉnh chiều ngang
                };

                // Tính toán chiều cao theo nội dung (nếu có dòng dài)
                Size textSize = TextRenderer.MeasureText(btn.Text, btn.Font, new Size(buttonWidth - btn.Padding.Horizontal, int.MaxValue), TextFormatFlags.WordBreak);
                btn.Height = Math.Max(60, textSize.Height + btn.Padding.Vertical + 10);

                // Tối ưu FlatAppearance
                btn.FlatAppearance.BorderSize = 0;
                btn.FlatAppearance.MouseOverBackColor = hoverColor;
                btn.FlatAppearance.MouseDownBackColor = Color.FromArgb(32, 102, 155);

                // Bo góc
                btn.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, btn.Width, btn.Height, 15, 15));

                // Hover effect
                btn.MouseEnter += (s, e) => btn.BackColor = hoverColor;
                btn.MouseLeave += (s, e) => btn.BackColor = primaryColor;

                flowPanelSentences.Controls.Add(btn);
            }

            flowPanelSentences.ResumeLayout();

        
        }
        private void EnableDoubleBuffering(Control control)
        {
            typeof(Control).InvokeMember("DoubleBuffered",
                System.Reflection.BindingFlags.SetProperty |
                System.Reflection.BindingFlags.Instance |
                System.Reflection.BindingFlags.NonPublic,
                null, control, new object[] { true });
        }

        // Hàm tạo region bo góc (thêm vào class)
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse);
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
