using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Enceladus.Api;
using System.Diagnostics;

namespace UIToolboxUnitTests
{
    [TestClass]
    public class Database
    {
        [TestMethod]
        public void GetRow()
        {
            IDatabaseStorage db = new DatabaseStorage();
            db.DropIndex("SATZ_INDEX");
            TimeSpan ts1 = DateTime.Now.TimeOfDay;
            Tractor t1 = db.Get(1);
            TimeSpan ts2 = DateTime.Now.TimeOfDay;
            TimeSpan tsDiff1 = ts2.Subtract(ts1);
            
            db.CreateIndex("SATZ_INDEX", "SATZ");
            ts1 = DateTime.Now.TimeOfDay;
            Tractor t2 = db.Get(1);
            ts2 = DateTime.Now.TimeOfDay;
            TimeSpan tsDiff2 = ts2.Subtract(ts1);

            Assert.IsNotNull(t1);
            Assert.IsNotNull(t2);
        }
    }
}
