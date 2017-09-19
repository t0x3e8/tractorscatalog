using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Enceladus.UIToolbox;
using Enceladus.StringLibrary;
using Enceladus.Properties;
using Enceladus.Api;
namespace Enceladus
{
    partial class BrandsLayout : BaseLayout, IBrandsView
    {
        #region Fields and Properties
        protected readonly BrandsPresenter presenter;
        public GradientButton MainMenuButton
        {
            get { return this.btnMainWindow; }
        }

        public BrandsPage BrandsPage
        {
            get { return this.Controls["Brands"] as BrandsPage; }
        }

        public IList<Brand> VisibleBrands
        {
            get { return this.BrandsPage.VisibleBrands; }
            set { this.BrandsPage.VisibleBrands = value; }
        }

        public BrandsLayout MainLayout { get { return this; } }

        public TabsBar TabNavigator { get { return this.tabsBar1; } }
        #endregion

        #region Constructors
        public BrandsLayout(MainWindow window)
        {
            InitializeComponent();

            this.presenter = new BrandsPresenter(this);
            this.InitializePage();

            this.presenter.InitializeCommands(window);
            this.presenter.InitializeControls();
        }

        private void InitializePage()
        {
            BrandsPage page = new BrandsPage(this.presenter);
            page.Name = "Brands";
            page.Location = new Point(218, 50);
            page.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
            this.Controls.Add(page);
        }
        #endregion
        
        #region Methods
        public override void ChangeLanguage()
        {
            this.btnMainWindow.Text = ResourceReader.GetString("Brands_MainWindowButtonText");
        }
        #endregion
    }
}
