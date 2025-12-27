using System.Text;

namespace AdventOfCode2020;

static class Day14
{
    static readonly string FILENAME = $"{AppContext.BaseDirectory}/resources/inputs/Day14.txt";

    static readonly string[] Input = File.ReadAllLines(FILENAME);

    static long Part01()
    {
        var mask = Input[0].Split(" = ")[1];
        var mem = new Dictionary<long, long>();

        for (var i = 1; i < Input.Length; i++)
        {
            if (Input[i].StartsWith("mem"))
            {
                var temp = Input[i].Split(" = ");
                var addr = long.Parse(temp[0][4..(temp[0].Length - 1)]);
                var num = new StringBuilder(Convert.ToString(int.Parse(temp[1]), 2).PadLeft(36, '0'));

                foreach (var j in Enumerable.Range(0, mask.Length))
                {
                    if (mask[j] == 'X') continue;
                    num[j] = mask[j];
                }
                mem[addr] = Convert.ToInt64(num.ToString(), 2);
                continue;
            }
            mask = Input[i].Split(" = ")[1];
        }

        return mem.Values.Sum();
    }

    static long Part02()
    {
        var mask = Input[0].Split(" = ")[1];
        var mem = new Dictionary<long, long>();

        for (var i = 1; i < Input.Length; i++)
        {
            if (Input[i].StartsWith("mem"))
            {
                var temp = Input[i].Split(" = ");
                var addr = new StringBuilder(
                    Convert.ToString(int.Parse(temp[0][4..(temp[0].Length - 1)]), 2).PadLeft(36, '0')
            );
                var num = long.Parse(temp[1]);

                foreach (var j in Enumerable.Range(0, mask.Length))
                {
                    if (mask[j] == '0') continue;
                    addr[j] = mask[j];
                }

                var validList = new List<string>();

                // Generate valid addresses from initial masked address
                void generateValidList(string s)
                {
                    var x = s.IndexOf('X');
                    if (x != -1)
                    {
                        var sb = new StringBuilder(s);
                        sb[x] = '0';
                        generateValidList(sb.ToString());
                        sb[x] = '1';
                        generateValidList(sb.ToString());
                    }
                    else
                        validList.Add(s);
                }
                generateValidList(addr.ToString());
                // Alternate but slower way of generating valid addresses
                /* var queue = new Queue<string>();
                queue.Enqueue(addr.ToString());
                while (queue.Count != 0)
                {
                    var str = new StringBuilder(queue.Dequeue());

                    var x = str.ToString().IndexOf('X');
                    if (x != -1)
                    {
                        str[x] = '0';
                        queue.Enqueue(str.ToString());
                        str[x] = '1';
                        queue.Enqueue(str.ToString());
                        continue;
                    }
                    validList.Add(str.ToString());
                } */

                foreach (var a in validList)
                    mem[Convert.ToInt64(a, 2)] = num;
            }
            else
                mask = Input[i].Split(" = ")[1];
        }

        return mem.Values.Sum();
    }

    internal static void Main()
    {
        Console.WriteLine("Advent of Code 2020, Day 14");
        Console.WriteLine(Part01());
        Console.WriteLine(Part02());
    }
}
