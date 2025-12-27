namespace AdventOfCode2020;

static class Day02
{
    static readonly string FILENAME = $"{AppContext.BaseDirectory}/resources/inputs/Day02.txt";

    static readonly string[] passwordPolicies = File.ReadAllLines(FILENAME);

    private readonly struct Policy
    {
        public int Min { get; init; }
        public int Max { get; init; }
        public char Letter { get; init; }
        public string Password { get; init; }
        public Policy(string str)
        {
            var policyAndPass = str.Split(": ");
            Password = policyAndPass[1];
            Letter = policyAndPass[0].Split(" ")[1][0];
            Min = int.Parse(policyAndPass[0].Split(" ")[0].Split("-")[0]);
            Max = int.Parse(policyAndPass[0].Split(" ")[0].Split("-")[1]);
        }
    }

    static int Part01()
    {
        var validCount = 0;
        foreach (var str in passwordPolicies)
        {
            var policy = new Policy(str);
            var letterCount = policy.Password.Count(i => i == policy.Letter);
            if (letterCount >= policy.Min && letterCount <= policy.Max)
                validCount++;
        }
        return validCount;
    }

    static int Part02()
    {
        var validCount = 0;
        foreach (var str in passwordPolicies)
        {
            var policy = new Policy(str);
            if ((policy.Letter == policy.Password[policy.Min - 1]) ^ (policy.Letter == policy.Password[policy.Max - 1]))
                validCount++;
        }
        return validCount;
    }

    internal static void Main()
    {
        Console.WriteLine("Advent of Code 2020, Day 02");
        Console.WriteLine(Part01());
        Console.WriteLine(Part02());
    }
}
