using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluetApiNetFrameworkDemo2._1._1
{
    public class MyConfiguration : DbConfiguration
    {
        public MyConfiguration()
        {
            //Database.SetInitializer(new DropCreateDatabaseIfModelChanges<EfDbContext>());



            //SetDefaultConnectionFactory(new
            //    System.Data.Entity.Infrastructure
            //    .LocalDbConnectionFactory("v13.0"));

            SetProviderServices("System.Data.SqlClient", System.Data.Entity.SqlServer.SqlProviderServices.Instance);

        }
    }
}
