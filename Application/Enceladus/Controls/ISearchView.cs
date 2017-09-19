using System;
using System.Collections.Generic;
using System.Text;
using Enceladus.UIToolbox;

namespace Enceladus
{
    interface ISearchView
    {
        AdvanceSearchPage AdvancePage { get; }
        GeneralSearchPage GeneralPage { get; }

        SearchLayout MainLayout { get; }

        GradientButton  MainMenuButton { get; }
        GradientButton StartSearchButton { get; }

        void ShowWarning(SearchError error);
        void HideWarning(int seconds);
    }
}
