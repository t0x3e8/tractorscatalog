using Enceladus.Api;
using System.ComponentModel;
using System.Windows.Forms;
using Enceladus.StringLibrary;
using System.IO.IsolatedStorage;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;
using System.Drawing.Text;
using System.Reflection;
using System.Drawing;
using System.Collections.Generic;
using Enceladus.Properties;

namespace Enceladus
{
    sealed class ApplicationState
    {
        #region Fields, Properties, Events
        private static readonly string MarkedTractorsFileName = "collection.obj";
        private static readonly string DefaultFontFamilyName = "Trebuchet MS";
        private static readonly string FontExtension = "ttf";

        private static BindingList<TractorBase> markedTractorsCollection = new BindingList<TractorBase>();
        public static BindingList<TractorBase> MarkedTractorsCollection
        {
            get { return ApplicationState.markedTractorsCollection; }
            set { ApplicationState.markedTractorsCollection = value; }
        }
        
        private static PrivateFontCollection fontCollection = new PrivateFontCollection();
        public static PrivateFontCollection FontCollection
        {
            get { return ApplicationState.fontCollection; }
            set { ApplicationState.fontCollection = value; }
        }

        public static event EventHandler OnApplicationCancelClosing;

        public static SearchObserver SearchObserver { get; private set; }
        #endregion

        #region Methods
        public static void ApplicationStarting()
        {
            ApplicationState.InitializeFonts();
            ApplicationState.RestoreTractors();
            SearchObserver = new SearchObserver();
        }

        /// <summary>
        /// The method is called when application is getting closed. 
        /// </summary>
        /// <returns>The value determines whether the closing process should be cancel</returns>
        public static void ApplicationClosing()
        {
            bool isClosingCancelled = ApplicationState.SaveTractors();
            if (!isClosingCancelled)
            {
                ApplicationState.DisposeFonts();
            }
        }
        
        #region Bookmarks

        private static void InitializeFonts()
        {
            Logger.Instance.Log(LogType.Info, "InitializeFonts");

            bool isFontInstalled = ApplicationState.IsFontInstalled(DefaultFontFamilyName);
            Logger.Instance.Log(LogType.Info, "Font installed: " + (isFontInstalled ? "1" : "0"));
            if (!isFontInstalled)
            {
                try
                {
                    IDictionary<string, byte[]> resourceMap = ApplicationState.InitializeResourceMap();
                    ApplicationState.CopyRequiredFonts(resourceMap);
                    ApplicationState.LoadPrivateFonts(resourceMap.Keys);
                }
                catch (Exception ex)
                {
                    Logger.Instance.Log(LogType.Error, "Font could not be correctly initialized: " + ex.ToString());
                }
            }
        }

        private static void RestoreTractors()
        {
            Logger.Instance.Log(LogType.Info, "RestoreTractors");

            try
            {
                IsolatedStorageFileStream fs = new IsolatedStorageFileStream(ApplicationState.MarkedTractorsFileName, FileMode.Open, FileAccess.Read, FileShare.Inheritable);
                try
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    ApplicationState.MarkedTractorsCollection = (BindingList<TractorBase>)bf.Deserialize(fs);
                }
                finally
                {
                    if (fs != null)
                        fs.Close();
                }
            }
            catch (Exception ex)
            {
                ApplicationState.MarkedTractorsCollection = new BindingList<TractorBase>();
                Logger.Instance.Log(LogType.Error,"RestoreTractors", "Tractors could not be restored" + ex.ToString());
            }
        }

        private static void RemoveTractorsCollectionFromDisk()
        {
            Logger.Instance.Log(LogType.Info, "RemoveTractorsCollectionFromDisk");

            try
            {
                IsolatedStorageFileStream fs = new IsolatedStorageFileStream(ApplicationState.MarkedTractorsFileName, FileMode.Open, FileAccess.Write, FileShare.Inheritable);
                try
                {
                    fs.SetLength(0);
                }
                finally
                {
                    fs.Close();
                }
            }
            catch (Exception ex) 
            {
                Logger.Instance.Log(LogType.Error, "RemoveTractorsCollectionFromDisk", "Remove tractors failed: " + ex.ToString());
            }
        }

        private static void StoreTractors()
        {
            Logger.Instance.Log(LogType.Info, "StoreTractors");

            try
            {
                IsolatedStorageFileStream fs = new IsolatedStorageFileStream(ApplicationState.MarkedTractorsFileName, FileMode.OpenOrCreate, FileAccess.Write, FileShare.Inheritable);
                try
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    bf.Serialize(fs, ApplicationState.MarkedTractorsCollection);
                }
                finally
                {
                    if (fs != null)
                        fs.Close();
                }
            }
            catch (Exception ex)
            {
                Logger.Instance.Log(LogType.Error, "StoreTractors", "Marked tractors serialization failed: " + ex.ToString());
            }
        }

        /// <summary>
        /// Method pops up a question about saving bookmarks and if the answer is yes stores them on the disk. If cancel was pressed then the form is inform that the closing process is suspend.
        /// </summary>
        private static bool SaveTractors()
        {
            if (ApplicationState.MarkedTractorsCollection.Count > 0)
            {
                DialogResult dr = MessageBox.Show(ResourceReader.GetString("MsgSaveMarkedTractors"), ResourceReader.GetString("MsgQuestion"), MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                if (dr == DialogResult.Yes)
                {
                    ApplicationState.StoreTractors();
                }
                else if (dr == DialogResult.No)
                {
                    ApplicationState.RemoveTractorsCollectionFromDisk();
                }
                else
                {
                    if (ApplicationState.OnApplicationCancelClosing != null)
                        ApplicationState.OnApplicationCancelClosing(typeof(ApplicationState), new EventArgs());

                    return true;
                }
            }
            return false;
        }
        #endregion

        #region Fonts
        public static FontFamily GetFont()
        {
            if (ApplicationState.IsFontInstalled(ApplicationState.DefaultFontFamilyName))
                return new FontFamily(ApplicationState.DefaultFontFamilyName);
            else if (FontCollection.Families.Length > 0)
                return FontCollection.Families[0];
            else
                return FontFamily.GenericSansSerif;
        }

        private static void LoadPrivateFonts(ICollection<string> resourcesName)
        {
            using (IsolatedStorageFile isolatedStorage = IsolatedStorageFile.GetUserStoreForDomain())
            {
                foreach (string resourceName in resourcesName)
                {
                    string resourceWithExtension = Path.ChangeExtension(resourceName, ApplicationState.FontExtension);
                    using (IsolatedStorageFileStream fontStream = new IsolatedStorageFileStream(resourceWithExtension, FileMode.Open))
                    {
                        var buffer = new byte[fontStream.Length];
                        fontStream.Read(buffer, 0, buffer.Length);
                        var handle = System.Runtime.InteropServices.GCHandle.Alloc(buffer, System.Runtime.InteropServices.GCHandleType.Pinned);
                        try
                        {
                            var ptr = System.Runtime.InteropServices.Marshal.UnsafeAddrOfPinnedArrayElement(buffer, 0);
                            FontCollection.AddMemoryFont(ptr, buffer.Length);
                        }
                        finally
                        {
                            // don't forget to unpin the array!
                            handle.Free();
                        }
                    }
                }
            }

            DirectoryInfo applicationFolder = new DirectoryInfo(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location));
            foreach (FileInfo fontFile in applicationFolder.GetFiles("*.ttf"))
                FontCollection.AddFontFile(fontFile.FullName);
        }
        
        private static void DisposeFonts()
        {
            foreach (FontFamily fontFamily in ApplicationState.FontCollection.Families)
                fontFamily.Dispose();
        }

        private static void CopyRequiredFonts(IDictionary<string, byte[]> resourceMap)
        {
            using (IsolatedStorageFile isolatedStorage = IsolatedStorageFile.GetUserStoreForDomain())
            {
                List<string> installedFonts = new List<string>(isolatedStorage.GetFileNames("*." + ApplicationState.FontExtension));

                foreach (KeyValuePair<string, byte[]> resourceEntry in resourceMap)
                {
                    string fontWithExtension = Path.ChangeExtension(resourceEntry.Key, ApplicationState.FontExtension);

                    if (!installedFonts.Contains(fontWithExtension))
                    {
                        using (IsolatedStorageFileStream fontStream = new IsolatedStorageFileStream(fontWithExtension, FileMode.Create, FileAccess.Write, FileShare.Inheritable))
                        {
                            fontStream.Write(resourceEntry.Value, 0, resourceEntry.Value.Length);
                            fontStream.Flush();
                            fontStream.Close();
                        }
                    }
                }
            }
        }

        private static IDictionary<string, byte[]> InitializeResourceMap()
        {
            IDictionary<string, byte[]> resourceMap = new Dictionary<string, byte[]>(4);
            resourceMap.Add("trebuc", Resources.trebuc);
            resourceMap.Add("trebucbd", Resources.trebucbd);
            resourceMap.Add("trebucbi", Resources.trebucbi);
            resourceMap.Add("trebucit", Resources.trebucit);

            return resourceMap;

        }

        private static bool IsFontInstalled(string defaultFontFamilyName)
        {
            foreach (FontFamily fontFamily in FontFamily.Families)
            {
                if (fontFamily.GetName(0).Equals(defaultFontFamilyName))
                {
                    return true;
                }
            }
            return false;
        }
        #endregion

        #endregion
    }
}
