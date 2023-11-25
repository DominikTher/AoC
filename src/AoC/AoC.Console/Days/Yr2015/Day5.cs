using AoC.Console.Interfaces;

namespace AoC.Console.Days.Yr2015;

public sealed class Day5 : IDay
{
    public int Year => 2015;

    public int DayNumber => 5;

    public object PartOne(IEnumerable<string> rows)
    {
        // Extreme overkill solution :D

        var niceStringSCount = 0;
        var vowels = new[] { 'a', 'e', 'i', 'o', 'u' };
        var badStrings = new Dictionary<char, char>
        {
            { 'a', 'b' },
            { 'c', 'd' },
            { 'p', 'q' },
            { 'x', 'y' },
        };

        foreach (var row in rows)
        {
            var vowelsCount = 0;
            var badString = false;
            var atLeastTwice = false;

            for (var i = 0; i < row.Length; i++)
            {
                if (vowels.Contains(row[i]))
                {
                    vowelsCount++;
                }

                var end = i != row.Length - 1;

                if (end && !atLeastTwice && row[i] == row[i + 1])
                {
                    atLeastTwice = true;
                }

                if (end && badStrings.TryGetValue(row[i], out char value) && value == row[i + 1])
                {
                    badString = !badString;
                    break;
                }
            }

            if (!badString && atLeastTwice && vowelsCount > 2)
            {
                niceStringSCount++;
            }
        }

        return niceStringSCount;
    }

    // With some help, tried with RegEx first
    public object PartTwo(IEnumerable<string> rows)
        => rows.Where(row => !string.IsNullOrWhiteSpace(row)).Count(row =>
        {
            var pair = Enumerable.Range(0, row.Length - 1).Any(i => row.IndexOf(row.Substring(i, 2), i + 2) >= 0);
            var repeats = Enumerable.Range(0, row.Length - 2).Any(i => row[i] == row[i + 2]);

            return pair && repeats;
        });
}
