namespace Enceladus
{
    partial class TractorLayout
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
            this.tractorStatus1 = new Enceladus.UIToolbox.TractorStatus();
            this.btnFirst = new Enceladus.UIToolbox.NavigationIconButton();
            this.btnPrevious = new Enceladus.UIToolbox.NavigationIconButton();
            this.btnNext = new Enceladus.UIToolbox.NavigationIconButton();
            this.btnLast = new Enceladus.UIToolbox.NavigationIconButton();
            this.btnBookmark = new Enceladus.UIToolbox.IconButton();
            this.btnPdf = new Enceladus.UIToolbox.IconButton();
            this.btnPrinter = new Enceladus.UIToolbox.IconButton();
            this.tabsBar1 = new Enceladus.UIToolbox.TabsBar();
            this.btnSearch = new Enceladus.UIToolbox.GradientButton();
            this.btnMainWindow = new Enceladus.UIToolbox.GradientButton();
            this.waitingBar1 = new Enceladus.UIToolbox.WaitingBar();
            this.btnBack = new Enceladus.UIToolbox.GradientButton();
            this.pcProfiAd = new System.Windows.Forms.PictureBox();
            this.doubleBufferedPictureBox1 = new Enceladus.UIToolbox.DoubleBufferedPictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pcProfiAd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.doubleBufferedPictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // tractorStatus1
            // 
            this.tractorStatus1.BackColor = System.Drawing.Color.Transparent;
            this.tractorStatus1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(71)))), ((int)(((byte)(71)))));
            this.tractorStatus1.LabelPattern = "of {0}";
            this.tractorStatus1.Location = new System.Drawing.Point(297, 5);
            this.tractorStatus1.MaximumValue = 15000;
            this.tractorStatus1.MinimumValue = 0;
            this.tractorStatus1.Name = "tractorStatus1";
            this.tractorStatus1.Size = new System.Drawing.Size(99, 16);
            this.tractorStatus1.TabIndex = 26;
            this.tractorStatus1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tractorStatus1.TextBoxBackColor = System.Drawing.Color.DarkGray;
            this.tractorStatus1.ValueWidth = 50;
            // 
            // btnFirst
            // 
            this.btnFirst.BackColor = System.Drawing.Color.Transparent;
            this.btnFirst.ButtonType = Enceladus.UIToolbox.IconButtonType.First;
            this.btnFirst.Checked = false;
            this.btnFirst.Command = null;
            this.btnFirst.Location = new System.Drawing.Point(241, 3);
            this.btnFirst.Name = "btnFirst";
            this.btnFirst.NavigationMode = Enceladus.UIToolbox.NavigatioMode.First;
            this.btnFirst.Size = new System.Drawing.Size(22, 22);
            this.btnFirst.TabIndex = 25;
            // 
            // btnPrevious
            // 
            this.btnPrevious.BackColor = System.Drawing.Color.Transparent;
            this.btnPrevious.ButtonType = Enceladus.UIToolbox.IconButtonType.Previous;
            this.btnPrevious.Checked = false;
            this.btnPrevious.Command = null;
            this.btnPrevious.Location = new System.Drawing.Point(269, 3);
            this.btnPrevious.Name = "btnPrevious";
            this.btnPrevious.NavigationMode = Enceladus.UIToolbox.NavigatioMode.Previous;
            this.btnPrevious.Size = new System.Drawing.Size(22, 22);
            this.btnPrevious.TabIndex = 24;
            // 
            // btnNext
            // 
            this.btnNext.BackColor = System.Drawing.Color.Transparent;
            this.btnNext.ButtonType = Enceladus.UIToolbox.IconButtonType.Next;
            this.btnNext.Checked = false;
            this.btnNext.Command = null;
            this.btnNext.Location = new System.Drawing.Point(402, 3);
            this.btnNext.Name = "btnNext";
            this.btnNext.NavigationMode = Enceladus.UIToolbox.NavigatioMode.Next;
            this.btnNext.Size = new System.Drawing.Size(22, 22);
            this.btnNext.TabIndex = 22;
            // 
            // btnLast
            // 
            this.btnLast.BackColor = System.Drawing.Color.Transparent;
            this.btnLast.ButtonType = Enceladus.UIToolbox.IconButtonType.Last;
            this.btnLast.Checked = false;
            this.btnLast.Command = null;
            this.btnLast.Location = new System.Drawing.Point(430, 3);
            this.btnLast.Name = "btnLast";
            this.btnLast.NavigationMode = Enceladus.UIToolbox.NavigatioMode.Last;
            this.btnLast.Size = new System.Drawing.Size(22, 22);
            this.btnLast.TabIndex = 21;
            // 
            // btnBookmark
            // 
            this.btnBookmark.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBookmark.BackColor = System.Drawing.Color.Transparent;
            this.btnBookmark.ButtonType = Enceladus.UIToolbox.IconButtonType.Bookmark;
            this.btnBookmark.Checked = false;
            this.btnBookmark.Command = null;
            this.btnBookmark.Location = new System.Drawing.Point(703, 3);
            this.btnBookmark.Name = "btnBookmark";
            this.btnBookmark.Size = new System.Drawing.Size(22, 22);
            this.btnBookmark.TabIndex = 20;
            // 
            // btnPdf
            // 
            this.btnPdf.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPdf.BackColor = System.Drawing.Color.Transparent;
            this.btnPdf.ButtonType = Enceladus.UIToolbox.IconButtonType.Pdf;
            this.btnPdf.Checked = false;
            this.btnPdf.Command = null;
            this.btnPdf.Enabled = false;
            this.btnPdf.Location = new System.Drawing.Point(731, 3);
            this.btnPdf.Name = "btnPdf";
            this.btnPdf.Size = new System.Drawing.Size(22, 22);
            this.btnPdf.TabIndex = 19;
            // 
            // btnPrinter
            // 
            this.btnPrinter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrinter.BackColor = System.Drawing.Color.Transparent;
            this.btnPrinter.ButtonType = Enceladus.UIToolbox.IconButtonType.Printer;
            this.btnPrinter.Checked = false;
            this.btnPrinter.Command = null;
            this.btnPrinter.Location = new System.Drawing.Point(759, 3);
            this.btnPrinter.Name = "btnPrinter";
            this.btnPrinter.Size = new System.Drawing.Size(22, 22);
            this.btnPrinter.TabIndex = 18;
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
            this.tabsBar1.TabIndex = 17;
            this.tabsBar1.TabSize = Enceladus.UIToolbox.TabSize.Big;
            // 
            // btnSearch
            // 
            this.btnSearch.Command = null;
            this.btnSearch.Location = new System.Drawing.Point(18, 68);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(171, 40);
            this.btnSearch.TabIndex = 14;
            // 
            // btnMainWindow
            // 
            this.btnMainWindow.Command = null;
            this.btnMainWindow.Location = new System.Drawing.Point(18, 168);
            this.btnMainWindow.Name = "btnMainWindow";
            this.btnMainWindow.Size = new System.Drawing.Size(171, 40);
            this.btnMainWindow.TabIndex = 13;
            // 
            // waitingBar1
            // 
            this.waitingBar1.BackColor = System.Drawing.Color.Transparent;
            this.waitingBar1.DelayInSeconds = 70;
            this.waitingBar1.DisplayStickNumber = 10;
            this.waitingBar1.EndStick = 8;
            this.waitingBar1.FirstColor = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(175)))), ((int)(((byte)(161)))));
            this.waitingBar1.LineStyle = System.Drawing.Drawing2D.LineCap.Round;
            this.waitingBar1.Location = new System.Drawing.Point(458, 3);
            this.waitingBar1.Name = "waitingBar1";
            this.waitingBar1.PenStyle = Enceladus.UIToolbox.PenStyles.Gradient;
            this.waitingBar1.Run = false;
            this.waitingBar1.SecondColor = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.waitingBar1.Size = new System.Drawing.Size(23, 22);
            this.waitingBar1.Smoothing = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            this.waitingBar1.StartStick = 6;
            this.waitingBar1.StickNumber = 10;
            this.waitingBar1.StickThickness = 2;
            this.waitingBar1.TabIndex = 27;
            this.waitingBar1.Text = "waitingBar1";
            // 
            // btnBack
            // 
            this.btnBack.Command = null;
            this.btnBack.Location = new System.Drawing.Point(18, 117);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(171, 40);
            this.btnBack.TabIndex = 28;
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
            this.pcProfiAd.TabIndex = 29;
            this.pcProfiAd.TabStop = false;
            // 
            // doubleBufferedPictureBox1
            // 
            this.doubleBufferedPictureBox1.Image = global::Enceladus.Properties.Resources.Superkatalog_Header;
            this.doubleBufferedPictureBox1.Location = new System.Drawing.Point(3, 3);
            this.doubleBufferedPictureBox1.Name = "doubleBufferedPictureBox1";
            this.doubleBufferedPictureBox1.Size = new System.Drawing.Size(204, 59);
            this.doubleBufferedPictureBox1.TabIndex = 31;
            this.doubleBufferedPictureBox1.TabStop = false;
            // 
            // TractorLayout
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackgroundImage = global::Enceladus.Properties.Resources.Background;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.Controls.Add(this.doubleBufferedPictureBox1);
            this.Controls.Add(this.pcProfiAd);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.waitingBar1);
            this.Controls.Add(this.tractorStatus1);
            this.Controls.Add(this.btnFirst);
            this.Controls.Add(this.btnPrevious);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.btnLast);
            this.Controls.Add(this.btnBookmark);
            this.Controls.Add(this.btnPdf);
            this.Controls.Add(this.btnPrinter);
            this.Controls.Add(this.tabsBar1);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.btnMainWindow);
            this.MaximumSize = new System.Drawing.Size(0, 0);
            this.Name = "TractorLayout";
            ((System.ComponentModel.ISupportInitialize)(this.pcProfiAd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.doubleBufferedPictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private UIToolbox.GradientButton btnSearch;
        private UIToolbox.GradientButton btnMainWindow;
        private UIToolbox.TabsBar tabsBar1;
        private UIToolbox.IconButton btnPrinter;
        private UIToolbox.IconButton btnPdf;
        private UIToolbox.IconButton btnBookmark;
        private UIToolbox.NavigationIconButton btnLast;
        private UIToolbox.NavigationIconButton btnNext;
        private UIToolbox.NavigationIconButton btnFirst;
        private UIToolbox.NavigationIconButton btnPrevious;
        public UIToolbox.TractorStatus tractorStatus1;
        protected internal UIToolbox.WaitingBar waitingBar1;
        private UIToolbox.GradientButton btnBack;
        private System.Windows.Forms.PictureBox pcProfiAd;
        private UIToolbox.DoubleBufferedPictureBox doubleBufferedPictureBox1;
    }
}
