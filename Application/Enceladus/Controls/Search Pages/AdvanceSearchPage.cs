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
    partial class AdvanceSearchPage : PageBase
    {
        #region Fields and properties
        private SearchPresenter presenter;
        #endregion

        #region Constructors
        public AdvanceSearchPage(SearchPresenter presenter)
        {
            InitializeComponent();

            this.presenter = presenter;
            this.dsEnginePower.ValueChanged += this.presenter.EnginePowerValueChanged;
            this.cbHoistOption1.CheckboxChecked += this.presenter.HoistCheckboxChanged;
        }
        #endregion               

        #region Methods
        public override void ChangeLanguage()
        {
            this.pnlEnginePower.Caption = ResourceReader.GetString("SearchAdv_EnginePowerPanelCaption");
            this.pnlGears.Caption = ResourceReader.GetString("SearchAdv_GearsPanelCaption");
            this.pnlHoist.Caption = ResourceReader.GetString("SearchAdv_HoistPanelCaption");
            this.pnlPrice.Caption = ResourceReader.GetString("SearchAdv_PricePanelCaption");
            this.pnlWeight.Caption = ResourceReader.GetString("SearchAdv_WeightPanelCaption");
            this.cbGearsOption1.Content = ResourceReader.GetString("SearchAdv_Gears1OptionText");
            this.cbGearsOption2.Content = ResourceReader.GetString("SearchAdv_Gears2OptionText");
            this.cbHoistOption1.Content = ResourceReader.GetString("SearchAdv_Hoist1OptionText");
            this.controlLabel1.Text = ResourceReader.GetString("SearchAdv_HoistScrollerLabel");
            this.controlLabel2.Text = ResourceReader.GetString("SearchAdv_WeightScroller1Label");
            this.controlLabel3.Text = ResourceReader.GetString("SearchAdv_WeightScroller2Label");
            this.controlLabel4.Text = ResourceReader.GetString("SearchAdv_WeightScroller3Label");
            this.controlLabel5.Text = ResourceReader.GetString("SearchAdv_WeightScroller4Label");
            this.controlLabel6.Text = ResourceReader.GetString("SearchAdv_PreisScrollerLabel");
            this.inputBoxLabel2.Label = ResourceReader.GetString("daNLabel");
            this.inputBoxLabel1.Label = ResourceReader.GetString("kgLabel");
            this.inputBoxLabel3.Label = ResourceReader.GetString("kgLabel");
            this.inputBoxLabel4.Label = ResourceReader.GetString("mLabel");
            this.inputBoxLabel5.Label = ResourceReader.GetString("cmLabel");
            this.inputBoxLabel6.Label = ResourceReader.GetString("moneyLabel");

        }
        #endregion
    }
}
