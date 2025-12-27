namespace AdventOfCode2020;

static class Day06
{
    static readonly string FILENAME = $"{AppContext.BaseDirectory}/resources/inputs/Day06.txt";

    static readonly string[] groupList = File.ReadAllText(FILENAME).Split("\n\n").Select(i => string.Join(" ", i.Split("\n"))).ToArray();

    static int Part01()
    {
        var totalYesCount = 0;
        foreach (var group in groupList)
        {
            var answerSet = new HashSet<char>();
            foreach (var answer in group.Split(" "))
                answerSet.UnionWith(answer.ToCharArray());
            totalYesCount += answerSet.Count;
        }
        return totalYesCount;
    }

    static int Part02()
    {
        var totalYesCount = 0;
        foreach (var group in groupList)
        {
            HashSet<char>? answerSet = null;
            foreach (var answer in group.Trim().Split(" "))
            {
                if (answerSet == null)
                    answerSet = answer.ToCharArray().ToHashSet();
                else
                    answerSet.IntersectWith(answer.ToCharArray());
            }
            totalYesCount += answerSet!.Count;
        }
        return totalYesCount;
    }

    internal static void Main()
    {
        Console.WriteLine("Advent of Code 2020, Day 06");
        Console.WriteLine(Part01());
        Console.WriteLine(Part02());
    }
}
