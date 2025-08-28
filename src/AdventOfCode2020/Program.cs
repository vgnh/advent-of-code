using System.Diagnostics;

namespace AdventOfCode2020;

class Program
{
    static async Task Main(string[] args)
    {
        var stopwatch = Stopwatch.StartNew();

        await Task.Run(Day01.Main);
        await Task.Run(Day02.Main);

        stopwatch.Stop();
        Console.WriteLine($"\nTime elapsed: {stopwatch.ElapsedMilliseconds / 1000f:F6}s");
    }
}
