namespace AdventOfCode2020;

static class Day25
{
    const string FILENAME = "resources/inputs/Day25.txt";

    static readonly int[] Input = File.ReadAllLines(FILENAME).Select(int.Parse).ToArray();

    static long Part01()
    {
        var encryptionKey = 1L;
        var value = 1;
        while (true)
        {
            encryptionKey = (encryptionKey * Input[1]) % 20201227;
            value = (value * 7) % 20201227;
            if (value == Input[0])
                return encryptionKey;
        }
    }

    internal static void Main()
    {
        Console.WriteLine("Advent of Code 2020, Day 25");
        Console.WriteLine(Part01());
    }
}
