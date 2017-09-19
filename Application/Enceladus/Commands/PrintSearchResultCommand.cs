using System;
using System.Collections.Generic;
using System.Text;
using Enceladus.Api;
using Enceladus.Controls;

namespace Enceladus
{
    class PrintSearchResultCommand  : CommandBase
    {
        public PrintSearchResultCommand() { Logger.Instance.Log(LogType.Info, "PrintSearchResultCommand initialized"); }

        public override void Execute()
        {
            throw new NotSupportedException("This method is not supported");
        }

        public override void Execute<T>(T singleTractorInstance)
        {
            Logger.Instance.Log(LogType.Info, "PrintSearchResultCommand.Execute");

            IList<TractorSearchResult> tractors = singleTractorInstance as IList<TractorSearchResult>;
            if (tractors != null && tractors.Count > 0)
            {
                SearchResultPrintDocument prnTractor = new SearchResultPrintDocument();
                prnTractor.Tractors = tractors;
                prnTractor.ShowPrint();
            }
        }
    }
}
