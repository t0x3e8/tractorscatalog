using System;
using System.Collections.Generic;
using System.Text;

namespace Enceladus
{
    enum SearchPageType
    {
        Advance, General
    }

    enum SearchError
    {
        AllYearsUnselected,
        AllBrandsUnselected,
        NoSearchCriterias,
        None
    }

    enum WaitingWindowStatus
    {
        Open, Closed
    }
}
