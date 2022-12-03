using AoC.Console.Interfaces;

namespace AoC.Console.Days;

internal sealed class Day1 : IDay
{
    public int Year => 2022;
    public int DayNumber => 1;

    public int PartOne(IEnumerable<string> rows)
    {
        var localMax = 0;
        var max = 0;
        var order = 0;

        foreach (var row in rows)
        {
            if (string.IsNullOrWhiteSpace(row))
            {
                max = Math.Max(max, localMax);
                order++;
                localMax = 0;
                continue;
            }

            localMax += int.Parse(row);
        }

        return Math.Max(max, localMax); ;
    }

    public int PartTwo(IEnumerable<string> rows)
    {
        var max = 0;
        var values = new List<int>();

        foreach (var row in rows)
        {
            if (string.IsNullOrWhiteSpace(row))
            {
                values.Add(max);
                max = 0;
                continue;
            }

            max += int.Parse(row);
        }

        return values
            .OrderByDescending(v => v)
            .Take(3)
            .Sum();
    }
}