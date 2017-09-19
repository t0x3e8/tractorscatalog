using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using Enceladus.Api;

namespace Enceladus.UIToolbox
{
    public partial class IconButton : UserControl
    {
        #region Fields and Properties
        protected bool isClicked;
        protected bool isHover;
        protected ICommand command;

        public ICommand Command
        {
            get { return this.command; }
            set { this.command = value; }
        }
        public virtual IconButtonType ButtonType { get; set; }
        protected bool isChecked;
        public virtual bool Checked
        {
            get { return this.isChecked; }
            set
            {
                this.isChecked = value;
                this.Invalidate();
            }
        }
        #endregion

        #region Constructors
        public IconButton()
        {
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.Size = new Size(22, 22);
            this.ButtonType = IconButtonType.None;
            this.BackColor = Color.Transparent;
        }
        #endregion

        #region Methods
        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            if (this.ButtonType == IconButtonType.None)
                this.DrawNone(e.Graphics);
            else
                this.DrawIcon(e.Graphics);
        }

        private void DrawIcon(Graphics g)
        {
            if (this.isClicked)
                g.DrawImage(this.GetClickedImage(), new Rectangle(0, 0, this.Width - 1, this.Height - 1));
            else if (this.isHover)
                g.DrawImage(this.GetHoverImage(), new Rectangle(0, 0, this.Width - 1, this.Height - 1));
            else if (!this.Enabled)
                g.DrawImage(this.GetDisabledImage(), new Rectangle(0, 0, this.Width - 1, this.Height - 1));
            else
                g.DrawImage(this.GetRegularImage(), new Rectangle(0, 0, this.Width - 1, this.Height - 1));
        }

        private Image GetClickedImage()
        {
            switch (this.ButtonType)
            {
                case IconButtonType.Pdf:
                    return Resource1.pdf;
                case IconButtonType.Printer:
                    return Resource1.printer;
                case IconButtonType.Bookmark:
                    return this.Checked ? Resource1.bookmark : Resource1.bookmark_add;
                case IconButtonType.First:
                    return Resource1.first;
                case IconButtonType.Previous:
                    return Resource1.prev;
                case IconButtonType.Next:
                    return Resource1.next;
                case IconButtonType.Last:
                    return Resource1.last;
                default: throw new ArgumentException("There is no graphics for clicked image " + this.ButtonType.ToString());
            }
        }

        private Image GetHoverImage()
        {
            switch (this.ButtonType)
            {
                case IconButtonType.Pdf:
                    return Resource1.pdf_hover;
                case IconButtonType.Printer:
                    return Resource1.printer_hover;
                case IconButtonType.Bookmark:
                    return this.Checked ? Resource1.bookmark_add_hover : Resource1.bookmark_hover;
                case IconButtonType.First:
                    return Resource1.first_hover;
                case IconButtonType.Previous:
                    return Resource1.prev_hover;
                case IconButtonType.Next:
                    return Resource1.next_hover;
                case IconButtonType.Last:
                    return Resource1.last_hover;
                default: throw new ArgumentException("There is no graphics for hovered image " + this.ButtonType.ToString());
            }
        }

        private Image GetDisabledImage()
        {
            switch (this.ButtonType)
            {
                case IconButtonType.Pdf:
                    return Resource1.pdf_disabled;
                case IconButtonType.Printer:
                    return Resource1.printer_disabled;
                case IconButtonType.Bookmark:
                    return Resource1.bookmark_disabled;
                case IconButtonType.First:
                    return Resource1.first_disabled;
                case IconButtonType.Previous:
                    return Resource1.prev_disabled;
                case IconButtonType.Next:
                    return Resource1.next_disabled;
                case IconButtonType.Last:
                    return Resource1.last_disabled;
                default: throw new ArgumentException("There is no graphics for hovered image " + this.ButtonType.ToString());
            }
        }

        private Image GetRegularImage()
        {
            switch (this.ButtonType)
            {
                case IconButtonType.Pdf:
                    return this.Enabled ? Resource1.pdf : Resource1.pdf_disabled;
                case IconButtonType.Printer:
                    return Resource1.printer;
                case IconButtonType.Bookmark:
                    return this.Checked ? Resource1.bookmark_add :  Resource1.bookmark;
                case IconButtonType.First:
                    return Resource1.first;
                case IconButtonType.Previous:
                    return Resource1.prev;
                case IconButtonType.Next:
                    return Resource1.next;
                case IconButtonType.Last:
                    return Resource1.last;
                default: throw new ArgumentException("There is no graphics for image " + this.ButtonType.ToString());
            }
        }

        protected virtual void DrawNone(Graphics g)
        {
            using (Pen pen = new Pen(Color.Red))
            {
                g.DrawLine(pen, new Point(0, 0), new Point(this.Width - 1, this.Height - 1));
                g.DrawLine(pen, new Point(this.Width, 0), new Point(0, this.Height));
            }
        }

        protected override void OnMouseDown(MouseEventArgs mevent)
        {
            base.OnMouseDown(mevent);
            if (mevent.Button == System.Windows.Forms.MouseButtons.Left)
            {
                this.isClicked = true;
                this.Invalidate();
            }
        }

        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            base.OnMouseUp(mevent);
            if (this.isClicked)
            {
                this.isClicked = false;
                this.Invalidate();
            }
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseHover(e);
            this.isHover = true;
            this.Cursor = Cursors.Hand;
            this.Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            this.isHover = false;
            this.Cursor = Cursors.Default;
            this.Invalidate();
        }

        #endregion
    }
}
