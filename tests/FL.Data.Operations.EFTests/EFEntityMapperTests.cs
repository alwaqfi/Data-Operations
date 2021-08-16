using Microsoft.VisualStudio.TestTools.UnitTesting;
using FL.Data.Operations.TestsData.Data;
using FL.Data.Operations.TestsData.DTO;

namespace FL.Data.Operations.EF.Tests
{
    [TestClass()]
    public class EFEntityMapperTests
    {
        [TestMethod()]
        public void MapTest()
        {
            var map = new EFEntityMapper(new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Product, ProductDTO>();
                cfg.CreateMap<ProductDTO, Product>();
            }).CreateMapper());

            var product = CreateNewProduct();

            var dto = map.Map<ProductDTO>(product);

            Assert.AreEqual(product.ProductID, dto.ProductID);
            Assert.AreEqual(product.ProductName, dto.ProductName);
            Assert.AreEqual(product.Discontinued, dto.Discontinued);
        }


        private Product CreateNewProduct()
        {
            return new Product()
            {
                ProductID = 10,
                ProductName = "Test Product",
                Discontinued = true
            };
        }
    }
}