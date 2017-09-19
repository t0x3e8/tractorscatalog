using System;
using System.Collections.Generic;
using System.Text;
using Enceladus.Api;

namespace Enceladus
{
    class SingleTractorCommand : LayoutCommandBase
    {
        private static TractorLayout layout= null;

        public SingleTractorCommand(MainWindow window)
            : base(window) { Logger.Instance.Log(LogType.Info, "SingleTractorCommand initialized"); }

        public override void Execute<T>(T state)
        {

            int currentTractorIndex = 0;
            if (state != null)
                int.TryParse(state.ToString(), out currentTractorIndex);

            Logger.Instance.Log(LogType.Info, "SingleTractorCommand.Exevute", currentTractorIndex.ToString());

            if (layout == null)
                layout = new TractorLayout(this.Window);
            
            if (currentTractorIndex != 0)
                layout.CurrentTractorIndex = currentTractorIndex;

            this.Window.ActiveLayout = layout;
        }

        public override void Execute()
        {
            this.Execute<object>(null);
        }
    }
}
