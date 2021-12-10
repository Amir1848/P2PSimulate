using SimulatedNode.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace SimulatedNode.Services
{
    public static class SimulateAppService
    {
        private static SimulateConfig appConfig;
        private static List<NodeInfo> nodesInfo;
        private static string absoluteFileNewPath = "";
        internal static void Initialize()
        {
            var deserializer = new DeserializerBuilder()
              .WithNamingConvention(UnderscoredNamingConvention.Instance)
              .Build();

            appConfig = deserializer.Deserialize<SimulateConfig>(File.OpenText("Config.yml"));
            nodesInfo = deserializer.Deserialize<NodesInfo>(File.OpenText("NodeFiles.yml")).NodeFiles.OrderBy(p => p.NodeName).ToList();
            absoluteFileNewPath  = Path.Combine(Directory.GetCurrentDirectory(), appConfig.NewFilesDir);
            Directory.CreateDirectory(absoluteFileNewPath);
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
            var commandParts = command.Split(" ");
            switch (commandParts[0].ToLower())
            {
                case "request":
                    var fileName = commandParts[1];
                    GetFile(fileName, GetNodePort(GetFileNodeNumber(fileName)));
                    break;
            }
            return CommandResult.Successful;
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
                    var port = result.Content.ReadAsStringAsync().Result;
                    if (string.IsNullOrWhiteSpace(port))
                    {
                        nodeInfo.NodePort = port;
                        break;
                    }
                }
            }
            return nodeInfo.NodePort;
        }

        public static void GetFile(string fileName,string portNumber)
        {
            DownloadFileAsync("http://localhost:" + portNumber + "/P2P/GetFile?fileName="+ fileName, Path.Combine(Directory.GetCurrentDirectory(), absoluteFileNewPath) + fileName);
        }

        public static void PrintHelp(string note = null)
        {

        }

        private static readonly HttpClient _httpClient = new HttpClient();

        public static async void DownloadFileAsync(string uri, string outputPath)
        {
            Uri uriResult;

            if (!Uri.TryCreate(uri, UriKind.Absolute, out uriResult))
                throw new InvalidOperationException("URI is invalid.");

            byte[] fileBytes = await _httpClient.GetByteArrayAsync(uri);
            File.WriteAllBytes(outputPath, fileBytes);
        }
    }


}

