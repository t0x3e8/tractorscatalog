using System;
using System.Collections.Generic;
using System.Text;
using Enceladus.Api;

namespace Enceladus
{
    class BrandsCommand : LayoutCommandBase
    {
        private static BrandsLayout layout = null;

        public BrandsCommand(MainWindow window)
            : base(window)
        {
            Logger.Instance.Log(LogType.Info, "BrandsCommand initialized"); 
        }

        public override void Execute<T>(T state)
        {
            this.Execute();
        }

        public override void Execute()
        {
            Logger.Instance.Log(LogType.Info, "BrandsCommand.Execute"); 

            if (layout == null)
                layout = new BrandsLayout(this.Window);

            this.Window.ActiveLayout = layout;
        }
    }
}
