using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Enceladus.Api.UI;

namespace Enceladus.UIToolbox
{
    public partial class BrandsPanel : BorderedPanel
    {
        #region Fields and Properties
        protected readonly int ProducerColumnsNumber = 5;
        protected readonly int RowsNumber = 20;
        protected readonly Size BoxSize = new Size(12, 12);
        protected readonly int boxLeftPadding = 5;
        protected readonly int spaceBetweenBoxAndText = 2;
        protected readonly int clickPadding = 3;

        protected Point pressedPointStart = Point.Empty;
        protected Point pressedPointEnd = Point.Empty;
        protected Rectangle selectedRegion = Rectangle.Empty;
        public bool IsSelectable { get; set; }

        public bool AllOptionsUnselected
        {
            get
            {
                bool result = true;
                foreach (var option in this.additionalOptions)
                {
                    if (option.IsChecked == true)
                    {
                        result = false;
                        break;
                    }
                }

                return result;
            }
        }
        public bool AllBrandsSelected
        {
            get
            {
                bool result = true;
                foreach (var brand in this.brandsData)
                {
                    if (brand.IsChecked == false)
                    {
                        result = false;
                        break;
                    }
                }

                return result;
            }
        }

        public bool AllBrandsUnselected
        {
            get
            {
                bool result = true;
                foreach (var brand in this.brandsData)
                {
                    if (brand.IsChecked)
                    {
                        result = false;
                        break;
                    }
                }

                return result;
            }
        }

        private IList<ICheckBoxLogic> brandsData = new List<ICheckBoxLogic>();
        public virtual List<string> Brands
        {
            set
            {
                if (value != null)
                {
                    this.PrepareBrandsData(value);
                    this.Invalidate();
                }
            }
        }

        private IList<ICheckBoxLogic> additionalOptions = new List<ICheckBoxLogic>();
        public virtual IDictionary<string, string> AdditionalOptions
        {
            set
            {
                if (value != null)
                {
                    this.PrepareAdditionalData(value);
                    this.Invalidate();
                }
            }
        }

        public event EventHandler ItemClicked;

        protected IToggler allBrandsToggler = null;
        public virtual IToggler AllBrandsToggler
        {
            get { return this.allBrandsToggler; }
            set
            {
                this.allBrandsToggler = value;

                this.allBrandsToggler.SelectAll += new EventHandler(allBrandsToggler_SelectAll);
                this.allBrandsToggler.DeselectAll += new EventHandler(allBrandsToggler_DeselectAll);
            }
        }

        /// <summary>
        /// Returns the maximal size of the single cell in the virtual table (all brands and options).
        /// </summary>
        protected Size MaxCellSize
        {
            get { return new Size((this.Width - this.Padding.Horizontal) / this.ProducerColumnsNumber, 
                                  (this.Height - this.Padding.Vertical) / this.RowsNumber); }
        }
        #endregion

        #region Constructors
        public BrandsPanel()
        {
            InitializeComponent();

            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.UpdateStyles();

            this.IsSelectable = true;
            this.MaximalExpectedFontSize = 1;
        }
        #endregion

        #region Methods
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            Font font = this.DetermineFont(this.CurrentFontSize);

            foreach (ICheckBoxLogic brandData in this.brandsData)
            {
                this.FillBox(e.Graphics, brandData.Location);
                this.DrawBoxBorder(e.Graphics, brandData.Location);
                this.DrawText(e.Graphics, brandData.TextLocation, brandData.Text, font);
                if (brandData.IsChecked)
                    this.DrawTickMark(e.Graphics, brandData.Location);
            }

            foreach (ICheckBoxLogic option in this.additionalOptions)
            {
                this.FillBox(e.Graphics, option.Location);
                this.DrawBoxBorder(e.Graphics, option.Location);
                this.DrawText(e.Graphics, option.TextLocation, option.Text, font);

                if (option.IsChecked)
                    this.DrawTickMark(e.Graphics, option.Location);
            }

            this.DrawSelectionRectangle(e.Graphics);
        }
        
        protected virtual void DrawSelectionRectangle(Graphics g)
        {
            if (this.pressedPointStart != Point.Empty && this.pressedPointEnd != Point.Empty)
            {
                Rectangle rect = DrawingToolbox.BuildRectangleUponTwoPoints(this.pressedPointStart, this.pressedPointEnd);
                this.selectedRegion = DrawingToolbox.Trim(rect, this.Size);

                using (Pen pen = new Pen(Defines.CherryColor))
                {
                    g.DrawRectangle(pen, this.selectedRegion);
                }

                using (Brush brush = new SolidBrush(Defines.RadishColor))
                {
                    g.FillRectangle(brush, this.selectedRegion);
                }
            }
        }

        protected virtual void FillBox(Graphics g, Point point)
        {
            using (SolidBrush brush = new SolidBrush(Defines.MangoColor))
            {
                g.FillRectangle(brush, point.X, point.Y, this.BoxSize.Width, this.BoxSize.Height);
            }
        }

        protected virtual void DrawBoxBorder(Graphics g, Point point)
        {
            using (Pen pen = new Pen(Defines.MilkColor, 1.0f))
            {
                g.DrawRectangle(pen, point.X, point.Y, this.BoxSize.Width, this.BoxSize.Height);
            }
        }

        protected virtual void DrawText(Graphics g, Point location, string brand, Font font)
        {
            using (SolidBrush brush = new SolidBrush(Defines.CarrotColor))
            {
                g.DrawString(brand, font, brush, location);
            }            
        }

        protected virtual void DrawTickMark(Graphics g, Point point)
        {
            using (Image image = Resource1.RedTick)
            {
                g.DrawImage(image, point);
            }
        }

        protected virtual void PrepareBrandsData(List<string> brandsName)
        {
            this.brandsData.Clear();
            brandsName.Sort();

            for (int i = 0; i < brandsName.Count; i++)
            {
                ICheckBoxLogic checkBox = new CheckBoxLogic();
                checkBox.IsChecked = false;
                checkBox.Text = brandsName[i];
                checkBox.Key = brandsName[i];
                //checkBox.TextSize = TextRenderer.MeasureText(brandsName[i], this.Font);

                this.brandsData.Add(checkBox);
            }

            this.PrepareBrandsDataLocation();
        }

        protected virtual void PrepareBrandsDataLocation()
        {
            if (this.brandsData != null && this.brandsData.Count > 0)
            {
                int x = this.Padding.Left;
                int y = this.Padding.Top;
                Size cellSize = this.MaxCellSize;

                for (int i = 0; i < this.brandsData.Count; i++)
                {
                    ICheckBoxLogic brandCheckBox = this.brandsData[i];
                    x = ((i % this.ProducerColumnsNumber) == 0) ? x = this.Padding.Left : x + cellSize.Width;
                    y = ((i % this.ProducerColumnsNumber) != 0) ? y : y + cellSize.Height;
                    brandCheckBox.Location = new Point(x, y);
                    brandCheckBox.TextLocation = new Point(x + this.BoxSize.Width + this.spaceBetweenBoxAndText, y - lastFontSizeChange);
                }
            }
        }

        protected virtual void PrepareAdditionalData(IDictionary<string, string> additionalOptions)
        {
            this.additionalOptions.Clear();

            foreach (var pair in additionalOptions)
            {
                ICheckBoxLogic radioBox = new CheckBoxLogic();
                radioBox.IsChecked = true;
                radioBox.Text = pair.Value;
                radioBox.Key = pair.Key;
                this.additionalOptions.Add(radioBox);
            }
            this.PrepareAdditionalDataLocation();

            this.SelectOption(0);
        }

        protected virtual void PrepareAdditionalDataLocation()
        {
            if (this.additionalOptions != null && this.additionalOptions.Count > 0)
            {
                Size cellSize = this.MaxCellSize;
                int x = this.brandsData[this.brandsData.Count - 1].Location.X;
                int y = this.brandsData[this.brandsData.Count - 1].Location.Y;

                for (int i = 0; i < this.additionalOptions.Count; i++)
                {
                    ICheckBoxLogic brandCheckBox = this.additionalOptions[i];
                    x = ((i % this.ProducerColumnsNumber) == 0) ? x = this.Padding.Left : x + (cellSize.Width * 2);
                    y = ((i % this.ProducerColumnsNumber) != 0) ? y : y + cellSize.Height;
                    brandCheckBox.Location = new Point(x, y);
                    brandCheckBox.TextLocation = new Point(x + this.BoxSize.Width + this.spaceBetweenBoxAndText, y - lastFontSizeChange);
                }
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            // check if any brand is clicked
            bool isClicked = false;
            for (int i = 0; i < this.brandsData.Count; i++)
            {
                Rectangle boxBoundaries = new Rectangle(this.brandsData[i].Location.X - this.clickPadding, this.brandsData[i].Location.Y - this.clickPadding,
                                                        this.BoxSize.Width + (2 * this.clickPadding), this.BoxSize.Height + (2 * this.clickPadding));
                if (boxBoundaries.Contains(e.Location))
                {
                    this.brandsData[i].ItemClicked();
                    if (this.ItemClicked != null)
                        this.ItemClicked(this, new EventArgs());
                    isClicked = true;

                    this.InformToggler();
                    this.Invalidate();
                    break;
                }
            }

            // check if any option is clicked
            if (!isClicked)
            {
                for (int i = 0; i < this.additionalOptions.Count; i++)
                {
                    Rectangle boxBoundaries = new Rectangle(this.additionalOptions[i].Location.X - this.clickPadding, this.additionalOptions[i].Location.Y - this.clickPadding,
                                                            this.BoxSize.Width + (2 * this.clickPadding), this.BoxSize.Height + (2 * this.clickPadding));
                    if (boxBoundaries.Contains(e.Location))
                    {
                        this.SelectOption(i);
                        if (this.ItemClicked != null)
                            this.ItemClicked(this, new EventArgs());
                        isClicked = true;

                        this.InformToggler();
                        this.Invalidate();
                        break;
                    }
                }
            }


            // start RegionSelection
            if (!isClicked && this.IsSelectable)
                this.pressedPointStart = e.Location;
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            if (this.pressedPointStart != Point.Empty && this.selectedRegion != Rectangle.Empty && this.IsSelectable)
            {
                this.SelectUnselect(this.selectedRegion);

                this.selectedRegion = Rectangle.Empty;
                this.pressedPointStart = Point.Empty;
                this.Invalidate();
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (this.pressedPointStart != Point.Empty)
            {
                this.pressedPointEnd = e.Location;
                this.Invalidate();
            }
        }
        
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);

            this.CalculateExpectedFontSize();
            this.PrepareBrandsDataLocation();
            this.PrepareAdditionalDataLocation();
        }
        
        protected virtual void SelectUnselect(Rectangle selectionRectangle)
        {
            List<int> itemsIndexToChange = new List<int>();
            bool newValue = true;

            for (int i = 0; i < this.brandsData.Count; i++)
            {
                Rectangle brandRectangle = new Rectangle(this.brandsData[i].Location, this.BoxSize);
                if (RectangleF.Intersect(selectionRectangle, brandRectangle) != Rectangle.Empty)
                {
                    itemsIndexToChange.Add(i);
                    if (newValue == true && this.brandsData[i].IsChecked)
                        newValue = false;
                }
            }

            for (int i = 0; i < itemsIndexToChange.Count; i++)
            {
                this.brandsData[itemsIndexToChange[i]].IsChecked = newValue;
            }

            if (itemsIndexToChange.Count > 0)
                this.InformToggler();
        }

        protected virtual void SelectOption(int selectedIndex)
        {
            for (int i = 0; i < this.additionalOptions.Count; i++)
            {
                if (i != selectedIndex && this.additionalOptions[i].IsChecked)
                    this.additionalOptions[i].IsChecked = false;
            }

            this.additionalOptions[selectedIndex].IsChecked = !this.additionalOptions[selectedIndex].IsChecked;
        }

        public IList<string> GetSelectedBrands()
        {
            IList<string> selectedBrands = new List<string>();

            foreach (var brand in this.brandsData)
            {
                if (brand.IsChecked)
                    selectedBrands.Add(brand.Key);
            }

            return selectedBrands;
        }

        public string GetSelectedOption()
        {
            string selectedOption = null;

            foreach (var option in this.additionalOptions)
            {
                if (option.IsChecked)
                {
                    selectedOption = option.Key;
                    break;
                }
            }

            return selectedOption;
        }

        #region Toggler implementation
        protected void allBrandsToggler_SelectAll(object sender, EventArgs e)
        {
            for (int i = 0; i < this.brandsData.Count; i++)
            {
                if (this.brandsData[i].IsChecked == false)
                    brandsData[i].ItemClicked(); ;
            }

            for (int i = 0; i < this.additionalOptions.Count; i++)
            {
                if (this.additionalOptions[i].IsChecked == true)
                    additionalOptions[i].ItemClicked();
            }

            // for sake of performance I didnt use ChangeItemState method
            this.InformToggler();
            this.Invalidate();
        }

        protected void allBrandsToggler_DeselectAll(object sender, EventArgs e)
        {
            for (int i = 0; i < this.brandsData.Count; i++)
            {
                if (this.brandsData[i].IsChecked == true)
                    brandsData[i].ItemClicked(); ;
            }

            for (int i = 0; i < this.additionalOptions.Count; i++)
            {
                if (this.additionalOptions[i].IsChecked == true)
                    additionalOptions[i].ItemClicked();
            }

            // for sake of performance I didnt use ChangeItemState method
            this.InformToggler();
            this.Invalidate();
        }

        protected void InformToggler()
        {
            if (this.AllBrandsToggler != null)
            {
                if (this.AllBrandsSelected && this.AllOptionsUnselected)
                    this.AllBrandsToggler.State = true;
                else
                    this.AllBrandsToggler.State = false;
            }
        }
        #endregion
        
        #region IResizeClient implementaion
        public new FontSize CurrentFontSize { get; protected set; }
        
        private int lastFontSizeChange = 0;
        public override void ApplyFontSize(int fontSizeChange)
        {
            base.ApplyFontSize(fontSizeChange);

            if (this.lastFontSizeChange != fontSizeChange)
            {
                this.lastFontSizeChange = fontSizeChange;
                this.CurrentFontSize = FontSize.Tiny + fontSizeChange - 1;
             
                this.Invalidate();
            }
        }

        public new Font DetermineFont(FontSize fontSize)
        {
            switch (fontSize)
            {
                case FontSize.Tiny: return Defines.TinyFont;
                case FontSize.Small: return Defines.SmallFont;
                case FontSize.Normal: return Defines.NormalFont;
                case FontSize.Big: return Defines.BigFont;
                case FontSize.Huge: return Defines.HugeFont;
                default: return Defines.NormalFont;
            }
        }

        protected void CalculateExpectedFontSize()
        {
            Size cellSize = this.MaxCellSize;
            cellSize.Width = cellSize.Width - this.boxLeftPadding - this.BoxSize.Width - spaceBetweenBoxAndText; 

            List<bool> fontSizes = new List<bool>(5);
            fontSizes.Add(true);
            fontSizes.Add(false);
            fontSizes.Add(false);
            fontSizes.Add(false);
            fontSizes.Add(false);

            List<Font> definedFontSizes = new List<Font>(5);
            definedFontSizes.Add(this.DetermineFont(FontSize.Tiny));
            definedFontSizes.Add(this.DetermineFont(FontSize.Small));
            definedFontSizes.Add(this.DetermineFont(FontSize.Normal));
            definedFontSizes.Add(this.DetermineFont(FontSize.Big));
            definedFontSizes.Add(this.DetermineFont(FontSize.Huge));

            bool isFirstRound = true;

            foreach (var brand in this.brandsData)
            {
                bool isFalseInRecord = false;
                for (int i = 1; i < 5; i++)
                {
                    if (!isFalseInRecord)
                    {
                        Size textSize = TextRenderer.MeasureText(brand.Text, definedFontSizes[i]);
                        bool isCellBigger = (textSize.Height < cellSize.Height) && (textSize.Width < cellSize.Width);
                        fontSizes[i] = isCellBigger && (fontSizes[i] || isFirstRound);
                        isFalseInRecord = !fontSizes[i];
                    }
                    else
                        fontSizes[i] = false;
                }
                isFirstRound = false;
            }

            int lastTrueIndex = fontSizes.FindLastIndex(b => b == true);
            if(this.brandsData.Count > 0)
                this.MaximalExpectedFontSize = lastTrueIndex  + 1;
        }
        #endregion
        #endregion
    }
}