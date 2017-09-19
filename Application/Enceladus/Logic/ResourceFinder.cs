using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;
using Enceladus.Api;

namespace Enceladus
{
    public class ResourcesFinder
    {
        #region methods
        public static bool ResourceExist(string resourceName, ResourceType type)
        {
            Logger.Instance.Log(LogType.Info, "ResourceExist", resourceName + " " + type.ToString());

            bool result = false;
            try
            {
                switch (type)
                {
                    case ResourceType.Picture:
                        FileInfo fiPicture = ResourcesFinder.GetPicture(resourceName);
                        if (fiPicture != null)
                            result = fiPicture.Exists;
                        else
                            result = false;
                        break;
                    case ResourceType.PDF:
                        FileInfo fiPdf = ResourcesFinder.GetPDF(resourceName);
                        if (fiPdf != null)
                            result = fiPdf.Exists;
                        else
                            result = false;
                        break;
                    case ResourceType.WerbungApplication:
                        FileInfo fiWerbungApp = ResourcesFinder.GetWAdvertisementPath();
                        if (fiWerbungApp != null)
                            result = fiWerbungApp.Exists;
                        else
                            result = false;
                        break;
                }
            }
            catch (Exception ex)
            {
                Logger.Instance.Log(LogType.Error, "ResourceExist", (string.Format("An exception occurs on searching resource '{0}'. {1}", resourceName, ex.ToString())));
            }

            return result;
        }

        public static FileInfo ResourcePath(string resourceName, ResourceType type)
        {
            Logger.Instance.Log(LogType.Info, "ResourcePath", resourceName + " " + type.ToString());
         
            FileInfo fileObject = null;
            try
            {
                switch (type)
                {
                    case ResourceType.Picture:
                        fileObject = ResourcesFinder.GetPicture(resourceName);
                        break;
                    case ResourceType.PDF:
                        fileObject = ResourcesFinder.GetPDF(resourceName);
                        break;
                    case ResourceType.WerbungApplication:
                        fileObject = ResourcesFinder.GetWAdvertisementPath();
                        break;
                }
            }
            catch (Exception ex)
            {
                Logger.Instance.Log(LogType.Error, "ResourcePath", (string.Format("An exception occurs on searching resource '{0}'. {1}", resourceName, ex.ToString())));
            }

            return fileObject;
        }
        #endregion

        #region Sub methods
        protected static FileInfo GetPicture(string resourceName)
        {
            FileInfo fileObject = null;
            foreach (DriveInfo drive in DriveInfo.GetDrives())
            {
                if (drive != null && drive.DriveType == DriveType.CDRom)
                {
                    string directory = Path.Combine(drive.Name, GlobalSettings.PicturecDirectoryPath);
                    string file = Path.Combine(directory, resourceName);

                    FileInfo fi = new FileInfo(file);

                    if (fi.Exists)
                    {
                        fileObject = fi;
                        break;
                    }
                }
            }
            return fileObject;
        }

        protected static FileInfo GetPDF(string resourceName)
        {
            FileInfo fileObject = null;
            foreach (DriveInfo drive in DriveInfo.GetDrives())
            {
                if (drive != null && drive.DriveType == DriveType.CDRom)
                {
                    string directory = Path.Combine(drive.Name, GlobalSettings.PdfDirectoryPath);
                    string file = Path.Combine(directory, resourceName + ".pdf");

                    FileInfo fi = new FileInfo(file);

                    if (fi.Exists)
                    {
                        fileObject = fi;
                        break;
                    }
                }
            }
            return fileObject;
        }

        protected static FileInfo GetWAdvertisementPath()
        {
            FileInfo fileObject = null;
            foreach (DriveInfo drive in DriveInfo.GetDrives())
            {
                if (drive != null && drive.DriveType == DriveType.CDRom)
                {
                    string file = Path.Combine(drive.Name, GlobalSettings.AdvertisementDirectoryPath);

                    FileInfo fi = new FileInfo(file);

                    if (fi.Exists)
                    {
                        fileObject = fi;
                        break;
                    }
                }
            }
            return fileObject;
        }
        #endregion
    }

    public enum ResourceType
    {
        Picture, PDF, WerbungApplication
    }
}
