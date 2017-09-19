using System;
using System.Collections.Generic;
using System.Text;
using Enceladus.Api;

namespace Enceladus
{
    class SearchResultCommand : LayoutCommandBase
    {
        private static SearchResultLayout layout = null;

        public SearchResultCommand(MainWindow window)
            : base(window) { Logger.Instance.Log(LogType.Info, "SearchResultCommand initialized"); }

        public override void Execute<T>(T state)
        {
            Logger.Instance.Log(LogType.Info, "SearchResultCommand.Execute");

            try
            {
                if (layout == null)
                    layout = new SearchResultLayout(this.Window);

                layout.SearchCriteria = state as string;
            }
            catch (Exception ex)
            {
                Logger.Instance.Log(LogType.Error, "SearchResultCommand.Execute", ex.ToString());
            }

            this.Window.ActiveLayout = layout;
        }

        public override void Execute()
        {
            this.Execute<object>(null);
        }

    }
}
