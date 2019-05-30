using Alkahest.Core.Net.Game.Serialization;

namespace Alkahest.Core.Net.Game.Packets
{
    public sealed class CShowInventoryPacket : Packet
    {
        const string Name = "C_SHOW_INVEN";

        public override string OpCode => Name;

        [Packet(Name)]
        internal static Packet Create()
        {
            return new CShowInventoryPacket();
        }

        [PacketField]
        public uint Unknown1 { get; set; }
    }
}
