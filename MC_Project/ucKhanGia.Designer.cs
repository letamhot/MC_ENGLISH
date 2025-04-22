namespace MC_Project
{
    partial class ucKhanGia
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblDA = new System.Windows.Forms.Label();
            this.labelDA = new System.Windows.Forms.Label();
            this.lblCauHoi = new System.Windows.Forms.Label();
            this.lblNoiDung = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblDA
            // 
            this.lblDA.BackColor = System.Drawing.Color.Transparent;
            this.lblDA.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDA.Location = new System.Drawing.Point(157, 405);
            this.lblDA.Name = "lblDA";
            this.lblDA.Size = new System.Drawing.Size(803, 96);
            this.lblDA.TabIndex = 84;
            this.lblDA.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblDA.Visible = false;
            // 
            // labelDA
            // 
            this.labelDA.AutoSize = true;
            this.labelDA.BackColor = System.Drawing.Color.Transparent;
            this.labelDA.Font = new System.Drawing.Font("Arial", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDA.Location = new System.Drawing.Point(48, 435);
            this.labelDA.Name = "labelDA";
            this.labelDA.Size = new System.Drawing.Size(108, 31);
            this.labelDA.TabIndex = 83;
            this.labelDA.Text = "Đáp án:";
            this.labelDA.Visible = false;
            // 
            // lblCauHoi
            // 
            this.lblCauHoi.BackColor = System.Drawing.Color.Transparent;
            this.lblCauHoi.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCauHoi.Location = new System.Drawing.Point(44, 54);
            this.lblCauHoi.Name = "lblCauHoi";
            this.lblCauHoi.Size = new System.Drawing.Size(871, 342);
            this.lblCauHoi.TabIndex = 82;
            this.lblCauHoi.Text = "test";
            // 
            // lblNoiDung
            // 
            this.lblNoiDung.AutoSize = true;
            this.lblNoiDung.BackColor = System.Drawing.Color.Transparent;
            this.lblNoiDung.Font = new System.Drawing.Font("Arial", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNoiDung.Location = new System.Drawing.Point(43, 17);
            this.lblNoiDung.Name = "lblNoiDung";
            this.lblNoiDung.Size = new System.Drawing.Size(200, 31);
            this.lblNoiDung.TabIndex = 81;
            this.lblNoiDung.Text = "Thể lệ phần thi:";
            // 
            // ucKhanGia
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.lblDA);
            this.Controls.Add(this.labelDA);
            this.Controls.Add(this.lblCauHoi);
            this.Controls.Add(this.lblNoiDung);
            this.Name = "ucKhanGia";
            this.Size = new System.Drawing.Size(994, 548);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblDA;
        private System.Windows.Forms.Label labelDA;
        private System.Windows.Forms.Label lblCauHoi;
        private System.Windows.Forms.Label lblNoiDung;
    }
}
