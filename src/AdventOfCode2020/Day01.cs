namespace AdventOfCode2020;

static class Day01
{
    const string FILENAME = "resources/inputs/Day01.txt";

    static readonly int[] expenseReport = File.ReadAllLines(FILENAME).Select(int.Parse).ToArray();

    static int Part01()
    {
        var expenseList = new HashSet<int>();
        foreach (var expense in expenseReport)
        {
            var diff = Math.Abs(2020 - expense);
            if (expenseList.Contains(diff))
                return expense * diff;
            else
                expenseList.Add(expense);
        }
        return -1;
    }

    static int Part02()
    {
        for (var i = 0; i < expenseReport.Length - 1; i++)
        {
            for (var j = i + 1; j < expenseReport.Length; j++)
            {
                var expenseOnePlusTwo = expenseReport[i] + expenseReport[j];
                var diff = 2020 - expenseOnePlusTwo;
                if (!(expenseOnePlusTwo < 2020) && diff < 0)
                    continue;
                if (expenseReport.Contains(diff))
                    return diff * expenseReport[i] * expenseReport[j];
            }
        }
        return -1;
    }

    internal static void Main()
    {
        Console.WriteLine("Advent of Code 2020, Day 01");
        Console.WriteLine(Part01());
        Console.WriteLine(Part02());
    }
}
