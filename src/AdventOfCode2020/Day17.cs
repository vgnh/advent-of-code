namespace AdventOfCode2020;

static class Day17
{
    const string FILENAME = "resources/inputs/Day17.txt";

    static readonly string[] Input = File.ReadAllLines(FILENAME);

    static int Part01()
    {
        var cube = new Dictionary<(int, int, int), bool>();
        var y = Input.Length - 1;
        foreach (var i in Enumerable.Range(0, Input.Length))
        {
            var x = 0;
            var z = 0;
            foreach (var j in Enumerable.Range(0, Input[i].Length))
            {
                var ch = Input[i][j];
                cube[(x, y, z)] = ch == '#';
                x++;
            }
            y--;
        }
        var (yMin, yMax, xMin, xMax, zMin, zMax) = (0, Input.Length - 1, 0, Input[0].Length - 1, 0, 0);

        for (var cycle = 0; cycle < 6; cycle++)
        {
            --yMin; ++yMax; --xMin; ++xMax; --zMin; ++zMax;

            var newCube = new Dictionary<(int, int, int), bool>();
            for (var j = yMax; j >= yMin; j--)
            {
                for (var i = xMin; i <= xMax; i++)
                {
                    for (var k = zMin; k <= zMax; k++)
                    {
                        newCube[(i, j, k)] = State((i, j, k), cube);
                    }
                }
            }

            cube = newCube;
        }

        var active = 0;
        foreach (var key in cube.Keys)
            if (cube[key] == true) active++;

        return active;
    }

    static bool State((int, int, int) triple, Dictionary<(int, int, int), bool> map)
    {
        var currentState = map.TryGetValue(triple, out var val) ? val : false;
        var (x, y, z) = triple;
        var neighbours = 0;
        for (var j = y + 1; j >= y - 1; j--)
        {
            for (var i = x - 1; i <= x + 1; i++)
            {
                for (var k = z - 1; k <= z + 1; k++)
                {
                    if (i == x && j == y && k == z) continue;
                    neighbours = map.TryGetValue((i, j, k), out var v)
                        ? v == true ? neighbours + 1 : neighbours
                        : neighbours;
                }
            }
        }
        return currentState == true ? (neighbours == 2 || neighbours == 3) : neighbours == 3;
    }

    static int Part02()
    {
        var cube = new Dictionary<(int, int, int, int), bool>();
        var y = Input.Length - 1;
        foreach (var i in Enumerable.Range(0, Input.Length))
        {
            var x = 0;
            var z = 0;
            var w = 0;
            foreach (var j in Enumerable.Range(0, Input[i].Length))
            {
                var ch = Input[i][j];
                cube[(x, y, z, w)] = ch == '#';
                x++;
            }
            y--;
        }
        var (yMin, yMax, xMin, xMax, zMin, zMax, wMin, wMax) = (0, Input.Length - 1, 0, Input[0].Length - 1, 0, 0, 0, 0);

        for (var cycle = 0; cycle < 6; cycle++)
        {
            --yMin; ++yMax; --xMin; ++xMax; --zMin; ++zMax; --wMin; ++wMax;

            var newCube = new Dictionary<(int, int, int, int), bool>();
            for (var j = yMax; j >= yMin; j--)
            {
                for (var i = xMin; i <= xMax; i++)
                {
                    for (var k = zMin; k <= zMax; k++)
                    {
                        for (var l = wMin; l <= wMax; l++)
                        {
                            newCube[(i, j, k, l)] = State2((i, j, k, l), cube);
                        }
                    }
                }
            }

            cube = newCube;
        }

        var active = 0;
        foreach (var key in cube.Keys)
            if (cube[key] == true) active++;

        return active;
    }

    static bool State2((int, int, int, int) tuple4, Dictionary<(int, int, int, int), bool> map)
    {
        var currentState = map.TryGetValue(tuple4, out var val) ? val : false;
        var (x, y, z, w) = tuple4;
        var neighbours = 0;
        for (var j = y + 1; j >= y - 1; j--)
        {
            for (var i = x - 1; i <= x + 1; i++)
            {
                for (var k = z - 1; k <= z + 1; k++)
                {
                    for (var l = w - 1; l <= w + 1; l++)
                    {
                        if (i == x && j == y && k == z && l == w) continue;
                        neighbours = map.TryGetValue((i, j, k, l), out var v)
                            ? v == true ? neighbours + 1 : neighbours
                            : neighbours;
                    }
                }
            }
        }
        return currentState == true ? (neighbours == 2 || neighbours == 3) : neighbours == 3;
    }

    internal static void Main()
    {
        Console.WriteLine("Advent of Code 2020, Day 17");
        Console.WriteLine(Part01());
        Console.WriteLine(Part02());
    }
}
