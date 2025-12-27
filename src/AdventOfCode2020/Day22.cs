namespace AdventOfCode2020;

static class Day22
{
    static readonly string FILENAME = $"{AppContext.BaseDirectory}/resources/inputs/Day22.txt";

    static readonly int[][] Input = File.ReadAllText(FILENAME).Split("\n\n").Select(x => x.Split("\n")[1..].Where(x => x.Length > 0).Select(int.Parse).ToArray()).ToArray();

    static int Part01()
    {
        var player1 = new List<int>(Input[0]);
        var player2 = new List<int>(Input[1]);

        while (player1.Count != 0 && player2.Count != 0)
        {
            var p1 = player1[0];
            var p2 = player2[0];
            player1.RemoveAt(0);
            player2.RemoveAt(0);

            if (p1 > p2)
            {
                player1.Add(p1);
                player1.Add(p2);
                continue;
            }
            player2.Add(p2);
            player2.Add(p1);
        }

        return player1.Count != 0 ? WinnerScore(player1) : WinnerScore(player2);
    }

    static int WinnerScore(List<int> deck)
    {
        var winnerScore = 0;
        foreach (var i in Enumerable.Range(0, deck.Count))
            winnerScore += deck[i] * (deck.Count - i);
        return winnerScore;
    }

    static int Part02()
    {
        var player1 = Input[0].ToList();
        var player2 = Input[1].ToList();

        var p1Win = RecursiveCombat(player1, player2);
        return p1Win ? WinnerScore(player1) : WinnerScore(player2);
    }

    static bool RecursiveCombat(List<int> deck1, List<int> deck2)
    {
        var history = new HashSet<(string, string)>();

        while (deck1.Count != 0 && deck2.Count != 0)
        {
            var s1 = string.Join(",", deck1);
            var s2 = string.Join(",", deck2);

            if (history.Contains((s1, s2)))
                return true;
            history.Add((s1, s2));

            var p1 = deck1[0];
            var p2 = deck2[0];
            deck1.RemoveAt(0);
            deck2.RemoveAt(0);

            if (p1 <= deck1.Count && p2 <= deck2.Count)
            {
                var p1WinSubgame = RecursiveCombat(deck1[..p1], deck2[..p2]);
                if (p1WinSubgame)
                {
                    deck1.Add(p1);
                    deck1.Add(p2);
                    continue;
                }
                deck2.Add(p2);
                deck2.Add(p1);
                continue;
            }

            if (p1 > p2)
            {
                deck1.Add(p1);
                deck1.Add(p2);
                continue;
            }
            deck2.Add(p2);
            deck2.Add(p1);
        }

        // If deck1 is empty, player2 wins (returns false). If deck1 has cards, then deck2 must be empty, and player1 wins (returns true)
        return deck1.Count != 0;
    }
    internal static void Main()
    {
        Console.WriteLine("Advent of Code 2020, Day 22");
        Console.WriteLine(Part01());
        Console.WriteLine(Part02());
    }
}
