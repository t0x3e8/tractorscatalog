using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Enceladus.StringLibrary;
using Enceladus.UIToolbox;

namespace Enceladus
{
    partial class TractorLayout : BaseLayout, ITractorView
    {
        #region Fields
        private static readonly Point PageLocation = new Point(218, 50);

        protected readonly TractorPresenter presenter;

        public GradientButton MainMenuButton
        {
            get { return this.btnMainWindow; }
        }

        public GradientButton BackButton
        {
            get { return this.btnBack; }
        }

        public GradientButton SearchButton
        {
            get { return this.btnSearch; }
        }

        public OverviewTractorPage OverviewPage
        {
            get { return this.Controls["Overview"] as OverviewTractorPage; }
        }

        public DetailsITractorPage DetailsIPage
        {
            get { return this.Controls["DetailsI"] as DetailsITractorPage; }
        }

        public DetailsIITractorPage DetailsIIPage
        {
            get { return this.Controls["DetailsII"] as DetailsIITractorPage; }
        }

        public DetailsIIITractorPage DetailsIIIPage
        {
            get { return this.Controls["DetailsIII"] as DetailsIIITractorPage; }
        }

        public ViewTractorPage ViewPage
        {
            get { return this.Controls["View"] as ViewTractorPage; }
        }

        public ComparisonTractorPage ComparisonPage
        {
            get { return this.Controls["Comparison"] as ComparisonTractorPage; }
        }

        public TractorLayout MainLayout
        {
            get { return this as TractorLayout; }
        }

        public TractorStatus StatusBar
        {
            get { return this.tractorStatus1; }
        }

        public NavigationIconButton FirstNaviButton
        {
            get { return this.btnFirst; }
        }

        public NavigationIconButton PreviousNaviButton
        {
            get { return this.btnPrevious; }
        }

        public NavigationIconButton NextNaviButton
        {
            get { return this.btnNext; }
        }

        public NavigationIconButton LastNaviButton
        {
            get { return this.btnLast; }
        }

        public IconButton OpenPdfButton
        {
            get { return this.btnPdf; }
        }

        public IconButton PrintButton
        {
            get { return this.btnPrinter; }
        }

        public IconButton BookmarkButton
        {
            get { return this.btnBookmark; }
        }

        public bool ProgressRun
        {
            get { return this.waitingBar1.Run; }
            set { this.waitingBar1.Run = value; }
        }

        public bool ProgressRunVisible
        {
            get { return this.waitingBar1.Visible; }
            set { this.waitingBar1.Visible = value; }
        }

        public GradientIconButton RemoveBookmarkButton
        {
            get { return this.ComparisonPage.simpleButton1; }
        }

        public GradientIconButton ViewTractorButton
        {
            get { return this.ComparisonPage.simpleButton2; }
        }

        public GradientIconButton CompareTractorsButton
        {
            get { return this.ComparisonPage.simpleButton3; }
        }

        public GradientIconButton CleanBookmarkListButton
        {
            get { return this.ComparisonPage.simpleButton4; }
        }

        public int CurrentTractorIndex { get; set; }
        #endregion

        #region Constructors
        public TractorLayout(MainWindow window)
             : base(window)
        {
            InitializeComponent();

            this.presenter = new TractorPresenter(this);
            this.InitializeTabs();

            this.presenter.InitializeCommands(this.Window);
            this.presenter.InitializeControls();
        }
        #endregion

        #region Methods
        private void InitializeTabs()
        {
            this.SuspendLayout();

            this.tabsBar1.ClearTabs();

            // overview part
            OverviewTractorPage overviewPage = new OverviewTractorPage(this.presenter);
            overviewPage.Name = "Overview";
            overviewPage.Visible = false;
            overviewPage.Location = PageLocation;
            overviewPage.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right | System.Windows.Forms.AnchorStyles.Top;
            this.Controls.Add(overviewPage);

            Tab tabOverview = new Tab();
            tabOverview.Content = ResourceReader.GetString("Tractor_OverviewTabName");
            tabOverview.Tag = overviewPage;
            this.tabsBar1.AddTab(tabOverview);
            
            // details I part
            DetailsITractorPage detailsIPage = new DetailsITractorPage(this.presenter);
            detailsIPage.Name = "DetailsI";
            detailsIPage.Visible = false;
            detailsIPage.Location = PageLocation;
            detailsIPage.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right | System.Windows.Forms.AnchorStyles.Top;
            this.Controls.Add(detailsIPage);

            Tab tabDetailsI = new Tab();
            tabDetailsI.Content = ResourceReader.GetString("Tractor_DetailsITabName");
            tabDetailsI.Tag = detailsIPage;
            this.tabsBar1.AddTab(tabDetailsI);
            
            // details II part
            DetailsIITractorPage detailsIIPage = new DetailsIITractorPage(this.presenter);
            detailsIIPage.Name = "DetailsII";
            detailsIIPage.Visible = false;
            detailsIIPage.Location = PageLocation;
            detailsIIPage.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right | System.Windows.Forms.AnchorStyles.Top;
            this.Controls.Add(detailsIIPage);

            Tab tabDetailsII = new Tab();
            tabDetailsII.Content = ResourceReader.GetString("Tractor_DetailsIITabName");
            tabDetailsII.Tag = detailsIIPage;
            this.tabsBar1.AddTab(tabDetailsII);
            
            // details III part
            DetailsIIITractorPage detailsIIIPage = new DetailsIIITractorPage(this.presenter);
            detailsIIIPage.Name = "DetailsIII";
            detailsIIIPage.Visible = false;
            detailsIIIPage.Location = PageLocation;
            detailsIIIPage.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right | System.Windows.Forms.AnchorStyles.Top;
            this.Controls.Add(detailsIIIPage);

            Tab tabDetailsIII = new Tab();
            tabDetailsIII.Content = ResourceReader.GetString("Tractor_DetailsIIITabName");
            tabDetailsIII.Tag = detailsIIIPage;
            this.tabsBar1.AddTab(tabDetailsIII);

            // view part
            ViewTractorPage viewPage = new ViewTractorPage(this.presenter);
            viewPage.Name = "View";
            viewPage.Visible = false;
            viewPage.Location = PageLocation;
            viewPage.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right | System.Windows.Forms.AnchorStyles.Top;
            this.Controls.Add(viewPage);

            Tab tabView = new Tab();
            tabView.Content = ResourceReader.GetString("Tractor_ViewTabName");
            tabView.Tag = viewPage;
            this.tabsBar1.AddTab(tabView);

            // view part
            ComparisonTractorPage comparisonPage = new ComparisonTractorPage(this.presenter);
            comparisonPage.Name = "Comparison";
            comparisonPage.Visible = false;
            comparisonPage.Location = PageLocation;
            comparisonPage.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right | System.Windows.Forms.AnchorStyles.Top;
            this.Controls.Add(comparisonPage);

            Tab tabComparison = new Tab();
            tabComparison.Content = ResourceReader.GetString("Tractor_ComparisonTabName");
            tabComparison.Tag = comparisonPage;
            this.tabsBar1.AddTab(tabComparison);

            this.tabsBar1.TabWidth = 88;
            this.tabsBar1.SelectedTabChanged += this.presenter.SelectedTabChanged;
            this.tabsBar1.SelectedIndex = 0;

            this.ResumeLayout();
        }

        public override void ChangeLanguage()
        {
            this.btnMainWindow.Text = ResourceReader.GetString("Tractor_MainWindowButtonText");
            this.btnSearch.Text = ResourceReader.GetString("Tractor_NewSearchButtonText");
            this.btnBack.Text = ResourceReader.GetString("Tractor_BackButtonText");
        }

        public override void Activate()
        {
            base.Activate();
            this.presenter.Activate();
        }       
        #endregion
    }
}
