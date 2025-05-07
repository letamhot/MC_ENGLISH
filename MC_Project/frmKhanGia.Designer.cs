namespace MC_Project
{
    partial class frmKhanGia
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
            System.Windows.Forms.PictureBox pbClose;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmKhanGia));
            this.pbDA = new System.Windows.Forms.Panel();
            this.lblDapAn = new System.Windows.Forms.Label();
            this.lblCauHoi = new System.Windows.Forms.Label();
            this.labelCauHoi = new System.Windows.Forms.Label();
            pbClose = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(pbClose)).BeginInit();
            this.pbDA.SuspendLayout();
            this.SuspendLayout();
            // 
            // pbClose
            // 
            pbClose.BackColor = System.Drawing.Color.Transparent;
            pbClose.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pbClose.BackgroundImage")));
            pbClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            pbClose.Location = new System.Drawing.Point(1340, 2);
            pbClose.Name = "pbClose";
            pbClose.Size = new System.Drawing.Size(25, 23);
            pbClose.TabIndex = 97;
            pbClose.TabStop = false;
            pbClose.Click += new System.EventHandler(this.pbClose_Click);
            // 
            // pbDA
            // 
            this.pbDA.BackColor = System.Drawing.Color.Transparent;
            this.pbDA.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pbDA.BackgroundImage")));
            this.pbDA.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pbDA.Controls.Add(this.lblDapAn);
            this.pbDA.Location = new System.Drawing.Point(101, 573);
            this.pbDA.Name = "pbDA";
            this.pbDA.Size = new System.Drawing.Size(1201, 137);
            this.pbDA.TabIndex = 96;
            // 
            // lblDapAn
            // 
            this.lblDapAn.Font = new System.Drawing.Font("Arial", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDapAn.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblDapAn.Location = new System.Drawing.Point(487, 4);
            this.lblDapAn.Name = "lblDapAn";
            this.lblDapAn.Size = new System.Drawing.Size(680, 129);
            this.lblDapAn.TabIndex = 0;
            this.lblDapAn.Text = "label3";
            this.lblDapAn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCauHoi
            // 
            this.lblCauHoi.BackColor = System.Drawing.Color.Transparent;
            this.lblCauHoi.Font = new System.Drawing.Font("Arial", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCauHoi.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblCauHoi.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.lblCauHoi.Location = new System.Drawing.Point(154, 245);
            this.lblCauHoi.Name = "lblCauHoi";
            this.lblCauHoi.Size = new System.Drawing.Size(1136, 314);
            this.lblCauHoi.TabIndex = 95;
            // 
            // labelCauHoi
            // 
            this.labelCauHoi.BackColor = System.Drawing.Color.Transparent;
            this.labelCauHoi.Font = new System.Drawing.Font("Arial", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCauHoi.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.labelCauHoi.Location = new System.Drawing.Point(154, 200);
            this.labelCauHoi.Name = "labelCauHoi";
            this.labelCauHoi.Size = new System.Drawing.Size(965, 42);
            this.labelCauHoi.TabIndex = 94;
            this.labelCauHoi.Text = "Câu hỏi";
            this.labelCauHoi.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // frmKhanGia
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1368, 768);
            this.Controls.Add(this.pbDA);
            this.Controls.Add(this.lblCauHoi);
            this.Controls.Add(this.labelCauHoi);
            this.Controls.Add(pbClose);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmKhanGia";
            this.Text = "frmKhanGia";
            ((System.ComponentModel.ISupportInitialize)(pbClose)).EndInit();
            this.pbDA.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pbDA;
        private System.Windows.Forms.Label lblDapAn;
        private System.Windows.Forms.Label lblCauHoi;
        private System.Windows.Forms.Label labelCauHoi;
    }
}