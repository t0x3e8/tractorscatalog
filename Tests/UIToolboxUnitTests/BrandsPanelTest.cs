using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Enceladus.UIToolbox;

namespace UIToolboxUnitTests
{
    [TestClass]
    public class BrandsPanelTest
    {
        [TestMethod]
        public void CheckAllSelected()
        {
            BrandsPanel_Accessor pnl = new BrandsPanel_Accessor();
            pnl.Brands = new List<string>() { "AAA", "AAB", "AAC", "AAD", "AAE" };
            pnl.AdditionalOptions = new Dictionary<string, string>() { { "BBB", "BBB" }, { "CCC", "CCC" } };
            Assert.IsTrue(pnl.AllBrandsSelected);
            Assert.IsTrue(pnl.AllOptionsUnselected);

            foreach (var item in pnl.brandsData)
            {
                item.ItemClicked();
            }
            Assert.IsTrue(pnl.AllBrandsUnselected);

            pnl.brandsData[0].ItemClicked(); // select first
            Assert.IsFalse(pnl.AllBrandsUnselected);
            Assert.IsFalse(pnl.AllBrandsSelected);
        }
    }
}
