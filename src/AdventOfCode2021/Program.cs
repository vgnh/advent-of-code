using System.Diagnostics;

namespace AdventOfCode2021;

class Program
{
    static void Main(string[] args)
    {
        var stopwatch = Stopwatch.StartNew();

        var actions = new List<Action>
        {
            Day01.Main,
            Day02.Main,
            Day03.Main,
            // Day04.Main
        };
        actions.ForEach(act => act.Invoke());

        stopwatch.Stop();
        Console.WriteLine($"\nTime elapsed: {stopwatch.ElapsedMilliseconds / 1000f:F6}s");
    }
}
