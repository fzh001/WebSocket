using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EFCoreWeb.Models;
using EFCoreLib;
using QueueHandle;
using System.Threading;

namespace EFCoreWeb.Controllers
{
    public class HomeController : Controller
    {
        private static readonly Object ob = new Object();
        private MyDBContext context = MyDBContext.GetInstance();
        private Role r;
        public HomeController(Role _r)
        {
          //  context = _context;
            r = _r;
        }
        public IActionResult Index()
        {
            // var list = context.User.ToList();
            var list = r.GetList();
            ViewBag.user = list;
            //var ss = "mmm";
            //var kk = Convert.ToInt32(ss);
            return View();
        }

        public IActionResult SunmitForm(int num)
        {
            Parallel.For(0, num, i =>
            {
                Message.queueUser.Enqueue(new EntityLib.User { name = DateTime.Now.ToString("yyyyMMddHHmmss") + "-" + i });
            });
           // Task.Factory.StartNew(handle);
            return Content("OK");
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public void handle()
        {
            RoleNew r = new RoleNew();
            while (true)
            {
                if (Message.queueUser.Count > 0)
                {
                    lock (ob)
                    {
                        var queue = Message.queueUser.Dequeue();
                        if (queue != null)
                        {
                            r.Submit(queue);
                            //context.Add(queue);
                            //context.SaveChanges();
                        }
                    }
                }
            }
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
