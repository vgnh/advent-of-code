namespace AdventOfCode2020;

static class Day05
{
    const string FILENAME = "resources/inputs/Day05.txt";

    static readonly string[] SeatList = File.ReadAllLines(FILENAME).Select(x => x.Trim()).ToArray();

    static readonly bool[] OccupiedList = new bool[((127 * 8) + 7) + 1]; // 0-1023, total = 1024, init to false

    static int Part01()
    {
        var highestSeatId = 0;
        foreach (var seat in SeatList)
        {
            var lRange = 0;
            var uRange = 127;
            foreach (var ch in seat[..7])
            {
                if (ch == 'F')
                    uRange = (int)Math.Floor((lRange + uRange) / 2.0);
                else if (ch == 'B')
                    lRange = (int)Math.Ceiling((lRange + uRange) / 2.0);
            }
            var row = seat[6] == 'F' ? lRange : uRange;

            lRange = 0;
            uRange = 7;
            foreach (var ch in seat[7..])
            {
                if (ch == 'L')
                    uRange = (int)Math.Floor((lRange + uRange) / 2.0);
                else if (ch == 'R')
                    lRange = (int)Math.Ceiling((lRange + uRange) / 2.0);
            }
            var col = seat[9] == 'L' ? lRange : uRange;

            var seatId = (row * 8) + col;
            OccupiedList[seatId] = true; // for Part02
            if (seatId > highestSeatId)
                highestSeatId = seatId;
        }
        return highestSeatId;
    }

    static int Part02()
    {
        var skippedInitSeats = false;
        for (var i = 0; i < OccupiedList.Length; i++)
        {
            if (OccupiedList[i] && skippedInitSeats == false)
                skippedInitSeats = true;
            if (skippedInitSeats && OccupiedList[i] == false)
                return i;
        }
        return -1;
    }

    internal static void Main()
    {
        Console.WriteLine("Advent of Code 2020, Day 05");
        Console.WriteLine(Part01());
        Console.WriteLine(Part02());
    }

    // Alternate solution
    /* static readonly int[] SeatList = File.ReadAllLines(FILENAME).Select(x =>
        Convert.ToInt32(x.Trim().Replace('F', '0').Replace('B', '1').Replace('L', '0').Replace('R', '1'), 2)
    ).ToArray();

    static int Part01() => SeatList.Max();

    static int Part02()
    {
        Array.Sort(SeatList);
        for (var i = 0; i < SeatList.Length - 1; i++)
        {
            if (SeatList[i + 1] - SeatList[i] > 1)
                return SeatList[i] + 1;
        }
        return -1;
    } */
}
