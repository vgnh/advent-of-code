namespace AdventOfCode2021;

static class Day03
{
    static readonly string FILENAME = $"{AppContext.BaseDirectory}/resources/inputs/Day03.txt";

    static readonly string[] BinaryNumbers = File.ReadAllLines(FILENAME);

    static long Part01()
    {
        string gamma = "", epsilon = "";

        var newBinaries = new string[BinaryNumbers[0].Length];
        foreach (var bin in BinaryNumbers)
        {
            for (var i = 0; i < bin.Length; i++)
            {
                newBinaries[i] += bin[i];
            }
        }

        foreach (var newBin in newBinaries)
        {
            if (newBin.Count(x => x == '0') > newBin.Count(x => x == '1'))
            {
                gamma += "0";
                epsilon += "1";
                continue;
            }
            gamma += "1";
            epsilon += "0";
        }

        var g = Convert.ToInt32(gamma, 2);
        var ep = Convert.ToInt32(epsilon, 2);
        return g * ep;
    }

    static long Part02()
    {
        long generator = 0, scrubber = 0;
        var genList = BinaryNumbers;
        var scrubList = BinaryNumbers;

        for (var i = 0; i < BinaryNumbers[0].Length; i++)
        {
            var binGenerator = Enumerable.Repeat(string.Empty, BinaryNumbers[0].Length).ToArray();
            foreach (var bin in genList)
            {
                for (var j = 0; j < bin.Length; j++)
                {
                    binGenerator[j] += bin[j];
                }
            }

            var binScrubber = Enumerable.Repeat(string.Empty, BinaryNumbers[0].Length).ToArray();
            foreach (var bin in scrubList)
            {
                for (var j = 0; j < bin.Length; j++)
                {
                    binScrubber[j] += bin[j];
                }
            }

            if (binGenerator[i].Count(x => x == '0') > binGenerator[i].Count(x => x == '1'))
            {
                genList = genList.Where(s => s[i] == '0').ToArray();
            }
            else
            {
                genList = genList.Where(s => s[i] == '1').ToArray();
            }

            if (binScrubber[i].Count(x => x == '0') > binScrubber[i].Count(x => x == '1'))
            {
                scrubList = scrubList.Where(s => s[i] == '1').ToArray();
            }
            else
            {
                scrubList = scrubList.Where(s => s[i] == '0').ToArray();
            }

            if (genList.Length == 1)
            {
                generator = Convert.ToInt32(genList[0], 2);
            }
            if (scrubList.Length == 1)
            {
                scrubber = Convert.ToInt32(scrubList[0], 2);
            }
        }
        return generator * scrubber;
    }

    internal static void Main()
    {
        Console.WriteLine("Advent of Code 2021, Day 03");
        Console.WriteLine(Part01());
        Console.WriteLine(Part02());
    }
}
