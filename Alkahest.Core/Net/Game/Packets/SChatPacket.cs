using Alkahest.Core.Game;

namespace Alkahest.Core.Net.Game.Packets
{
    public sealed class SChatPacket : Packet
    {
        const string Name = "S_CHAT";

        public override string OpCode => Name;

        [Packet(Name)]
        internal static Packet Create()
        {
            return new SChatPacket();
        }

        [PacketField]
        public string SenderName { get; set; }

        [PacketField]
        public string Message { get; set; }

        [PacketField]
        public ChatChannel Channel { get; set; }

        [PacketField]
        public GameId Source { get; set; }

        [PacketField]
        public byte Unknown1 { get; set; }

        [PacketField]
        public bool IsGameMaster { get; set; }

        [PacketField]
        public byte Unknown2 { get; set; }
    }
}
