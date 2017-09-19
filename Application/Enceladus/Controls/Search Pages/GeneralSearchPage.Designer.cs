namespace Enceladus
{
    partial class GeneralSearchPage
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
            this.switchControl1 = new Enceladus.UIToolbox.SwitchControl();
            this.pnlBrands = new Enceladus.UIToolbox.BrandsPanel();
            this.switchControl2 = new Enceladus.UIToolbox.SwitchControl();
            this.pnYears = new Enceladus.UIToolbox.DateControl();
            this.pnlYear = new Enceladus.UIToolbox.BorderedPanel();
            this.pnlBrands.SuspendLayout();
            this.pnlYear.SuspendLayout();
            this.SuspendLayout();
            // 
            // switchControl1
            // 
            this.switchControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.switchControl1.Caption = "demonstration caption";
            this.switchControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.switchControl1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(35)))));
            this.switchControl1.Hint = "Off/On";
            this.switchControl1.HintFont = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.switchControl1.HintFontColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(111)))), ((int)(((byte)(111)))));
            this.switchControl1.InformResizer = null;
            this.switchControl1.Location = new System.Drawing.Point(280, 3);
            this.switchControl1.MaximalExpectedFontSize = 5;
            this.switchControl1.Name = "switchControl1";
            this.switchControl1.ShowHintText = true;
            this.switchControl1.Size = new System.Drawing.Size(273, 23);
            this.switchControl1.State = false;
            this.switchControl1.SupportResizing = true;
            this.switchControl1.TabIndex = 0;
            this.switchControl1.TextWidth = 200;
            // 
            // pnlBrands
            // 
            this.pnlBrands.AllBrandsToggler = this.switchControl1;
            this.pnlBrands.AutoChildrenArrange = false;
            this.pnlBrands.BackColor = System.Drawing.Color.Transparent;
            this.pnlBrands.BorderColor = System.Drawing.Color.White;
            this.pnlBrands.BorderWidth = 1F;
            this.pnlBrands.Caption = "Hersteller?";
            this.pnlBrands.Controls.Add(this.switchControl1);
            this.pnlBrands.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBrands.InformResizer = null;
            this.pnlBrands.IsSelectable = false;
            this.pnlBrands.Location = new System.Drawing.Point(0, 104);
            this.pnlBrands.Margin = new System.Windows.Forms.Padding(0);
            this.pnlBrands.MaximalExpectedFontSize = 1;
            this.pnlBrands.Name = "pnlBrands";
            this.pnlBrands.Padding = new System.Windows.Forms.Padding(10, 8, 4, 2);
            this.pnlBrands.ShowBorder = false;
            this.pnlBrands.Size = new System.Drawing.Size(566, 409);
            this.pnlBrands.SupportResizing = true;
            this.pnlBrands.TabIndex = 1;
            // 
            // switchControl2
            // 
            this.switchControl2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.switchControl2.BackColor = System.Drawing.Color.Transparent;
            this.switchControl2.Caption = "demonstration caption";
            this.switchControl2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.switchControl2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(35)))));
            this.switchControl2.Hint = "Off/On";
            this.switchControl2.HintFont = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.switchControl2.HintFontColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(111)))), ((int)(((byte)(111)))));
            this.switchControl2.InformResizer = null;
            this.switchControl2.Location = new System.Drawing.Point(280, 5);
            this.switchControl2.MaximalExpectedFontSize = 5;
            this.switchControl2.Name = "switchControl2";
            this.switchControl2.ShowHintText = true;
            this.switchControl2.Size = new System.Drawing.Size(273, 26);
            this.switchControl2.State = false;
            this.switchControl2.SupportResizing = true;
            this.switchControl2.TabIndex = 1;
            this.switchControl2.TextWidth = 200;
            // 
            // pnYears
            // 
            this.pnYears.AllYearsToggler = this.switchControl2;
            this.pnYears.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pnYears.InformResizer = null;
            this.pnYears.Location = new System.Drawing.Point(7, 37);
            this.pnYears.MaximalExpectedFontSize = 5;
            this.pnYears.Name = "pnYears";
            this.pnYears.Padding = new System.Windows.Forms.Padding(10, 19, 10, 20);
            this.pnYears.Size = new System.Drawing.Size(550, 59);
            this.pnYears.SupportResizing = true;
            this.pnYears.TabIndex = 0;
            // 
            // pnlYear
            // 
            this.pnlYear.AutoChildrenArrange = false;
            this.pnlYear.BackColor = System.Drawing.Color.Transparent;
            this.pnlYear.BorderColor = System.Drawing.Color.White;
            this.pnlYear.BorderWidth = 1F;
            this.pnlYear.Caption = "CalendarYear?";
            this.pnlYear.Controls.Add(this.pnYears);
            this.pnlYear.Controls.Add(this.switchControl2);
            this.pnlYear.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlYear.InformResizer = null;
            this.pnlYear.Location = new System.Drawing.Point(0, 0);
            this.pnlYear.Margin = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.pnlYear.MaximalExpectedFontSize = 5;
            this.pnlYear.Name = "pnlYear";
            this.pnlYear.Padding = new System.Windows.Forms.Padding(2);
            this.pnlYear.ShowBorder = false;
            this.pnlYear.Size = new System.Drawing.Size(566, 104);
            this.pnlYear.SupportResizing = true;
            this.pnlYear.TabIndex = 0;
            // 
            // GeneralSearchPage
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Transparent;
            this.BackgroundImage = null;
            this.Controls.Add(this.pnlBrands);
            this.Controls.Add(this.pnlYear);
            this.MaximumSize = new System.Drawing.Size(0, 0);
            this.Name = "GeneralSearchPage";
            this.pnlBrands.ResumeLayout(false);
            this.pnlYear.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        internal UIToolbox.SwitchControl switchControl1;
        internal UIToolbox.BrandsPanel pnlBrands;
        internal UIToolbox.SwitchControl switchControl2;
        internal UIToolbox.DateControl pnYears;
        internal UIToolbox.BorderedPanel pnlYear;


    }
}
