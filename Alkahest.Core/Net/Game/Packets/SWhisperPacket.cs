using Alkahest.Core.Game;
using Alkahest.Core.Net.Game.Serialization;

namespace Alkahest.Core.Net.Game.Packets
{
    public sealed class SWhisperPacket : Packet
    {
        const string Name = "S_WHISPER";

        public override string OpCode => Name;

        [Packet(Name)]
        internal static Packet Create()
        {
            return new SWhisperPacket();
        }

        [PacketField]
        public string SenderName { get; set; }

        [PacketField]
        public string RecipientName { get; set; }

        [PacketField]
        public string Message { get; set; }

        [PacketField]
        public GameId Source { get; set; }

        [PacketField]
        public bool IsWorldEventTarget { get; set; }

        [PacketField]
        public bool IsGameMaster { get; set; }

        [PacketField]
        public bool IsFounder { get; set; }
    }
}