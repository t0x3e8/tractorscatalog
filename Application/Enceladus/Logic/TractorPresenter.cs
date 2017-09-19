using System;
using System.Collections.Generic;
using System.Text;
using Enceladus.UIToolbox;
using Enceladus.Api;
using System.Diagnostics;
using System.Threading;
using Enceladus.Logic;
using System.Drawing;
using System.Windows.Forms;
using Enceladus.StringLibrary;

namespace Enceladus
{
    class TractorPresenter:IObserver
    {
        #region Fields
        protected ITractorView view;
        protected int currentTractorIndex;
        public int CurrentTractorIndex
        {
            get { return this.currentTractorIndex; }
            set
            {
                if (this.currentTractorIndex != value)
                {
                    Logger.Instance.Log(LogType.Info, "CurrentTractorIndex changed", value.ToString());
                    this.currentTractorIndex = value;
                    this.BeginUpdatingControls();
                }
            }
        }
        protected readonly int MaxTractorsNumber;
        protected readonly int MinTractorsNumber = 1;
        protected Tractor activeTractor = null;
        protected ICommand freezeWindowCommand;
        #endregion

        #region Constructors
        public TractorPresenter(ITractorView view)
        {
            this.view = view;
            Logger.Instance.Log(LogType.Info, "TractorPresenter.ctor");

            this.view.SizeChanged += new EventHandler(view_SizeChanged);

            // this must be initialized here, since it's readonly 
            ConstantsReader constantReader = new ConstantsReader();
            MaxTractorsNumber = constantReader.GetTotalTractorsNumber();

            ApplicationState.SearchObserver.Attach(this);
        }
        #endregion

        #region Methods
        public void InitializeCommands(MainWindow window)
        {
            // Main navigation button 
            this.view.MainMenuButton.Command = new MainCommand(window);
            this.view.SearchButton.Command = new SearchCommand(window);
            this.view.BackButton.Command = new SearchResultCommand(window);

            // icon buttons bookmark, print and pdf
            this.view.PrintButton.Command = new PrintTractorCommand();
            this.view.PrintButton.Click += new EventHandler(ExecuteButtonCommand);
            this.view.BookmarkButton.Command = new BookmarkCommand();
            this.view.BookmarkButton.Click += new EventHandler(ExecuteButtonCommand);
            this.view.OpenPdfButton.Command = new OpenPdfDocumentCommand();
            this.view.OpenPdfButton.Click += new EventHandler(ExecuteButtonCommand);

            // navigation buttons
            this.view.FirstNaviButton.Click += ChangeTractorIndex;
            this.view.PreviousNaviButton.Click += ChangeTractorIndex;
            this.view.NextNaviButton.Click += ChangeTractorIndex;
            this.view.LastNaviButton.Click += ChangeTractorIndex;

            // comparison page buttons
            this.view.RemoveBookmarkButton.Command = new BookmarkCommand();
            this.view.RemoveBookmarkButton.Click += new EventHandler(RemoveBookmarkButton_Click);
            this.view.ViewTractorButton.Click += new EventHandler(ViewTractorButton_Click);
            this.view.CleanBookmarkListButton.Command = new CleanBookmarkListCommand();
            this.view.CompareTractorsButton.Command = new GenerateComparisonSheetCommand();
            this.view.CompareTractorsButton.Click += new EventHandler(CompareTractorsButton_Click);

            this.freezeWindowCommand = new FreezeWindowCommand(window);
        }
        
        void view_SizeChanged(object sender, EventArgs e)
        {
            int fontSize = 0;

            if (this.view.Size.Width > 1100)
                fontSize = 2;
            else if (this.view.Size.Width > 1050 && this.view.Size.Width <= 1100)
                fontSize = 1;

            this.view.OverviewPage.CurrentFontSize = fontSize;
            this.view.DetailsIPage.CurrentFontSize = fontSize;
            this.view.DetailsIIPage.CurrentFontSize = fontSize;
            this.view.DetailsIIIPage.CurrentFontSize = fontSize;
            this.view.ViewPage.CurrentFontSize = fontSize;
            this.view.ComparisonPage.CurrentFontSize = fontSize;
        }

        void CompareTractorsButton_Click(object sender, EventArgs e)
        {
            if (ApplicationState.MarkedTractorsCollection.Count > 0)
            {
                ICommand command = (sender as GradientIconButton).Command;
                command.Execute<IEnumerable<TractorBase>>(ApplicationState.MarkedTractorsCollection);
            }
        }

        void ViewTractorButton_Click(object sender, EventArgs e)
        {
            if (this.view.ComparisonPage.lbSelectedTractors.SelectedItem != null)
            {
                this.UpdateStatus((this.view.ComparisonPage.lbSelectedTractors.SelectedItem as TractorBase).SatzAsInteger);
            }
        }
        
        public void SelectedTabChanged(object sender, SelectionChangedEventArgs e)
        {
            this.view.MainLayout.SuspendLayout();

            (e.ActiveSelection.Tag as TractorBasePage).Visible = true;

            if (e.PreviousSelection.Tag != null)
                (e.PreviousSelection.Tag as TractorBasePage).Visible = false;

            this.view.MainLayout.ResumeLayout();
        }

        public void InitializeControls()
        {
            this.view.StatusBar.MaximumValue = MaxTractorsNumber;
            this.view.StatusBar.MinimumValue = MinTractorsNumber;
            this.UpdateStatus();
            this.view.StatusBar.StatusChanged += StatusBar_StatusChanged;

            this.view.StatusBar.TextBoxBackColor = Defines.ParsnipColor;
            this.view.ComparisonPage.lbSelectedTractors.DataSource = ApplicationState.MarkedTractorsCollection;
            ApplicationState.MarkedTractorsCollection.ListChanged += new System.ComponentModel.ListChangedEventHandler(MarkedTractorsCollection_ListChanged);
        }

        public void Activate()
        {
            this.UpdateStatus(this.view.CurrentTractorIndex);            
        }

        protected void RemoveBookmarkButton_Click(object sender, EventArgs e)
        {
            if (this.view.ComparisonPage.lbSelectedTractors.SelectedItem != null)
            {
                ICommand command = (sender as GradientIconButton).Command;
                command.Execute<TractorBase>(this.view.ComparisonPage.lbSelectedTractors.SelectedItem as TractorBase);
            }
        }

        protected void ExecuteButtonCommand(object sender, EventArgs e)
        {
            ICommand command = (sender as IconButton).Command;
            command.Execute<Tractor>(this.activeTractor);
        }

        protected void ChangeTractorIndex(object sender, EventArgs e)
        {
            NavigationIconButton button = sender as NavigationIconButton;
            switch (button.NavigationMode)
            {
                case NavigatioMode.First:
                    this.CurrentTractorIndex = MinTractorsNumber;
                    break;
                case NavigatioMode.Previous:
                    if (this.CurrentTractorIndex > MinTractorsNumber)
                        this.CurrentTractorIndex--;
                    break;
                case NavigatioMode.Next:
                    if (this.CurrentTractorIndex < MaxTractorsNumber)
                        this.CurrentTractorIndex++;
                    break;
                case NavigatioMode.Last:
                    this.CurrentTractorIndex = MaxTractorsNumber;
                    break;
            }

            this.UpdateStatus();
        }

        protected void MarkedTractorsCollection_ListChanged(object sender, System.ComponentModel.ListChangedEventArgs e)
        {
            this.UpdateBookmarkIcon(this.activeTractor);

            this.view.ComparisonPage.lbSelectedTractors.SelectedIndex = this.view.ComparisonPage.lbSelectedTractors.Items.Count - 1;
        }

        protected void StatusBar_StatusChanged(object sender, EventArgs e)
        {
            int currentIndex = int.Parse(this.view.StatusBar.Text);
            this.CurrentTractorIndex = currentIndex;
        }
                
        protected void BeginUpdatingControls()
        {
            Logger.Instance.Log(LogType.Info, "TractorPresenter.BeginUpdatingControls");

            IDatabaseStorage dbStorage = new DatabaseStorage();

            this.StartProgressing(dbStorage.IsTractorInCache(this.currentTractorIndex));
            dbStorage.BeginGet(new AsyncCallback(EndUpdatingControls), dbStorage, this.currentTractorIndex);
        }

        protected void EndUpdatingControls(IAsyncResult result)
        {
            Logger.Instance.Log(LogType.Info, "TractorPresenter.EndUpdatingControls");

            try
            {
                // This method can cause a cross-threading exception,
                // if the UI is updated be careful and use InvokeRequired property
                IDatabaseStorage dbStorage = (result.AsyncState as IDatabaseStorage);
                Tractor tractor = dbStorage.EndGet(result);

                Interlocked.Exchange<Tractor>(ref this.activeTractor, tractor);
                this.AsyncUpdateFields(tractor);

            }
            catch (Exception ex)
            {
                Logger.Instance.Log(LogType.Error, "TractorPresenter.EndUpdatingControls", "An exception happened: " + ex.ToString());

                this.AsyncUpdateFields(new Tractor());
                MessageBox.Show(ResourceReader.GetString("MsgDatabaseProblem"), ResourceReader.GetString("MsgError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }              

        protected void AsyncUpdateFields(Tractor tractor)
        {
            if (this.view.MainLayout.InvokeRequired)
                this.view.MainLayout.Invoke(new TractorUpdateCallback(this.AsyncUpdateFields), new object[] { tractor });
            else
            {
                Logger.Instance.Log(LogType.Info, "TractorPresenter.AsyncUpdateFields");
                if (tractor != null)
                {
                    this.view.OverviewPage.BindTractor(tractor);
                    this.view.DetailsIPage.BindTractor(tractor);
                    this.view.DetailsIIPage.BindTractor(tractor);
                    this.view.DetailsIIIPage.BindTractor(tractor);
                    this.view.ViewPage.BindTractor(tractor);
                    this.view.ComparisonPage.BindTractor(tractor);
                    this.UpdatePdfButtons(tractor);
                    this.UpdateBookmarkIcon(tractor);
                    this.StopProgressing();
                }
            }
        }

        protected void UpdatePdfButtons(Tractor tractor)
        {
                this.view.OpenPdfButton.Enabled = !string.IsNullOrEmpty(tractor.Seitencodeprofi) || !string.IsNullOrEmpty(tractor.Seitencodetop);
        }
        
        protected void UpdateBookmarkIcon(Tractor tractor)
        {
            if (tractor != null)
            {
                TractorBase tractorBase = tractor as TractorBase;
                if (ApplicationState.MarkedTractorsCollection.Contains(tractorBase))
                    this.view.BookmarkButton.Checked = true;
                else
                    this.view.BookmarkButton.Checked = false;
            }
        }
        
        protected void StartProgressing(bool isTractorLoadedFromCache)
        {
            if (!isTractorLoadedFromCache)
            {
                if (!this.view.ProgressRun)
                    this.view.ProgressRun = true;
                if (!this.view.ProgressRunVisible)
                    this.view.ProgressRunVisible = true;

                this.freezeWindowCommand.Execute<bool>(false);
            }
        }

        protected void StopProgressing()
        {
            if (this.view.ProgressRun)
                this.view.ProgressRun = false;
            if (this.view.ProgressRunVisible)
                this.view.ProgressRunVisible = false;

            this.freezeWindowCommand.Execute<bool>(true);
        }

        protected virtual void UpdateStatus()
        {
            Logger.Instance.Log(LogType.Info, "UpdateStatus", CurrentTractorIndex.ToString());
            this.view.StatusBar.Text = this.CurrentTractorIndex.ToString();
        }

        protected virtual void UpdateStatus(int tractorIndex)
        {
            Logger.Instance.Log(LogType.Info, "UpdateStatus", tractorIndex.ToString());
            this.view.StatusBar.Text = tractorIndex.ToString();
        }
        #endregion

        public void Update(bool searchState)
        {
            // if the search result conttains any result then enable button
            this.view.BackButton.Enabled = searchState;
        }
    }
}
