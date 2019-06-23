using Microsoft.EntityFrameworkCore;
using System;
using System.Web;

namespace EFCoreLib
{
    public class  MyDBContext : DbContext
    {
        /// <summary>
        /// 您将需要在网站的 Web.config 文件中配置此处理程序 
        /// 并向 IIS 注册它，然后才能使用它。有关详细信息，
        /// 请参阅以下链接: https://go.microsoft.com/?linkid=8101007
        /// </summary>
        public MyDBContext(DbContextOptions<MyDBContext> options)
            : base(options)
        {
        }

        public DbSet<User> User { get; set; }
    }

    public class User
    {
        public int id { get; set; }
        public string name { get; set; }
    }
}
