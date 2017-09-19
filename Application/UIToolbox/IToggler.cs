using System;
using System.Collections.Generic;
using System.Text;

namespace Enceladus.UIToolbox
{
    public interface IToggler
    {
        bool State { get; set; }
        event EventHandler SelectAll;
        event EventHandler DeselectAll;
    }
}
