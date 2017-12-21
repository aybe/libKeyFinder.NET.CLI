using System;
using System.IO;
using System.Linq;
using CommandLine;
using libsndfile.NET;

namespace libKeyFinder.NET.CLI
{
    internal static class Program
    {
        private static int Main(string[] args)
        {
            var result = Parser.Default.ParseArguments<Options>(args);
            if (result.Errors.Any()) // prints usage
            {
                Console.WriteLine("Return codes:");
                Console.WriteLine("  Help         : -2");
                Console.WriteLine("  Error        : -1");
                foreach (var k in Enum.GetValues(typeof(Key)).Cast<Key>())
                    Console.WriteLine($"  {k,-12} : {(int) k}");

                Console.WriteLine();
                Console.WriteLine("Credits :");
                Console.WriteLine("  https://github.com/ibsh/libKeyFinder");
                Console.WriteLine("  https://github.com/aybe/libKeyFinder.NET");
                Console.WriteLine("  https://github.com/erikd/libsndfile");

                return -2;
            }

            var options = result.Value;

            var path = options.Path;

            if (!File.Exists(path))
            {
                Console.WriteLine("File not found");
                return -1;
            }

            var sf = SndFile.OpenRead(path);
            if (sf == null)
            {
                Console.WriteLine($"File couldn't be opened: '{SndFile.GetErrorMessage()}'.");
                return -1;
            }

            var format = sf.Format;
            var frames = Math.Min(Options.DefaultBlockSize, options.BlockSize);
            var samples = frames * format.Channels;
            var buffer = new double[samples];
            var totalFrames = 0L;
            var totalPercent = 0;
            var kd = new KeyDetector(format.SampleRate, format.Channels, samples);

            while (true)
            {
                var readFrames = sf.ReadFrames(buffer, frames);
                if (readFrames <= 0) break;

                var readSamples = readFrames * format.Channels;
                for (var i = 0; i < readSamples; i++)
                    kd.SetSample((uint) i, buffer[i]);
                for (var i = readSamples; i < samples; i++)
                    kd.SetSample((uint) i, 0.0d);

                kd.ProgressiveChromagram();
                totalFrames += readFrames;

                var percent = (int) ((double) totalFrames / sf.Frames * 100.0d);
                if (percent <= totalPercent) continue;
                var key1 = kd.KeyOfChromagram();
                Console.WriteLine($"{percent,6}% : {key1,-12} ({(int) key1})");
                totalPercent = percent;
            }

            kd.FinalChromagram();
            var key2 = kd.KeyOfChromagram();
            kd.Dispose();
            sf.Dispose();
            Console.WriteLine();
            Console.WriteLine($"Finale  : {key2,-12} ({(int) key2})");
            return (int) key2;
        }
    }
}