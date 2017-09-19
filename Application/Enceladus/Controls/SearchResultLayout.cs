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
    partial class SearchResultLayout : BaseLayout, ISearchResultView
    {
        #region Fields and properties
        protected readonly SearchResultPage searchResultPage; 
        protected SearchResultPresenter presenter;
        public GradientButton MainMenuButton
        {
            get { return this.btnMainWindow; }
        }

        public GradientButton SearchButton
        {
            get { return this.btnSearch; }
        }

        public GradientButton ShowTractorButton
        {
            get { return this.btnShowTractor; }
        }

        public string SearchCriteria { get; set; }

        public SearchResultLayout MainLayout
        {
            get { return this; }
        }

        public DataGridView DataGrid
        {
            get { return this.searchResultPage.DataGrid; }
        }

        public InputBoxLabel ShowAllLabelButton 
        {
            get { return this.searchResultPage.ShowAllLabelButton; }
        }

        public InputBoxLabel SearchResultLabel
        {
            get { return this.searchResultPage.SearchResultLabel; }
        }
        
        public ListBox SelectedTractors 
        {
            get { return this.searchResultPage.lbSelectedTractors; }     
        }

        public GradientIconButton RemoveBookmarkButton
        {
            get { return this.searchResultPage.simpleButton1; }
        }

        public GradientIconButton CompareTractorsButton
        {
            get { return this.searchResultPage.simpleButton2; }
        }

        public GradientIconButton PrintSearchResultButton
        {
            get { return this.searchResultPage.simpleButton3; }
        }

        public GradientIconButton CleanBookmarkListButton
        {
            get { return this.searchResultPage.simpleButton4; }
        }

        //public bool RunProgressBar
        //{
        //    get { return this.searchResultPage.ProgressBar.Run; }
        //    set { this.searchResultPage.ProgressBar.Run = value; }
        //}

        //public bool RunProgressBarVisibility
        //{
        //    get { return this.searchResultPage.ProgressBar.Visible; }
        //    set { this.searchResultPage.ProgressBar.Visible = value; }
        //}
        #endregion

        #region Constructors
        public SearchResultLayout(MainWindow window)
            : base(window)
        {
            InitializeComponent();
            this.presenter = new SearchResultPresenter(this, window);

            this.searchResultPage = new SearchResultPage(this.presenter);
            this.searchResultPage.Location = new Point(218, 13);
            this.searchResultPage.Visible = true;
            this.searchResultPage.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
            this.Controls.Add(this.searchResultPage);
                        
            this.presenter.InitializeCommands(this.Window);
            this.presenter.InitializeControls();
        }
        #endregion

        #region Methods
        public override void ChangeLanguage()
        {
            this.btnMainWindow.Text = ResourceReader.GetString("SearchResult_MainWindowButtonText");
            this.btnShowTractor.Text = ResourceReader.GetString("SearchResult_ShowTractorButtonText");
            this.btnSearch.Text = ResourceReader.GetString("SearchResult_NewSearchButtonText");
        }

        public override void Activate()
        {
            this.presenter.Activate();
        }
        #endregion
    }
}
