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
    partial class GeneralSearchPage : PageBase
    {
        #region Fields and properties
        private SearchPresenter presenter;
        #endregion

        #region Constructors
        public GeneralSearchPage(SearchPresenter presenter)
        {
            InitializeComponent();
            this.presenter = presenter;
        }
        #endregion

        #region Methods
        public override void ChangeLanguage()
        {
            this.pnlYear.Caption = ResourceReader.GetString("SearchGen_YearPanelCaption");
            this.pnlBrands.Caption = ResourceReader.GetString("SearchGen_BrandsPanelCaption");
            this.switchControl2.Caption = ResourceReader.GetString("SearchGen_SwitchYearCaption");
            this.switchControl2.Hint = ResourceReader.GetString("SearchGen_SwitchStateHint");
            this.switchControl1.Caption = ResourceReader.GetString("SearchGen_SwitchBrandsCaption");
            this.switchControl1.Hint = ResourceReader.GetString("SearchGen_SwitchStateHint");
        }
        #endregion
    }
}
