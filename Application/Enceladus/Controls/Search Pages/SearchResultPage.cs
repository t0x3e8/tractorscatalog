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
    partial class SearchResultPage : PageBase
    {
        #region Fields and Properties
        protected readonly SearchResultPresenter presenter;
        #endregion

        #region Constructors
        public SearchResultPage(SearchResultPresenter presenter)
        {
            this.InitializeComponent();
            this.DataGrid.ColumnHeadersDefaultCellStyle.Font = Defines.NormalBoldFont;
            this.DataGrid.ColumnHeadersDefaultCellStyle.ForeColor = Defines.CarrotColor;
            this.DataGrid.DefaultCellStyle.Font = Defines.NormalBoldFont;
            this.DataGrid.DefaultCellStyle.ForeColor = Defines.CarrotColor;
            this.ShowAllLabelButton.Font = Defines.TinyUnderlineFont;
            this.SearchResultLabel.Font = Defines.NormalFont;
            this.inputBoxLabel1.Font = Defines.NormalFont;
            this.lbSelectedTractors.Font = Defines.NormalFont;

            this.presenter = presenter;
        }
        #endregion

        #region Methods
        public override void ChangeLanguage()
        {
            this.inputBoxLabel1.Label = ResourceReader.GetString("SearchResult_MarkierteMaschinenText");

            this.simpleButton1.Text = ResourceReader.GetString("SearchResult_RemoveSelectedButtonCaption");
            this.simpleButton2.Text = ResourceReader.GetString("SearchResult_CompareTractorsButtonCaption");
            this.simpleButton3.Text = ResourceReader.GetString("SearchResult_PrintTractorsButtonCaption");
            this.simpleButton4.Text = ResourceReader.GetString("SearchResult_CleanListButtonCaption");

            this.ShowAllLabelButton.Label = ResourceReader.GetString("SearchResult_ShowAllResultButtonCaption");
            this.SearchResultLabel.Pattern = ResourceReader.GetString("SearchResult_ResultsFoundPattern");
        }
        #endregion
    }
}
