using Alkahest.Core.Game;

namespace Alkahest.Core.Net.Protocol.Packets
{
    public sealed class SAbnormalityDamageAbsorbPacket : Packet
    {
        const string Name = "S_ABNORMALITY_DAMAGE_ABSORB";

        public override string OpCode => Name;

        [Packet(Name)]
        internal static Packet Create()
        {
            return new SAbnormalityDamageAbsorbPacket();
        }

        [PacketField]
        public GameId Target { get; set; }

        [PacketField]
        public uint Damage { get; set; }
    }
}
