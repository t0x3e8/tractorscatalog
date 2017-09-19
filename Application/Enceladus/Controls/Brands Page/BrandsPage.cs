using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Enceladus.Api;
using Enceladus.Api.UI;

namespace Enceladus
{
    partial class BrandsPage : PageBase
    {
        #region Fields & Properties
        protected BrandsPresenter presenter;
        protected Resizer resizer;

        protected IList<Brand> visibleBrands;
        public IList<Brand> VisibleBrands
        {
            get { return this.visibleBrands; }
            set
            {
                this.visibleBrands = value;
                this.UpdateBrandBoxes();
            }
        }
        #endregion

        #region Constructors
        public BrandsPage(BrandsPresenter presenter)
        {
            InitializeComponent();
            this.presenter = presenter;

            this.resizer = new Resizer();
            this.resizer.Suspend();
            this.resizer.AddClient(this.brandTextBlock1);
            this.resizer.AddClient(this.brandTextBlock2);
            this.resizer.AddClient(this.brandTextBlock3);
            this.resizer.AddClient(this.brandTextBlock4);
            this.resizer.AddClient(this.brandTextBlock5);
            this.resizer.AddClient(this.brandTextBlock6);
            this.resizer.Release();
        }
        #endregion

        #region Methods
        public override void ChangeLanguage()
        {
            base.ChangeLanguage();
        }

        private void UpdateBrandBoxes()
        {
            this.brandTextBlock1.BrandEntity = this.visibleBrands[0];
            this.brandTextBlock2.BrandEntity = this.visibleBrands[1];
            this.brandTextBlock3.BrandEntity = this.visibleBrands[2];
            this.brandTextBlock4.BrandEntity = this.visibleBrands[3];
            this.brandTextBlock5.BrandEntity = this.visibleBrands[4];
            this.brandTextBlock6.BrandEntity = this.visibleBrands[5];
        }
        #endregion
    }
}
