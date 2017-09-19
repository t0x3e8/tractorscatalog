using Enceladus.UIToolbox;
namespace Enceladus
{
    partial class MainLayout
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
            this.pcBigLogo = new Enceladus.UIToolbox.DoubleBufferedPictureBox();
            this.pcProfiAd = new Enceladus.UIToolbox.DoubleBufferedPictureBox();
            this.btnAbout = new Enceladus.UIToolbox.GradientButton();
            this.btnProductInfo = new Enceladus.UIToolbox.GradientButton();
            this.btnVendor = new Enceladus.UIToolbox.GradientButton();
            this.btnShowOne = new Enceladus.UIToolbox.GradientButton();
            this.btnSearch = new Enceladus.UIToolbox.GradientButton();
            this.doubleBufferedPictureBox1 = new Enceladus.UIToolbox.DoubleBufferedPictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pcBigLogo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcProfiAd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.doubleBufferedPictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pcBigLogo
            // 
            this.pcBigLogo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pcBigLogo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pcBigLogo.Image = global::Enceladus.Properties.Resources.BigLogo;
            this.pcBigLogo.Location = new System.Drawing.Point(213, 30);
            this.pcBigLogo.Name = "pcBigLogo";
            this.pcBigLogo.Size = new System.Drawing.Size(558, 501);
            this.pcBigLogo.TabIndex = 8;
            this.pcBigLogo.TabStop = false;
            // 
            // pcProfiAd
            // 
            this.pcProfiAd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pcProfiAd.BackColor = System.Drawing.Color.Transparent;
            this.pcProfiAd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pcProfiAd.Image = global::Enceladus.Properties.Resources.profi_gray;
            this.pcProfiAd.Location = new System.Drawing.Point(18, 459);
            this.pcProfiAd.Name = "pcProfiAd";
            this.pcProfiAd.Size = new System.Drawing.Size(171, 78);
            this.pcProfiAd.TabIndex = 6;
            this.pcProfiAd.TabStop = false;
            // 
            // btnAbout
            // 
            this.btnAbout.Command = null;
            this.btnAbout.Location = new System.Drawing.Point(18, 268);
            this.btnAbout.Name = "btnAbout";
            this.btnAbout.Size = new System.Drawing.Size(171, 40);
            this.btnAbout.TabIndex = 4;
            // 
            // btnProductInfo
            // 
            this.btnProductInfo.Command = null;
            this.btnProductInfo.Location = new System.Drawing.Point(18, 218);
            this.btnProductInfo.Name = "btnProductInfo";
            this.btnProductInfo.Size = new System.Drawing.Size(171, 40);
            this.btnProductInfo.TabIndex = 3;
            // 
            // btnVendor
            // 
            this.btnVendor.Command = null;
            this.btnVendor.Location = new System.Drawing.Point(18, 168);
            this.btnVendor.Name = "btnVendor";
            this.btnVendor.Size = new System.Drawing.Size(171, 40);
            this.btnVendor.TabIndex = 2;
            // 
            // btnShowOne
            // 
            this.btnShowOne.Command = null;
            this.btnShowOne.Location = new System.Drawing.Point(18, 117);
            this.btnShowOne.Name = "btnShowOne";
            this.btnShowOne.Size = new System.Drawing.Size(171, 40);
            this.btnShowOne.TabIndex = 1;
            // 
            // btnSearch
            // 
            this.btnSearch.Command = null;
            this.btnSearch.Location = new System.Drawing.Point(18, 68);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(171, 40);
            this.btnSearch.TabIndex = 0;
            // 
            // doubleBufferedPictureBox1
            // 
            this.doubleBufferedPictureBox1.Image = global::Enceladus.Properties.Resources.Superkatalog_Header;
            this.doubleBufferedPictureBox1.Location = new System.Drawing.Point(3, 3);
            this.doubleBufferedPictureBox1.Name = "doubleBufferedPictureBox1";
            this.doubleBufferedPictureBox1.Size = new System.Drawing.Size(204, 59);
            this.doubleBufferedPictureBox1.TabIndex = 9;
            this.doubleBufferedPictureBox1.TabStop = false;
            // 
            // MainLayout
            // 
            this.BackgroundImage = global::Enceladus.Properties.Resources.Background;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.Controls.Add(this.doubleBufferedPictureBox1);
            this.Controls.Add(this.pcBigLogo);
            this.Controls.Add(this.pcProfiAd);
            this.Controls.Add(this.btnAbout);
            this.Controls.Add(this.btnProductInfo);
            this.Controls.Add(this.btnVendor);
            this.Controls.Add(this.btnShowOne);
            this.Controls.Add(this.btnSearch);
            this.MaximumSize = new System.Drawing.Size(0, 0);
            this.MinimumSize = new System.Drawing.Size(0, 0);
            this.Name = "MainLayout";
            ((System.ComponentModel.ISupportInitialize)(this.pcBigLogo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcProfiAd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.doubleBufferedPictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private UIToolbox.GradientButton btnSearch;
        private UIToolbox.GradientButton btnShowOne;
        private UIToolbox.GradientButton btnVendor;
        private UIToolbox.GradientButton btnProductInfo;
        private UIToolbox.GradientButton btnAbout;
        private DoubleBufferedPictureBox pcProfiAd;
        private DoubleBufferedPictureBox pcBigLogo;
        private DoubleBufferedPictureBox doubleBufferedPictureBox1;
    }
}
