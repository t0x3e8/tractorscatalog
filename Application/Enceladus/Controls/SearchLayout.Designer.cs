using Enceladus.UIToolbox;
namespace Enceladus
{
    partial class SearchLayout
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
            this.WarningLabel = new Enceladus.UIToolbox.DisappearingLabel();
            this.pcProfiAd = new System.Windows.Forms.PictureBox();
            this.tabsBar1 = new Enceladus.UIToolbox.TabsBar();
            this.btnMainWindow = new Enceladus.UIToolbox.GradientButton();
            this.btnStartSearch = new Enceladus.UIToolbox.RedGradientButton();
            this.doubleBufferedPictureBox1 = new Enceladus.UIToolbox.DoubleBufferedPictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pcProfiAd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.doubleBufferedPictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // WarningLabel
            // 
            this.WarningLabel.BackColor = System.Drawing.Color.Transparent;
            this.WarningLabel.Font = new System.Drawing.Font("Trebuchet MS", 11F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.WarningLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(97)))), ((int)(((byte)(96)))));
            this.WarningLabel.InformResizer = null;
            this.WarningLabel.Location = new System.Drawing.Point(18, 164);
            this.WarningLabel.MaximalExpectedFontSize = 5;
            this.WarningLabel.Name = "WarningLabel";
            this.WarningLabel.Size = new System.Drawing.Size(171, 45);
            this.WarningLabel.SupportResizing = true;
            this.WarningLabel.TabIndex = 9;
            this.WarningLabel.Visible = false;
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
            this.pcProfiAd.TabIndex = 6;
            this.pcProfiAd.TabStop = false;
            // 
            // tabsBar1
            // 
            this.tabsBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabsBar1.BackColor = System.Drawing.Color.Transparent;
            this.tabsBar1.Location = new System.Drawing.Point(229, 11);
            this.tabsBar1.Name = "tabsBar1";
            this.tabsBar1.SelectedIndex = 0;
            this.tabsBar1.Size = new System.Drawing.Size(550, 34);
            this.tabsBar1.TabIndex = 8;
            this.tabsBar1.TabSize = Enceladus.UIToolbox.TabSize.Big;
            // 
            // btnMainWindow
            // 
            this.btnMainWindow.Command = null;
            this.btnMainWindow.Location = new System.Drawing.Point(18, 117);
            this.btnMainWindow.Name = "btnMainWindow";
            this.btnMainWindow.Size = new System.Drawing.Size(171, 40);
            this.btnMainWindow.TabIndex = 7;
            // 
            // btnStartSearch
            // 
            this.btnStartSearch.Command = null;
            this.btnStartSearch.Location = new System.Drawing.Point(18, 68);
            this.btnStartSearch.Name = "btnStartSearch";
            this.btnStartSearch.Size = new System.Drawing.Size(171, 40);
            this.btnStartSearch.TabIndex = 6;
            // 
            // doubleBufferedPictureBox1
            // 
            this.doubleBufferedPictureBox1.Image = global::Enceladus.Properties.Resources.Superkatalog_Header;
            this.doubleBufferedPictureBox1.Location = new System.Drawing.Point(3, 3);
            this.doubleBufferedPictureBox1.Name = "doubleBufferedPictureBox1";
            this.doubleBufferedPictureBox1.Size = new System.Drawing.Size(204, 59);
            this.doubleBufferedPictureBox1.TabIndex = 10;
            this.doubleBufferedPictureBox1.TabStop = false;
            // 
            // SearchLayout
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackgroundImage = global::Enceladus.Properties.Resources.Background;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.Controls.Add(this.doubleBufferedPictureBox1);
            this.Controls.Add(this.WarningLabel);
            this.Controls.Add(this.pcProfiAd);
            this.Controls.Add(this.tabsBar1);
            this.Controls.Add(this.btnMainWindow);
            this.Controls.Add(this.btnStartSearch);
            this.MaximumSize = new System.Drawing.Size(0, 0);
            this.MinimumSize = new System.Drawing.Size(0, 0);
            this.Name = "SearchLayout";
            ((System.ComponentModel.ISupportInitialize)(this.pcProfiAd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.doubleBufferedPictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private UIToolbox.RedGradientButton btnStartSearch;
        private UIToolbox.GradientButton btnMainWindow;
        private TabsBar tabsBar1;
        private System.Windows.Forms.PictureBox pcProfiAd;
        private DisappearingLabel WarningLabel;
        private DoubleBufferedPictureBox doubleBufferedPictureBox1;
    }
}
