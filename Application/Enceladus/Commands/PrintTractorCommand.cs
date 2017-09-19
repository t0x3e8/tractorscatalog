using System;
using System.Collections.Generic;
using System.Text;
using Enceladus.Controls;
using Enceladus.Api;

namespace Enceladus
{
    class PrintTractorCommand : CommandBase
    {
        public PrintTractorCommand() { Logger.Instance.Log(LogType.Info, "PrintTractorCommand initialized"); }

        public override void Execute()
        {
            throw new NotSupportedException("This method is not supported");
        }

        public override void Execute<T>(T singleTractorInstance)
        {
            Tractor tractor = singleTractorInstance as Tractor;

            Logger.Instance.Log(LogType.Info, "PrintTractorCommand.Execute", (tractor == null) ? "null" : tractor.ToString());

            if (tractor != null)
            {
                SingleTractorPrintDocument prnTractor = new SingleTractorPrintDocument();
                prnTractor.Tractor = tractor;
                prnTractor.ShowPrint();
            }
        }
    }
}
