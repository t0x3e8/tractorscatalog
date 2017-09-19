using System;
using System.Collections.Generic;
using System.Text;

namespace Enceladus.UIToolbox
{
    public interface INavigationButton
    {
        NavigatioMode NavigationMode { get; set; }
    }

    public enum NavigatioMode
    {
        First, Previous, Next, Last
    }
}
