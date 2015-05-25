namespace KT3.Entities
{
    /*
     The message cache is maintained by each node to detect duplicates.
   When a node receives a new Join Query or data, it stores the source
   address and the unique identifier of the packet. Note that entries
   in the message cache need not be maintained permanently. Schemes
   such as LRU (Least Recently Used) or FIFO (First In First Out) can
   be employed to expire and remove old entries and prevent the size
   of the message cache to be extensive.
     */
    public class MessageCache
    {
        public string SourceIpAddress { get; set; } //The IP address of the node originating the packet.
        public int? SequenceNumber { get; set; } //The sequence number assigned by the source to uniquely identify the packet.
    }
}
