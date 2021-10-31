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
    /// 2.2.3 关系约定
    /// </summary>
    public class Program
    {
        static void Main(string[] args)
        {

            using (var efDb = new EfDbContext())
            {
                efDb.Department.Add(
                    new Department
                    {
                        Name = "xcll"
                    }
                    );
                efDb.SaveChanges();



                var name = efDb.Department.First().Name;
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

        public DbSet<Department> Department { get; set; }
        public DbSet<Course> Course {  get;}
    }



    public class Department
    {
        //primary key
        public int DepartmentId { get; set; } //Id结尾的默认生成为主键自动递增;
        public string Name { get; set; }


        //Navigation property
        public virtual ICollection<Course> Courses { get; set; }

    }

    public class Course
    {
        //主键
        public int CourseId { get; set; }
        public string Title { get; set; }
        public int Credits { get; set; }

        //外键
        public int DepartmentId { get; set; }

        //导航属性
        public virtual Department Department { get; set; }

    }
}
