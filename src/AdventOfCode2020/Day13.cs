namespace AdventOfCode2020;

static class Day13
{
    const string FILENAME = "resources/inputs/Day13.txt";

    static readonly string[] Input = File.ReadAllLines(FILENAME);

    static int Part01()
    {
        var depart = int.Parse(Input[0]);
        var bus = Input[1].Split(",").Where(i => i != "x").Select(int.Parse).ToArray();
        var busToTake = 0;
        var leastDiff = depart;
        foreach (var id in bus)
        {
            var time = 0;
            while (time < depart)
            {
                time += id;
            }

            var diff = time - depart;
            if (diff < leastDiff)
            {
                leastDiff = diff;
                busToTake = id;
            }
        }
        return leastDiff * busToTake;
    }

    static long Part02()
    {
        var bus = Input[1].Split(",");
        var time = 0L;
        var inc = long.Parse(bus[0]);
        foreach (var i in Enumerable.Range(1, bus.Length - 1))
        {
            if (bus[i] != "x")
            {
                var newTime = int.Parse(bus[i]);
                while (true)
                {
                    time += inc;
                    if ((time + i) % newTime == 0L)
                    {
                        inc *= newTime;
                        break;
                    }
                }
            }
        }
        return time;
    }

    internal static void Main()
    {
        Console.WriteLine("Advent of Code 2020, Day 13");
        Console.WriteLine(Part01());
        Console.WriteLine(Part02());
    }
}
