using FL.Data.Operations.TestsData;
using FL.Data.Operations.TestsData.Data;
using Microsoft.EntityFrameworkCore;
  
namespace FL.Data.Operations.EFCore.Tests.Utilities
{
    public class TestContext : DbContext
    {
  
        public TestContext(DbContextOptions options) :base(options)
        {  
            Products.AddRange(FakeProducts.GetFakeProducts());
            SaveChanges();
        }
    
        public DbSet<Product> Products { get; set; }
    }     
}
