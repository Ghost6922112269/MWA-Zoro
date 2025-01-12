namespace Alkahest.Core.Net.Game
{
    public struct PacketHeader
    {
        public const int HeaderSize = sizeof(ushort) * 2;

        public const int MaxPayloadSize = ushort.MaxValue - HeaderSize;

        public const int MaxPacketSize = HeaderSize + MaxPayloadSize;

        public readonly ushort Length;

        public readonly ushort Code;

        public ushort FullLength => (ushort)(Length + HeaderSize);

        public PacketHeader(ushort length, ushort code)
        {
            Length = length;
            Code = code;
        }
    }
}
