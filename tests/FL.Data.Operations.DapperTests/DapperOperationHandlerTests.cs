using Microsoft.VisualStudio.TestTools.UnitTesting;
using FL.Data.Operations.Dapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FL.Data.Operations.DapperTests;
using FL.ExpressionToSQL.Formatters;
using FL.Data.Operations.TestsData.DTO;
using FL.Data.Operations.TestsData.Data;
using System.Linq.Expressions;
using System.Linq;

namespace FL.Data.Operations.Dapper.Tests
{
    [TestClass()]
    public class DapperOperationHandlerTests
    {
        [TestMethod()]
        public async Task GetAsyncTest()
        {
            var connection = new SqliteConnectionProvider();
            var handler = new DapperOperationHandler<Product>(connection.Connection, new MSSQLSchemaFormatter());
            var prod = await handler.GetAsync(p => p.ProductID == 1);
            Assert.IsNotNull(prod);
        }

        [TestMethod()]
        public async Task DeleteAsyncTest()
        {
            var connection = new SqliteConnectionProvider();
            var handler = new DapperOperationHandler<Product>(connection.Connection, new MSSQLSchemaFormatter());
            Expression<Func<Product, bool>> exp = p => p.CategoryID == 1;
            await handler.DeleteAsync(exp);

            var prod = await handler.GetAsync(exp);
            Assert.IsNull(prod);
        }

        [TestMethod()]
        public async Task FindAsyncTest()
        {
            var connection = new SqliteConnectionProvider();
            var handler = new DapperOperationHandler<Product>(connection.Connection, new MSSQLSchemaFormatter());
            Expression<Func<Product, bool>> exp = p => p.ProductID > 10;
            var list = await handler.FindAsync(exp);
            Assert.IsTrue(list.Count() == 10);
        }

        [TestMethod()]
        public async Task GetAllAsyncTest()
        {
            var connection = new SqliteConnectionProvider();
            var handler = new DapperOperationHandler<Product>(connection.Connection, new MSSQLSchemaFormatter());
            var list = await handler.GetAllAsync();
            Assert.IsTrue(list.Count() == 20);
        }

        [TestMethod()]
        public async Task InsertAsyncTest()
        {
            var product = new Product
            {
                ProductID = 21,
                ProductName = "Inserted product"
            };

            var connection = new SqliteConnectionProvider();
            var handler = new DapperOperationHandler<Product>(connection.Connection, new MSSQLSchemaFormatter());
            var val = await handler.InsertAsync(product);
            Assert.IsNotNull(val);

            Expression<Func<Product, bool>> exp = p => p.ProductID == 21;
            var prod = await handler.GetAsync(exp);
            Assert.IsTrue(prod.ProductID == 21);
        }

        [TestMethod()]
        public async Task UpdateAsyncTest()
        {
            var product = new Product
            {
                ProductID = 1,
                ProductName = "Updated product"
            };
            Expression<Func<Product, bool>> exp = p => p.ProductID == 1;

            var connection = new SqliteConnectionProvider();
            var handler = new DapperOperationHandler<Product>(connection.Connection, new MSSQLSchemaFormatter());
            await handler.UpdateAsync(product, exp);

            var prod = await handler.GetAsync(exp);
            Assert.AreEqual(prod.ProductName, product.ProductName);
        }

        [TestMethod()]
        public async Task PagingAsyncTest()
        {
            var connection = new SqliteConnectionProvider();
            var handler = new DapperOperationHandler<Product>(connection.Connection, new MySQLSchemaFormatter());
            int recordsPerPage = 5;
            int pageNumber = 3;
            Expression<Func<Product, bool>> exp = p => p.ProductID > 2;

            var list = await handler.PagingAsync( p=>p.ProductID , pageNumber, recordsPerPage, exp);

            Assert.IsTrue(list.Count() == 5);
            Assert.IsTrue(list.ElementAt(4).ProductID == 17);
        }

        [TestMethod()]
        public async Task PagingWithoutExpressionAsyncTest()
        {
            var connection = new SqliteConnectionProvider();
            var handler = new DapperOperationHandler<Product>(connection.Connection, new MySQLSchemaFormatter());
            int recordsPerPage = 5;
            int pageNumber = 3;
  
            var list = await handler.PagingAsync(p => p.ProductID, pageNumber, recordsPerPage);

            Assert.IsTrue(list.Count() == 5);
            Assert.IsTrue(list.ElementAt(4).ProductID == 15);
        }
    }
}