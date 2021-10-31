using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FluetApiNetFrameworkDemo2._1._1
{
    /// <summary>
    /// 2.2 约定
    /// 2.2.4 复杂类型约定
    /// </summary>
    public class Program
    {
        static void Main(string[] args)
        {

            using (var efDb = new EfDbContext())
            {
                efDb.Order.Add(
                     new Order
                     {
                         Name = "xcllxc",
                    
                     });

                efDb.SaveChanges();



                var name = efDb.Order.First().Name;
                Console.WriteLine(name); //运行可以直接打印出来 "xcll"
            }


            Console.WriteLine("ok");
            Console.ReadLine();

        }
    }

    public class EfDbContext : DbContext
    {
        public EfDbContext() : base("name=ConnectionString")
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<EfDbContext>());
        }

        public DbSet<Order> Order { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Types()
                .Configure(c => c.ToTable(GetTableName(c.ClrType)));


            base.OnModelCreating(modelBuilder);

        }

        //给表名字统一加后缀,前缀
        private string GetTableName(Type type)
        {
            //var result = Regex.Replace(type.Name, ".[A-Z]",
            //    m => m.Value[0] + "_" + m.Value[1]);


            var result = Regex.Replace(type.Name, "[A-Z]+","nn__nn");

            return result.ToString();
        }
    }



    public class Order
    {

        public int Id { get; set; }
        public string Name { get; set; }

    }



}
