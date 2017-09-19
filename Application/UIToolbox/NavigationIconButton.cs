using System;
using System.Collections.Generic;
using System.Text;

namespace Enceladus.UIToolbox
{
    public class NavigationIconButton : IconButton, INavigationButton
    {
        #region Fields and Propeties
        public NavigatioMode NavigationMode
        {
            get;
            set;
        }
        #endregion
    }
}
