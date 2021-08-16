using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading.Tasks;
using FL.ExpressionToSQL.Formatters;
using FL.Data.Operations.TestsData.Data;
using FL.Data.Operations.TestsData;
using FL.Data.Operations.NHibernate;

namespace FL.Data.Operations.NHibernate.Tests
{
    [TestClass()]
    public class NHibernateOperationHandlerTests
    {
        [TestMethod()]
        public async Task DeleteAsyncTest()
        {
            var session = new SQLiteInMemorySessionProvider().Session;
            var handler = new NHibernateOperationHandler<Product>(session, new MSSQLSchemaFormatter());
            await handler.DeleteAsync(p => p.ProductID == 1);
            var isDeleted = await handler.FindAsync(p => p.ProductID == 1);
            Assert.IsTrue(isDeleted.Count() == 0);
        }

        [TestMethod()]
        public async Task InsertAsyncTest()
        {
            var session = new SQLiteInMemorySessionProvider().Session;
            var handler = new NHibernateOperationHandler<Product>(session, new MSSQLSchemaFormatter());

            var product = new Product()
            {
                ProductID = 99,
                ProductName = "Test Insert"
            };

            var prod = await handler.InsertAsync(product);
            Assert.IsNotNull(prod);
        }

        [TestMethod()]
        public async Task UpdateAsyncTest()
        {
            var session = new SQLiteInMemorySessionProvider().Session;
            var handler = new NHibernateOperationHandler<Product>(session, new MSSQLSchemaFormatter());

            var product = new Product()
            {
                ProductID = 2,
                ProductName = "Glass Updated"
            };

            await handler.UpdateAsync(product, p => p.ProductID == 2);
            var updated = await handler.FindAsync(p => p.ProductID == 2);

            Assert.AreNotEqual("Glass", updated.First().ProductName);
        }

        [TestMethod()]
        public async Task GetAsyncTest()
        {
            var session = new SQLiteInMemorySessionProvider().Session;
            var handler = new NHibernateOperationHandler<Product>(session, new MSSQLSchemaFormatter());

            var entity = await handler.GetAsync(p => p.ProductID == 1);

            Assert.IsNotNull(entity);
        }

        [TestMethod()]
        public async Task FindAsyncTest()
        {
            var session = new SQLiteInMemorySessionProvider().Session;
            var handler = new NHibernateOperationHandler<Product>(session, new MSSQLSchemaFormatter());
            var list = await handler.FindAsync(p => p.ProductID <= 5);

            Assert.AreEqual(list.Count(), 5);
            Assert.IsTrue(list.First().ProductID == 1);
            Assert.IsTrue(list.Last().ProductID == 5);
        }

        [TestMethod()]
        public async Task GetAllAsyncTest()
        {
            var session = new SQLiteInMemorySessionProvider().Session;
            var handler = new NHibernateOperationHandler<Product>(session, new MSSQLSchemaFormatter());
            var items = await handler.GetAllAsync();
            Assert.IsTrue(items.Count() == FakeProducts.GetFakeProducts().Count());
        }

        [TestMethod()]
        public async Task PagingAsyncWithExpressionTest()
        {
            var session = new SQLiteInMemorySessionProvider().Session;
            var handler = new NHibernateOperationHandler<Product>(session, new MSSQLSchemaFormatter());
            var results = await handler.PagingAsync(p => p.ProductID, 2, 10, p => p.ProductID > 2);

            Assert.IsTrue(results.Count() == 8);
            Assert.IsTrue(results.ElementAt(0).ProductID == 13);
        }

        [TestMethod()]
        public async Task PagingAsyncWithoutExpressionTest()
        {
            var session = new SQLiteInMemorySessionProvider().Session;
            var handler = new NHibernateOperationHandler<Product>(session, new MSSQLSchemaFormatter());
            var results = await handler.PagingAsync(p => p.ProductID, 2, 10);

            Assert.IsTrue(results.Count() == 10);
            Assert.IsTrue(results.ElementAt(0).ProductID == 11);
        }
    }
}