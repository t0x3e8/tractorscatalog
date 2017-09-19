using System;
using System.Collections.Generic;
using System.Text;
using Enceladus.UIToolbox;
using Enceladus.Api;

namespace Enceladus
{
    interface IBrandsView
    {
        GradientButton MainMenuButton { get; }
        IList<Brand> VisibleBrands { get; set; }
        BrandsLayout MainLayout { get; }
        TabsBar TabNavigator { get; }
    }
}
