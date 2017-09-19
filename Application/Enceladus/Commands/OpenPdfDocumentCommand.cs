using System;
using System.Collections.Generic;
using System.Text;
using Enceladus.Api;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;
using Enceladus.StringLibrary;

namespace Enceladus
{
    class OpenPdfDocumentCommand : CommandBase
    {
        public OpenPdfDocumentCommand() { Logger.Instance.Log(LogType.Info, "OpenPdfDocumentCommand initialized"); }

        public override void Execute<T>(T objectToProcess)
        {
            Tractor tractor = (objectToProcess as Tractor);

            Logger.Instance.Log(LogType.Info, "OpenPdfDocumentCommand.Execute", (tractor == null) ? "null":tractor.ToString());

            if (tractor != null)
            {
                // this supports a case where there are two pdf files for profi and top agrar, both will be open
                if (!string.IsNullOrEmpty(tractor.Seitencodeprofi))
                    this.OpenPdf(tractor.Seitencodeprofi);

                if (!string.IsNullOrEmpty(tractor.Seitencodetop))
                    this.OpenPdf(tractor.Seitencodetop);
            }
        }

        private void OpenPdf(string resourceName)
        {
            if (ResourcesFinder.ResourceExist(resourceName, ResourceType.PDF))
            {
                try
                {
                    FileInfo resourceFileInfo = ResourcesFinder.ResourcePath(resourceName, ResourceType.PDF);
                    ProcessStartInfo psi = new ProcessStartInfo(resourceFileInfo.FullName);
                    Process.Start(psi);
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ResourceReader.GetString("MsgPdfViewerNotInstalled"), ResourceReader.GetString("MsgError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.Instance.Log(LogType.Info, "OpenPdf", ex.ToString());
                }
            }
            else
            {
                MessageBox.Show(ResourceReader.GetString("MsgPdfViewNeedsDVD"), ResourceReader.GetString("MsgInformation"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public override void Execute()
        {
            throw new NotSupportedException("This method is not supported");

        }
    }
}
