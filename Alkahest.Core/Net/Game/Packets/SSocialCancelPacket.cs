using Alkahest.Core.Game;
using Alkahest.Core.Net.Game.Serialization;

namespace Alkahest.Core.Net.Game.Packets
{
    public sealed class SSocialCancelPacket : Packet
    {
        const string Name = "S_SOCIAL_CANCEL";

        public override string OpCode => Name;

        [Packet(Name)]
        internal static Packet Create()
        {
            return new SSocialCancelPacket();
        }

        [PacketField]
        public GameId Source { get; set; }
    }
}
