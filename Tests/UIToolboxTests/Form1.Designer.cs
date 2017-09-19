namespace UIToolboxTests
{
    partial class Form1
    {
        /// <summary>v
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.disappearingLabel1 = new Enceladus.UIToolbox.DisappearingLabel();
            this.gradientIconButton1 = new Enceladus.UIToolbox.GradientIconButton();
            this.borderedPanel3 = new Enceladus.UIToolbox.BorderedPanel();
            this.dualScroller1 = new Enceladus.UIToolbox.DualScroller();
            this.borderedPanel2 = new Enceladus.UIToolbox.BorderedPanel();
            this.dateControl1 = new Enceladus.UIToolbox.DateControl();
            this.borderedPanel1 = new Enceladus.UIToolbox.BorderedPanel();
            this.controlLabel1 = new Enceladus.UIToolbox.ControlLabel();
            this.redInputBox1 = new Enceladus.UIToolbox.RedInputBox();
            this.gradientButton1 = new Enceladus.UIToolbox.GradientButton();
            this.redRadioBox1 = new Enceladus.UIToolbox.RedRadioBox();
            this.redCheckBox2 = new Enceladus.UIToolbox.RedCheckBox();
            this.tabsBar1 = new Enceladus.UIToolbox.TabsBar();
            this.singleScroller1 = new Enceladus.UIToolbox.SingleScroller();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.borderedPanel3.SuspendLayout();
            this.borderedPanel2.SuspendLayout();
            this.borderedPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::UIToolboxTests.Properties.Resources.ActiveTab;
            this.pictureBox1.Location = new System.Drawing.Point(522, 131);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(150, 50);
            this.pictureBox1.TabIndex = 16;
            this.pictureBox1.TabStop = false;
            // 
            // disappearingLabel1
            // 
            this.disappearingLabel1.AutoSize = true;
            this.disappearingLabel1.Font = new System.Drawing.Font("Trebuchet MS", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.disappearingLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(97)))), ((int)(((byte)(96)))));
            this.disappearingLabel1.Location = new System.Drawing.Point(519, 247);
            this.disappearingLabel1.Name = "disappearingLabel1";
            this.disappearingLabel1.Size = new System.Drawing.Size(100, 15);
            this.disappearingLabel1.TabIndex = 14;
            this.disappearingLabel1.Text = "disappearingLabel1";
            // 
            // gradientIconButton1
            // 
            this.gradientIconButton1.BackColor = System.Drawing.Color.Transparent;
            this.gradientIconButton1.Command = null;
            this.gradientIconButton1.Icon = null;
            this.gradientIconButton1.Location = new System.Drawing.Point(398, 353);
            this.gradientIconButton1.Name = "gradientIconButton1";
            this.gradientIconButton1.Size = new System.Drawing.Size(195, 27);
            this.gradientIconButton1.TabIndex = 17;
            // 
            // borderedPanel3
            // 
            this.borderedPanel3.BackColor = System.Drawing.Color.Transparent;
            this.borderedPanel3.BorderColor = System.Drawing.Color.White;
            this.borderedPanel3.BorderWidth = 1F;
            this.borderedPanel3.Caption = null;
            this.borderedPanel3.Controls.Add(this.dualScroller1);
            this.borderedPanel3.Location = new System.Drawing.Point(12, 353);
            this.borderedPanel3.Margin = new System.Windows.Forms.Padding(4, 0, 0, 0);
            this.borderedPanel3.Name = "borderedPanel3";
            this.borderedPanel3.ShowBorder = false;
            this.borderedPanel3.Size = new System.Drawing.Size(383, 100);
            this.borderedPanel3.TabIndex = 15;
            // 
            // dualScroller1
            // 
            this.dualScroller1.LabelDown = "PS";
            this.dualScroller1.LabelUp = "kW";
            this.dualScroller1.Location = new System.Drawing.Point(33, 34);
            this.dualScroller1.MaximalValue = 300000;
            this.dualScroller1.MinimalValue = 20000;
            this.dualScroller1.Name = "dualScroller1";
            this.dualScroller1.Padding = new System.Windows.Forms.Padding(40, 20, 40, 20);
            this.dualScroller1.Size = new System.Drawing.Size(299, 45);
            this.dualScroller1.SpaceValue = 0;
            this.dualScroller1.TabIndex = 10;
            this.dualScroller1.ThumbSize = new System.Drawing.Size(12, 20);
            this.dualScroller1.ValueLeft = 25000;
            this.dualScroller1.ValueRight = 20000;
            // 
            // borderedPanel2
            // 
            this.borderedPanel2.BackColor = System.Drawing.Color.Transparent;
            this.borderedPanel2.BorderColor = System.Drawing.Color.White;
            this.borderedPanel2.BorderWidth = 1F;
            this.borderedPanel2.Caption = "Panel 0 ble ble ble";
            this.borderedPanel2.Controls.Add(this.dateControl1);
            this.borderedPanel2.Location = new System.Drawing.Point(12, 12);
            this.borderedPanel2.Margin = new System.Windows.Forms.Padding(4, 0, 0, 0);
            this.borderedPanel2.Name = "borderedPanel2";
            this.borderedPanel2.ShowBorder = false;
            this.borderedPanel2.Size = new System.Drawing.Size(574, 68);
            this.borderedPanel2.TabIndex = 14;
            // 
            // dateControl1
            // 
            this.dateControl1.BackColor = System.Drawing.Color.Transparent;
            this.dateControl1.Location = new System.Drawing.Point(14, 9);
            this.dateControl1.MaximumSize = new System.Drawing.Size(550, 50);
            this.dateControl1.MinimumSize = new System.Drawing.Size(550, 50);
            this.dateControl1.Name = "dateControl1";
            this.dateControl1.Size = new System.Drawing.Size(550, 50);
            this.dateControl1.TabIndex = 12;
            // 
            // borderedPanel1
            // 
            this.borderedPanel1.BackColor = System.Drawing.Color.Transparent;
            this.borderedPanel1.BorderColor = System.Drawing.Color.White;
            this.borderedPanel1.BorderWidth = 1F;
            this.borderedPanel1.Caption = "Panel 1";
            this.borderedPanel1.Controls.Add(this.controlLabel1);
            this.borderedPanel1.Controls.Add(this.redInputBox1);
            this.borderedPanel1.Controls.Add(this.gradientButton1);
            this.borderedPanel1.Controls.Add(this.redRadioBox1);
            this.borderedPanel1.Controls.Add(this.redCheckBox2);
            this.borderedPanel1.Controls.Add(this.tabsBar1);
            this.borderedPanel1.Controls.Add(this.singleScroller1);
            this.borderedPanel1.Location = new System.Drawing.Point(12, 86);
            this.borderedPanel1.Margin = new System.Windows.Forms.Padding(4, 0, 0, 0);
            this.borderedPanel1.Name = "borderedPanel1";
            this.borderedPanel1.ShowBorder = false;
            this.borderedPanel1.Size = new System.Drawing.Size(494, 249);
            this.borderedPanel1.TabIndex = 13;
            // 
            // controlLabel1
            // 
            this.controlLabel1.AutoSize = true;
            this.controlLabel1.Font = new System.Drawing.Font("Trebuchet MS", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.controlLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(71)))), ((int)(((byte)(71)))));
            this.controlLabel1.Location = new System.Drawing.Point(361, 96);
            this.controlLabel1.Name = "controlLabel1";
            this.controlLabel1.Size = new System.Drawing.Size(74, 15);
            this.controlLabel1.TabIndex = 13;
            this.controlLabel1.Text = "controlLabel1";
            // 
            // redInputBox1
            // 
            this.redInputBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(176)))), ((int)(((byte)(176)))), ((int)(((byte)(176)))));
            this.redInputBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.redInputBox1.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.redInputBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(71)))), ((int)(((byte)(71)))));
            this.redInputBox1.Label = null;
            this.redInputBox1.Location = new System.Drawing.Point(254, 20);
            this.redInputBox1.MaximumValue = 0;
            this.redInputBox1.MinimumValue = 100;
            this.redInputBox1.Name = "redInputBox1";
            this.redInputBox1.Size = new System.Drawing.Size(33, 21);
            this.redInputBox1.TabIndex = 12;
            this.redInputBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // gradientButton1
            // 
            this.gradientButton1.Command = null;
            this.gradientButton1.Location = new System.Drawing.Point(3, 20);
            this.gradientButton1.Name = "gradientButton1";
            this.gradientButton1.Size = new System.Drawing.Size(171, 40);
            this.gradientButton1.TabIndex = 6;
            this.gradientButton1.Load += new System.EventHandler(this.gradientButton1_Load);
            // 
            // redRadioBox1
            // 
            this.redRadioBox1.ClickEdge = 3;
            this.redRadioBox1.Content = "red box 32";
            this.redRadioBox1.IsChecked = false;
            this.redRadioBox1.Location = new System.Drawing.Point(46, 70);
            this.redRadioBox1.Name = "redRadioBox1";
            this.redRadioBox1.Size = new System.Drawing.Size(150, 25);
            this.redRadioBox1.TabIndex = 11;
            // 
            // redCheckBox2
            // 
            this.redCheckBox2.ClickEdge = 3;
            this.redCheckBox2.Content = "Hello world";
            this.redCheckBox2.IsChecked = true;
            this.redCheckBox2.Location = new System.Drawing.Point(21, 101);
            this.redCheckBox2.Name = "redCheckBox2";
            this.redCheckBox2.Size = new System.Drawing.Size(257, 29);
            this.redCheckBox2.TabIndex = 5;
            // 
            // tabsBar1
            // 
            this.tabsBar1.BackColor = System.Drawing.Color.Transparent;
            this.tabsBar1.Location = new System.Drawing.Point(14, 197);
            this.tabsBar1.Name = "tabsBar1";
            this.tabsBar1.SelectedIndex = 0;
            this.tabsBar1.Size = new System.Drawing.Size(355, 37);
            this.tabsBar1.TabIndex = 7;
            // 
            // singleScroller1
            // 
            this.singleScroller1.Horizont = Enceladus.UIToolbox.Position.Left;
            this.singleScroller1.InputBox = null;
            this.singleScroller1.Label = null;
            this.singleScroller1.Location = new System.Drawing.Point(33, 161);
            this.singleScroller1.MaximalValue = 1000;
            this.singleScroller1.MinimalValue = 0;
            this.singleScroller1.Name = "singleScroller1";
            this.singleScroller1.Padding = new System.Windows.Forms.Padding(20);
            this.singleScroller1.Size = new System.Drawing.Size(299, 45);
            this.singleScroller1.TabIndex = 8;
            this.singleScroller1.ThumbSize = new System.Drawing.Size(12, 20);
            this.singleScroller1.Value = 100;
            this.singleScroller1.Vertical = Enceladus.UIToolbox.Position.Bottom;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(500, 283);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 18;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(696, 484);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.disappearingLabel1);
            this.Controls.Add(this.gradientIconButton1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.borderedPanel3);
            this.Controls.Add(this.borderedPanel2);
            this.Controls.Add(this.borderedPanel1);
            this.ForeColor = System.Drawing.Color.Silver;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.borderedPanel3.ResumeLayout(false);
            this.borderedPanel2.ResumeLayout(false);
            this.borderedPanel1.ResumeLayout(false);
            this.borderedPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Enceladus.UIToolbox.RedCheckBox redCheckBox2;
        private Enceladus.UIToolbox.GradientButton gradientButton1;
        private Enceladus.UIToolbox.TabsBar tabsBar1;
        private Enceladus.UIToolbox.SingleScroller singleScroller1;
        private Enceladus.UIToolbox.DualScroller dualScroller1;
        private Enceladus.UIToolbox.RedRadioBox redRadioBox1;
        private Enceladus.UIToolbox.DateControl dateControl1;
        private Enceladus.UIToolbox.BorderedPanel borderedPanel1;
        private Enceladus.UIToolbox.BorderedPanel borderedPanel2;
        private Enceladus.UIToolbox.BorderedPanel borderedPanel3;
        private Enceladus.UIToolbox.RedInputBox redInputBox1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private Enceladus.UIToolbox.GradientIconButton gradientIconButton1;
        private Enceladus.UIToolbox.ControlLabel controlLabel1;
        private Enceladus.UIToolbox.DisappearingLabel disappearingLabel1;
        private System.Windows.Forms.Button button1;
    }
}

