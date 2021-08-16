using FL.Data.Operations.TestsData;
using FL.Data.Operations.TestsData.Data;
using FluentNHibernate;
using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FL.Data.Operations.NHibernate.Tests
{
    public class SQLiteInMemorySessionProvider : IDisposable
    {
        public ISession Session { get; private set; }
        public ISessionFactory SessionFactory { get; private set; }

        public SQLiteInMemorySessionProvider() { Session = CreateSession(); }
        private ISession CreateSession()
        {
            Configuration config = null;
            SessionFactory = Fluently.Configure()
             .Database(SQLiteConfiguration.Standard.InMemory().ShowSql())
             .Mappings(m => m.AutoMappings.Add(CreateAutomappings))
             .ExposeConfiguration(x => config = x)
             .BuildSessionFactory();

            var session = SessionFactory.OpenSession();
            new SchemaExport(config).Execute(true, true, false, session.Connection, null);

            Seed(session);

            return session;
        }

        private void Seed(ISession session)
        {
            using (var ts = session.BeginTransaction())
            {
                foreach (var prod in FakeProducts.GetFakeProducts())
                    session.Save(prod);
                ts.Commit();
            }
        }

        static AutoPersistenceModel CreateAutomappings()
        {
            return AutoMap.AssemblyOf<Product>(new SQLiteAutomappingConfiguration());
        }

        public void Dispose()
        {
            Session.Dispose();
        }
    }

    public class SQLiteAutomappingConfiguration : DefaultAutomappingConfiguration
    {
        public override bool ShouldMap(Type type)
        {
            return type.Namespace == "FL.Data.Operations.TestsData.Data";
        }
        public override bool IsId(Member member)
        {
            return member.Name == member.DeclaringType.Name + "ID";
        }

    }
}
