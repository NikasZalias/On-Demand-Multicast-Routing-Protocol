namespace KT3.Entities
{
    public class JoinQueryPacket
    {
        public int TimeToLive { get; set; } //Number of hops this packet can traverse.
        public string MulticastGroupIpAddress { get; set; } //The IP address of the multicast group.
        public int SequenceNumber { get; set; } //The sequence number assigned by the source to uniquely identify the packet.
        public string SourceIpAddress { get; set; } //The IP address of the node originating the packet.
        public string PreviousHopIpAddress { get; set; } //The IP address of the last node that has processed this packet.
    }
}
