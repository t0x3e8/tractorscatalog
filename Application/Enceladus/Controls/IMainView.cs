using System;
using System.Collections.Generic;
using System.Text;
using Enceladus.UIToolbox;

namespace Enceladus
{
    interface IMainView
    {
        GradientButton SearchButton { get; }
        GradientButton ShowTractorButton { get; }
        GradientButton VendorsButton { get; }
        GradientButton ProductInformationButton { get; }
        GradientButton AboutButton { get; }
    }
}
