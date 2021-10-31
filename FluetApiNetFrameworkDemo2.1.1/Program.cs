using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FluetApiNetFrameworkDemo2._1._1
{
    /// <summary>
    /// 2.2 约定
    /// 2.2.5 
    /// 6. 自定义特性
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
                         BookName = "english"
                    
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

            modelBuilder.Properties()
                .Where(x => x.GetCustomAttributes(false).OfType<NonUnicode>().Any())
                .Configure(c => c.IsUnicode(false));

            //modelBuilder.Properties()
            //    .Where(x=>x.Name == "BookName")
            //    .

            //modelBuilder.Properties()
            //    .Where(x => 
            //    x.GetCustomAttributes(false)
            //    .OfType<IsUnicode>().Any())
            //    .Configure(c =>
            //    c.IsUnicode(c.ClrPropertyInfo.GetCustomAttributes<IsUnicode>));


            modelBuilder.Properties()
                .Having(x =>
                x.GetCustomAttributes(false).OfType<IsUnicode>().FirstOrDefault())
                .Configure((config, att) => config.IsUnicode(att.Unicode));
            //Having 起到过滤的作用;

            base.OnModelCreating(modelBuilder);

        }
    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]public class NonUnicode : Attribute
    { 
   
    }


    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class IsUnicode : Attribute
    {
        public bool Unicode { get; set; }
        public IsUnicode(bool isUnicode)
        {
            Unicode = isUnicode;
        }
    }


    public class Order
    {

        public int Id { get; set; }
        
        [NonUnicode]
        public string Name { get; set; }

        public string City { get; set; }

        public string BookName { get; set; }
    }



}
