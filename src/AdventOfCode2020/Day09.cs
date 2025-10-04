namespace AdventOfCode2020;

static class Day09
{
    const string FILENAME = "resources/inputs/Day09.txt";

    static readonly long[] XMAS = File.ReadAllLines(FILENAME).Select(x => long.Parse(x)).ToArray();

    static long Part01()
    {
        const int preambleSize = 5; // Use 25 for actual input
        var preamble = new List<long>(XMAS[..preambleSize]);
        var remaining = new Queue<long>(XMAS[preambleSize..]);

        while (remaining.Count > 0)
        {
            var n = remaining.Peek();
            var exists = false;
            foreach (var i in preamble)
            {
                if (preamble.Contains(n - i))
                {
                    exists = true;
                    preamble.Add(n);
                    remaining.Dequeue();
                    preamble.RemoveAt(0);
                    break;
                }
            }
            if (!exists)
            {
                Console.WriteLine(n);
                return n;
            }
        }
        return -1;
    }

    static long Part02()
    {
        var invalid = Part01();
        for (var i = 2; i <= XMAS.Length; i++)
        {
            for (var j = 0; j <= XMAS.Length - i; j++)
            {
                var contiguous = XMAS[j..(j + i)];
                if (contiguous.Sum() == invalid)
                    return contiguous.Min() + contiguous.Max();
            }
        }
        return -1;
    }

    internal static void Main()
    {
        Console.WriteLine("Advent of Code 2020, Day 09");
        Console.WriteLine(Part02()); // Calls Part01() as well
    }
}
