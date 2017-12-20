using System;
using libsndfile.NET;

namespace libKeyFinder.NET.CLI
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var path = @"C:\Users\Aybe\Music\Forme - Söndag.wav";

            using (var sf = SndFile.OpenRead(path))
            {
                if (sf == null)
                    throw new InvalidOperationException();

                var format = sf.Format;
                var frames = 16384;
                var channels = format.Channels;
                var samples = frames * channels;
                using (var kd = new KeyDetector(format.SampleRate, channels, samples))
                {
                    var buffer = new double[samples];
                    long read;
                    long total = 0;
                    do
                    {
                        read = sf.ReadFrames(buffer, frames);

                        var len1 = read * channels;

                        for (var i = 0; i < len1; i++)
                            kd.SetSample((uint) i, buffer[i]);

                        for (var i = len1; i < samples; i++)
                            kd.SetSample((uint) i, 0.0d);

                        kd.ProgressiveChromagram();

                        total += read;
                        var progress = (double) total / sf.Frames;
                        Console.WriteLine($"{progress:P} {kd.KeyOfChromagram()}");

                    } while (read == frames);

                    Console.WriteLine(kd.KeyOfChromagram());
                }
            }
        }
    }
}