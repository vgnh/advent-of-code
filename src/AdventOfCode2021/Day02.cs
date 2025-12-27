namespace AdventOfCode2021;

static class Day02
{
    static readonly string FILENAME = $"{AppContext.BaseDirectory}/resources/inputs/Day02.txt";

    static readonly string[] Instructions = File.ReadAllLines(FILENAME);

    static int Part01()
    {
        int horizontal = 0, depth = 0;
        foreach (var ins in Instructions)
        {
            var command = ins.Trim().Split(" ");
            var value = int.Parse(command[1]);
            switch (command[0])
            {
                case "forward":
                    horizontal += value;
                    break;
                case "down":
                    depth += value;
                    break;
                case "up":
                    depth -= value;
                    break;
            }
        }
        return horizontal * depth;
    }

    static int Part02()
    {
        int horizontal = 0, depth = 0, aim = 0;

        foreach (var ins in Instructions)
        {
            var command = ins.Trim().Split(" ");
            var value = int.Parse(command[1]);
            switch (command[0])
            {
                case "forward":
                    horizontal += value;
                    depth += aim * value;
                    break;
                case "down":
                    aim += value;
                    break;
                case "up":
                    aim -= value;
                    break;
            }
        }
        return horizontal * depth;
    }

    internal static void Main()
    {
        Console.WriteLine("Advent of Code 2021, Day 02");
        Console.WriteLine(Part01());
        Console.WriteLine(Part02());
    }
}
