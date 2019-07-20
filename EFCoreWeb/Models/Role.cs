using EFCoreLib;
using EntityLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCoreWeb.Models
{
   public interface Role
    {
         List<User> GetList();
    }
}
