namespace AdventOfCode2024;

static class Day01
{
    static readonly string FILENAME = $"{AppContext.BaseDirectory}/resources/inputs/Day01.txt";

    static (int[] Left, int[] Right) ParseFileAndReturnLists(string filename)
    {
        var lines = File.ReadAllLines(filename);
        var left = new int[lines.Length];
        var right = new int[lines.Length];
        for (var i = 0; i < lines.Length; i++)
        {
            var nums = lines[i].Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            left[i] = int.Parse(nums[0]);
            right[i] = int.Parse(nums[1]);
        }
        return (left, right);
    }

    static readonly (int[] Left, int[] Right) lists = ParseFileAndReturnLists(FILENAME);

    static int Part01()
    {
        var left = lists.Left.Order().ToArray();
        var right = lists.Right.Order().ToArray();

        var totalDistance = 0;
        for (var i = 0; i < left.Length; i++)
        {
            totalDistance += Math.Abs(right[i] - left[i]);
        }
        return totalDistance;
    }

    static int Part02()
    {
        var rightCount = new Dictionary<int, int>();
        foreach (var n in lists.Right)
        {
            if (rightCount.TryGetValue(n, out var val))
            {
                rightCount[n] = val + 1;
                continue;
            }
            rightCount[n] = 1;
        }

        var similarityScore = 0;
        foreach (var n in lists.Left)
        {
            if (rightCount.TryGetValue(n, out var val))
            {
                similarityScore += n * val;
            }
        }
        return similarityScore;
    }

    public static void Main()
    {
        Console.WriteLine("Advent of Code 2024, Day 01");
        Console.WriteLine(Part01());
        Console.WriteLine(Part02());
    }
}
