namespace AdventOfCode2025;

static class Day01
{
    const string FILENAME = "resources/inputs/Day01.txt";

    static readonly string[] rotations = File.ReadAllLines(FILENAME);

    static int Part01()
    {
        var password = 0;
        var dialPos = 50;

        foreach (var rotation in rotations)
        {
            var dir = rotation[0];
            var clicks = int.Parse(rotation[1..]);

            dialPos = dir switch
            {
                'L' => (dialPos - clicks + 100) % 100,
                _ => (dialPos + clicks) % 100,
            };

            if (dialPos == 0)
                password++;
        }
        return password;
    }

    static int Part02()
    {
        var password = 0;
        var dialPos = 50;

        foreach (var rotation in rotations)
        {
            var dir = rotation[0];
            var clicks = int.Parse(rotation[1..]);

            for (var i = 0; i < clicks; i++)
            {
                switch (dir)
                {
                    case 'L':
                        dialPos--;
                        if (dialPos == 0)
                        {
                            password++;
                            continue;
                        }
                        if (dialPos < 0)
                        {
                            dialPos = 99;
                        }
                        break;
                    default:
                        dialPos++;
                        if (dialPos > 99)
                        {
                            dialPos = 0;
                            password++;
                        }
                        break;
                }
            }
        }
        return password;
    }

    internal static void Main()
    {
        Console.WriteLine("Advent of Code 2025, Day 01");
        Console.WriteLine(Part01());
        Console.WriteLine(Part02());
    }
}
