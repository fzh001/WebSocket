using EFLib;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var db = new MyDbContext();
            var data = db.User.Find(1999);
            data.UserName = "测试哈11111111111";
            var db1 = new MyDbContext();
            var s = db1.User.Find(1999);





            //  var A = db.User.Attach(data);
            // db.Set(data).State = System.Data.Entity.EntityState.Modified;
            var data1 = db.User.Where(a => a.ID == 1999).FirstOrDefault();
           // db.SaveChanges();
            //Stopwatch sw = new Stopwatch();
            //sw.Start();
            //for(int i=1 ;i<1000;i++)
            //{
            //    User u = new User();
            //    u.UserName = i.ToString();
            //    u.UserPwd = i.ToString();
            //    u.Remark = i.ToString();
            //    db.User.Add(u);
            //    db.SaveChanges();
            //}
            //sw.Stop();
            //Console.WriteLine(sw.ElapsedMilliseconds);

            //sw = new Stopwatch();
            //sw.Start();
            //for (int i = 1; i < 1000; i++)
            //{
            //    User u = new User();
            //    u.UserName = i.ToString();
            //    u.UserPwd = i.ToString();
            //    u.Remark = i.ToString();
            //    db.User.Add(u);
                
            //}
            //db.SaveChanges();
            //sw.Stop();
            //Console.WriteLine(sw.ElapsedMilliseconds);
            Console.ReadLine();
        }
    }
}
