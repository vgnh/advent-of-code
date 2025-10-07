namespace AdventOfCode2020;

static class Day12
{
    const string FILENAME = "resources/inputs/Day12.txt";

    static readonly string[] Instructions = File.ReadAllLines(FILENAME);

    static readonly Dictionary<char, int[]> Values = new()
    {
        {'N', [0, 1]},
        {'E', [1, 0]},
        {'S', [0, -1]},
        {'W', [-1, 0]},
    };

    static int Part01()
    {
        var pos = 1; // "NESW"[pos] is 'E'
        var x = 0;
        var y = 0;

        foreach (var ins in Instructions)
        {
            var num = int.Parse(ins[1..]);
            if (ins[0] == 'F')
            {
                x += Values["NESW"[pos]][0] * num;
                y += Values["NESW"[pos]][1] * num;
            }
            else if (Values.TryGetValue(ins[0], out int[]? v))
            {
                x += v[0] * num;
                y += v[1] * num;
            }
            else
            {
                var turns = num / 90;
                foreach (var i in Enumerable.Range(0, turns))
                {
                    if (ins[0] == 'R')
                        pos = pos + 1 == 4 ? 0 : pos + 1;
                    else if (ins[0] == 'L')
                        pos = pos - 1 == -1 ? 3 : pos - 1;
                }
            }
        }

        return Math.Abs(x) + Math.Abs(y);
    }

    static int Part02()
    {
        var x = 0;
        var y = 0;
        var wayX = 10;
        var wayY = 1;

        foreach (var ins in Instructions)
        {
            var num = int.Parse(ins[1..]);
            if (ins[0] == 'F')
            {
                x += wayX * num;
                y += wayY * num;
            }
            else if (Values.TryGetValue(ins[0], out int[]? v))
            {
                wayX += v[0] * num;
                wayY += v[1] * num;
            }
            else
            {
                var turns = num / 90;
                foreach (var i in Enumerable.Range(0, turns))
                {
                    if (ins[0] == 'R')
                    {
                        var swap = wayY;
                        wayY = -1 * wayX;
                        wayX = swap;
                    }
                    else if (ins[0] == 'L')
                    {
                        var swap = wayX;
                        wayX = -1 * wayY;
                        wayY = swap;
                    }
                }
            }
        }

        return Math.Abs(x) + Math.Abs(y);
    }

    internal static void Main()
    {
        Console.WriteLine("Advent of Code 2020, Day 12");
        Console.WriteLine(Part01());
        Console.WriteLine(Part02());
    }
}
