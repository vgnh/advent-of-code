using System.Text.RegularExpressions;

namespace AdventOfCode2020;

static class Day16
{
    static readonly string FILENAME = $"{AppContext.BaseDirectory}/resources/inputs/Day16.txt";

    static readonly string[] Input = File.ReadAllText(FILENAME).Split("\n\n").Select(x => x.Trim()).ToArray();

    static readonly string[] Rules = Input[0].Split("\n");

    static readonly string[] FullList = Input[2].Split("\n");

    static readonly string[] NearbyTickets = FullList[1..];

    static readonly int[] MyTicket = Input[1].Split("\n")[1].Split(",").Select(int.Parse).ToArray();

    static int Part01()
    {
        var listOfRanges = new List<Func<int, bool>>();

        foreach (var rule in Rules)
        {
            var match = Regex.Match(rule, @".*: ([0-9]*)-([0-9]*) or ([0-9]*)-([0-9]*)");
            var (r1, r2, r3, r4) = (int.Parse(match.Groups[1].Value), int.Parse(match.Groups[2].Value), int.Parse(match.Groups[3].Value), int.Parse(match.Groups[4].Value));
            listOfRanges.Add((num) => ((num >= r1) && (num <= r2)) || ((num >= r3) && (num <= r4)));
        }

        // Convert each string of comma separated values in NearbyTickets to an int[]
        // Then with errorRate initialized to 0
        // Take each array, select those numbers in the array that do not return true for even one of the ranges
        // Then with total initialized to errorRate, add each of them to total
        // Then errorRate <- total
        // Finally once each array in the list is exhausted, return errorRate
        return NearbyTickets.Select(str => str.Split(",").Select(int.Parse).ToArray())
            .Aggregate(0, (errorRate, arr) =>
                arr.Where(num => !listOfRanges.Any(fn => fn(num)))
                    .Aggregate(errorRate, (total, next) => total + next)
            );
    }

    static long Part02()
    {
        var departure = 1L;

        var pattern = @".*: ([0-9]*)-([0-9]*) or ([0-9]*)-([0-9]*)";

        var listOfRanges = new List<Func<int, bool>>();
        foreach (var rule in Rules)
        {
            var match = Regex.Match(rule, pattern);
            var (r1, r2, r3, r4) = (int.Parse(match.Groups[1].Value), int.Parse(match.Groups[2].Value), int.Parse(match.Groups[3].Value), int.Parse(match.Groups[4].Value));
            listOfRanges.Add((num) => (num >= r1 && num <= r2) || (num >= r3 && num <= r4));
        }

        var validTicketList = NearbyTickets
            .Select(ticket => ticket.Split(',').Select(int.Parse).ToArray())
            .Where(ticketNumbers => ticketNumbers.All(num => listOfRanges.Any(fn => fn(num))))
            .ToList();

        var ruleArrMap = new Dictionary<string, bool[]>();
        var columnCount = validTicketList[0].Length;
        foreach (var rule in Rules)
        {
            var match = Regex.Match(rule, pattern);
            var ruleName = match.Value;
            var (r1, r2, r3, r4) = (int.Parse(match.Groups[1].Value), int.Parse(match.Groups[2].Value), int.Parse(match.Groups[3].Value), int.Parse(match.Groups[4].Value));

            var colArr = new bool[columnCount];
            for (var j = 0; j < columnCount; j++)
            {
                var valid = validTicketList.All(ticket =>
                {
                    var num = ticket[j];
                    return (num >= r1 && num <= r2) || (num >= r3 && num <= r4);
                });

                if (valid)
                {
                    colArr[j] = true;
                }
            }
            ruleArrMap[ruleName] = colArr;
        }

        ruleArrMap = ruleArrMap
            .OrderBy(kvp => kvp.Value.Count(v => v))
            .ToDictionary();

        var colIgnoreList = new List<int>();
        foreach (var (key, arr) in ruleArrMap)
        {
            foreach (var col in colIgnoreList) arr[col] = false;
            if (arr.Count(x => x) == 1)
            {
                var index = Array.IndexOf(arr, true);
                if (key.StartsWith("departure"))
                {
                    departure *= MyTicket[index];
                }
                colIgnoreList.Add(index);
            }
        }

        return departure;
    }

    internal static void Main()
    {
        Console.WriteLine("Advent of Code 2020, Day 16");
        Console.WriteLine(Part01());
        Console.WriteLine(Part02());
    }
}
