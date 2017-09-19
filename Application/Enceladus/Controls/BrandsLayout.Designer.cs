namespace Enceladus
{
    partial class BrandsLayout
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
            this.tabsBar1 = new Enceladus.UIToolbox.TabsBar();
            this.btnMainWindow = new Enceladus.UIToolbox.GradientButton();
            this.doubleBufferedPictureBox1 = new Enceladus.UIToolbox.DoubleBufferedPictureBox();
            this.pcProfiAd = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.doubleBufferedPictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcProfiAd)).BeginInit();
            this.SuspendLayout();
            // 
            // tabsBar1
            // 
            this.tabsBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabsBar1.BackColor = System.Drawing.Color.Transparent;
            this.tabsBar1.Location = new System.Drawing.Point(229, 20);
            this.tabsBar1.Name = "tabsBar1";
            this.tabsBar1.SelectedIndex = 0;
            this.tabsBar1.Size = new System.Drawing.Size(550, 35);
            this.tabsBar1.TabIndex = 19;
            this.tabsBar1.TabSize = Enceladus.UIToolbox.TabSize.Little;
            // 
            // btnMainWindow
            // 
            this.btnMainWindow.Command = null;
            this.btnMainWindow.Location = new System.Drawing.Point(18, 68);
            this.btnMainWindow.Name = "btnMainWindow";
            this.btnMainWindow.Size = new System.Drawing.Size(171, 40);
            this.btnMainWindow.TabIndex = 15;
            // 
            // doubleBufferedPictureBox1
            // 
            this.doubleBufferedPictureBox1.Image = global::Enceladus.Properties.Resources.Superkatalog_Header;
            this.doubleBufferedPictureBox1.Location = new System.Drawing.Point(3, 3);
            this.doubleBufferedPictureBox1.Name = "doubleBufferedPictureBox1";
            this.doubleBufferedPictureBox1.Size = new System.Drawing.Size(204, 59);
            this.doubleBufferedPictureBox1.TabIndex = 20;
            this.doubleBufferedPictureBox1.TabStop = false;
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
            this.pcProfiAd.TabIndex = 21;
            this.pcProfiAd.TabStop = false;
            // 
            // BrandsLayout
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackgroundImage = global::Enceladus.Properties.Resources.Background;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.Controls.Add(this.pcProfiAd);
            this.Controls.Add(this.doubleBufferedPictureBox1);
            this.Controls.Add(this.tabsBar1);
            this.Controls.Add(this.btnMainWindow);
            this.MaximumSize = new System.Drawing.Size(0, 0);
            this.Name = "BrandsLayout";
            ((System.ComponentModel.ISupportInitialize)(this.doubleBufferedPictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcProfiAd)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private UIToolbox.GradientButton btnMainWindow;
        private UIToolbox.TabsBar tabsBar1;
        private UIToolbox.DoubleBufferedPictureBox doubleBufferedPictureBox1;
        private System.Windows.Forms.PictureBox pcProfiAd;
    }
}
