using System;
using System.Collections.Generic;
using System.Text;
using Enceladus.UIToolbox;
using System.Windows.Forms;
using System.Drawing;

namespace Enceladus
{
    interface ISearchResultView
    {
        GradientButton MainMenuButton { get; }
        GradientButton SearchButton { get; }
        GradientButton ShowTractorButton { get; }
        string SearchCriteria { get; }

        SearchResultLayout MainLayout { get; }
        DataGridView DataGrid { get; }
        InputBoxLabel ShowAllLabelButton { get;}
        InputBoxLabel SearchResultLabel { get; }
        ListBox SelectedTractors { get; }

        GradientIconButton RemoveBookmarkButton { get; }
        GradientIconButton CompareTractorsButton { get; }
        GradientIconButton PrintSearchResultButton { get; }
        GradientIconButton CleanBookmarkListButton { get; }

        //bool RunProgressBar { get; set; }
        //bool RunProgressBarVisibility { get; set; }

        Size Size { get; }
    }
}
