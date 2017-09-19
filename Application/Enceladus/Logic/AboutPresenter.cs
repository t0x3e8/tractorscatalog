using System;
using System.Collections.Generic;
using System.Text;
using Enceladus.UIToolbox;
using Enceladus.Api;

namespace Enceladus
{
    class AboutPresenter
    {
        #region Fields and Properties
        protected readonly IAboutView view;
        #endregion

        #region Constructors
        public AboutPresenter(IAboutView view)
        {
            this.view = view;
            Logger.Instance.Log(LogType.Info, "AboutPresenter.ctor");
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
            this.view.RightBrandBox.Font = Defines.NormalBoldFont;
            this.view.RightBrandBox.ForeColor = Defines.CarrotColor;

            this.view.RightsBox.Font = Defines.NormalBoldFont;
            this.view.RightsBox.ForeColor = Defines.CabbageColor;

            this.view.AuthorBox.Font = Defines.NormalBoldFont;
            this.view.AuthorBox.ForeColor = Defines.CarrotColor;
        }
        #endregion
    }
}
