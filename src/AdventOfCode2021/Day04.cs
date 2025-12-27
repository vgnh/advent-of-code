using System.Text.RegularExpressions;

namespace AdventOfCode2021;

static class Day04
{
    static readonly string FILENAME = $"{AppContext.BaseDirectory}/resources/inputs/Day04.txt";

    static readonly string[] Input = File.ReadAllText(FILENAME).Split("\n\n");

    static int[] BingoNumbers()
    {
        return Input[0].Trim().Split(",").Select(int.Parse).ToArray();
    }

    static int[][][] AllBoards()
    {
        var input = Input[1..].Select(x => x.Trim()).ToArray();
        var allBoards = new int[input.Length][][];

        foreach (var i in Enumerable.Range(0, input.Length))
        {
            var inputString = input[i];
            var strs = inputString.Split("\n");
            var board = new int[strs.Length][];

            foreach (var j in Enumerable.Range(0, strs.Length))
            {
                var line = strs[j];
                var strArray = Regex.Split(line.Trim(), @"\s+")
                    .Where(s => s.Length > 0)
                    .ToArray();

                var intArray = new int[strArray.Length];
                foreach (var k in Enumerable.Range(0, strArray.Length))
                {
                    var s = strArray[k];
                    intArray[k] = int.Parse(s);
                }

                board[j] = intArray;
            }

            allBoards[i] = board;
        }

        return allBoards;
    }

    static bool CheckWin(int[][] board)
    {
        // Check rows
        foreach (var row in board)
        {
            if (row.Sum() == -5)
            {
                return true;
            }
        }

        // Check columns
        foreach (var j in Enumerable.Range(0, board[0].Length))
        {
            var allTrueInCol = true;
            foreach (var ints in board)
            {
                if (ints[j] != -1)
                {
                    allTrueInCol = false;
                    break;
                }
            }
            if (allTrueInCol)
            {
                return true;
            }
        }

        return false;
    }

    static void MarkOnAllBoards(int n, int[][][] boards)
    {
        foreach (var board in boards)
        {
            foreach (var i in Enumerable.Range(0, board.Length))
            {
                foreach (var j in Enumerable.Range(0, board[0].Length))
                {
                    if (board[i][j] == n)
                    {
                        board[i][j] = -1;
                    }
                }
            }
        }
    }

    static int SumOfUnmarked(int[][] board)
    {
        var sum = 0;
        foreach (var ints in board)
        {
            foreach (var j in Enumerable.Range(0, board[0].Length))
            {
                if (ints[j] != -1)
                {
                    sum += ints[j];
                }
            }
        }
        return sum;
    }

    static int Part01()
    {
        var boards = AllBoards();
        foreach (var num in BingoNumbers())
        {
            MarkOnAllBoards(num, boards);
            foreach (var board in boards)
            {
                if (CheckWin(board))
                {
                    return SumOfUnmarked(board) * num;
                }
            }
        }
        return -1;
    }

    static int[][][] ArrayRemoveAll(int[][][] fromArray, List<int> indexes)
    {
        var newArray = new List<int[][]>();
        foreach (var i in Enumerable.Range(0, fromArray.Length))
        {
            var item = fromArray[i];
            if (indexes.Contains(i))
            {
                continue;
            }
            newArray.Add(item);
        }
        return newArray.ToArray();
    }

    static int Part02()
    {
        var boards = AllBoards();
        foreach (var num in BingoNumbers())
        {
            MarkOnAllBoards(num, boards);
            if (boards.Length != 1)
            {
                var boardsToRemove = new List<int>();
                foreach (var i in Enumerable.Range(0, boards.Length))
                {
                    var board = boards[i];
                    if (CheckWin(board))
                    {
                        boardsToRemove.Add(i);
                    }
                }
                boards = ArrayRemoveAll(boards, boardsToRemove);
            }
            else
            {
                if (CheckWin(boards[0]))
                {
                    return SumOfUnmarked(boards[0]) * num;
                }
            }
        }
        return -1;
    }

    internal static void Main()
    {
        Console.WriteLine("Advent of Code 2021, Day 04");
        Console.WriteLine(Part01());
        Console.WriteLine(Part02());
    }
}
