using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Enceladus.StringLibrary;

namespace Enceladus
{
    partial class MainLayout : BaseLayout, IMainView
    {
        #region Fields and Properties
        private MainPresenter presenter;
        public UIToolbox.GradientButton SearchButton
        {
            get { return this.btnSearch; }
        }

        public UIToolbox.GradientButton ShowTractorButton
        {
            get { return this.btnShowOne; }
        }

        public UIToolbox.GradientButton VendorsButton
        {
            get { return this.btnVendor; }
        }

        public UIToolbox.GradientButton ProductInformationButton
        {
            get { return this.btnProductInfo; }
        }

        public UIToolbox.GradientButton AboutButton
        {
            get { return this.btnAbout; }
        }
        #endregion

        #region Constructors
        public MainLayout(MainWindow window)
            : base(window)
        {
            InitializeComponent();

            this.presenter = new MainPresenter(this);
            this.presenter.InitializeCommands(this.Window);
        }

        public override void ChangeLanguage()
        {
            this.btnSearch.Text = ResourceReader.GetString("Main_SearchButtonText");
            this.btnShowOne.Text = ResourceReader.GetString("Main_ShowOneButtonText");
            this.btnProductInfo.Text = ResourceReader.GetString("Main_ProductInfoButtonText");
            this.btnVendor.Text = ResourceReader.GetString("Main_VendorButtonText");
            this.btnAbout.Text = ResourceReader.GetString("Main_AboutButtonText");
        }
        #endregion
    }
}
