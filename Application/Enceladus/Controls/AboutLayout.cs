using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Enceladus.UIToolbox;
using Enceladus.StringLibrary;

namespace Enceladus
{
    partial class AboutLayout : BaseLayout, IAboutView
    {
        #region Fields and Properties
        protected readonly AboutPresenter presenter;
    
        public GradientButton MainMenuButton
        {
            get { return this.btnMainWindow; }
        }

        public BrandInfoBox RightBrandBox
        {
            get { return this.brandBox2; }
        }

        public InfoBox RightsBox
        {
            get { return this.rightsBox1; }
        }

        public InfoBox AuthorBox
        {
            get { return this.rightsBox2; }
        }
        #endregion

        #region Constructors
        public AboutLayout(MainWindow window)
        {
            InitializeComponent();

            this.presenter = new AboutPresenter(this);
            this.presenter.InitializeCommands(window);
            this.presenter.InitializeControls();
        }
        #endregion
        
        #region Methods
        public override void ChangeLanguage()
        {
            this.btnMainWindow.Text = ResourceReader.GetString("About_MainWindowButtonText");
        }
        #endregion
    }
}
