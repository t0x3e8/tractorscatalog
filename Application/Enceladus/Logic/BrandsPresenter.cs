using System;
using System.Collections.Generic;
using System.Text;
using Enceladus.UIToolbox;
using Enceladus.Api;

namespace Enceladus
{
    class BrandsPresenter
    {
        #region Fields and Properties
        protected readonly IBrandsView view;
        #endregion

        #region Constructors
        public BrandsPresenter(IBrandsView view)
        {
            this.view = view;
            Logger.Instance.Log(LogType.Info, "BrandsPresenter.ctor");
        }
        #endregion

        #region Methods
        public void InitializeCommands(MainWindow window)
        {
            // Main navigation button 
            this.view.MainMenuButton.Command = new MainCommand(window);
        }

        public void InitializeControls()
        {
            this.view.TabNavigator.SelectedTabChanged += SelectedTabChanged;
            this.InitializeTabs();
        }
        
        protected void InitializeTabs()
        {
            this.view.MainLayout.SuspendLayout();
            this.view.TabNavigator.ClearTabs();

            IList<BrandsGroup> brandsGroups = BrandsReader.Instance.BuildBrandsGroup(6);
            foreach (var brandGroup in brandsGroups)
            {
                Tab newTab = new Tab();
                newTab.Content = brandGroup.GroupName;
                newTab.Tag = brandGroup.Brands;
                this.view.TabNavigator.AddTab(newTab);
            }

            this.view.TabNavigator.SelectedIndex = 0;
            this.view.MainLayout.ResumeLayout();
        }

        private void SelectedTabChanged(object sender, SelectionChangedEventArgs e)
        {
            this.view.MainLayout.SuspendLayout();

            if (e.ActiveSelection != null && e.ActiveSelection.Tag != null)
            {
                this.view.VisibleBrands = e.ActiveSelection.Tag as IList<Brand>;
            }

            this.view.MainLayout.ResumeLayout();
        }
        #endregion
    }
}
