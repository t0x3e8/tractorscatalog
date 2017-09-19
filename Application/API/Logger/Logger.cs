using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Enceladus.Api
{
    public class Logger : ILogger
    {
        public bool Enabled { get; set; }

        private bool isInitialized;
        private static readonly object WriteFileLock = new object();

        private static Logger instance;
        private static readonly object instanceLock = new object();
        public static Logger Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new Logger(false);
                    }
                    return instance;
                }
            }
        }

        private Logger(bool isInitialized)
        {
            this.isInitialized = isInitialized;
        }

        public void Log(LogType logtype, string stepName)
        {
            this.Log(logtype, stepName, string.Empty);
        }

        public void Log(LogType logtype, string stepName, string message)
        {
            if (this.Enabled)
            {
                if (isInitialized == false)
                {
                    try
                    {
                        DirectoryInfo logFileDirectory = new DirectoryInfo(Path.GetDirectoryName(GlobalSettings.LogFile));
                        if (!logFileDirectory.Exists)
                            logFileDirectory.Create();
                    }
                    catch (Exception ex)
                    {
                        this.WriteOnConsole(LogType.Error, "The log directory didn't exist and could not be created", ex.ToString());
                        this.Enabled = false;
                    }

                    if (!this.Enabled)
                    {
                        this.Log(LogType.Info, "================================" + DateTime.Now.ToShortDateString() + ", " + DateTime.Now.ToShortTimeString());
                        isInitialized = true;
                    }
                }

                lock (WriteFileLock)
                {
                    //this.WriteOnConsole(logtype, stepName, message);
                    this.WriteToFile(logtype, stepName, message);
                }
            }
        }

        private void WriteToFile(LogType logtype, string stepName, string message)
        {
            using (var file = File.AppendText(GlobalSettings.LogFile))
            {
                file.WriteLine("{0}: {1}, {2}", logtype, stepName, message);
                file.Flush();
            }
        }

        private void WriteOnConsole(LogType logtype, string stepName, string message)
        {
            Console.WriteLine("{0}: {1}, {2}", logtype, stepName, message);
        }
    }
}
