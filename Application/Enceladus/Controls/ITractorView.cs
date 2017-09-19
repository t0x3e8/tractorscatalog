using System;
using System.Collections.Generic;
using System.Text;
using Enceladus.UIToolbox;
using System.Drawing;

namespace Enceladus
{
    interface ITractorView
    {
        OverviewTractorPage OverviewPage { get; }
        DetailsITractorPage DetailsIPage { get; }
        DetailsIITractorPage DetailsIIPage { get; }
        DetailsIIITractorPage DetailsIIIPage { get; }
        ViewTractorPage ViewPage { get; }

        TractorLayout MainLayout { get; }
        event EventHandler SizeChanged;
        Size Size { get; }

        ComparisonTractorPage ComparisonPage { get; }
        GradientButton MainMenuButton { get; }
        GradientButton BackButton { get; }
        GradientButton SearchButton { get; }
        TractorStatus StatusBar { get; }

        NavigationIconButton FirstNaviButton { get; }
        NavigationIconButton PreviousNaviButton { get; }
        NavigationIconButton NextNaviButton { get; }
        NavigationIconButton LastNaviButton { get; }
        IconButton OpenPdfButton { get; }
        IconButton PrintButton { get; }
        IconButton BookmarkButton { get; }
        bool ProgressRun { get; set; }
        bool ProgressRunVisible { get; set; }
        int CurrentTractorIndex { get; set; }
        void Activate();

        GradientIconButton RemoveBookmarkButton { get; }
        GradientIconButton ViewTractorButton { get; }
        GradientIconButton CompareTractorsButton { get; }
        GradientIconButton CleanBookmarkListButton { get; }
    }
}
