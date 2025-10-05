namespace AdventOfCode2020;

static class Day10
{
    const string FILENAME = "resources/inputs/Day10.txt";

    static readonly int[] Adapters = File.ReadAllLines(FILENAME).Select(int.Parse).ToArray();

    static List<int> AllAdapters()
    {
        var adapters = new List<int>(Adapters);
        adapters.Sort();
        adapters.Insert(0, 0);
        adapters.Add(adapters[adapters.Count - 1] + 3);
        return adapters;
    }

    static int Part01()
    {
        var adapters = AllAdapters();
        var oneCount = 0;
        var threeCount = 0;
        for (var i = 0; i < adapters.Count - 1; i++)
        {
            if (adapters[i + 1] - adapters[i] == 1)
                oneCount++;
            else if (adapters[i + 1] - adapters[i] == 3)
                threeCount++;
        }
        return oneCount * threeCount;
    }

    static long Part02(int? _index = null, long[]? _ways = null, List<int>? _adapters = null)
    {
        var adapters = _adapters ?? AllAdapters();
        var ways = _ways ?? new long[adapters.Count];
        var index = _index ?? adapters.Count - 1;

        if (index == 0)
            return 1;

        if (ways[index] != 0L)
            return ways[index];

        ways[index] = 0;
        for (var j = index - 1; j >= 0; j--)
        {
            if (adapters[index] - adapters[j] <= 3)
                ways[index] += Part02(j, ways, adapters);
            else
                break;
        }
        return ways[index];
    }

    internal static void Main()
    {
        Console.WriteLine("Advent of Code 2020, Day 10");
        Console.WriteLine(Part01());
        Console.WriteLine(Part02());
    }

    // Alternate shorter, cleaner and non-recursive solution for Part02()
    /* static long Part02()
    {
        var adapters = AllAdapters();
        var ways = new long[adapters.Count];
        for (var index = 0; index < ways.Length; index++)
        {
            if (index == 0)
                ways[index] = 1;
            else
            {
                ways[index] = 0;
                for (var j = index - 1; j >= 0; j--)
                {
                    if (adapters[index] - adapters[j] <= 3)
                        ways[index] += ways[j];
                    else
                        break;
                }
            }
        }
        return ways[ways.Length - 1];
    } */
}
