using System.Text;

namespace AdventOfCode2020;

static class Day23
{
    const string FILENAME = "resources/inputs/Day23.txt";

    static readonly int[] Cups = File.ReadAllText(FILENAME).Trim().Select(x => int.Parse($"{x}")).ToArray();

    static string Part01(bool runPart02 = false)
    {
        var cups = new List<int>(Cups);

        var currentCup = cups[0];
        var minCup = cups.Min();
        if (runPart02)
        {
            for (var i = cups.Max() + 1; i <= 1_000_000; i++)
                cups.Add(i);
        }
        var maxCup = cups.Max();

        //var next = new Dictionary<int, int>(); // Alternate, but slower method
        var next = new int[cups.Count + 1];
        for (var i = 0; i < cups.Count - 1; i++)
            next[cups[i]] = cups[i + 1];
        next[cups[cups.Count - 1]] = cups[0];

        var moves = runPart02 ? 10_000_000 : 100;
        for (var i = 0; i < moves; i++)
        {
            var threeCups = new List<int>();
            var cupToInsert = currentCup;
            for (var j = 0; j < 3; j++)
            {
                cupToInsert = next[cupToInsert];
                threeCups.Add(cupToInsert);
            }

            var destCup = currentCup - 1 < minCup ? maxCup : currentCup - 1;
            while (threeCups.Contains(destCup))
                destCup = destCup - 1 < minCup ? maxCup : destCup - 1;

            // Remove the three cups from middle
            next[currentCup] = next[threeCups[threeCups.Count - 1]];

            // Add three cups after destination cup
            var nextOfDest = next[destCup];
            next[destCup] = threeCups[0];
            next[threeCups[threeCups.Count - 1]] = nextOfDest;

            currentCup = next[currentCup];
        }

        if (runPart02)
            return $"{(long)next[1] * next[next[1]]}";
        else
        {
            var cupOrder = new StringBuilder("");
            var tempCup = next[1];
            while (true)
            {
                cupOrder.Append(tempCup);
                tempCup = next[tempCup];
                if (tempCup == 1)
                    return cupOrder.ToString();
            }
        }
    }

    static string Part02() => Part01(true);


    internal static void Main()
    {
        Console.WriteLine("Advent of Code 2020, Day 23");
        Console.WriteLine(Part01());
        Console.WriteLine(Part02());
    }
}
