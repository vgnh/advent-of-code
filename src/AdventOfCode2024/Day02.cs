namespace AdventOfCode2024;

static class Day02
{
    static readonly string FILENAME = $"{AppContext.BaseDirectory}/resources/inputs/Day02.txt";

    static readonly int[][] reports = File.ReadAllLines(FILENAME)
        .Select(str => str.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(s => int.Parse(s)).ToArray())
        .ToArray();

    static int Part01()
    {
        var safe = 0;

        foreach (var report in reports)
        {
            if (IsSafe(report))
                safe++;
        }

        return safe;
    }

    static bool IsSafe(int[] report)
    {
        var safeDiff = true;
        var allInc = true;
        var allDec = true;

        for (var i = 0; i < report.Length - 1; i++)
        {
            var diff = Math.Abs(report[i] - report[i + 1]);
            if (diff > 3 || diff == 0)
            {
                safeDiff = false;
                break;
            }

            if (report[i] > report[i + 1])
            {
                allInc = false;
            }
            if (report[i] < report[i + 1])
            {
                allDec = false;
            }
        }

        return (allInc || allDec) && safeDiff;
    }

    static int Part02()
    {
        var safe = 0;

        foreach (var report in reports)
        {
            for (var i = 0; i < report.Length; i++)
            {
                var beforeIndex = report.Take(i).ToArray();
                var afterIndex = report.TakeLast(report.Length - 1 - i).ToArray();
                if (IsSafe([.. beforeIndex, .. afterIndex]))
                {
                    safe++;
                    break;
                }
            }

        }

        return safe;
    }

    public static void Main()
    {
        Console.WriteLine("Advent of Code 2024, Day 02");
        Console.WriteLine(Part01());
        Console.WriteLine(Part02());
    }
}
