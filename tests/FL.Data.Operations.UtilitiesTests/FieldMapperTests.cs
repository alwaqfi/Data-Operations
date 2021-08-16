using Microsoft.VisualStudio.TestTools.UnitTesting;
using FL.Data.Operations.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using FL.Data.Operations.TestsData.Data;
using FL.Data.Operations.TestsData;
using FL.Data.Operations.TestsData.DTO;

namespace FL.Data.Operations.Utilities.Tests
{
    [TestClass()]
    public class FieldMapperTests
    {
        
        [TestMethod()]
        public void MapTest()
        {
            var product = new Product()
            {
                ProductID = 10,
                ProductName = "Test Product"
            };
            var prodDto = new ProductDTO();
            FieldMapper.Map(product, prodDto);
            Assert.AreEqual(product.ProductID, prodDto.ProductID);
            Assert.AreEqual(product.ProductName, prodDto.ProductName);
        }
    }
}