using EFCoreLib;
using EntityLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCoreWeb.Models
{
    public class RoleNew:Role
    {
        private MyDBContext MyDB = new MyDBContext();
       public List<User> GetList()
        {
            return new[] { new User { name="1"}, new User { name = "2" }, new User { name = "3" } }.ToList();
        }


        public void Submit(User u)
        {
            MyDB.User.AddAsync(u);
            MyDB.SaveChangesAsync();
        }
    }
}
