using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;
using Enceladus.StringLibrary;
using Enceladus.Api;

namespace Enceladus
{
    class ProductInformationCommand : CommandBase
    {
        protected delegate void ToggleWindowCallback(bool state);

        public ProductInformationCommand() : base(true) { Logger.Instance.Log(LogType.Info, "ProductInformationCommand initialized"); }

        public override void Execute<T>(T state)
        {
            this.Execute();
        }

        public override void Execute()
        {
            Logger.Instance.Log(LogType.Info, "ProductInformationCommand.Execute");

            FileInfo fi = ResourcesFinder.ResourcePath(null, ResourceType.WerbungApplication);
            if (fi != null && fi.Exists)
            {
                try
                {
                    ProcessStartInfo psi = new ProcessStartInfo(fi.FullName);
                    Process.Start(psi);
                }
                catch (Exception ex)
                {
                    Logger.Instance.Log(LogType.Error, "ProductInformationCommand.Execute", ex.ToString());
                }
            }
            else
            {
                MessageBox.Show(ResourceReader.GetString("MsgNoDisc"), ResourceReader.GetString("MsgInformation"), MessageBoxButtons.OK, MessageBoxIcon.Information); 
            }
        }
    }
}
