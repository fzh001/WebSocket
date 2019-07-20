using Microsoft.EntityFrameworkCore;
using System;
using System.Web;
using EntityLib;

namespace EFCoreLib
{
    public class  MyDBContext : DbContext
    {
        public static string ConnectionString = "";
        private static MyDBContext _instance;
        private static readonly Object obcontext = new object();
        /// <summary>
        /// 您将需要在网站的 Web.config 文件中配置此处理程序 
        /// 并向 IIS 注册它，然后才能使用它。有关详细信息，
        /// 请参阅以下链接: https://go.microsoft.com/?linkid=8101007
        /// </summary>
        //public MyDBContext()
        //    : base()
        //{

        //}
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL(ConnectionString);
            //optionsBuilder.UseMySQL(ConnectionString);
        }
        public static MyDBContext GetInstance()
        {
            if(_instance==null)
            {
                lock(obcontext)
                {
                    if (_instance == null)
                        _instance = new MyDBContext();
                }
            }
            return _instance;
        }
        public DbSet<User> User { get; set; }
    }

   
}
