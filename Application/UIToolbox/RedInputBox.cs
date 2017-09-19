using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Enceladus.UIToolbox
{
    public partial class RedInputBox : UserControl
    {
        #region Fields and Properties
        protected TextBox textBox = new TextBox();
        public int MaximumValue { get; set; }
        public int MinimumValue { get; set; }

        public override string Text
        {
            get { return this.textBox.Text; }
            set { this.textBox.Text = value; }
        }

        public override Color BackColor
        {
            get { return this.textBox.BackColor; }
            set 
            {
                base.BackColor = value;
                this.textBox.BackColor = value; 
            }
        }

        public virtual HorizontalAlignment TextAlign
        {
            set { this.textBox.TextAlign = value; }
            get { return this.textBox.TextAlign; }
        }

        protected InputBoxLabel label;
        public virtual InputBoxLabel Label
        {
            get { return this.label; }
            set
            {
                this.label = value;
                this.CalculateLabelPosition();
            }
        }
        #endregion

        #region Constructors
        public RedInputBox()
        {
            InitializeComponent();
            this.Disposed += new EventHandler(RedInputBox_Disposed);

            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, false);
            this.UpdateStyles();

            this.textBox.Font = Defines.TinyFont;
            this.textBox.TextAlign = HorizontalAlignment.Center;
            this.BackColor = Defines.ParsnipColor;
            this.ForeColor = Defines.CarrotColor;
            this.textBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox.KeyPress += new KeyPressEventHandler(textBox_KeyPress);
            this.textBox.TextChanged += new EventHandler(textBox_TextChanged);
            
            this.Controls.Add(this.textBox);
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
            e.Graphics.DrawRectangle(new Pen(Defines.LeekColor), 0, 0, this.Width - 1, this.Height - 1);
        }

        protected override void OnResize(EventArgs e)
        {
            this.textBox.Size = new Size(this.Width - 2, this.Height - 1);
            this.textBox.Location = new Point(1, 1);
        }

        protected override void OnMove(EventArgs e)
        {
            base.OnMove(e);
            this.CalculateLabelPosition();
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
            }
        }

        protected void textBox_KeyPress(object sender, KeyPressEventArgs e)
        {            
            if (!System.Text.RegularExpressions.Regex.IsMatch(e.KeyChar.ToString(), "\\d+") && e.KeyChar != '\b')
                e.Handled = true;
        }
        
        protected void CalculateLabelPosition()
        {
            if (label != null)
            {
                this.label.Location = new Point(this.Location.X + this.Width,
                    (this.Location.Y + (this.Height / 2)) - (this.label.Height / 2));
            }
        }
        #endregion
    }
}
