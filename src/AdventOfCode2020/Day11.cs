namespace AdventOfCode2020;

static class Day11
{
    static readonly string FILENAME = $"{AppContext.BaseDirectory}/resources/inputs/Day11.txt";

    static char[,] Map()
    {
        var strings = File.ReadAllLines(FILENAME);
        var map = new char[strings.Length, strings[0].Length];
        for (var i = 0; i < map.GetLength(0); i++)
        {
            for (var j = 0; j < map.GetLength(1); j++)
            {
                map[i, j] = strings[i][j];
            }
        }
        return map;
    }

    static int Part01(int part = 1)
    {
        var map = Map();
        var rows = map.GetLength(0);
        var cols = map.GetLength(1);

        var map2 = CreateCopy(map);
        while (true)
        {
            var stateChange = false;
            foreach (var i in Enumerable.Range(0, rows))
            {
                foreach (var j in Enumerable.Range(0, cols))
                {
                    if (map[i, j] == 'L' && !AdjacentOccupied(part, map, i, j))
                    {
                        stateChange = true;
                        map2[i, j] = '#';
                    }
                    else if (map[i, j] == '#' && OccupiedBy(part == 1 ? 4 : 5, map, i, j))
                    {
                        stateChange = true;
                        map2[i, j] = 'L';
                    }
                }
            }
            if (!stateChange)
                break;
            else
                map = CreateCopy(map2);
        }

        return OccupiedCount(map2);
    }

    static int Part02() => Part01(2);

    static char[,] CreateCopy(char[,] src)
    {
        var copy = new char[src.GetLength(0), src.GetLength(1)];
        foreach (var i in Enumerable.Range(0, src.GetLength(0)))
            foreach (var j in Enumerable.Range(0, src.GetLength(1)))
                copy[i, j] = src[i, j];
        return copy;
    }

    static bool AdjacentOccupied(int part, char[,] map, int row, int col)
    {
        var pos = new int[,] {
            {-1, -1},
            {-1, 0},
            {-1, 1},
            {0, -1},
            {0, 1},
            {1, -1},
            {1, 0},
            {1, 1 }
        };
        if (part == 1)
        {
            foreach (var i in Enumerable.Range(0, pos.GetLength(0)))
            {
                var x = row + pos[i, 0];
                var y = col + pos[i, 1];
                if (IsValid(map, x, y) && map[x, y] == '#')
                    return true;
            }
        }
        else
        {
            foreach (var i in Enumerable.Range(0, pos.GetLength(0)))
            {
                var x = row;
                var y = col;
                while (true)
                {
                    x += pos[i, 0];
                    y += pos[i, 1];
                    if (IsValid(map, x, y))
                    {
                        if (map[x, y] == '#')
                            return true;
                        else if (map[x, y] == 'L')
                            break;
                    }
                    else
                        break;
                }
            }
        }
        return false;
    }

    static bool IsValid(char[,] map, int x, int y) => (x >= 0 && x < map.GetLength(0)) && (y >= 0 && y < map.GetLength(1));

    static bool OccupiedBy(int howMany, char[,] map, int row, int col)
    {
        var occupied = 0;
        var pos = new int[,] {
            {-1, -1},
            {-1, 0},
            {-1, 1},
            {0, -1},
            {0, 1},
            {1, -1},
            {1, 0},
            {1, 1 }
        };
        if (howMany == 4)
        {
            // Part01()
            foreach (var i in Enumerable.Range(0, pos.GetLength(0)))
            {
                var x = row + pos[i, 0];
                var y = col + pos[i, 1];
                if (IsValid(map, x, y) && map[x, y] == '#')
                    occupied++;
            }
        }
        else
        {
            // Part02()
            foreach (var i in Enumerable.Range(0, pos.GetLength(0)))
            {
                var x = row;
                var y = col;
                while (true)
                {
                    x += pos[i, 0];
                    y += pos[i, 1];
                    if (IsValid(map, x, y))
                    {
                        if (map[x, y] == '#')
                        {
                            occupied++;
                            break;
                        }
                        else if (map[x, y] == 'L')
                            break;
                    }
                    else
                        break;
                }
            }
        }
        return occupied >= howMany;
    }

    static int OccupiedCount(char[,] map)
    {
        var occupied = 0;
        foreach (var i in Enumerable.Range(0, map.GetLength(0)))
        {
            foreach (var j in Enumerable.Range(0, map.GetLength(1)))
            {
                if (map[i, j] == '#')
                    occupied++;
            }
        }
        return occupied;
    }

    internal static void Main()
    {
        Console.WriteLine("Advent of Code 2020, Day 11");
        Console.WriteLine(Part01());
        Console.WriteLine(Part02());
    }
}
