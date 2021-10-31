using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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
    /// 2.2 介于代码配置
    /// </summary>
    public class Program
    {
        static void Main(string[] args)
        {

            using (var efDb = new EfDbContext())
            {
                efDb.Blog.Add(
                     new Blog
                     {
                         Name = "博客1",
                         BlogId = 1
                    
                     });

                efDb.SaveChanges();





                var query = (from b in
                 efDb.Blog.OfType<Blog>()
                             select b).ToList();

                foreach (var item in query)
                {
                    Console.WriteLine(item.Name);
                }
             

            }


            Console.WriteLine("ok");
            Console.ReadLine();

        }
    }

    [DbConfigurationType(typeof(MyConfiguration))]
    public class EfDbContext : DbContext
    {
        public EfDbContext() : base("name=ConnectionString")
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<EfDbContext>());
        }

        public DbSet<Blog> Blog { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Blog>().ToTable("Blogs");

            //联合主键
            modelBuilder.Entity<Blog>()
                .HasKey(k => new
                {
                    Id = k.Id
                }).Property(p=>p.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<Blog>()
                .Property(p => p.Decimal)
                .HasPrecision(18, 4);
                
            base.OnModelCreating(modelBuilder);
        }

    }

    public class Blog
    {
        public int Id { get; set; }
        public int BlogId { get; set; }
        public string Name { get; set; }

        public double Double { get; set; }
        public float Float { get; set; }
        public Guid Guid { get; set; }
        public TimeSpan TimeSpan { get; set; }

        public decimal Decimal { get; set; }
        //public DateTime DateTime { get; set; } //直接映射会报错;
    }
}
