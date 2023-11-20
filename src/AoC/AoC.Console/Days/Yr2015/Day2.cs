using AoC.Console.Interfaces;

namespace AoC.Console.Days.Yr2015;

public sealed class Day2 : IDay
{
    public int Year => 2015;

    public int DayNumber => 2;

    public object PartOne(IEnumerable<string> rows)
    {
        var result = 0;

        foreach (var row in rows.Where(r => !string.IsNullOrWhiteSpace(r)))
        {
            var dimensions = row
                .Split("x")
                .Select(int.Parse);

            var length = dimensions.ElementAt(0);
            var width = dimensions.ElementAt(1);
            var height = dimensions.ElementAt(2);

            var firstArea = length * width;
            var secondArea = width * height;
            var thirdArea = height * length;

            result += (2 * firstArea) + (2 * secondArea) + (2 * thirdArea) + Math.Min(firstArea, Math.Min(secondArea, thirdArea));
        }

        return result;
    }

    public object PartTwo(IEnumerable<string> rows)
    {
        var bow = 0;
        var ribbon = 0;

        foreach (var row in rows.Where(r => !string.IsNullOrWhiteSpace(r)))
        {
            var dimensions = row
                .Split("x")
                .Select(int.Parse)
                .OrderBy(number => number);

            bow += (dimensions.ElementAt(0) * dimensions.ElementAt(1) * dimensions.ElementAt(2));
            ribbon += (dimensions.ElementAt(0) * 2) + (dimensions.ElementAt(1) * 2);
        }

        return bow + ribbon;
    }
}
