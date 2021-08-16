using FL.Data.Operations.EF;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using System.Data.Entity;
using AutoMapper.Extensions.ExpressionMapping;
using FL.Data.Operations.TestsData.Data;
using FL.Data.Operations.TestsData.DTO;
using TestContext = FL.Data.Operations.EF.Tests.Utilities.TestContext;
using System.Linq;
using FL.Data.Operations.TestsData;

namespace FL.Data.Operations.EF.Tests
{
    [TestClass()]
    public class EFOperationHandlerTests
    {
        private readonly EFEntityMapper _mapper;

        public EFOperationHandlerTests()
        {
            _mapper = new EFEntityMapper(new AutoMapper.MapperConfiguration(conf =>
            {
                conf.AddExpressionMapping();
                conf.CreateMap<Product, ProductDTO>();
                conf.CreateMap<ProductDTO, Product>();
            }).CreateMapper());
        }

        [TestMethod()]
        public async Task GetAsyncTest()
        {
            var setup = Initialize();
            var result = await setup.Service.GetAsync(setup.Context.Products, _mapper, p => p.ProductID == 1);
            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public async Task DeleteAsyncTest()
        {
            var setup = Initialize();
            await setup.Service.DeleteAsync(setup.Context.Products, _mapper, p => p.ProductID == 1);
            var result = await setup.Service.GetAsync(setup.Context.Products, _mapper, x => x.ProductID == 1);
            Assert.IsNull(result);
        }

        [TestMethod()]
        public async Task FindAsyncTest()
        {
            var setup = Initialize();
            var results = await setup.Service.FindAsync(setup.Context.Products, _mapper, p => p.ProductID < 10);
            Assert.IsTrue(results.Count() == 9);
        }

        [TestMethod()]
        public async Task PagingAsyncTest()
        {
            var setup = Initialize();
            var page1 = await setup.Service.PagingAsync(setup.Context.Products, _mapper, 1, 7);
            var page2 = await setup.Service.PagingAsync(setup.Context.Products, _mapper, 2, 7);
            var page3 = await setup.Service.PagingAsync(setup.Context.Products, _mapper, 3, 7);

            Assert.IsTrue(page1.Count() == 7);
            Assert.IsTrue(page2.Count() == 7);
            Assert.IsTrue(page3.Count() == 6);
        }

        [TestMethod()]
        public async Task FilteredPagingAsyncTest()
        {
            var setup = Initialize();
            var page1 = await setup.Service.PagingAsync(setup.Context.Products, _mapper, 1, 7, p => p.ProductID <= 10);
            var page2 = await setup.Service.PagingAsync(setup.Context.Products, _mapper, 2, 7, p => p.ProductID <= 10);

            Assert.IsTrue(page1.Count() == 7);
            Assert.IsTrue(page2.Count() == 3);
        }

        [TestMethod()]
        public async Task InsertAsyncTest()
        {
            var setup = Initialize();
            var newProduct = new ProductDTO()
            {
                ProductID = 21,
                ProductName = "New Product"
            };

            var product = await setup.Service.InsertAsync(setup.Context.Products, newProduct, _mapper);

            var insertedProduct = await setup.Service.GetAsync(setup.Context.Products, _mapper, p => p.ProductID == newProduct.ProductID);

            Assert.IsNotNull(insertedProduct);
        }

        [TestMethod()]
        public async Task UpdateAsyncTest()
        {
            var setup = Initialize();

            var toUpdate = new ProductDTO()
            {
                ProductID = 1,
                ProductName = "Updated"
            };

            await setup.Service.UpdateAsync(setup.Context.Products, toUpdate, _mapper, p => p.ProductID == toUpdate.ProductID);

            var updatedProduct = await setup.Service.GetAsync(setup.Context.Products, _mapper, p => p.ProductID == toUpdate.ProductID);

            Assert.AreEqual(toUpdate.ProductName, updatedProduct.ProductName);
        }

        [TestMethod()]
        public async Task GetAllAsyncTest()
        {
            var setup = Initialize();
            var results = await setup.Service.GetAllAsync(setup.Context.Products, _mapper);

            Assert.IsTrue(results.Count() == FakeProducts.GetFakeProducts().Count());
        }
        private (TestContext Context, EFOperationHandler<DbSet<Product>, Product, ProductDTO> Service) Initialize()
        {
            var context = new TestContext();
            var hanlder = new EFOperationHandler<DbSet<Product>, Product, ProductDTO>(context);
            return (context, hanlder);
        }
    }
}