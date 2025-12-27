namespace AdventOfCode2021;

static class Day01
{
    static readonly string FILENAME = $"{AppContext.BaseDirectory}/resources/inputs/Day01.txt";

    static readonly int[] Input = File.ReadAllLines(FILENAME).Select(int.Parse).ToArray();

    static int Part01()
    {
        var count = 0;
        for (var i = 0; i < Input.Length - 1; i++)
        {
            if (Input[i + 1] > Input[i])
            {
                count++;
            }
        }
        return count;
    }

    static int Part02()
    {
        var count = 0;
        for (var i = 0; i < Input.Length - 3; i++)
        {
            if (Input[(i + 1)..(i + 4)].Sum() > Input[i..(i + 3)].Sum())
            {
                count++;
            }
        }
        return count;
    }

    internal static void Main()
    {
        Console.WriteLine("Advent of Code 2021, Day 01");
        Console.WriteLine(Part01());
        Console.WriteLine(Part02());
    }
}
