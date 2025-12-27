namespace AdventOfCode2020;

static class Day08
{
    static readonly string FILENAME = $"{AppContext.BaseDirectory}/resources/inputs/Day08.txt";

    static readonly string[] Instructions = File.ReadAllLines(FILENAME);

    static int Part01(bool runningPart02 = false)
    {
        var accumulator = 0;
        var reachedEnd = false;
        var executed = new bool[Instructions.Length];
        var i = 0;
        while (i < Instructions.Length)
        {
            if (i == Instructions.Length - 1)
                reachedEnd = true;
            if (executed[i])
                break;

            if (Instructions[i].Contains("nop"))
            {
                executed[i] = true;
                i++;
            }
            else if (Instructions[i].Contains("acc"))
            {
                accumulator += int.Parse(Instructions[i].Substring(4));
                executed[i] = true;
                i++;
            }
            else if (Instructions[i].Contains("jmp"))
            {
                int jumpTo = i + int.Parse(Instructions[i].Substring(4));
                executed[i] = true;
                i = jumpTo;
            }
        }
        // If running only Part01, return accumulator as it is
        if (!runningPart02)
            return accumulator;

        // If running again for Part02, return accumulator only if the final instruction
        // is reached. Else return -1, as final instruction has not been reached.
        if (reachedEnd)
            return accumulator;
        else
            return -1;
    }

    static int Part02()
    {
        for (var i = 0; i < Instructions.Length; i++)
        {
            if (Instructions[i][0] != 'a')
            {
                var beforeReplace = Instructions[i];
                if (Instructions[i][0] == 'n')
                    Instructions[i] = Instructions[i].Replace("nop", "jmp");
                else if (Instructions[i][0] == 'j')
                    Instructions[i] = Instructions[i].Replace("jmp", "nop");

                var accumulator = Part01(true);
                if (accumulator > 0)
                    return accumulator;
                else
                    Instructions[i] = beforeReplace;
            }
        }
        return -1;
    }

    internal static void Main()
    {
        Console.WriteLine("Advent of Code 2020, Day 08");
        Console.WriteLine(Part01());
        Console.WriteLine(Part02());
    }
}
