using System;
using KT3.Entities;

namespace KT3
{
    public class PacketsWorker //TODO: Multicast
    {
        public RoutingTable RoutingTable { get; private set; }

        public PacketsWorker()
        {
            RoutingTable = new RoutingTable();
        }

        public JoinQueryPacket CreateJoinQuery(int sequenceNumber, Node node)
        {
            node.MessageCache.SequenceNumber = sequenceNumber;
            node.MessageCache.SourceIpAddress = node.IpAddress;
            return new JoinQueryPacket
            {
                TimeToLive = 10,
                SequenceNumber = sequenceNumber,
                SourceIpAddress = node.IpAddress,
                PreviousHopIpAddress = node.IpAddress
            };
        }

        public JoinQueryPacket ProcessingJoinQuery(JoinQueryPacket joinQueryPacket, Node sender, Node node)
        {
            if (joinQueryPacket.SourceIpAddress == node.MessageCache.SourceIpAddress &&
                joinQueryPacket.SequenceNumber == node.MessageCache.SequenceNumber)
            {
                Console.WriteLine("Discarded.");
                return null;
            }
            node.MessageCache.SourceIpAddress = joinQueryPacket.SourceIpAddress;
            node.MessageCache.SequenceNumber = joinQueryPacket.SequenceNumber;

            //Update Routing Table
            RoutingTable.Rows.Add(new RoutingTable.Row
            {
                Sender = sender, 
                Reveicer = node
            });

            joinQueryPacket.TimeToLive--;
            if (joinQueryPacket.TimeToLive <= 0)
            {
                return null;
            }
            joinQueryPacket.PreviousHopIpAddress = node.IpAddress;
            
            return joinQueryPacket;
        }

        public void BroadcastJoinQuery(JoinQueryPacket joinQueryPacket, Node sender, Node receiver)
        {
            foreach (var node in sender.Neighbor)
            {
                if (joinQueryPacket != null && receiver != sender)
                {
                    Console.WriteLine("{0} ---> {1}",sender.IpAddress, node.IpAddress);
                    BroadcastJoinQuery(ProcessingJoinQuery(joinQueryPacket, sender, node), node, receiver);
                }
                else if (receiver == sender)
                {
                    Console.WriteLine("Join Query Packet reached the receiver.");
                    throw new Exception();
                }
            }
        }

        public JoinReplyPacket CreateJoinReply(int sequenceNumber, Node sender, Node nextHop)
        {
            return new JoinReplyPacket
            {
                SenderIpAddress = sender.IpAddress,
                NextHopIpAddress = nextHop.IpAddress
            };
        }

        public JoinReplyPacket ProcessingJoinReply(JoinReplyPacket joinReplyPacket, Node node, Node nextHop)
        {
            if (joinReplyPacket.NextHopIpAddress != node.IpAddress)
            {
                return null;
            }
            joinReplyPacket.NextHopIpAddress = nextHop.IpAddress;
            return joinReplyPacket;
        }

        public void BroadcastJoinReply(JoinReplyPacket joinReplyPacket, Node sender)
        {
            if (sender.IpAddress == joinReplyPacket.SenderIpAddress)
            {
                throw new Exception();
            }
            var nextHop = RoutingTable.Rows.Find(x => sender == x.Reveicer).Sender;
            Console.WriteLine("{0} ---> {1}", sender.IpAddress, nextHop.IpAddress);
            var newJoinReplyPacket = ProcessingJoinReply(joinReplyPacket, sender, nextHop);
            BroadcastJoinReply(newJoinReplyPacket, nextHop);
        }
    }
}