using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
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
            modelBuilder.Entity<Order>().ToTable("Orders");

            //全局约定方案一:
            //modelBuilder.Properties()
            //    .Where(p=>p.Name == "Id")
            //    .Configure(p => p.IsKey());

            //配置所有模型, 属性类型字段为deciaml的为: 10位且保留2位小数;
            modelBuilder.Properties<decimal>()
                .Configure(config => config.HasPrecision(10, 2));

            //全局约定方案二: 单独写一个类
            modelBuilder.Conventions.Add<CustomKeyConvention>();


            //第一次定义
            modelBuilder.Properties<string>()
                .Where(x=>x.Name == "Name")
                .Configure(c => c.HasMaxLength(500));

            //第二次定义(重复定义) , 以第二次定义为准;
            modelBuilder.Properties<string>()
                .Where(x => x.Name == "Name")
                .Configure(c => c.HasMaxLength(250));


            //4. 时间类型指定其他类型
            modelBuilder.Conventions.Add(new DateTime2Convention());

            base.OnModelCreating(modelBuilder);
        }
    }


    public class CustomKeyConvention : Convention
    {
        public CustomKeyConvention()
        {
            Properties()
                .Where(prop => prop.Name == "Id")
                .Configure(config => config.IsKey());
        }
    }


    //4. 自定义类约定
    public class DateTime2Convention : Convention
    {
        public DateTime2Convention()
        {
            this.Properties<DateTime>()
                .Configure(c => c.HasColumnType("datetime2"));
        }
    }


    public class Order
    {

        public int Id { get; set; }
        public string Name { get; set; }

        public DateTime CreateTime { get; set; }
    }



}
