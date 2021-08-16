using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FL.ExpressionToSQL.ETSAttributes;

namespace FL.Data.Operations.TestsData.Data
{
    //[ETSTable(TableName = "Product Not")]
    public class Product
    {
        public virtual int ProductID { get; set; }
        public virtual string ProductName { get; set; }
        public virtual Nullable<int> SupplierID { get; set; }
        public virtual Nullable<int> CategoryID { get; set; }
        public virtual string QuantityPerUnit { get; set; }
        public virtual Nullable<int> UnitPrice { get; set; }
        public virtual Nullable<short> UnitsInStock { get; set; }
        public virtual Nullable<short> UnitsOnOrder { get; set; }
        public virtual Nullable<short> ReorderLevel { get; set; }
        public virtual bool Discontinued { get; set; }
    }
}
