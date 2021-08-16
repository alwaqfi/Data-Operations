using Microsoft.VisualStudio.TestTools.UnitTesting;
using FL.Data.Operations.EFCore;
using System;
using System.Collections.Generic;
using System.Text;
using FL.Data.Operations.TestsData.DTO;
using FL.Data.Operations.TestsData.Data;

namespace FL.Data.Operations.EFCore.Tests
{
    [TestClass()]
    public class EFCoreEntityMapperTests
    {
        [TestMethod()]
        public void MapTest()
        {
            var map = new EFCoreEntityMapper(new AutoMapper.MapperConfiguration(cfg =>
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