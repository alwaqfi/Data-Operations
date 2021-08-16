using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FL.Data.Operations.TestsData.DTO
{
   public class ProductDTO
    {
        public virtual int ProductID { get; set; }
        public virtual string ProductName { get; set; }
        public virtual bool Discontinued { get; set; }
    }
}
