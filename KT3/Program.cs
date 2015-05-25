using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using KT3.Entities;

namespace KT3
{
    class Program
    {
        static void Main()
        {
            var network = new Network();
            for (var i = 1; i <= 5; i++)
            {
                network.AddNode(ipAddress: String.Format("{0}.{0}.{0}.{0}", i));
            }
            network.Nodes["1.1.1.1"].Neighbor.Add(network.Nodes["3.3.3.3"]);
            network.Nodes["2.2.2.2"].Neighbor.Add(network.Nodes["3.3.3.3"]);
            network.Nodes["3.3.3.3"].Neighbor.Add(network.Nodes["1.1.1.1"]);
            network.Nodes["3.3.3.3"].Neighbor.Add(network.Nodes["2.2.2.2"]);
            network.Nodes["3.3.3.3"].Neighbor.Add(network.Nodes["4.4.4.4"]);
            network.Nodes["4.4.4.4"].Neighbor.Add(network.Nodes["3.3.3.3"]);
            network.Nodes["4.4.4.4"].Neighbor.Add(network.Nodes["5.5.5.5"]);
            network.Nodes["5.5.5.5"].Neighbor.Add(network.Nodes["4.4.4.4"]);

            var sender = network.Nodes["2.2.2.2"];
            var receiver = network.Nodes["5.5.5.5"];

            var packetsWorker = new PacketsWorker();
            var joinQuery = packetsWorker.CreateJoinQuery(1, sender);
            try
            {
                //TODO: schedule logic (for refreshing routes)
                packetsWorker.BroadcastJoinQuery(joinQuery, sender, receiver);
            }
            catch (Exception)
            {
                //Join Query packet reached the receiver   
                try
                {
                    packetsWorker.BroadcastJoinReply(packetsWorker.CreateJoinReply(1, sender, receiver), receiver);
                }
                catch (Exception)
                {
                    Console.WriteLine("Sender received Join Reply packet.");
                }
            }

            Console.ReadKey();
        }
    }
}
