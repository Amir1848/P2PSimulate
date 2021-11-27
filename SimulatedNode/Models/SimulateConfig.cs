using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimulatedNode.Models
{
    public class SimulateConfig
    {
        public long NodeNumber { get; set; }
        public string NodePort { get; set; }
        public string OwnedFilesDir { get; set; }
        public string NewFilesDir { get; set; }
        public List<string> OwnedFiles { get; set; }
        public List<FriendNode> FriendNodes { get; set; }

        public SimulateConfig cloneConfig()
        {
            return new SimulateConfig()
            {
                NodeNumber = this.NodeNumber,
                NodePort = this.NodePort,
                FriendNodes = this.FriendNodes,
                NewFilesDir = this.NewFilesDir,
                OwnedFiles = this.OwnedFiles,
                OwnedFilesDir = this.OwnedFilesDir,
            };
        }
    }

    public class FriendNode
    {
        public long NodeName { get; set; }
        public string NodePort { get; set; }
    }
}
