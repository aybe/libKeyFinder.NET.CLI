using CommandLine;

namespace libKeyFinder.NET.CLI
{
    internal class Options
    {
        public const int DefaultBlockSize = 1024;

        [Option('p', "path",
            Required = true,
            SetName = "detect",
            HelpText = "Path to an audio file"
        )]
        public string Path { get; set; }

        [Option('b', "blocksize",
            Required = false,
            SetName = "detect",
            DefaultValue = DefaultBlockSize,
            HelpText = "Block size in samples"
        )]
        public int BlockSize { get; set; }
    }
}