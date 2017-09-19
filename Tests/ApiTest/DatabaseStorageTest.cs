using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Enceladus.Api;
using Enceladus;
using System.Threading;

namespace ApiTest
{
    [TestClass]
    public class DatabaseStorageTest
    {
        public int allTractorNumber;
  
        [TestInitialize]
        public void TestSetup()
        {
            this.allTractorNumber = 16062;// UPDATE THIS FIELD WHEN A NEW DATABASE ARRIVES
        }

        [TestMethod]
        public void GetAllTractors()
        {
            IDatabaseStorage db = new DatabaseStorage();

            for (int i = 1; i <= this.allTractorNumber; i++)
            {
                Tractor tractor = db.Get(i);
                Assert.IsTrue(tractor != null);
            }            
        }

        [TestMethod]
        public void GetAllTractorsAsync()
        {
            IDatabaseStorage db = new DatabaseStorage();
            AutoResetEvent eventReset = new AutoResetEvent(false);

            for (int i = 1; i <= this.allTractorNumber; i++)
            {
                db.BeginGet(new AsyncCallback(delegate(IAsyncResult result)
                {
                    IDatabaseStorage dbStorage = (result.AsyncState as IDatabaseStorage);
                    Tractor tractor = dbStorage.EndGet(result);
                    
                    Assert.IsTrue(tractor != null, "ID " + i.ToString());
                    eventReset.Set();
                }), db, i);
                eventReset.WaitOne();
            }
        }
    }
}
