using AoC.Console.Interfaces;

namespace AoC.Console.Days;

internal class Day4 : IDay
{
    public int Year => 2022;

    public int DayNumber => 4;

    public int PartOne(IEnumerable<string> rows)
    {
        var result = 0;

        foreach (var row in rows.Where(r => !string.IsNullOrWhiteSpace(r)))
        {
            var ranges = row.Split(",");
            var firstRange = ranges[0].Split("-").Select(int.Parse).ToArray();
            var secondRange = ranges[1].Split("-").Select(int.Parse).ToArray();
            var firstSections = Enumerable.Range(firstRange[0], firstRange[1] - firstRange[0] + 1);
            var secondections = Enumerable.Range(secondRange[0], secondRange[1] - secondRange[0] + 1);

            if (firstSections.All(secondections.Contains))
            {
                result++;
            }
            else if (secondections.All(firstSections.Contains))
            {
                result++;
            }
        }

        return result;
    }

    public int PartTwo(IEnumerable<string> rows)
    {
        var result = 0;

        foreach (var row in rows.Where(r => !string.IsNullOrWhiteSpace(r)))
        {
            var ranges = row.Split(",");
            var firstRange = ranges[0].Split("-").Select(int.Parse).ToArray();
            var secondRange = ranges[1].Split("-").Select(int.Parse).ToArray();
            var firstSections = Enumerable.Range(firstRange[0], firstRange[1] - firstRange[0] + 1);
            var secondections = Enumerable.Range(secondRange[0], secondRange[1] - secondRange[0] + 1);

            if (firstSections.Any(secondections.Contains))
            {
                result++;
            }
            else if (secondections.Any(firstSections.Contains))
            {
                result++;
            }
        }

        return result;
    }
}