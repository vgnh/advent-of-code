using System.Diagnostics;

namespace AdventOfCode2020;

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
            Day04.Main,
            Day05.Main,
            Day06.Main,
            Day07.Main,
            Day08.Main,
            Day09.Main,
            Day10.Main,
            Day11.Main,
            Day12.Main,
            // Day13.Main,
            // Day14.Main,
            // Day15.Main,
            // Day16.Main,
            // Day17.Main,
            // Day18.Main,
            // Day19.Main,
            // Day20.Main,
            // Day21.Main,
            // Day22.Main,
            // Day23.Main,
            // Day24.Main,
            // Day25.Main
        };
        actions.ForEach(act => act.Invoke());

        stopwatch.Stop();
        Console.WriteLine($"\nTime elapsed: {stopwatch.ElapsedMilliseconds / 1000f:F6}s");
    }
}
