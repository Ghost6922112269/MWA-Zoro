using Alkahest.Core.Game;
using System.Numerics;

namespace Alkahest.Core.Net.Protocol.Packets
{
    public sealed class CPressSkillPacket : Packet
    {
        const string Name = "C_PRESS_SKILL";

        public override string OpCode => Name;

        [Packet(Name)]
        internal static Packet Create()
        {
            return new CPressSkillPacket();
        }

        [PacketField]
        public SkillId Skill { get; set; }

        [PacketField]
        public bool IsPress { get; set; }

        [PacketField]
        public Vector3 Position { get; set; }

        [PacketField]
        public Angle Direction { get; set; }
    }
}
