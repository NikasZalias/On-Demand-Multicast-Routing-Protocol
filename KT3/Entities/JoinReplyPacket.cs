namespace KT3.Entities
{
    public class JoinReplyPacket
    {
        public string MulticastGroupIpAddress { get; set; } //The IP address of the multicast group.
        public string PreviousHopIpAddress { get; set; } //The IP address of the last node that has processed this packet.
        public int SequenceNumber { get; set; } //The sequence number assigned by the previous hop node to uniquely identify the packet.
        public string SenderIpAddress { get; set; } //The IP addresses of the sources of this multicast group.
        public string NextHopIpAddress { get; set; } //The IP addresses of next nodes that this packet is target to.
    }
}
