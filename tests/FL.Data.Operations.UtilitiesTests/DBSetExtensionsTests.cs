using Microsoft.VisualStudio.TestTools.UnitTesting;
using FL.Data.Operations.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using FL.Data.Operations.TestsData;
using System.Linq;

namespace FL.Data.Operations.Utilities.Tests
{
    [TestClass()]
    public class DBSetExtensionsTests
    {
        [TestMethod()]
        public void PageTest()
        {
            // # of Pages = 3 
            // # of items per page = 7
            var ds = FakeProducts.GetFakeProducts().Page(2, 7);
            Assert.IsTrue(ds.Count() == 7);
        }

        [TestMethod()]
        public void LastPageTest()
        {
            // # of Pages = 3 
            // # of items per page = 7
            var ds = FakeProducts.GetFakeProducts().Page(3, 7);
            Console.WriteLine(ds.Count());
            Assert.IsTrue(ds.Count() == 6);
        }
    }
}