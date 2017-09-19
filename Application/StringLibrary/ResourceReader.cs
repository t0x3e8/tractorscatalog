using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace Enceladus.StringLibrary
{
    public static class ResourceReader
    {
        #region Fields
        private static CultureInfo culture = new CultureInfo("de-DE");
        #endregion

        #region Methods
        public static void SetCulture(CultureInfo culture)
        {
            ResourceReader.culture = culture;
        }
        
        public static string GetString(string name)
        {
            string localizedString = Strings.ResourceManager.GetString(name, ResourceReader.culture);

            if (localizedString == null)
                throw new NullReferenceException("There is no string like " + name);

            return localizedString;
        }
        #endregion
    }
}
