using Alkahest.Core.Game;
using Alkahest.Core.Net.Game;
using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using System.Text;

namespace Alkahest.Core.IO
{
    public sealed class GameBinaryReader : IDisposable
    {
        public static Encoding Encoding { get; } = Encoding.Unicode;

        public Stream Stream => _reader.BaseStream;

        public int Position
        {
            get => (int)Stream.Position;
            set => Stream.Position = value;
        }

        public int Length => (int)Stream.Length;

        public bool EndOfStream => Position == Length;

        readonly BinaryReader _reader;

        public GameBinaryReader(byte[] data)
            : this(new MemoryStream(data, false))
        {
        }

        public GameBinaryReader(Stream stream)
            : this(stream, false)
        {
        }

        public GameBinaryReader(Stream stream, bool leaveOpen)
        {
            _reader = new BinaryReader(stream, Encoding, leaveOpen);
        }

        public void Dispose()
        {
            _reader.Dispose();
        }

        public byte ReadByte()
        {
            return _reader.ReadByte();
        }

        public sbyte ReadSByte()
        {
            return _reader.ReadSByte();
        }

        public ushort ReadUInt16()
        {
            return _reader.ReadUInt16();
        }

        public short ReadInt16()
        {
            return _reader.ReadInt16();
        }

        public uint ReadUInt32()
        {
            return _reader.ReadUInt32();
        }

        public int ReadInt32()
        {
            return _reader.ReadInt32();
        }

        public ulong ReadUInt64()
        {
            return _reader.ReadUInt64();
        }

        public long ReadInt64()
        {
            return _reader.ReadInt64();
        }

        public float ReadSingle()
        {
            return _reader.ReadSingle();
        }

        public double ReadDouble()
        {
            return _reader.ReadDouble();
        }

        public bool ReadBoolean()
        {
            return _reader.ReadBoolean();
        }

        public string ReadString()
        {
            var list = new List<char>();

            char c;

            while ((c = (char)_reader.ReadUInt16()) != char.MinValue)
                list.Add(c);

            return new string(list.ToArray());
        }

        public Vector3 ReadVector3()
        {
            return new Vector3(_reader.ReadSingle(), _reader.ReadSingle(), _reader.ReadSingle());
        }

        public GameId ReadGameId()
        {
            return new GameId(_reader.ReadUInt64());
        }

        public SkillId ReadSkillId()
        {
            return new SkillId(_reader.ReadUInt64());
        }

        public SkillId ReadSimpleSkillId()
        {
            return SkillId.FromValues(_reader.ReadUInt32());
        }

        public Angle ReadAngle()
        {
            return new Angle(_reader.ReadInt16());
        }

        public TemplateId ReadTemplateId()
        {
            return new TemplateId(_reader.ReadUInt32());
        }

        public Appearance ReadAppearance()
        {
            return new Appearance(_reader.ReadUInt64());
        }

        public int ReadOffset()
        {
            return _reader.ReadUInt16() - PacketHeader.HeaderSize;
        }

        public byte[] ReadBytes(int count)
        {
            return _reader.ReadBytesFull(count);
        }

        public T Seek<T>(int position, Func<GameBinaryReader, int, T> func)
        {
            if (func == null)
                throw new ArgumentNullException(nameof(func));

            var pos = Position;

            Position = position;

            // This method is intentionally not using try/finally.
            var ret = func(this, pos);

            Position = pos;

            return ret;
        }

        public void Seek(int position, Action<GameBinaryReader, int> action)
        {
            if (action == null)
                throw new ArgumentNullException(nameof(action));

            Seek(position, (r, op) =>
            {
                action(r, op);

                return (object)null;
            });
        }

        public bool CanRead(int count)
        {
            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count));

            return Length - Position >= count;
        }

        public byte[] ToArray()
        {
            using var stream = new MemoryStream(PacketHeader.MaxPayloadSize);

            return Seek(0, (r, op) =>
            {
                r.Stream.CopyTo(stream);

                return stream.ToArray();
            });
        }
    }
}
