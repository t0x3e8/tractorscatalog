using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Enceladus.Api;
using System.Drawing.Text;
using Enceladus.Properties;
using System.IO;
using Enceladus.UIToolbox;
using System.Drawing;

namespace Enceladus
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string [] args)
        {
            if (args.Length == 1 && args[0].ToUpper().Equals("DEBUG"))
            {
                Logger.Instance.Enabled = true;
                Logger.Instance.Log(LogType.Info, "Application started");
                Logger.Instance.Log(LogType.Info, "Logger: 1");
                Logger.Instance.Log(LogType.Info, GlobalSettings.DatabaseFilePath + ": " + (File.Exists(GlobalSettings.DatabaseFilePath) ? "1" : "0"));
                Logger.Instance.Log(LogType.Info, GlobalSettings.HTTPDataDirectoryPath + ": " + (Directory.Exists(GlobalSettings.HTTPDataDirectoryPath) ? "1" : "0"));
                Logger.Instance.Log(LogType.Info, GlobalSettings.ApplicationDataDirectoryPath + ": " + (Directory.Exists(GlobalSettings.ApplicationDataDirectoryPath) ? "1" : "0"));
            }

            Application.EnableVisualStyles();            
            Application.SetCompatibleTextRenderingDefault(true);

            MainWindow mainWindow = new MainWindow();
            
            ApplicationState.ApplicationStarting();
            Defines.DefaultFontFamily = ApplicationState.GetFont();
            Logger.Instance.Log(LogType.Info, "Default font: " + Defines.DefaultFontFamily);

            BrandsReader.Instance.InitializeCollection();
            
            ICommand command = new MainCommand(mainWindow);
            command.Execute();

            Application.Run(mainWindow);
        }
    }
}
