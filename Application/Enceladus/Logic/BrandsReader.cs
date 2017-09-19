using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.IO;
using System.Xml;
using Enceladus.Api;
using Enceladus.Logic;

namespace Enceladus
{
    sealed class BrandsReader
    {
        #region Fields & Properties
        private readonly IList<Brand> brandsCollection = new List<Brand>();
        public IList<Brand> BrandsCollection
        {
            get { return this.brandsCollection; }
        }

        private static BrandsReader reader; 
        #endregion

        #region Constructor
        private BrandsReader() { }

        public static BrandsReader Instance
        {
            get
            {
                if (reader == null)
                    reader = new BrandsReader();

                return reader;
            }
        }
        #endregion

        #region Methods
        public void InitializeCollection()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            Stream stream = assembly.GetManifestResourceStream("Enceladus.brands.xml");
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(stream);

            XmlNodeList brandNodes = xmlDocument.SelectNodes("/Brands/Brand");
            foreach (XmlNode brandNode in brandNodes)
            {
                Brand brand = new Brand();
                brand.Producer = brandNode.SelectSingleNode("Producer").InnerText.Trim();
                brand.FirstLetter = brand.Producer.Substring(0,1);
                brand.Company = brandNode.SelectSingleNode("Company").InnerText.Trim();
                brand.Address = brandNode.SelectSingleNode("Street").InnerText.Trim();
                brand.CodeAndCity = brandNode.SelectSingleNode("CodeCity").InnerText.Trim();
                brand.Phone = brandNode.SelectSingleNode("Phone").InnerText.Trim();
                brand.Fax = brandNode.SelectSingleNode("Fax").InnerText.Trim();
                brand.Internet = brandNode.SelectSingleNode("Internet").InnerText.Trim();
                brand.Internet2 = brandNode.SelectSingleNode("Internet2").InnerText.Trim();
                this.brandsCollection.Add(brand);
            }    
        }

        public IList<BrandsGroup> BuildBrandsGroup(int groupSize)
        {
            IList<BrandsGroup> brandsGroups = new List<BrandsGroup>();
            Brand [] tempArray = new Brand[groupSize];
            int tempIndexer = 0;

            for (int i = 1; i <= this.brandsCollection.Count; i++)
            {
                tempArray[tempIndexer++] = this.brandsCollection[i - 1];

                if (((i % groupSize) == 0 || (i == this.brandsCollection.Count)) && (i != 0))
                {
                    string firstLetter = tempArray[0].Producer.Substring(0, 1);
                    string lastLetter = tempArray[tempIndexer - 1].Producer.Substring(0, 1);
                    BrandsGroup brandsGroup = new BrandsGroup();
                    brandsGroup.GroupName = string.Format("{0}-{1}", firstLetter, lastLetter);
                    brandsGroup.Brands = tempArray;

                    tempArray = new Brand[groupSize];
                    tempIndexer = 0;
                    
                    brandsGroups.Add(brandsGroup);
                }
            }

            return brandsGroups;
        }
        #endregion
    }
}
