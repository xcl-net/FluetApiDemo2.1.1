using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
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
                    
                 }
                 
                    );
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

        public DbSet<Order> Order {  get;}

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>().ToTable("Orders");
            modelBuilder.ComplexType<Order.Address>();

            base.OnModelCreating(modelBuilder);
        }
    }


    public class Order
    {
        public Order()
        { 
            
        }

        //public Order(Address address)
        //{ 
        //    Address = address;
        //}

        public int Id { get; set; }
        public string Name { get; set; }

        public class Address
        {
            public string Street { get; set; }
            public string Region { get; set; }
            public string Country { get; set; }
        }
    }



}
