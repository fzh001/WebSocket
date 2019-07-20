using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using IdentityServer4Demo.Models;

namespace IdentityServer4Demo.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var csredis = new CSRedis.CSRedisClient("127.0.0.1:6379,defaultDatabase=1,poolsize=50,ssl=false,writeBuffer=10240");
            //初始化 RedisHelper
            RedisHelper.Initialization(csredis);
            var list = RedisHelper.LRange("video", 0, 200);
            return View(list);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
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
