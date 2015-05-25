using System;
using System.Collections.Generic;
using KT3.Entities;

namespace KT3
{
    /*
     A routing table is created on demand and is maintained by each node.
   An entry is inserted or updated when a non-duplicate Join Query is
   received. The node stores the destination (i.e., the source of the
   Join Query) and the next hop to the destination (i.e., the last
   node that propagated the Join Query). The routing table provides
   the next hop information when transmitting Join Replies.
     */
    public class RoutingTable
    {
        public List<Row> Rows { get; set; }

        public RoutingTable()
        {
            Rows = new List<Row>();
        }

        public class Row
        {
            public Node Sender { get; set; } 
            public Node Reveicer { get; set; }
        }

        public void Dump()
        {
            foreach (var row in Rows)
            {
                Console.WriteLine("{0},{1}", row.Sender.IpAddress, row.Reveicer.IpAddress);
            }
        }
    }
}
