using Alkahest.Core.Cryptography;
using Alkahest.Core.IO;
using Alkahest.Core.Logging;
using Mono.Options;
using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Security.Cryptography;

namespace Alkahest.Commands
{
    sealed class DecryptCommand : AlkahestCommand
    {
        static readonly Log _log = new Log(typeof(DecryptCommand));

        const string DecryptedExtension = "dec";

        string _output;

        public DecryptCommand()
            : base("Extractor", "decrypt", "Decrypt a data center file")
        {
            Options = new OptionSet
            {
                $"Usage: {Program.Name} {Name} DCFILE KEYFILE IVFILE [OPTIONS]",
                string.Empty,
                "Available options:",
                string.Empty,
                {
                    "o|output=",
                    $"Specify output file (defaults to input file name with extension changed to `{DecryptedExtension}`)",
                    o => _output = o
                },
            };
        }

        protected override int Invoke(string[] args)
        {
            if (args.Length != 3)
            {
                Console.Error.WriteLine("Expected exactly 3 arguments");
                return 1;
            }

            var input = args[0];

            if (_output == null)
                _output = Path.ChangeExtension(input, DecryptedExtension);

            _log.Basic("Decrypting {0}...", input);

            using var aes = new RijndaelManaged
            {
                Mode = CipherMode.CFB,
                Key = ReadKey(args[1]),
                IV = ReadKey(args[2]),
                Padding = PaddingMode.Zeros,
            };

            using var transform = new PaddingCryptoTransform(aes.CreateDecryptor());
            var stream = new CryptoStream(File.OpenRead(input), transform, CryptoStreamMode.Read);
            using var reader = new BinaryReader(stream, GameBinaryReader.Encoding, true);

            reader.ReadUInt32();
            reader.ReadUInt16();

            using var stream2 = new DeflateStream(stream, CompressionMode.Decompress);
            using var stream3 = File.Open(_output, FileMode.Create, FileAccess.Write);

            stream2.CopyTo(stream3);

            _log.Basic("Decrypted data center to {0}", _output);

            return 0;
        }

        static byte[] ReadKey(string path)
        {
            return File.ReadAllLines(path).Single().Split(' ').Select(x => Convert.ToByte(x, 16)).ToArray();
        }
    }
}
