using AoC.Console.Extensions;
using AoC.Console.Interfaces;
using System.Text.RegularExpressions;

namespace AoC.Console.Days.Yr2023;

public sealed class Day4 : IDay
{
    public int Year => 2023;

    public int DayNumber => 4;

    public object PartOne(IEnumerable<string> rows)
        => rows
            .WithoutNullOrWhiteSpace()
            .Select(row =>
            {
                var card = row.Split(":");
                var numbers = card[1].Split("|");
                var winning = Regex.Matches(numbers[0], @"\d+").Select(m => int.Parse(m.Value));
                var myNumbers = Regex.Matches(numbers[1], @"\d+").Select(m => int.Parse(m.Value));
                var myWinning = myNumbers.Intersect(winning).Count();

                return (int)Math.Pow(2, myWinning - 1);
            })
            .Sum();

    public object PartTwo(IEnumerable<string> rows)
    {
        var instances = new Dictionary<int, int>();
        foreach (var row in rows.WithoutNullOrWhiteSpace())
        {
            var card = row.Split(":");
            var id = int.Parse(Regex.Match(card[0], @"\d+").Value);
            var numbers = card[1].Split("|");
            var winning = Regex.Matches(numbers[0], @"\d+").Select(m => int.Parse(m.Value));
            var myNumbers = Regex.Matches(numbers[1], @"\d+").Select(m => int.Parse(m.Value));
            var myWinning = myNumbers.Intersect(winning).Count();

            if (!instances.TryGetValue(id, out var value))
            {
                instances.Add(id, 1);
            }
            else
            {
                instances[id] = value + 1;
            }

            foreach (var item in Enumerable.Range(id + 1, myWinning))
            {
                if (!instances.TryGetValue(item, out value))
                {
                    instances.Add(item, 1);
                }

                instances[item] = value + instances[id];
            }
        }

        return instances.Sum(x => x.Value);
    }
}
