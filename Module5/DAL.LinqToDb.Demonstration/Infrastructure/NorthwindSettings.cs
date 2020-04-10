using LinqToDB.Configuration;
using System.Collections.Generic;
using System.Linq;

namespace DAL.LinqToDb.Demonstration.Infrastructure
{
    public class NorthwindSettings : ILinqToDBSettings
    {
        public IEnumerable<IDataProviderSettings> DataProviders => Enumerable.Empty<IDataProviderSettings>();

        public string DefaultConfiguration => "SqlServer";
        public string DefaultDataProvider => "SqlServer";

        public IEnumerable<IConnectionStringSettings> ConnectionStrings
        {
            get
            {
                yield return
                    new ConnectionStringSettings
                    {
                        Name = "Northwind",
                        ProviderName = "SqlServer",
                        ConnectionString = @"Server=.\;Database=Northwind;Trusted_Connection=True;Enlist=False;"
                    };
            }
        }
    }
}
