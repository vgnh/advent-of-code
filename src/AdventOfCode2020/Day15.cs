namespace AdventOfCode2020;

static class Day15
{
    static readonly string FILENAME = $"{AppContext.BaseDirectory}/resources/inputs/Day15.txt";

    static readonly int[] Starting = File.ReadAllText(FILENAME).Split(",").Select(int.Parse).ToArray();

    static int Part01(int limit = 2020)
    {
        var prevOccurrence = new int[limit];
        //var prevOccurrence = new Dictionary<int, int>();
        var turn = 0;
        foreach (var i in Starting) prevOccurrence[i] = ++turn;

        var curr = Starting[^1];
        var next = 0;
        while (turn < limit)
        {
            turn++;
            curr = next;
            next = prevOccurrence[curr] == 0 ? 0 : turn - prevOccurrence[curr]; // Uncomment when using array
            //next = !prevOccurrence.ContainsKey(curr) ? 0 : turn - prevOccurrence[curr]; // Uncomment when using map
            prevOccurrence[curr] = turn;
        }

        return curr;
    }

    static int Part02() => Part01(30_000_000);

    internal static void Main()
    {
        Console.WriteLine("Advent of Code 2020, Day 15");
        Console.WriteLine(Part01());
        Console.WriteLine(Part02());
    }
}
