namespace Enceladus
{
    partial class TractorBasePage
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
            this.lblBrandType = new Enceladus.UIToolbox.InputBoxLabel();
            this.tableLayoutPanel1 = new UIToolbox.TransparentTableLayoutPanel();
            this.lblBrandName = new Enceladus.UIToolbox.InputBoxLabel();
            this.lblYear = new Enceladus.UIToolbox.InputBoxLabel();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblBrandType
            // 
            this.lblBrandType.BackColor = System.Drawing.Color.Transparent;
            this.lblBrandType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblBrandType.Font = new System.Drawing.Font("Trebuchet MS", 15F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Pixel);
            this.lblBrandType.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(71)))), ((int)(((byte)(71)))));
            this.lblBrandType.HorizontalAligment = System.Drawing.StringAlignment.Far;
            this.lblBrandType.Label = null;
            this.lblBrandType.Location = new System.Drawing.Point(310, 0);
            this.lblBrandType.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.lblBrandType.Name = "lblBrandType";
            this.lblBrandType.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.lblBrandType.Pattern = null;
            this.lblBrandType.Size = new System.Drawing.Size(253, 30);
            this.lblBrandType.TabIndex = 5;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45F));
            this.tableLayoutPanel1.Controls.Add(this.lblBrandType, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblBrandName, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblYear, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(566, 30);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // lblBrandName
            // 
            this.lblBrandName.BackColor = System.Drawing.Color.Transparent;
            this.lblBrandName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblBrandName.Font = new System.Drawing.Font("Trebuchet MS", 15F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Pixel);
            this.lblBrandName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(71)))), ((int)(((byte)(71)))));
            this.lblBrandName.HorizontalAligment = System.Drawing.StringAlignment.Near;
            this.lblBrandName.Label = null;
            this.lblBrandName.Location = new System.Drawing.Point(3, 0);
            this.lblBrandName.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.lblBrandName.Name = "lblBrandName";
            this.lblBrandName.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.lblBrandName.Pattern = null;
            this.lblBrandName.Size = new System.Drawing.Size(251, 30);
            this.lblBrandName.TabIndex = 4;
            // 
            // lblYear
            // 
            this.lblYear.BackColor = System.Drawing.Color.Transparent;
            this.lblYear.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblYear.Font = new System.Drawing.Font("Trebuchet MS", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.lblYear.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(71)))), ((int)(((byte)(71)))));
            this.lblYear.HorizontalAligment = System.Drawing.StringAlignment.Near;
            this.lblYear.Label = null;
            this.lblYear.Location = new System.Drawing.Point(254, 0);
            this.lblYear.Margin = new System.Windows.Forms.Padding(0);
            this.lblYear.Name = "lblYear";
            this.lblYear.Pattern = null;
            this.lblYear.Size = new System.Drawing.Size(56, 30);
            this.lblYear.TabIndex = 4;
            // 
            // TractorBasePage
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Transparent;
            this.BackgroundImage = null;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "TractorBasePage";
            this.Load += new System.EventHandler(this.TractorBasePage_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private UIToolbox.InputBoxLabel lblBrandType;
        private UIToolbox.TransparentTableLayoutPanel tableLayoutPanel1;
        private UIToolbox.InputBoxLabel lblBrandName;
        private UIToolbox.InputBoxLabel lblYear;

    }
}
