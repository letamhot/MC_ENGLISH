namespace MC_Project
{
    partial class ucThuThach
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
            this.labelDapAn = new System.Windows.Forms.Label();
            this.lblThele = new System.Windows.Forms.Label();
            this.lblDapAn = new System.Windows.Forms.Label();
            this.flowPanelSentences = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // labelDapAn
            // 
            this.labelDapAn.AutoSize = true;
            this.labelDapAn.Font = new System.Drawing.Font("Arial Narrow", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDapAn.Location = new System.Drawing.Point(7, 418);
            this.labelDapAn.Name = "labelDapAn";
            this.labelDapAn.Size = new System.Drawing.Size(95, 31);
            this.labelDapAn.TabIndex = 1;
            this.labelDapAn.Text = "Đáp án:";
            // 
            // lblThele
            // 
            this.lblThele.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblThele.Location = new System.Drawing.Point(14, 0);
            this.lblThele.Name = "lblThele";
            this.lblThele.Size = new System.Drawing.Size(952, 71);
            this.lblThele.TabIndex = 73;
            this.lblThele.Text = "Thể lệ phần thi:";
            // 
            // lblDapAn
            // 
            this.lblDapAn.Font = new System.Drawing.Font("Arial Narrow", 12.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDapAn.Location = new System.Drawing.Point(102, 419);
            this.lblDapAn.Name = "lblDapAn";
            this.lblDapAn.Size = new System.Drawing.Size(864, 133);
            this.lblDapAn.TabIndex = 1;
            this.lblDapAn.Text = "Đáp án:";
            // 
            // flowPanelSentences
            // 
            this.flowPanelSentences.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.flowPanelSentences.Location = new System.Drawing.Point(19, 73);
            this.flowPanelSentences.Margin = new System.Windows.Forms.Padding(2);
            this.flowPanelSentences.Name = "flowPanelSentences";
            this.flowPanelSentences.Size = new System.Drawing.Size(947, 328);
            this.flowPanelSentences.TabIndex = 74;
            // 
            // ucThuThach
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.flowPanelSentences);
            this.Controls.Add(this.lblThele);
            this.Controls.Add(this.lblDapAn);
            this.Controls.Add(this.labelDapAn);
            this.Name = "ucThuThach";
            this.Size = new System.Drawing.Size(987, 552);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label labelDapAn;
        private System.Windows.Forms.Label lblThele;
        private System.Windows.Forms.Label lblDapAn;
        private System.Windows.Forms.FlowLayoutPanel flowPanelSentences;
    }
}
