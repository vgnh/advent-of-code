using System.Diagnostics;

namespace AdventOfCode2024;

class Program
{
    static void Main(string[] args)
    {
        var stopwatch = Stopwatch.StartNew();

        Day01.Main();
        Day02.Main();

        stopwatch.Stop();
        Console.WriteLine($"\nTime elapsed: {stopwatch.ElapsedMilliseconds / 1000f:F6}s");
    }
}
