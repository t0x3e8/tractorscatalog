using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Enceladus.UIToolbox
{
    public partial class TractorStatus : UserControl
    {
        #region Fields and Properties
        protected TextBox textBox = new TextBox();       
        public int MaximumValue { get; set; }
        public int MinimumValue { get; set; }
        public string LabelPattern { get; set; }

        public override string Text
        {
            get { return this.textBox.Text; }
            set { this.textBox.Text = value; }
        }

        public virtual HorizontalAlignment TextAlign
        {
            set { this.textBox.TextAlign = value; }
            get { return this.textBox.TextAlign; }
        }

        public Color TextBoxBackColor
        {
            get { return this.textBox.BackColor; }
            set { this.textBox.BackColor = value; }
        }

        public int ValueWidth
        {
            get { return this.textBox.Width; }
            set { this.textBox.Width = value; }
        }

        public event EventHandler StatusChanged;
        #endregion

        #region Constructor
        public TractorStatus()
        {
            InitializeComponent();
            this.Disposed += new EventHandler(RedInputBox_Disposed);

            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.UpdateStyles();

            this.textBox.Font = Defines.SmallBoldFont;
            this.textBox.TextAlign = HorizontalAlignment.Center;
            this.BackColor = Defines.ParsnipColor;
            this.ForeColor = Defines.CarrotColor;
            this.textBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox.KeyPress += new KeyPressEventHandler(textBox_KeyPress);
            this.textBox.TextChanged += new EventHandler(textBox_TextChanged);
            this.textBox.Padding = new Padding(3);
            this.Controls.Add(this.textBox);

            this.LabelPattern = "of {0}";
        }

        protected void RedInputBox_Disposed(object sender, EventArgs e)
        {
            this.textBox.KeyPress -= new KeyPressEventHandler(textBox_KeyPress);
            this.textBox.TextChanged -= new EventHandler(textBox_TextChanged);
        }
        #endregion

        #region Methods
        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            this.DrawLabel(e.Graphics);
            using (SolidBrush brush = new SolidBrush(this.TextBoxBackColor))
                e.Graphics.FillRectangle(brush, 0, 0, this.textBox.Width + 3, this.textBox.Height + 2);

            using (Pen pen = new Pen(Defines.LeekColor))
                e.Graphics.DrawRectangle(pen, 0, 0, this.textBox.Width + 3, this.textBox.Height + 2);
        }

        private void DrawLabel(Graphics g)
        {
            StringFormat sf = DrawingToolbox.CreateStringFormat(StringAlignment.Center, StringAlignment.Far);
            Rectangle labelRect = new Rectangle(this.ValueWidth, 0, this.Width - this.ValueWidth, this.Height - 1);
            using (Brush brush = new SolidBrush(Defines.CarrotColor))
            {
                g.DrawString(string.Format(this.LabelPattern, this.MaximumValue), Defines.TinyBoldFont, brush, labelRect, sf);
            }
        }

        protected override void OnResize(EventArgs e)
        {
            this.textBox.Size = new Size(this.Width - 2, this.Height - 1);
            this.textBox.Location = new Point(1, 1);
        }

        protected void textBox_TextChanged(object sender, EventArgs e)
        {
            base.OnTextChanged(e);

            if (!string.IsNullOrEmpty(this.textBox.Text))
            {
                int value = int.Parse(this.textBox.Text);
                if (value < this.MinimumValue)
                    this.textBox.Text = MinimumValue.ToString();
                else if (value > this.MaximumValue)
                    this.textBox.Text = this.MaximumValue.ToString();

                if (this.StatusChanged != null)
                    this.StatusChanged(this, new EventArgs());
            }
        }

        protected void textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!System.Text.RegularExpressions.Regex.IsMatch(e.KeyChar.ToString(), "\\d+") && e.KeyChar != '\b')
                e.Handled = true;
        }
        #endregion
    }
}
