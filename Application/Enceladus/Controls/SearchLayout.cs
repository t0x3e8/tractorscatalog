using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Enceladus.UIToolbox;
using Enceladus.StringLibrary;
using Enceladus.Api.UI;
namespace Enceladus
{
    partial class SearchLayout : BaseLayout, ISearchView
    {
        #region Fields and Properties
        private static readonly Point PageLocation = new Point(218, 50);

        private SearchPresenter presenter;
        
        private int activePageIndex = 0;
        public int ActivePageIndex
        {
            get { return this.activePageIndex; }
            set
            {
                if (this.activePageIndex != value)
                {
                    this.tabsBar1.SelectedIndex = value;
                    this.activePageIndex = value;
                }
            }
        }

        public AdvanceSearchPage AdvancePage
        {
            get { return this.Controls["Advance"] as AdvanceSearchPage; }
        }

        public GeneralSearchPage GeneralPage
        {
            get { return this.Controls["General"] as GeneralSearchPage; }
        }

        public SearchLayout MainLayout
        {
            get { return this; }
        }

        public GradientButton  MainMenuButton 
        { 
            get {return this.btnMainWindow;} 
        }

        public GradientButton StartSearchButton
        {
            get { return this.btnStartSearch; }
        }
        #endregion

        #region Constructors
        public SearchLayout(MainWindow window)
            : base(window)
        {            
            InitializeComponent();
            
            this.presenter = new SearchPresenter(this);
            this.InitializeTabs();

            this.presenter.InitializeControls();
            this.presenter.InitializeCommands(window);
            this.presenter.InitializeResizer();
        }
        #endregion

        #region Methods
        public override void ChangeLanguage()
        {
            this.btnStartSearch.Text = ResourceReader.GetString("Search_StartSerchButtonText");
            this.btnMainWindow.Text = ResourceReader.GetString("Search_MainWindowButtonText");
        }

        private void InitializeTabs()
        {
            this.SuspendLayout();

            this.tabsBar1.ClearTabs();

            // general part
            GeneralSearchPage generalSearchPage = new GeneralSearchPage(this.presenter);
            generalSearchPage.Name = "General";
            generalSearchPage.Location = PageLocation;
            generalSearchPage.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right | System.Windows.Forms.AnchorStyles.Top;
            this.Controls.Add(generalSearchPage);

            Tab tabGeneral = new Tab();
            tabGeneral.Content = ResourceReader.GetString("Search_GeneralTabName");
            tabGeneral.Tag = generalSearchPage;
            this.tabsBar1.AddTab(tabGeneral);

            // advance part
            AdvanceSearchPage advanceSearchPage = new AdvanceSearchPage(this.presenter);
            advanceSearchPage.Name = "Advance";
            advanceSearchPage.Location = PageLocation;
            advanceSearchPage.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right | System.Windows.Forms.AnchorStyles.Top;
            this.Controls.Add(advanceSearchPage);

            Tab tabAdvance = new Tab();
            tabAdvance.Content = ResourceReader.GetString("Search_AdvanceTabName");
            tabAdvance.Tag = advanceSearchPage;
            this.tabsBar1.AddTab(tabAdvance);

            this.tabsBar1.SelectedTabChanged += this.presenter.SelectedTabChanged;
            this.tabsBar1.SelectedIndex = 0;

            this.ResumeLayout();
        }

        public void ShowWarning(SearchError error)
        {
            string errorLine = string.Empty;
            switch (error)
            {
                case SearchError.AllBrandsUnselected:
                    errorLine = ResourceReader.GetString("MsgAllBrandsUnselected");
                    break;
                case SearchError.AllYearsUnselected:
                    errorLine = ResourceReader.GetString("MsgAllYearsUnselected");
                    break;
                case SearchError.NoSearchCriterias:
                    errorLine = ResourceReader.GetString("MsgNoSearchCriterias");
                    break;
            }

            this.WarningLabel.Text = errorLine;
            this.WarningLabel.Show();
        }

        public void HideWarning(int seconds)
        {
            this.WarningLabel.Hide(seconds);
        }
        #endregion
    }
}
