using System;
using System.Collections.Generic;
using System.Text;

namespace Enceladus.Api
{
    public interface ILogger
    {
        void Log(LogType logtype, string stepName);
        void Log(LogType logtype, string stepName, string message);
    }

    public enum LogType
    {
        Error, Info
    }
}
