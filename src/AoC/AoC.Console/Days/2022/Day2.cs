using AoC.Console.Interfaces;

namespace AoC.Console.Days;

internal class Day2 : IDay
{
    public int Year => 2022;

    public int DayNumber => 2;

    public object PartOne(IEnumerable<string> rows)
    {
        var strategy = new Dictionary<string, (int, int)>
        {
            { "A X", (3, 1) },
            { "A Y", (6, 2) },
            { "A Z", (0, 3) },
            { "B X", (0, 1) },
            { "B Y", (3, 2) },
            { "B Z", (6, 3) },
            { "C X", (6, 1) },
            { "C Y", (0, 2) },
            { "C Z", (3, 3) }
        };

        var totalScore = 0;
        foreach (var row in rows.Where(r => !string.IsNullOrEmpty(r)))
        {
            var (score, shape) = strategy[row];
            totalScore += (score + shape);
        }

        return totalScore;
    }

    public object PartTwo(IEnumerable<string> rows)
    {
        var strategy = new Dictionary<string, (int, int)>
        {
            { "A X", (0, 3) },
            { "A Y", (3, 1) },
            { "A Z", (6, 2) },
            { "B X", (0, 1) },
            { "B Y", (3, 2) },
            { "B Z", (6, 3) },
            { "C X", (0, 2) },
            { "C Y", (3, 3) },
            { "C Z", (6, 1) }
        };

        var totalScore = 0;
        foreach (var row in rows.Where(r => !string.IsNullOrEmpty(r)))
        {
            var (score, shape) = strategy[row];
            totalScore += (score + shape);
        }

        return totalScore;
    }
}
