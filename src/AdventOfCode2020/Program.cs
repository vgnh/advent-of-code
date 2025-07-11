using System.Diagnostics;

namespace AdventOfCode2020;

class Program
{
    static void Main(string[] args)
    {
        var stopwatch = Stopwatch.StartNew();

        Day01.Main();

        stopwatch.Stop();
        Console.WriteLine($"\nTime elapsed: {stopwatch.ElapsedMilliseconds / 1000f:F6}s");
    }
}
