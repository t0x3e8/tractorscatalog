namespace Enceladus
{
    partial class ViewTractorPage
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
            this.pbImage = new System.Windows.Forms.PictureBox();
            this.NoImageWarning = new Enceladus.UIToolbox.ControlLabel();
            ((System.ComponentModel.ISupportInitialize)(this.pbImage)).BeginInit();
            this.SuspendLayout();
            // 
            // pbImage
            // 
            this.pbImage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pbImage.BackColor = System.Drawing.Color.Transparent;
            this.pbImage.Location = new System.Drawing.Point(9, 50);
            this.pbImage.Name = "pbImage";
            this.pbImage.Size = new System.Drawing.Size(548, 437);
            this.pbImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbImage.TabIndex = 3;
            this.pbImage.TabStop = false;
            this.pbImage.Visible = false;
            // 
            // NoImageWarning
            // 
            this.NoImageWarning.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.NoImageWarning.ForeColor = System.Drawing.Color.Black;
            this.NoImageWarning.InformResizer = null;
            this.NoImageWarning.Location = new System.Drawing.Point(9, 114);
            this.NoImageWarning.MaximalExpectedFontSize = 5;
            this.NoImageWarning.Name = "NoImageWarning";
            this.NoImageWarning.Size = new System.Drawing.Size(548, 85);
            this.NoImageWarning.SupportResizing = false;
            this.NoImageWarning.TabIndex = 5;
            this.NoImageWarning.Text = "controlLabel1";
            this.NoImageWarning.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ViewTractorPage
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.NoImageWarning);
            this.Controls.Add(this.pbImage);
            this.MaximumSize = new System.Drawing.Size(0, 0);
            this.Name = "ViewTractorPage";
            this.Controls.SetChildIndex(this.pbImage, 0);
            this.Controls.SetChildIndex(this.NoImageWarning, 0);
            ((System.ComponentModel.ISupportInitialize)(this.pbImage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbImage;
        private UIToolbox.ControlLabel NoImageWarning;

    }
}
