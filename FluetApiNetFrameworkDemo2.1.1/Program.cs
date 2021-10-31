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
    /// 2.4.1 属性映射
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
                         BlogId = 1,
                         DateTime = DateTime.Now,

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
                }).Property(p => p.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<Blog>()
                .Property(p => p.Decimal)
                .HasPrecision(18, 4);

            modelBuilder.Entity<Blog>().Property(p => p.Name).IsRequired();//不可为空

            modelBuilder.Entity<Blog>().Property(p => p.Double).IsOptional();
            modelBuilder.Entity<Blog>().Property(p => p.Float).IsOptional();//设置为可空

            //modelBuilder.Entity<Blog>().Property(p => p.Name)
            //    .HasColumnType("VARCHAR(50)");// 错误的写法;

            //modelBuilder.Entity<Blog>().Property(p => p.Name)
            //    .HasColumnType("VARCHAR").HasMaxLength(50); //映射到数据库为varchar(50)

            modelBuilder.Entity<Blog>().Property(p => p.Name)
                .HasColumnType("NVARCHAR").HasMaxLength(50); //默认映射就是NVARCHAR类型, 不用显示的再写 .HasColumnType("NVARCHAR")

            modelBuilder.Entity<Blog>().Property(p => p.Name).IsMaxLength(); //nvarchar(4000)

            //modelBuilder.Entity<Blog>().Property(p => p.Char).HasColumnType("char").HasMaxLength(1); // char(1)

            //modelBuilder.Entity<Blog>().Property(p => p.Char).HasColumnType("char").IsFixedLength(); // char(128)

            modelBuilder.Entity<Blog>().Property(p=>p.Char)
                .HasMaxLength(11).IsFixedLength().IsUnicode();

            modelBuilder.Entity<Blog>()
                .Property(p => p.DateTime)
                .HasColumnType("DATETIME2");


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

        public string Char { get; set; } //char类型 数据库类型, 只对应C# 中的字符串类型
        public DateTime DateTime { get; set; } //直接映射会报错;
    }
}
