using System.Security;
using System.Text;

namespace AdventOfCode2020;

static class Day18
{
    const string FILENAME = "resources/inputs/Day18.txt";

    static readonly string[] Input = File.ReadAllLines(FILENAME).Select(line => line.Replace(" ", "")).ToArray();

    static long Evaluate(string str)
    {
        var total = 0L;

        var stack = new Stack<char>(str.Reverse().ToList());
        while (true)
        {
            char ch;
            try
            {
                ch = stack.Pop();
            }
            catch (InvalidOperationException)
            {
                break;
            }

            if (ch == '(')
                total = Evaluate(InsideExpr(stack));
            else if ("*+".Contains(ch))
            {
                if (stack.Peek() == '(')
                {
                    stack.Pop();
                    if (ch == '*')
                        total *= Evaluate(InsideExpr(stack));
                    else
                        total += Evaluate(InsideExpr(stack));
                }
                else
                {
                    if (ch == '*')
                        total *= int.Parse($"{stack.Pop()}");
                    else
                        total += int.Parse($"{stack.Pop()}");
                }
            }
            else
                total = long.Parse($"{ch}");
        }

        return total;
    }

    static string InsideExpr(Stack<char> stack)
    {
        var expr = new StringBuilder("");
        var open = 0;
        while (true)
        {
            var ch = stack.Pop();
            if (ch == '(')
                open++;
            else if (ch == ')')
            {
                if (open != 0)
                    open--;
                else if (open == 0)
                    break;
            }
            expr.Append(ch);
        }
        return expr.ToString();
    }

    static long Evaluate2(string str)
    {
        var total = 0L;

        var stack = new Stack<char>(str.Reverse().ToList());
        while (true)
        {
            char ch;
            try
            {
                ch = stack.Pop();
            }
            catch (InvalidOperationException)
            {
                break;
            }

            if (ch == '(')
                total = Evaluate2(InsideExpr(stack));
            else if ("*+".Contains(ch))
            {
                if (ch == '+')
                {
                    if (stack.Peek() == '(')
                    {
                        stack.Pop();
                        total += Evaluate2(InsideExpr(stack));
                    }
                    else
                        total += int.Parse($"{stack.Pop()}");
                }
                else if (ch == '*')
                {
                    var expr = string.Concat(stack);
                    stack.Clear();
                    total *= Evaluate2(expr);
                }
            }
            else
                total = long.Parse($"{ch}");
        }

        return total;
    }

    static long Part01() => Input.Select(Evaluate).Aggregate(0L, (sum, next) => sum + next);

    static long Part02() => Input.Select(Evaluate2).Aggregate(0L, (sum, next) => sum + next);


    internal static void Main()
    {
        Console.WriteLine("Advent of Code 2020, Day 18");
        Console.WriteLine(Part01());
        Console.WriteLine(Part02());
    }
}
