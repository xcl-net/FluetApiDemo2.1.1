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
                efDb.BillingDetail.Add(
                     new BankAccount
                     {
                         BankName = "建设银行111",
                         Swift = "jsyh"
                    
                     });

                efDb.SaveChanges();



                var name = efDb.BillingDetail.First().BillingDetailId;


                var query = (from b in
                 efDb.BillingDetail.OfType<BankAccount>()
                             select b).ToList();

                foreach (var item in query)
                {
                    Console.WriteLine(item.BankName);
                }
             

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

        public DbSet<BillingDetail> BillingDetail { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Entity<BillingDetail>()
                .Map<BankAccount>(m =>
                                  m.Requires("BillingDetailType").HasValue(1))
                .Map<CreditCard>(m => 
                                  m.Requires("BillingDetailType").HasValue(2));
                
            base.OnModelCreating(modelBuilder);
        }

    }

    /// <summary>
    /// 账户明细
    /// </summary>
    public abstract class BillingDetail
    {
        /// <summary>
        /// 账户明细id
        /// </summary>
        public int BillingDetailId { get; set; }

        /// <summary>
        /// 账户所属者
        /// </summary>
        public string Owner { get; set; }

        /// <summary>
        /// 账号
        /// </summary>
        public string Number { get; set; }
    }


    /// <summary>
    /// 银行账号
    /// </summary>
    public class BankAccount : BillingDetail
    {
        public string BankName { get; set; }

        /// <summary>
        /// 所属金融银行
        /// </summary>
        public string Swift { get; set; }
    }


    /// <summary>
    /// 信用卡
    /// </summary>
    public class CreditCard : BillingDetail
    {
        /// <summary>
        /// 信用卡类型
        /// </summary>
        public int CardType { get; set; }

        /// <summary>
        /// 信用卡失效月份
        /// </summary>
        public string ExpiryMonth { get; set; }

        /// <summary>
        /// 信用卡失效年份
        /// </summary>
        public string ExpiryYear { get; set; }
    }
}
