using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace SimulatedNode
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Thread thread = new Thread(Print);
            Thread thread2 = new Thread(CreateHostBuilder(args).Build().Run);

            thread.Start();
            thread2.Start();

            //CreateHostBuilder(args).Build().Run();
        }

        static void Print()
        {
            while (true)
            {
                var a = Console.ReadLine();
                Console.WriteLine(a);
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
