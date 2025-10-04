using System.Text.RegularExpressions;

namespace AdventOfCode2020;

static class Day07
{
    const string FILENAME = "resources/inputs/Day07.txt";

    static readonly Dictionary<string, string> RuleMap = File.ReadAllLines(FILENAME).Select(x =>
        x[0..(x.Length - 1)].Replace(" bags", "").Replace(" bag", "")
    ).ToDictionary(
        x => x.Split(" contain ")[0],
        x => x.Split(" contain ")[1]
    );

    static int Part01()
    {
        var bagCount = 0;
        foreach (var key in RuleMap.Keys)
        {
            if (HasShinyGold(key))
                bagCount++;
        }
        return bagCount;
    }

    static bool HasShinyGold(string str)
    {
        if (RuleMap[str].Contains("shiny gold"))
            return true;
        else
        {
            foreach (var value in RuleMap[str].Split(", "))
            {
                if (value != "no other")
                {
                    if (!HasShinyGold(value[2..]))
                        continue;
                    else
                        return true;
                }
            }
        }
        return false;
    }

    static int Part02(string bagColor = "shiny gold")
    {
        var totalBags = 0;
        foreach (var s in RuleMap[bagColor].Split(", "))
        {
            if (s != "no other")
            {
                var num = int.Parse(s[0..1]);
                totalBags += num + num * Part02(s[2..]);
            }
            else
                break;
        }
        return totalBags;
    }

    internal static void Main()
    {
        Console.WriteLine("Advent of Code 2020, Day 07");
        Console.WriteLine(Part01());
        Console.WriteLine(Part02());
    }

    // Alternate but slower part01
    /* static int Part01()
    {
        var bagsWithSG = new HashSet<string>();

        var queue = new Queue<string>();
        queue.Enqueue("shiny gold");
        while (queue.Count > 0)
        {
            var bag = queue.Dequeue();
            var newKeys = RuleMap.Where(x => new Regex($".*{bag}.*").IsMatch(x.Value)).ToDictionary().Keys;
            foreach (var str in newKeys)
            {
                if (bagsWithSG.Contains(str))
                    continue;
                bagsWithSG.Add(str);
                queue.Enqueue(str);
            }
        }
        return bagsWithSG.Count;
    } */
}
