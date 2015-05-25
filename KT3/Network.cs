using System.Collections.Generic;
using KT3.Entities;

namespace KT3
{
    public class Network : INetwork
    {
        public Dictionary<string, Node> Nodes { get; private set; }

        public Network()
        {
            Nodes = new Dictionary<string, Node>();
        }

        public void AddNode(string ipAddress) //Adds a new node to dictionary     
        {
            Nodes.Add(ipAddress, new Node
            {
                IpAddress = ipAddress,
                Neighbor = new List<Node>(),
                NextNodeAddress = null,
                MessageCache = new MessageCache
                {
                    SequenceNumber = null,
                    SourceIpAddress = null
                }
            });
        }
    }
}
