using Dapper;
using FL.Data.Operations.TestsData;
using FL.Data.Operations.TestsData.Data;
using FL.ExpressionToSQL;
using FL.ExpressionToSQL.Formatters;
using Microsoft.Data.Sqlite;
using System;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


namespace FL.Data.Operations.DapperTests
{

    public class SqliteConnectionProvider : IDisposable
    {
        public DbConnection Connection { set; get; }
        public SqliteConnectionProvider()
        {
            Connection = SqliteFactory.Instance.CreateConnection();
            SqliteConnectionStringBuilder builder = new SqliteConnectionStringBuilder();
            builder.Mode = SqliteOpenMode.Memory;
            Connection.ConnectionString = builder.ConnectionString;
            Seed();
        }
        private void Seed()
        {
            Connection.Open();

            using (var t = Connection.BeginTransaction())
            {
                string cptStm = CreateProductTable();
                Connection.Execute(cptStm);
                foreach (var product in FakeProducts.GetFakeProducts())
                {
                    var prodStm = product.BuildInsertStatement<Product>(false, new MSSQLSchemaFormatter());
                    Connection.Execute(prodStm);
                }
                t.Commit();
            }
        }

        private string CreateProductTable()
        {
            var ctStatement = "CREATE TABLE \"Product\" ( " + "\n";
            ctStatement = ctStatement + "	\"ProductID\" \"int\" IDENTITY (1, 1) NOT NULL , " + "\n";
            ctStatement = ctStatement + "	\"ProductName\" nvarchar (40) NOT NULL , " + "\n";
            ctStatement = ctStatement + "	\"SupplierID\" \"int\" NULL , " + "\n";
            ctStatement = ctStatement + "	\"CategoryID\" \"int\" NULL , " + "\n";
            ctStatement = ctStatement + "	\"QuantityPerUnit\" nvarchar (20) NULL , " + "\n";
            ctStatement = ctStatement + "	\"UnitPrice\" \"int\" NULL, " + "\n";
            ctStatement = ctStatement + "	\"UnitsInStock\" \"smallint\" NULL CONSTRAINT \"DF_Products_UnitsInStock\" DEFAULT (0), " + "\n";
            ctStatement = ctStatement + "	\"UnitsOnOrder\" \"smallint\" NULL CONSTRAINT \"DF_Products_UnitsOnOrder\" DEFAULT (0), " + "\n";
            ctStatement = ctStatement + "	\"ReorderLevel\" \"smallint\" NULL CONSTRAINT \"DF_Products_ReorderLevel\" DEFAULT (0), " + "\n";
            ctStatement = ctStatement + "	\"Discontinued\" \"bit\" NOT NULL CONSTRAINT \"DF_Products_Discontinued\" DEFAULT (0) " + "\n";
            ctStatement = ctStatement + ")";
            return ctStatement;
        }

        public void Dispose()
        {
            Connection.Close();
        }
    }
}
