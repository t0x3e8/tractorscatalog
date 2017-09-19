using System;
using System.Collections.Generic;
using System.Text;
using Enceladus.Api;
using System.Drawing;
using System.Windows.Forms;
using Enceladus.StringLibrary;
using System.Threading;
using System.ComponentModel;
using Enceladus.UIToolbox;
using System.Reflection;

namespace Enceladus
{
    class SearchResultPresenter
    {
        #region Fields and properties
        protected readonly ISearchResultView view;
        protected IList<TractorSearchResult> activeTractorsCollection;
        protected IList<TractorSearchResult> foundTractors;
        protected readonly static int RecommendedResultNumber = 1200;
        protected readonly static int PagedResultNumber = 1000;
        protected ICommand freezeCommand;
        protected MainWindow mainWindow;
        #endregion

        #region Constructors
        public SearchResultPresenter(ISearchResultView view, MainWindow mainWindow)
        {
            this.view = view;
            Logger.Instance.Log(LogType.Info, "SearchResultPresenter.ctor");

            this.mainWindow = mainWindow;
        }
        #endregion

        #region Methods
        public void Activate()
        {
            // if the SearchCriteria is null then it means that Zuruck was pressed, otherwise search was used
            if (this.view.SearchCriteria != null)
                this.BeginSearch(this.view.SearchCriteria);
        }

        public void InitializeControls()
        {
            this.SetupDataGrid();
            this.view.DataGrid.CellValueNeeded += new DataGridViewCellValueEventHandler(DataGrid_CellValueNeeded);
            this.view.DataGrid.CellDoubleClick += new DataGridViewCellEventHandler(DataGrid_CellDoubleClick);
            this.view.DataGrid.ColumnHeaderMouseClick += new DataGridViewCellMouseEventHandler(DataGrid_ColumnHeaderMouseClick);

            this.view.ShowAllLabelButton.Click += new EventHandler(ShowAllLabelButton_Click);

            this.view.SelectedTractors.DataSource = ApplicationState.MarkedTractorsCollection;
            ApplicationState.MarkedTractorsCollection.ListChanged += new System.ComponentModel.ListChangedEventHandler(MarkedTractorsCollection_ListChanged);
        }

        public void InitializeCommands(MainWindow window)
        {
            this.view.ShowTractorButton.Command = new SingleTractorCommand(window);
            this.view.ShowTractorButton.CommandExecuting += new EventHandler<CommandEventArgs>(ShowTractorButton_CommandExecuting);
            this.view.SearchButton.Command = new SearchCommand(window);
            this.view.MainMenuButton.Command = new MainCommand(window);

            // Selecte Tractors navigation button
            this.view.RemoveBookmarkButton.Command = new BookmarkCommand();
            this.view.RemoveBookmarkButton.Click += new EventHandler(RemoveBookmarkButton_Click);
            this.view.CompareTractorsButton.Command = new GenerateComparisonSheetCommand();
            this.view.CompareTractorsButton.Click += new EventHandler(CompareTractorsButton_Click);
            this.view.PrintSearchResultButton.Command = new PrintSearchResultCommand();
            this.view.PrintSearchResultButton.Click += new EventHandler(PrintSearchResultButton_Click);
            this.view.CleanBookmarkListButton.Command = new CleanBookmarkListCommand();

            this.freezeCommand = new FreezeWindowCommand(window);
        }

        protected void CompareTractorsButton_Click(object sender, EventArgs e)
        {
            if (ApplicationState.MarkedTractorsCollection.Count > 0)
            {
                ICommand command = (sender as GradientIconButton).Command;
                command.Execute<IEnumerable<TractorBase>>(ApplicationState.MarkedTractorsCollection);
            }
        }

        protected void PrintSearchResultButton_Click(object sender, EventArgs e)
        {
            if (this.activeTractorsCollection != null && this.activeTractorsCollection.Count > 0)
            {
                ICommand command = (sender as GradientIconButton).Command;
                command.Execute<IList<TractorSearchResult>>(this.activeTractorsCollection);
            }
        }

        protected void RemoveBookmarkButton_Click(object sender, EventArgs e)
        {
            if (this.view.SelectedTractors.SelectedItem != null)
            {
                ICommand command = (sender as GradientIconButton).Command;
                command.Execute<TractorBase>(this.view.SelectedTractors.SelectedItem as TractorBase);
            }
        }

        protected void ShowTractorButton_CommandExecuting(object sender, CommandEventArgs e)
        {
            if (this.view.DataGrid.SelectedRows.Count == 1)
            {
                e.CommandArgument = this.view.DataGrid[0, this.view.DataGrid.SelectedRows[0].Index].Tag.ToString();
            }
        }
        
        protected void DataGrid_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            var column = this.view.DataGrid.Columns[e.ColumnIndex];

            this.DisableSortGlyph(column);
            SortOrder sortMode = this.SetSortGlyph(column);
            this.activeTractorsCollection = CollectionHelper.SortCollectionByColumn(this.activeTractorsCollection, column, sortMode);
            this.UpdateDataView(this.activeTractorsCollection);
            
            this.HideWaitingWindow();
        }
        
        protected SortOrder SetSortGlyph(DataGridViewColumn column)
        {
            if (column.SortMode == DataGridViewColumnSortMode.Programmatic)
            {
                if (column.HeaderCell.SortGlyphDirection == SortOrder.Ascending)
                    column.HeaderCell.SortGlyphDirection = SortOrder.Descending;
                else
                    column.HeaderCell.SortGlyphDirection = SortOrder.Ascending;
            }

            return column.HeaderCell.SortGlyphDirection;
        }

        protected void DisableSortGlyph(DataGridViewColumn skipColumn)
        {
            foreach (DataGridViewColumn column in this.view.DataGrid.Columns)
            {
                if (skipColumn == null || !column.HeaderText.Equals(skipColumn.HeaderText))
                {
                    column.HeaderCell.SortGlyphDirection = SortOrder.None;
                }
            }
        }
        
        protected void ShowAllLabelButton_Click(object sender, EventArgs e)
        {
            if (this.foundTractors != null && this.view.DataGrid.RowCount < this.foundTractors.Count)
            {
                this.activeTractorsCollection = CollectionHelper.CopyCollection(this.foundTractors, this.foundTractors.Count);
                this.UpdateDataView(this.activeTractorsCollection, true);
            }
        }

        protected void SetupDataGrid()
        {
            if (this.view.DataGrid != null)
            {
                try
                {
                    this.view.DataGrid.SuspendLayout();
                    this.view.DataGrid.Columns.Clear();
                    this.view.DataGrid.AutoGenerateColumns = false;

                    #region Columns
                    DataGridViewTextBoxColumn col = new DataGridViewTextBoxColumn();
                    col.HeaderText = ResourceReader.GetString("DataGrid_colSchlepperhersteller");
                    col.SortMode = DataGridViewColumnSortMode.Programmatic;
                    col.DataPropertyName = "Schlepperhersteller";
                    col.ValueType = typeof(string);
                    this.view.DataGrid.Columns.Add(col);

                    col = new DataGridViewTextBoxColumn();
                    col.HeaderText = ResourceReader.GetString("DataGrid_colSchleppertyp");
                    col.SortMode = DataGridViewColumnSortMode.Programmatic;
                    col.DataPropertyName = "Schleppertyp";
                    col.ValueType = typeof(string);
                    this.view.DataGrid.Columns.Add(col);

                    col = new DataGridViewTextBoxColumn();
                    col.HeaderText = ResourceReader.GetString("DataGrid_colLetzteAktualisierung");
                    col.SortMode = DataGridViewColumnSortMode.Programmatic;
                    col.DataPropertyName = "LetzteAktualisierung";
                    col.ValueType = typeof(int);
                    this.view.DataGrid.Columns.Add(col);

                    col = new DataGridViewTextBoxColumn();
                    col.HeaderText = ResourceReader.GetString("DataGrid_colNennleistungKW");
                    col.SortMode = DataGridViewColumnSortMode.Programmatic;
                    col.DataPropertyName = "NennleistungkW";
                    col.ValueType = typeof(int);
                    this.view.DataGrid.Columns.Add(col);

                    col = new DataGridViewTextBoxColumn();
                    col.HeaderText = ResourceReader.GetString("DataGrid_colNennleistungPS");
                    col.SortMode = DataGridViewColumnSortMode.Programmatic;
                    col.DataPropertyName = "NennleistungPS";
                    col.ValueType = typeof(int);
                    this.view.DataGrid.Columns.Add(col);

                    col = new DataGridViewTextBoxColumn();
                    col.HeaderText = ResourceReader.GetString("DataGrid_colGesamtgewicht");
                    col.SortMode = DataGridViewColumnSortMode.Programmatic;
                    col.DataPropertyName = "Gesamtgewicht";
                    col.ValueType = typeof(int);
                    this.view.DataGrid.Columns.Add(col);

                    col = new DataGridViewTextBoxColumn();
                    col.HeaderText = ResourceReader.GetString("DataGrid_colLeergewicht");
                    col.SortMode = DataGridViewColumnSortMode.Programmatic;
                    col.DataPropertyName = "Nutzlast";
                    col.ValueType = typeof(int);
                    this.view.DataGrid.Columns.Add(col);

                    col = new DataGridViewTextBoxColumn();
                    col.HeaderText = ResourceReader.GetString("DataGrid_colWendekreis");
                    col.SortMode = DataGridViewColumnSortMode.Programmatic;
                    col.DataPropertyName = "Wendekreis";
                    col.ValueType = typeof(int);
                    this.view.DataGrid.Columns.Add(col);

                    col = new DataGridViewTextBoxColumn();
                    col.HeaderText = ResourceReader.GetString("DataGrid_colHoehe");
                    col.SortMode = DataGridViewColumnSortMode.Programmatic;
                    col.DataPropertyName = "Hoehe";
                    col.ValueType = typeof(int);
                    this.view.DataGrid.Columns.Add(col);

                    col = new DataGridViewTextBoxColumn();
                    col.HeaderText = ResourceReader.GetString("DataGrid_colLsGetriebe");
                    col.SortMode = DataGridViewColumnSortMode.Programmatic;
                    col.DataPropertyName = "LS_Getriebe";
                    col.ValueType = typeof(string);
                    this.view.DataGrid.Columns.Add(col);

                    col = new DataGridViewTextBoxColumn();
                    col.HeaderText = ResourceReader.GetString("DataGrid_colKriechgetriebe");
                    col.SortMode = DataGridViewColumnSortMode.Programmatic;
                    col.DataPropertyName = "Kriechgetriebe";
                    col.ValueType = typeof(string);
                    this.view.DataGrid.Columns.Add(col);

                    col = new DataGridViewTextBoxColumn();
                    col.HeaderText = ResourceReader.GetString("DataGrid_colFronthubwerkUndZW");
                    col.SortMode = DataGridViewColumnSortMode.Programmatic;
                    col.DataPropertyName = "FronthubwerkundZW";
                    col.ValueType = typeof(string);
                    this.view.DataGrid.Columns.Add(col);

                    col = new DataGridViewTextBoxColumn();
                    col.HeaderText = ResourceReader.GetString("DataGrid_colHubkraftMaximalDan");
                    col.SortMode = DataGridViewColumnSortMode.Programmatic;
                    col.DataPropertyName = "HubkraftmaximaldaN";
                    col.ValueType = typeof(int);
                    this.view.DataGrid.Columns.Add(col);

                    col = new DataGridViewTextBoxColumn();
                    col.HeaderText = ResourceReader.GetString("DataGrid_colPreisVonEuro");
                    col.SortMode = DataGridViewColumnSortMode.Programmatic;
                    col.DataPropertyName = "PreisvonEuro";
                    col.ValueType = typeof(int);
                    this.view.DataGrid.Columns.Add(col);
                    #endregion
                }
                catch (Exception ex)
                {
                    Logger.Instance.Log(LogType.Info, "SetupDataGrid", ex.ToString());
                }
                finally
                {
                    this.view.DataGrid.ResumeLayout();
                }
            }
        }

        protected void DataGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                TractorBase tractor = this.activeTractorsCollection[this.view.DataGrid.Rows[e.RowIndex].Index];
                if (!ApplicationState.MarkedTractorsCollection.Contains(tractor))
                    ApplicationState.MarkedTractorsCollection.Add(tractor);
            }
        }

        protected void DataGrid_CellValueNeeded(object sender, DataGridViewCellValueEventArgs e)
        {
            if (e.ColumnIndex == 0)
                this.view.DataGrid[0, e.RowIndex].Tag = this.activeTractorsCollection[e.RowIndex].Satz;

            e.Value = this.activeTractorsCollection[e.RowIndex].GetValue(e.ColumnIndex);

            if (e.RowIndex == (this.view.DataGrid.RowCount - 1) && e.ColumnIndex == (this.view.DataGrid.ColumnCount - 1))
                this.HideWaitingWindow();
        }

        protected void HideWaitingWindow()
        {
            this.freezeCommand.Execute<bool>(true);
            WaitingWindowDispatcher.Instance.Hide();
        }

        protected void ShowWaitingWindow()
        {
            // Make sure that MainWindow is not sent to back
            this.mainWindow.Focus();

            this.freezeCommand.Execute<bool>(false);
            WaitingWindowDispatcher.Instance.Show(this.mainWindow.PointToScreen(new Point((this.view.Size.Width / 2), (this.view.Size.Height / 2) - 100)));
        }

        protected void BeginSearch(string searchCriteria)
        {
            Logger.Instance.Log(LogType.Info, "BeginSearch");

            IDatabaseStorage databaseStorage = new DatabaseStorage();
            databaseStorage.BeginSearch(new AsyncCallback(EndSearch), databaseStorage, searchCriteria, -1, -1);
        }

        protected void EndSearch(IAsyncResult result)
        {
            Logger.Instance.Log(LogType.Info, "EndSearch");

            try
            {
                IDatabaseStorage dbStorage = (result.AsyncState as IDatabaseStorage);
                IList<TractorSearchResult> tractors = dbStorage.EndSearch(result);

                Logger.Instance.Log(LogType.Info, "EndSearch", "Tractors found: " + ((tractors == null) ? "null" : tractors.Count.ToString()));
                this.AsyncUpdateFields(tractors);
            }
            catch (Exception ex)
            {
                Logger.Instance.Log(LogType.Error, "EndSearch", "An exception happened: " + ex.ToString());

                this.AsyncUpdateFields(new List<TractorSearchResult>());
                MessageBox.Show(ResourceReader.GetString("MsgDatabaseProblem"), ResourceReader.GetString("MsgError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        protected void AsyncUpdateFields(IList<TractorSearchResult> tractors)
        {
            if (this.view.MainLayout.InvokeRequired)
                this.view.MainLayout.Invoke(new TractorSetUpdateCallback(this.AsyncUpdateFields), new object[] { tractors });
            else
            {
                Logger.Instance.Log(LogType.Info, "AsyncUpdateFields");
                this.HideWaitingWindow();
                
                this.view.DataGrid.Rows.Clear();
                this.DisableSortGlyph(null);
                this.foundTractors = tractors;
                this.UpdateDataView(tractors, false);
                this.UpdateSearchStatus(tractors.Count);

                ApplicationState.SearchObserver.HasResults = (this.foundTractors.Count > 0);
                ApplicationState.SearchObserver.Notify();
            }
        }

        protected void UpdateSearchStatus(int count)
        {
            this.view.SearchResultLabel.Label = string.Format(this.view.SearchResultLabel.Pattern, count);
        }

        protected void UpdateShowAllButton(bool visible)
        {
            this.view.ShowAllLabelButton.Visible = visible;
        }
        
        protected void UpdateDataView(IList<TractorSearchResult> tractors, bool forceShow)
        {
            if (tractors != null)
            {
                int showTractorsNumber = tractors.Count;
                // for the sake of performance, there is question which will inform an user that this operation can last a while
                if (tractors.Count > RecommendedResultNumber && !forceShow)
                {
                    DialogResult questionResult = MessageBox.Show(ResourceReader.GetString("MsgTooManyResults"), ResourceReader.GetString("MsgQuestion"), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    showTractorsNumber = (questionResult == DialogResult.Yes) ? PagedResultNumber : tractors.Count;
                }

                this.UpdateShowAllButton(showTractorsNumber < this.foundTractors.Count);
                if (showTractorsNumber > 0)
                {
                    this.ShowWaitingWindow();

                    this.activeTractorsCollection = CollectionHelper.CopyCollection(tractors, showTractorsNumber);

                    Application.DoEvents();
                    this.view.DataGrid.RowCount = showTractorsNumber;
                }
            }
        }

        protected void UpdateDataView(IList<TractorSearchResult> tractors)
        {
            if (tractors != null)
            {
                this.activeTractorsCollection = CollectionHelper.CopyCollection(tractors, tractors.Count);
                this.view.DataGrid.RowCount = this.view.DataGrid.Rows.Count;
                this.view.DataGrid.Invalidate();
            }
        }

        protected void MarkedTractorsCollection_ListChanged(object sender, System.ComponentModel.ListChangedEventArgs e)
        {
            this.view.SelectedTractors.SelectedIndex = this.view.SelectedTractors.Items.Count - 1;
        }
        #endregion
    }
}