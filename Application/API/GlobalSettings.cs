using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Enceladus.Api
{
    public static class GlobalSettings
    {
        public readonly static string PdfDirectoryPath = @"de\pdf";
        public readonly static string PicturecDirectoryPath = @"pictures";
        public readonly static string AdvertisementDirectoryPath = @"de\demo\Produktinformation.pdf";
        public readonly static string ApplicationDataDirectoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "Profi\\Schleppermarkt2017\\");
        public readonly static string DatabaseFilePath = Path.Combine(ApplicationDataDirectoryPath, "Database\\db.sdf");
        public readonly static string HTTPDataDirectoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Schleppermarkt\\HTTP");
        public readonly static string LogFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Schleppermarkt\\HTTP\\" + Path.GetRandomFileName() + ".log" );
    }
}
