using SimulatedNode.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace SimulatedNode.Services
{
    public static class SimulateAppService
    {
        private static SimulateConfig appConfig;
        internal static void Initialize()
        {
            var deserializer = new DeserializerBuilder()
              .WithNamingConvention(UnderscoredNamingConvention.Instance)
              .Build();

            SimulateAppService.appConfig = deserializer.Deserialize<SimulateConfig>(File.OpenText("Config.yml"));
        }
        
        internal static SimulateConfig GetAppConfig()
        {
            return SimulateAppService.appConfig;
        }
    }
}
