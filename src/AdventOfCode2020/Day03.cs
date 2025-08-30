namespace AdventOfCode2020;

static class Day03
{
    const string FILENAME = "resources/inputs/Day03.txt";

    static readonly char[][] map = File.ReadAllLines(FILENAME).Select(i => i.Trim().ToCharArray()).ToArray();

    static int Part01(int right = 3, int down = 1)
    {
        var treeCount = 0;
        var i = 0;
        var j = 0;
        while (i < map.Length - 1)
        {
            j += right;
            i += down;
            if (j >= map[0].Length)
                j %= map[0].Length;
            if (map[i][j] == '#')
                treeCount++;
        }
        return treeCount;
    }

    static int Part02() => Part01(1, 1) * Part01() * Part01(5, 1) * Part01(7, 1) * Part01(1, 2);

    internal static void Main()
    {
        Console.WriteLine("Advent of Code 2020, Day 03");
        Console.WriteLine(Part01());
        Console.WriteLine(Part02());
    }
}
