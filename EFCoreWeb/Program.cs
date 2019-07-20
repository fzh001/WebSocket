using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EFCoreWeb.Models;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using QueueHandle;

namespace EFCoreWeb
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();

            RoleNew r = new RoleNew();
            while (true)
            {

                if (Message.queueUser.Count > 0)
                {
                    //lock (ob)
                    //{
                    var queue = Message.queueUser.Dequeue();
                    if (queue != null)
                    {
                        //context.Add(queue);
                        //context.SaveChanges();
                        r.Submit(queue);
                    }
                    //}
                }

            }
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
