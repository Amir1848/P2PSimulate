using System.Collections.Generic;

namespace SimulatedNode.Models
{
    public class NodesInfo
    {
        public List<NodeInfo> NodeFiles { get; set; }
    }

    public class NodeInfo
    {
        public long NodeName { get; set; }
        public string[] NodeFiles { get; set; }
    }
}
