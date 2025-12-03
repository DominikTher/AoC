using AoC.Console.Extensions;
using AoC.Console.Interfaces;

namespace AoC.Console.Days._2025;

public sealed class Day2 : IDay
{
    public int Year => 2025;

    public int DayNumber => 2;

    public object PartOne(IEnumerable<string> rows)
    {
        var result = 0L;

        foreach (var row in rows
            .WithoutNullOrWhiteSpace()
            .SelectMany(r => r.Split(','))
            .Select(r => r.Split('-'))
            .SelectMany(NextNumber)
            .Select(r => r.ToString())
            .Where(r => r.Length % 2 == 0))
        {
            var halfLength = row.Length / 2;
            var firstHalf = row[..halfLength];
            var secondHalf = row[halfLength..];

            if (firstHalf == secondHalf)
            {
                result += long.Parse(row);
            }
        }

        return result;

        static IEnumerable<string> NextNumber(string[] number)
        {
            var nextNumber = long.Parse(number[0]);

            while (nextNumber <= long.Parse(number[1]))
            {
                yield return nextNumber.ToString();
                nextNumber++;
            }
        }
    }

    public object PartTwo(IEnumerable<string> rows)
    {
        var result = 0L;

        foreach (var row in rows
            .WithoutNullOrWhiteSpace()
            .SelectMany(r => r.Split(','))
            .Select(r => r.Split('-'))
            .SelectMany(NextNumber)
            .Select(r => r.ToString()))
        {
            var halfLength = row.Length / 2;

            for (int i = 1; i <= halfLength; i++)
            {
                var num = row[..i];
                var repeated = string.Concat(Enumerable.Repeat(num, row.Length / num.Length));

                if (repeated == row)
                {
                    result += long.Parse(row);
                    break;
                }
            }
        }

        return result;

        static IEnumerable<string> NextNumber(string[] number)
        {
            var nextNumber = long.Parse(number[0]);

            while (nextNumber <= long.Parse(number[1]))
            {
                yield return nextNumber.ToString();
                nextNumber++;
            }
        }
    }
}
