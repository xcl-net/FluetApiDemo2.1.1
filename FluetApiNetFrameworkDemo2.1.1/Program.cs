using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluetApiNetFrameworkDemo2._1._1
{

    public class Program
    {
        static void Main(string[] args)
        {

            using (var efDb = new EfDbContext())
            {
                efDb.Blog.Add(
                    new Blog
                    {
                        Name = "xcll",
                        Url = "www.baidu.com"
                    }
                    );
                efDb.SaveChanges();



                var name = efDb.Blog.First().Name;
                Console.WriteLine(name); //运行可以直接打印出来 "xcll"
            }


            Console.WriteLine("ok");
            Console.ReadLine();

        }
    }

    public class EfDbContext : DbContext
    {
        public EfDbContext():base("name=ConnectionString")
        {
            //禁用数据库初始化策略
            //Database.SetInitializer<EfDbContext>(null); //打开注释,ef则不会自动迁移数据库


            #region 方案一: 构造函数里,指定EF数据库生成策略;
            ////如果数据库不存在, 就创建
            //Database.SetInitializer(new CreateDatabaseIfNotExists<EfDbContext>());

            ////总是创建数据库, 无论存在与否
            //Database.SetInitializer(new DropCreateDatabaseAlways<EfDbContext>());

            ////如果EF检测到数据库模型发生了变化, 将更新模型
            //Database.SetInitializer(new DropCreateDatabaseIfModelChanges<EfDbContext>()); 
            #endregion

        }

        public DbSet<Blog> Blog { get; set; }
    }

}
