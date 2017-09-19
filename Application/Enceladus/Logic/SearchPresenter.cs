using System;
using System.Collections.Generic;
using System.Text;
using Enceladus.Api;
using Enceladus.StringLibrary;
using Enceladus.UIToolbox;
using System.Windows.Forms;
using System.Diagnostics;
using Enceladus.Api.UI;

namespace Enceladus
{
    class SearchPresenter
    {
        #region Fields and properties
        private ISearchView view;
        private ConstantsReader constantsReader;
        private Resizer resizer = new Resizer();
        #endregion

        #region Constructors
        public SearchPresenter(ISearchView view)
        {
            this.view = view;
            Logger.Instance.Log(LogType.Info, "SearchPresenter.ctor");

            this.constantsReader = new ConstantsReader();
        }
        #endregion

        #region Methods
        public void InitializeControls()
        {
            this.view.GeneralPage.Font = Defines.TinyBoldFont;

            // General Page
            this.view.GeneralPage.pnlBrands.Brands = this.constantsReader.GetBrands();
            this.view.GeneralPage.pnlBrands.AdditionalOptions = this.CreateBrandsOptionsList();

            // Advance Page
            MinMaxRange psRange = this.constantsReader.GetPSRange();
            this.view.AdvancePage.dsEnginePower.MaximalValue = psRange.Max;
            this.view.AdvancePage.dsEnginePower.MinimalValue = psRange.Min;
            this.view.AdvancePage.dsEnginePower.ValueLeft = psRange.Min;
            this.view.AdvancePage.dsEnginePower.ValueRight = psRange.Max;
            
            this.view.AdvancePage.cbGearsOption1.IsChecked = false;
            this.view.AdvancePage.cbGearsOption2.IsChecked = false;
            
            this.view.AdvancePage.cbHoistOption1.IsChecked = false;
            this.view.AdvancePage.HoistScroller.Enabled = false;
            MinMaxRange hoistRange = this.constantsReader.GetHoistRange();
            this.view.AdvancePage.HoistScroller.MinimalValue = 0; // hoistRange.Min;
            this.view.AdvancePage.HoistScroller.MaximalValue = hoistRange.Max;
            this.view.AdvancePage.HoistScroller.Value = 0;

            MinMaxRange weightRange = this.constantsReader.GetWeightRange();
            this.view.AdvancePage.WeightScroller1.MinimalValue = 0;// weightRange.Min;
            this.view.AdvancePage.WeightScroller1.MaximalValue = weightRange.Max;
            this.view.AdvancePage.WeightScroller1.Value = weightRange.Max;

            MinMaxRange emptyWeightRange = this.constantsReader.GetEmptyWeightRange();
            this.view.AdvancePage.WeightScroller2.MinimalValue = 0;// mptyWeightRange.Min;
            this.view.AdvancePage.WeightScroller2.MaximalValue = emptyWeightRange.Max;
            this.view.AdvancePage.WeightScroller2.Value = emptyWeightRange.Max;

            MinMaxRange tropicRange = this.constantsReader.GetTropicRange();
            this.view.AdvancePage.WeightScroller3.MinimalValue = 0;// tropicRange.Min;
            this.view.AdvancePage.WeightScroller3.MaximalValue = tropicRange.Max;
            this.view.AdvancePage.WeightScroller3.Value = tropicRange.Max;

            MinMaxRange heightRange = this.constantsReader.GetHeightRange();
            this.view.AdvancePage.WeightScroller4.MinimalValue = 0;// heightRange.Min;
            this.view.AdvancePage.WeightScroller4.MaximalValue = heightRange.Max;
            this.view.AdvancePage.WeightScroller4.Value = heightRange.Max;

            MinMaxRange priceRange = this.constantsReader.GetPriceRange();
            this.view.AdvancePage.PreisScroller.MinimalValue = 0;// priceRange.Min;
            this.view.AdvancePage.PreisScroller.MaximalValue = priceRange.Max;
            this.view.AdvancePage.PreisScroller.Value = priceRange.Max;
        }

        private IDictionary<string, string> CreateBrandsOptionsList()
        {
            IDictionary<string,string> options = new Dictionary<string, string>(2);
            options.Add("Standardschlepper", ResourceReader.GetString("SearchGen_StandTractorsOption"));
            options.Add("Schmalspurschlepper", ResourceReader.GetString("SearchGen_SmallTractorsOption"));

            return options;
        }

        internal void InitializeCommands(MainWindow window)
        {
            this.view.MainMenuButton.Command = new MainCommand(window);
            this.view.StartSearchButton.Command = new SearchResultCommand(window);
            this.view.StartSearchButton.CommandExecuting += new EventHandler<CommandEventArgs>(StartSearchButton_CommandExecuting);
        }

        internal void InitializeResizer()
        {
            resizer.Suspend();
            resizer.AddClient(this.view.GeneralPage.pnlYear);
            resizer.AddClient(this.view.GeneralPage.pnYears);
            resizer.AddClient(this.view.GeneralPage.pnlBrands);
            resizer.AddClient(this.view.GeneralPage.switchControl1);
            resizer.AddClient(this.view.GeneralPage.switchControl2);

            resizer.AddClient(this.view.AdvancePage.pnlEnginePower);
            resizer.AddClient(this.view.AdvancePage.pnlGears);
            resizer.AddClient(this.view.AdvancePage.pnlHoist);
            resizer.AddClient(this.view.AdvancePage.pnlPrice);
            resizer.AddClient(this.view.AdvancePage.pnlWeight);
            resizer.AddClient(this.view.AdvancePage.controlLabel1);
            resizer.AddClient(this.view.AdvancePage.controlLabel2);
            resizer.AddClient(this.view.AdvancePage.controlLabel3);
            resizer.AddClient(this.view.AdvancePage.controlLabel4);
            resizer.AddClient(this.view.AdvancePage.controlLabel5);
            resizer.AddClient(this.view.AdvancePage.controlLabel6);
            resizer.AddClient(this.view.AdvancePage.cbGearsOption1);
            resizer.AddClient(this.view.AdvancePage.cbGearsOption2);
            resizer.AddClient(this.view.AdvancePage.cbHoistOption1);
            resizer.AddClient(this.view.AdvancePage.dsEnginePower);
            this.resizer.Release();

        }

        public void StartSearchButton_CommandExecuting(object sender, CommandEventArgs e)
        {
            SearchCriteria searchCriteria = this.BuildSearchCriteria();
            Logger.Instance.Log(LogType.Info, "StartSearchButton", searchCriteria.Criterias);

            if ((searchCriteria.Error != SearchError.None) && (searchCriteria.Error != SearchError.NoSearchCriterias))
            {
                e.Cancel = true;
                this.view.ShowWarning(searchCriteria.Error);
                this.view.HideWarning(10);
            }
            else
            {
                e.CommandArgument = searchCriteria.Criterias;
            }
        }

        public void HoistCheckboxChanged(object sender, EventArgs e)
        {
            this.view.AdvancePage.HoistScroller.Enabled = this.view.AdvancePage.cbHoistOption1.IsChecked;
        }

        public void EnginePowerValueChanged(object sender, EventArgs e)
        {
            int leftValue = this.view.AdvancePage.dsEnginePower.ValueLeft;
            int rightValue = this.view.AdvancePage.dsEnginePower.ValueRight;

            this.view.AdvancePage.dsEnginePower.DisplayLeftValue = new DualScrollerDisplayValue(this.ConvertPStoKW(leftValue).ToString(), leftValue.ToString());
            this.view.AdvancePage.dsEnginePower.DisplayRightValue = new DualScrollerDisplayValue(this.ConvertPStoKW(rightValue).ToString(), rightValue.ToString());
        }

        private decimal ConvertPStoKW(int ps)
        {
            decimal d = ps * 0.735m;
            return Math.Round(d, 0);
        }

        public void SelectedTabChanged(object sender, SelectionChangedEventArgs e)
        {
            this.view.MainLayout.SuspendLayout();

            (e.ActiveSelection.Tag as PageBase).Visible = true;

            if (e.PreviousSelection.Tag != null)
                (e.PreviousSelection.Tag as PageBase).Visible = false;

            this.view.MainLayout.ResumeLayout();
        }

        #region Search Criterias
        protected SearchCriteria BuildSearchCriteria()
        {
            string criterias = string.Empty;
            SearchError error = SearchError.None;

            #region Years
            if (this.view.GeneralPage.pnYears.AllYearsUnselected)
                error = SearchError.AllYearsUnselected;
            else if (!this.view.GeneralPage.pnYears.AllYearsSelected)
            {
                var years = this.view.GeneralPage.pnYears.GetSelectedYears();
                var yearsCriteria = this.CombineYearsArrayIntoString(years);

                if (!string.IsNullOrEmpty(yearsCriteria))
                    criterias = String.Format("(LETZTEAKTUALISIERUNG IN ({0}))", yearsCriteria);
            }
            #endregion

            #region Brands
            if (this.view.GeneralPage.pnlBrands.AllBrandsUnselected)
                error = SearchError.AllBrandsUnselected;
            else if (!this.view.GeneralPage.pnlBrands.AllBrandsSelected)
            {
                var brands = this.view.GeneralPage.pnlBrands.GetSelectedBrands();
                var brandsCritera = this.CombineBrandsArrayIntoString(brands);

                if (!string.IsNullOrEmpty(brandsCritera))
                {
                    if (criterias.Length > 0)
                        criterias = String.Format("{0} AND ({1})", criterias, brandsCritera);
                    else
                        criterias = brandsCritera;
                }
            }
            #endregion

            #region Standard tractor or Small tractor option
            string optionCriteria = this.view.GeneralPage.pnlBrands.GetSelectedOption();

            if (!string.IsNullOrEmpty(optionCriteria))
            {
                if (criterias.Length > 0)
                    criterias = String.Format("{0} AND (KATALOGTEIL like '{1}')", criterias, optionCriteria);
                else
                    criterias = string.Format("(KATALOGTEIL like '{0}')", optionCriteria);
            }
            #endregion

            #region Power Engine
            if (this.view.AdvancePage.dsEnginePower.ValueLeft != this.view.AdvancePage.dsEnginePower.MinimalValue ||
                this.view.AdvancePage.dsEnginePower.ValueRight != this.view.AdvancePage.dsEnginePower.MaximalValue)
            {
                string powerEngineCriteria = this.CombinePowerEngineCritera();

                if (!string.IsNullOrEmpty(powerEngineCriteria))
                {
                    if (criterias.Length > 0)
                        criterias = String.Format("{0} AND ({1})", criterias, powerEngineCriteria);
                    else
                        criterias = powerEngineCriteria;
                }
            }
            #endregion

            #region Gears
            string gearsCriteria = this.CombineGearsCriteria();
            if (!string.IsNullOrEmpty(gearsCriteria))
            {
                if (criterias.Length > 0)
                    criterias = String.Format("{0} AND ({1})", criterias, gearsCriteria);
                else
                    criterias = gearsCriteria;
            }
            #endregion

            #region Hoist
            string hoistCriteria = this.CombineHoistCriteria();
            if (!string.IsNullOrEmpty(hoistCriteria))
            {
                if (criterias.Length > 0)
                    criterias = String.Format("{0} AND ({1})", criterias, hoistCriteria);
                else
                    criterias = hoistCriteria;
            }
            #endregion

            #region Weight
            string weightCriteria = this.CombineWeightCriteria();
            if (!string.IsNullOrEmpty(weightCriteria))
            {
                if (criterias.Length > 0)
                    criterias = String.Format("{0} AND ({1})", criterias, weightCriteria);
                else
                    criterias = weightCriteria;
            }
            #endregion

            #region Preis
            string preisCriteria = this.CombinePriseCriteria();
            if (!string.IsNullOrEmpty(preisCriteria))
            {
                if (criterias.Length > 0)
                    criterias = String.Format("{0} AND ({1})", criterias, preisCriteria);
                else
                    criterias = preisCriteria;
            }
            #endregion

            if (string.IsNullOrEmpty(criterias) && error == SearchError.None)
                error = SearchError.NoSearchCriterias;

            return new SearchCriteria(criterias, error);
        }

        protected string CombinePriseCriteria()
        {
            string condition = string.Empty;

            if (this.view.AdvancePage.PreisScroller.Value < this.view.AdvancePage.PreisScroller.MaximalValue)
                condition = String.Format("PREISVONEURO <= {0}", this.view.AdvancePage.PreisScroller.Value);

            return condition;
        }

        protected string CombineWeightCriteria()
        {
            string condition = string.Empty;

            if (this.view.AdvancePage.WeightScroller1.Value < this.view.AdvancePage.WeightScroller1.MaximalValue)
                condition = String.Format("GESAMTGEWICHT <= {0}", this.view.AdvancePage.WeightScroller1.Value);

            if (this.view.AdvancePage.WeightScroller2.Value < this.view.AdvancePage.WeightScroller2.MaximalValue)
            {
                if (!string.IsNullOrEmpty(condition))
                    condition = String.Format("{0} AND NUTZLAST <= {1}", condition, this.view.AdvancePage.WeightScroller2.Value);
                else
                    condition = String.Format("NUTZLAST <= {0}", this.view.AdvancePage.WeightScroller2.Value);
            }

            if (this.view.AdvancePage.WeightScroller3.Value < this.view.AdvancePage.WeightScroller3.MaximalValue)
            {
                if (!string.IsNullOrEmpty(condition))
                    condition = String.Format("{0} AND WENDEKREIS <= {1}", condition, this.view.AdvancePage.WeightScroller3.Value);
                else
                    condition = String.Format("WENDEKREIS <= {0}", this.view.AdvancePage.WeightScroller3.Value);
            }

            if (this.view.AdvancePage.WeightScroller4.Value < this.view.AdvancePage.WeightScroller4.MaximalValue)
            {
                if (!string.IsNullOrEmpty(condition))
                    condition = String.Format("{0} AND HOEHE <= {1}", condition, this.view.AdvancePage.WeightScroller4.Value);
                else
                    condition = String.Format("HOEHE <= {0}", this.view.AdvancePage.WeightScroller4.Value);
            }

            return condition;
        }

        protected string CombineHoistCriteria()
        {
            string condition = string.Empty;
            if (this.view.AdvancePage.cbHoistOption1.IsChecked)
                condition = "FRONTHUBWERKUNDZW <> 'N'";

            if (this.view.AdvancePage.HoistScroller.Value > 0)
            {
                if (!string.IsNullOrEmpty(condition))
                    condition = String.Format("{0} AND HUBKRAFTMAXIMALDAN >= {1}", condition, this.view.AdvancePage.HoistScroller.Value);
                else
                    String.Format("HUBKRAFTMAXIMALDAN >= {0}", this.view.AdvancePage.HoistScroller.Value);
            }
            return condition;
        }

        protected string CombineGearsCriteria()
        {
            string condition = string.Empty;

            if (this.view.AdvancePage.cbGearsOption1.IsChecked)
                condition = "(LS_GETRIEBE <> 'N')";

            if (this.view.AdvancePage.cbGearsOption2.IsChecked)
            {
                if (!string.IsNullOrEmpty(condition))
                    condition = String.Format("{0} AND (KRIECHGETRIEBE <> 'N')", condition);
                else
                    condition = "(KRIECHGETRIEBE <> 'N')";
            }

            return condition;
        }

        protected string CombinePowerEngineCritera()
        {
            string condition = string.Empty;

            if (this.view.AdvancePage.dsEnginePower.ValueLeft > this.view.AdvancePage.dsEnginePower.MinimalValue)
            {
                var leftSide = this.view.AdvancePage.dsEnginePower.DisplayLeftValue;
                condition = String.Format("(NENNLEISTUNGKW >= {0} OR NENNLEISTUNGPS >= {1})", leftSide.Up, leftSide.Down);
            }

            if (this.view.AdvancePage.dsEnginePower.ValueRight < this.view.AdvancePage.dsEnginePower.MaximalValue)
            {
                var rightSide = this.view.AdvancePage.dsEnginePower.DisplayRightValue;
                if (!string.IsNullOrEmpty(condition))
                    condition = String.Format("{0} AND (NENNLEISTUNGKW <= {1} OR NENNLEISTUNGPS <= {2})", condition, rightSide.Up, rightSide.Down);
                else
                    condition = String.Format("(NENNLEISTUNGKW <= {0} OR NENNLEISTUNGPS <= {1})", rightSide.Up, rightSide.Down);
            }

            return condition;
        }

        protected string CombineYearsArrayIntoString(IEnumerable<string> array)
        {
            StringBuilder singleString = new StringBuilder();

            foreach (string item in array)
            {
                singleString = singleString.Append(item + ", ");
            }

            singleString = singleString.Remove(singleString.Length - 2, 2);

            return singleString.ToString();
        }

        protected string CombineBrandsArrayIntoString(IEnumerable<string> array)
        {
            StringBuilder singleString = new StringBuilder();

            foreach (string item in array)
            {
                singleString = singleString.Append("SCHLEPPERHERSTELLER like '" + item + "' OR ");
            }

            singleString = singleString.Remove(singleString.Length - 4, 4);

            return singleString.ToString();
        }
        #endregion
        #endregion
    }
}
