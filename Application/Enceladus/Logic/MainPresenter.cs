using System;
using System.Collections.Generic;
using System.Text;
using Enceladus.Api;

namespace Enceladus
{
    class MainPresenter
    {
        #region Fields
        protected IMainView view;
        #endregion

        #region Constructors
        public MainPresenter(IMainView view)
        {
            this.view = view;
            Logger.Instance.Log(LogType.Info, "MainPresenter.ctor");
        }
        #endregion

        #region Methods
        internal void InitializeCommands(MainWindow window)
        {            
            this.view.SearchButton.Command = new SearchCommand(window);
            this.view.ShowTractorButton.Command = new SingleTractorCommand(window);
            this.view.ProductInformationButton.Command = new ProductInformationCommand();
            this.view.VendorsButton.Command = new BrandsCommand(window);
            this.view.AboutButton.Command = new AboutCommand(window);
        }
        #endregion
    }
}
