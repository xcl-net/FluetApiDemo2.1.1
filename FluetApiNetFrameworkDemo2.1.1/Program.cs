using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluetApiNetFrameworkDemo2._1._1
{
    /// <summary>
    /// 数据直接保存到了SqlLocalDb.exe
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
        public EfDbContext()
        {

        }

        public DbSet<Blog> Blog { get; set; }
    }

}
