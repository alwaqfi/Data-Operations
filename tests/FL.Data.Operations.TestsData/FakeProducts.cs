
using FL.Data.Operations.TestsData.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FL.Data.Operations.TestsData
{
    public class FakeProducts
    {
        public static IQueryable<Product> GetFakeProducts()
        {
            return new List<Product>
            {
                new Product {   ProductID = 1, ProductName = "Product 1", Discontinued = false,
                      UnitsInStock = 200,
                    QuantityPerUnit ="3/1 Items" },
                 new Product {  ProductID = 2, ProductName = "Product 2",
                    QuantityPerUnit ="3/1 Items" },
                 new Product {   ProductID = 3, ProductName = "Product 3", Discontinued = false,
                     UnitsInStock = 250,
                    QuantityPerUnit ="3/3 Items" },
                 new Product {   ProductID = 4, ProductName = "Product 4", Discontinued = false,
                    UnitsInStock = 250,
                    QuantityPerUnit ="3/3 Items" },
                 new Product {   ProductID = 5, ProductName = "Product 5", Discontinued = false,
                     UnitsInStock = 250,
                    QuantityPerUnit ="3/3 Items" },
                 new Product {   ProductID = 6, ProductName = "Product 6", Discontinued = false,
                    UnitsInStock = 290,
                    QuantityPerUnit ="3/3 Items" },
                 new Product {   ProductID = 7, ProductName = "Product 7", Discontinued = false  },
                 new Product {   ProductID = 8, ProductName = "Product 8", Discontinued = false },
                 new Product {   ProductID = 9, ProductName = "Product 9", Discontinued = false  },
                 new Product {   ProductID = 10, ProductName = "Product 10", Discontinued = false },
                 new Product {   ProductID = 11, ProductName = "Product 11", Discontinued = false  },
                 new Product {   ProductID = 12, ProductName = "Product 12", Discontinued = false  },
                 new Product {   ProductID = 13, ProductName = "Product 13", Discontinued = false  },
                 new Product {   ProductID = 14, ProductName = "Product 14", Discontinued = false  },
                 new Product {   ProductID = 15, ProductName = "Product 15", Discontinued = false  },
                 new Product {   ProductID = 16, ProductName = "Product 16", Discontinued = false  },
                 new Product {   ProductID = 17, ProductName = "Product 17", Discontinued = false  },
                 new Product {   ProductID = 18, ProductName = "Product 18", Discontinued = false  },
                 new Product {   ProductID = 19, ProductName = "Product 19", Discontinued = false  },
                 new Product {   ProductID = 20, ProductName = "Product 20", Discontinued = false  },
            }.AsQueryable();
        }
    }
}
