using System.Collections.Generic;

namespace KT3.Entities
{
    public class Node
    {
        public string IpAddress { get; set; }
        public List<Node> Neighbor { get; set; } //List of neighbors and their neighbors and so on...
        public string NextNodeAddress { get; set; }
        public MessageCache MessageCache { get; set; }
    }
}
