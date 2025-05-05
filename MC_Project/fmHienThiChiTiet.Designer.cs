namespace MC_Project
{
    partial class fmHienThiChiTiet
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fmHienThiChiTiet));
            this.lblDapAnCT = new System.Windows.Forms.Label();
            this.pbMini = new System.Windows.Forms.PictureBox();
            this.pbClose = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbMini)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbClose)).BeginInit();
            this.SuspendLayout();
            // 
            // lblDapAnCT
            // 
            this.lblDapAnCT.BackColor = System.Drawing.Color.Transparent;
            this.lblDapAnCT.Font = new System.Drawing.Font("Arial", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDapAnCT.Location = new System.Drawing.Point(148, 231);
            this.lblDapAnCT.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblDapAnCT.Name = "lblDapAnCT";
            this.lblDapAnCT.Size = new System.Drawing.Size(1083, 465);
            this.lblDapAnCT.TabIndex = 1;
            this.lblDapAnCT.Text = "B. There are about 1.3 million\nD. Khmer people\nA. living in the southern province" +
    "s \nE. of Soc Trang, Tra Vinh\nC. and Can Tho.";
            this.lblDapAnCT.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pbMini
            // 
            this.pbMini.BackColor = System.Drawing.Color.Transparent;
            this.pbMini.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pbMini.BackgroundImage")));
            this.pbMini.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pbMini.Location = new System.Drawing.Point(1309, 23);
            this.pbMini.Name = "pbMini";
            this.pbMini.Size = new System.Drawing.Size(16, 17);
            this.pbMini.TabIndex = 83;
            this.pbMini.TabStop = false;
            this.pbMini.Click += new System.EventHandler(this.pbMini_Click);
            // 
            // pbClose
            // 
            this.pbClose.BackColor = System.Drawing.Color.Transparent;
            this.pbClose.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pbClose.BackgroundImage")));
            this.pbClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pbClose.Location = new System.Drawing.Point(1331, 23);
            this.pbClose.Name = "pbClose";
            this.pbClose.Size = new System.Drawing.Size(16, 17);
            this.pbClose.TabIndex = 82;
            this.pbClose.TabStop = false;
            this.pbClose.Click += new System.EventHandler(this.pbClose_Click);
            // 
            // fmHienThiChiTiet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1368, 768);
            this.Controls.Add(this.pbMini);
            this.Controls.Add(this.pbClose);
            this.Controls.Add(this.lblDapAnCT);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "fmHienThiChiTiet";
            this.Text = "Hiển thị chi tiết đáp án";
            this.Load += new System.EventHandler(this.fmHienThiChiTiet_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbMini)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbClose)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label lblDapAnCT;
        private System.Windows.Forms.PictureBox pbMini;
        private System.Windows.Forms.PictureBox pbClose;
    }
}