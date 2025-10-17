namespace AdventOfCode2020;

static class Day24
{
    const string FILENAME = "resources/inputs/Day24.txt";

    static readonly string[] Directions = File.ReadAllLines(FILENAME);

    static Dictionary<(int, int), bool> Part01()
    {
        var tile = new Dictionary<(int, int), bool>
        {
            [(0, 0)] = true
        };

        foreach (var direction in Directions)
        {
            var (x, y) = (0, 0);

            var i = 0;
            while (i < direction.Length)
            {
                switch (direction[i])
                {
                    case 'w': x -= 2; break;
                    case 'e': x += 2; break;
                    default:
                        y = direction[i] == 's' ? y - 1 : y + 1;
                        x = direction[i + 1] == 'e' ? x + 1 : x - 1;
                        i++;
                        break;
                }

                i++;
            }

            if (tile.ContainsKey((x, y)))
                tile[(x, y)] = !tile[(x, y)];
            else
                tile[(x, y)] = false;
        }

        Console.WriteLine(tile.Values.Count(color => color == false));
        return tile;
    }

    static int Part02()
    {
        var tile = Part01();
        var (xList, yList) = (tile.Keys.Select(x => x.Item1), tile.Keys.Select(x => x.Item2));
        var (xMin, xMax, yMin, yMax) = (xList.Min(), xList.Max(), yList.Min(), yList.Max());

        for (var days = 0; days < 100; days++)
        {
            xMin -= 1; xMax += 1; yMin -= 1; yMax += 1;

            var newTile = new Dictionary<(int, int), bool>();
            for (var j = yMax; j >= yMin; j--)
            {
                for (var i = xMin; i <= xMax; i++)
                {
                    if (!(i % 2 == 0 && j % 2 == 0 || i % 2 != 0 && j % 2 != 0)) continue;
                    newTile[(i, j)] = State((i, j), tile);
                }
            }

            tile = newTile;
        }

        return tile.Values.Count(color => color == false);
    }

    static bool State((int, int) pair, Dictionary<(int, int), bool> map)
    {
        var currentState = map.ContainsKey(pair) ? map[pair] : true;
        var (x, y) = (pair.Item1, pair.Item2);

        var white = 0;
        for (var j = y + 1; j >= y - 1; j--)
        {
            for (var i = x - 2; i <= x + 2; i++)
            {
                if ((i == x && j == y) || !(i % 2 == 0 && j % 2 == 0 || i % 2 != 0 && j % 2 != 0)) continue;
                white = map.ContainsKey((i, j))
                    ? map[(i, j)] == true ? white + 1 : white
                    : white + 1;
            }
        }

        return currentState
            ? 6 - white != 2
            : (6 - white == 0 || 6 - white > 2);
    }

    internal static void Main()
    {
        Console.WriteLine("Advent of Code 2020, Day 24");
        Console.WriteLine(Part02()); // Calls Part01()
    }
}
