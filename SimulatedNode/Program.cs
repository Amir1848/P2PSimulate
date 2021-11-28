using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SimulatedNode.Models;
using SimulatedNode.Services;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace SimulatedNode
{
    public class Program
    {
        public static void Main(string[] args)
        {
            SimulateAppService.Initialize();
            Thread thread = new Thread(GetCommandsFromUser);
            Thread thread2 = new Thread(CreateHostBuilder(args, SimulateAppService.GetAppConfig().NodePort).Build().Run);
            
            thread.Start();
            thread2.Start();
        }

        static void GetCommandsFromUser()
        {
            while (true)
            {
                var a = Console.ReadLine();
                
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args, string portNumber) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                   webBuilder.UseUrls("http://localhost:" + portNumber);
                });
    }
}
