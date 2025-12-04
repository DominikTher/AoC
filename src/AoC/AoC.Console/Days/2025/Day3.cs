using AoC.Console.Extensions;
using AoC.Console.Interfaces;

namespace AoC.Console.Days._2025;

public sealed class Day3 : IDay
{
    public int Year => 2025;

    public int DayNumber => 3;

    public object PartOne(IEnumerable<string> rows)
    {
        var total = 0;

        foreach (var row in rows.WithoutNullOrWhiteSpace())
        {
            var numbers = row
                .Select((c, i) => (Index: i + 1, Number: char.GetNumericValue(c)))
                .ToList();

            var first = numbers[..^1].MaxBy(x => x.Number);
            var second = numbers[first.Index..].MaxBy(x => x.Number);

            total += int.Parse($"{first.Number}{second.Number}");
        }

        return total;
    }

    public object PartTwo(IEnumerable<string> rows)
    {
        var total = 0L;

        foreach (var numbers in rows.WithoutNullOrWhiteSpace().Select(r => r.Select((c, i) => (Index: i, Number: char.GetNumericValue(c))).ToList()))
        {
            var result = string.Empty;

            for (var i = 0; i < numbers.Count;)
            {
                var max = numbers[i];

                for (int j = i + 1; j < numbers.Count - (12 - result.Length - 1); j++)
                {
                    if (numbers[j].Number > max.Number)
                    {
                        max = numbers[j];
                    }
                }

                if (i < max.Index)
                    i = max.Index + 1;
                else
                    i++;

                result += max.Number;

                if (result.Length == 12)
                    break;
            }

            total += long.Parse(result);
        }

        return total;
    }
}