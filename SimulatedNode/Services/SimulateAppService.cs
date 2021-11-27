using SimulatedNode.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace SimulatedNode.Services
{
    public static class SimulateAppService
    {
        private static SimulateConfig appConfig;
        private static List<NodeInfo> nodesInfo;
        internal static void Initialize()
        {
            var deserializer = new DeserializerBuilder()
              .WithNamingConvention(UnderscoredNamingConvention.Instance)
              .Build();

            appConfig = deserializer.Deserialize<SimulateConfig>(File.OpenText("Config.yml"));
            nodesInfo = deserializer.Deserialize<NodesInfo>(File.OpenText("NodeFiles.yml")).NodeFiles.OrderBy(p => p.NodeName).ToList();
        }
        
        internal static SimulateConfig GetAppConfig()
        {
            return appConfig.cloneConfig();
        }

        internal static List<NodeInfo> GetNodesInfo()
        {
            return nodesInfo;
        }


        internal static string GetNodeAddress(long nodeNumber)
        {
            return GetAppConfig().FriendNodes.Where(p => p.NodeName == nodeNumber).Select(p => p.NodePort).SingleOrDefault();
        }

        public static CommandResult ProcessCommand(string command)
        {
            return CommandResult.Failed;
            var commandParts = command.Split(" ");
            switch (commandParts[0].ToLower())
            {
                case "request":
                    var fileName = commandParts[0];

                    // code block
                    break;
                //case y:
                    // code block
                  //  break;
                //default:
                    // code block
                  //  break;
            }
        }

        public static long GetFileNodeNumber(string fileName)
        {
            return nodesInfo.Where(p => p.NodeFiles.Contains(fileName)).Select(p => p.NodeName).Single();
            
        }

        public static string GetNodePort(long nodeNumber)
        {
            var nodeInfo = appConfig.FriendNodes.Where(p => p.NodeName == nodeNumber).SingleOrDefault();
            if(nodeInfo == null)
            {
                nodeInfo = new FriendNode();
                nodeInfo.NodeName = nodeNumber;
                var httpClient = new HttpClient();
                foreach (var node in appConfig.FriendNodes.OrderBy(p => p.NodeName))
                {
                    var result = httpClient.GetAsync("http://localhost:"+ node.NodePort).Result;
                    nodeInfo.NodePort = "1";//result.Content.ReadAsStringAsync();
                }
            }
            return nodeInfo.NodePort;
        }

        public static void GetFile(string fileName,string portNumber)
        {
            WebClient client = new WebClient();
            client.DownloadFile("http://localhost:" + portNumber, appConfig.NewFilesDir);
        }

        public static void PrintHelp(string note = null)
        {

        }


    }
}
