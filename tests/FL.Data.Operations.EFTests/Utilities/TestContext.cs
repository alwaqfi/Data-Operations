using FL.Data.Operations.EF.Tests.Utilities;
using FL.Data.Operations.TestsData;
using FL.Data.Operations.TestsData.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FL.Data.Operations.EF.Tests.Utilities
{
    public class TestContext : DbContext
    {
        public TestContext()
        {
            Products = new TestDbSet<Product>();
            Products.AddRange(FakeProducts.GetFakeProducts());
        }
        public TestDbSet<Product> Products { get; set; }
    }     
}
