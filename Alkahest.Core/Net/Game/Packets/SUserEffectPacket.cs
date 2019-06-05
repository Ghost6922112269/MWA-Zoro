using Alkahest.Core.Game;
using Alkahest.Core.Net.Game.Serialization;

namespace Alkahest.Core.Net.Game.Packets
{
    public sealed class SUserEffectPacket : Packet
    {
        const string Name = "S_USER_EFFECT";

        public override string OpCode => Name;

        [Packet(Name)]
        internal static Packet Create()
        {
            return new SUserEffectPacket();
        }

        [PacketField]
        public GameId Target { get; set; }

        [PacketField]
        public GameId Source { get; set; }

        [PacketField]
        public UserEffectKind Effect { get; set; }

        [PacketField]
        public UserEffectOperation Operation { get; set; }
    }
}