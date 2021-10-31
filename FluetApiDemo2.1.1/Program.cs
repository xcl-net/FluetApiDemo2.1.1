using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace FluetApiDemo2._1._1
{
    internal class Program
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
            }
        }
    }



    public class EfDbContext : DbContext
    {
        public EfDbContext()
        {

        }

        public DbSet<Blog> Blog{  get; set; }
    }

}
