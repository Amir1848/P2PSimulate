using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimulatedNode.Models
{
    public class SimulateConfig
    {
        public long NodeNumber { get; set; }
        public long NodePort { get; set; }
        public string OwnedFilesDir { get; set; }
        public string NewFilesDir { get; set; }
        public List<string> OwnedFiles { get; set; }
        public List<FriendNode> FriendNodes { get; set; }
    }

    public class FriendNode
    {
        public long NodeName { get; set; }
        public long NodePort { get; set; }
    }
}
