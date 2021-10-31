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
    /// 2.2.1 类型发现
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
        public DbSet<StudentAddress> StudentAddress {  get;}
    }



    public class Department
    {
        //primary key
        public int DepartmentId { get; set; } //Id结尾的默认生成为主键自动递增;
        public string Name { get; set; }


        ////Navigation property
        //public virtual ICollection<Course> Courses { get; set; }

    }

    public class StudentAddress
    {
        /// <summary>
        /// 必须 显示添加一个字段为主键
        /// </summary>
        public int Id { get; set; }
        public int AddressId { get; set; }
        public int StudentId { get; set; }
    }

    //public class Course
    //{
    //    public int MyProperty { get; set; }
    //}
}
