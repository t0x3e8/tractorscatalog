namespace Enceladus
{
    partial class SearchResultLayout
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
            this.btnMainWindow = new Enceladus.UIToolbox.GradientButton();
            this.btnShowTractor = new Enceladus.UIToolbox.GradientButton();
            this.btnSearch = new Enceladus.UIToolbox.GradientButton();
            this.pcProfiAd = new System.Windows.Forms.PictureBox();
            this.doubleBufferedPictureBox1 = new Enceladus.UIToolbox.DoubleBufferedPictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pcProfiAd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.doubleBufferedPictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnMainWindow
            // 
            this.btnMainWindow.Command = null;
            this.btnMainWindow.Location = new System.Drawing.Point(18, 168);
            this.btnMainWindow.Name = "btnMainWindow";
            this.btnMainWindow.Size = new System.Drawing.Size(171, 40);
            this.btnMainWindow.TabIndex = 8;
            // 
            // btnShowTractor
            // 
            this.btnShowTractor.Command = null;
            this.btnShowTractor.Location = new System.Drawing.Point(18, 68);
            this.btnShowTractor.Name = "btnShowTractor";
            this.btnShowTractor.Size = new System.Drawing.Size(171, 40);
            this.btnShowTractor.TabIndex = 11;
            // 
            // btnSearch
            // 
            this.btnSearch.Command = null;
            this.btnSearch.Location = new System.Drawing.Point(18, 117);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(171, 40);
            this.btnSearch.TabIndex = 12;
            // 
            // pcProfiAd
            // 
            this.pcProfiAd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pcProfiAd.BackColor = System.Drawing.Color.Transparent;
            this.pcProfiAd.BackgroundImage = global::Enceladus.Properties.Resources.profi;
            this.pcProfiAd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pcProfiAd.Location = new System.Drawing.Point(18, 459);
            this.pcProfiAd.Name = "pcProfiAd";
            this.pcProfiAd.Size = new System.Drawing.Size(171, 78);
            this.pcProfiAd.TabIndex = 13;
            this.pcProfiAd.TabStop = false;
            // 
            // doubleBufferedPictureBox1
            // 
            this.doubleBufferedPictureBox1.Image = global::Enceladus.Properties.Resources.Superkatalog_Header;
            this.doubleBufferedPictureBox1.Location = new System.Drawing.Point(3, 3);
            this.doubleBufferedPictureBox1.Name = "doubleBufferedPictureBox1";
            this.doubleBufferedPictureBox1.Size = new System.Drawing.Size(204, 59);
            this.doubleBufferedPictureBox1.TabIndex = 15;
            this.doubleBufferedPictureBox1.TabStop = false;
            // 
            // SearchResultLayout
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackgroundImage = global::Enceladus.Properties.Resources.Background;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.Controls.Add(this.doubleBufferedPictureBox1);
            this.Controls.Add(this.pcProfiAd);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.btnShowTractor);
            this.Controls.Add(this.btnMainWindow);
            this.MaximumSize = new System.Drawing.Size(0, 0);
            this.Name = "SearchResultLayout";
            ((System.ComponentModel.ISupportInitialize)(this.pcProfiAd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.doubleBufferedPictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private UIToolbox.GradientButton btnMainWindow;
        private UIToolbox.GradientButton btnShowTractor;
        private UIToolbox.GradientButton btnSearch;
        private System.Windows.Forms.PictureBox pcProfiAd;
        private UIToolbox.DoubleBufferedPictureBox doubleBufferedPictureBox1;
    }
}
