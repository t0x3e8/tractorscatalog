using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Reflection;
using System.IO;
using Enceladus.Api;

namespace Enceladus
{
    public class ConstantsReader
    {
        #region Fields and Properties
        private static XmlDocument xmlDocument;
        #endregion

        #region Constructors
        public ConstantsReader()
        {
            Initialize();
            Logger.Instance.Log(LogType.Info, "ConstantsReader.ctor");
        }        
        #endregion

        #region Methods
        private static void Initialize()
        {
            if (xmlDocument == null)
            {
                try
                {
                    Assembly assembly = Assembly.GetExecutingAssembly();
                    Stream stream = assembly.GetManifestResourceStream("Enceladus.constants.xml");

                    xmlDocument = new XmlDocument();
                    xmlDocument.Load(stream);
                }
                catch (Exception ex)
                {
                    Logger.Instance.Log(LogType.Info, "ConstantsReader.Initialize", "constants could not be loaded: " + ex.ToString());
                }
            }
        }

        public List<string> GetBrands()
        {
            List<string> brands = new List<string>();
            XmlNodeList nodes = xmlDocument.SelectNodes("/SearchCriteria/Brands/Brand");
            foreach (XmlNode node in nodes)
            {
                brands.Add(node.InnerText);
            }            

            return brands;
        }

        public MinMaxRange GetPSRange()
        {
            MinMaxRange range = new MinMaxRange();
            XmlNodeList nodes = xmlDocument.SelectNodes("/SearchCriteria/KW_PS");
            if (nodes.Count == 1)
            {
                range.Min = int.Parse(nodes[0].Attributes["Min_PS"].InnerText);
                range.Max = int.Parse(nodes[0].Attributes["Max_PS"].InnerText);
            }

            return range;
        }
        #endregion

        internal MinMaxRange GetHoistRange()
        {
            MinMaxRange range = new MinMaxRange();
            XmlNodeList nodes = xmlDocument.SelectNodes("/SearchCriteria/Hoist");
            if (nodes.Count == 1)
            {
                range.Min = int.Parse(nodes[0].Attributes["Min"].InnerText);
                range.Max = int.Parse(nodes[0].Attributes["Max"].InnerText);
            }

            return range;
        }

        internal MinMaxRange GetWeightRange()
        {
            MinMaxRange range = new MinMaxRange();
            XmlNodeList nodes = xmlDocument.SelectNodes("/SearchCriteria/Weight");
            if (nodes.Count == 1)
            {
                range.Min = int.Parse(nodes[0].Attributes["Min"].InnerText);
                range.Max = int.Parse(nodes[0].Attributes["Max"].InnerText);
            }

            return range;
        }

        internal MinMaxRange GetEmptyWeightRange()
        {
            MinMaxRange range = new MinMaxRange();
            XmlNodeList nodes = xmlDocument.SelectNodes("/SearchCriteria/Capacity");
            if (nodes.Count == 1)
            {
                range.Min = int.Parse(nodes[0].Attributes["Min"].InnerText);
                range.Max = int.Parse(nodes[0].Attributes["Max"].InnerText);
            }

            return range;
        }

        internal MinMaxRange GetTropicRange()
        {
            MinMaxRange range = new MinMaxRange();
            XmlNodeList nodes = xmlDocument.SelectNodes("/SearchCriteria/Tropic");
            if (nodes.Count == 1)
            {
                range.Min = int.Parse(nodes[0].Attributes["Min"].InnerText);
                range.Max = int.Parse(nodes[0].Attributes["Max"].InnerText);
            }

            return range;
        }

        internal MinMaxRange GetHeightRange()
        {
            MinMaxRange range = new MinMaxRange();
            XmlNodeList nodes = xmlDocument.SelectNodes("/SearchCriteria/Height");
            if (nodes.Count == 1)
            {
                range.Min = int.Parse(nodes[0].Attributes["Min"].InnerText);
                range.Max = int.Parse(nodes[0].Attributes["Max"].InnerText);
            }

            return range;
        }

        internal MinMaxRange GetPriceRange()
        {
            MinMaxRange range = new MinMaxRange();
            XmlNodeList nodes = xmlDocument.SelectNodes("/SearchCriteria/Price");
            if (nodes.Count == 1)
            {
                range.Min = int.Parse(nodes[0].Attributes["Min"].InnerText);
                range.Max = int.Parse(nodes[0].Attributes["Max"].InnerText);
            }

            return range;
        }

        public int GetTotalTractorsNumber()
        {
            int max = 0;
            XmlNodeList nodes = xmlDocument.SelectNodes("/SearchCriteria/RecordNumber");
            if (nodes.Count == 1)
            {
                max = int.Parse(nodes[0].Attributes["Max"].InnerText);
            }

            return max;
        }
    }

    public struct MinMaxRange
    {
        public int Min, Max;
        public MinMaxRange(int min, int max)
        {
            this.Min = min;
            this.Max = max;

        }
    }
}
