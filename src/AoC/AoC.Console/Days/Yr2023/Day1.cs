using AoC.Console.Extensions;
using AoC.Console.Interfaces;
using System.Text.RegularExpressions;

namespace AoC.Console.Days.Yr2023;

public sealed class Day1 : IDay
{
    public int Year => 2023;
    public int DayNumber => 1;

    public object PartOne(IEnumerable<string> rows)
        => Solve(rows.WithoutNullOrWhiteSpace(), @"\d");

    public object PartTwo(IEnumerable<string> rows)
        => Solve(rows.WithoutNullOrWhiteSpace(), @"\d|one|two|three|four|five|six|seven|eight|nine");

    private static object Solve(IEnumerable<string> rows, string pattern)
        => rows
            .Select(row =>
            {
                var first = Regex.Match(row, pattern).Value;
                var last = Regex.Match(row, pattern, RegexOptions.RightToLeft).Value;

                return ParseMatch(first) * 10 + ParseMatch(last);
            })
            .Sum();

    private static int ParseMatch(string value) => value switch
    {
        "one" => 1,
        "two" => 2,
        "three" => 3,
        "four" => 4,
        "five" => 5,
        "six" => 6,
        "seven" => 7,
        "eight" => 8,
        "nine" => 9,
        _ => int.Parse(value)
    };

    //public object PartOne(IEnumerable<string> rows)
    //    => rows
    //        .Where(row => !string.IsNullOrWhiteSpace(row))
    //        .Select(row => row.Where(char.IsNumber))
    //        .Aggregate(0, (initial, next) => initial + int.Parse($"{next.First()}{next.Last()}"));

    //public object PartTwo(IEnumerable<string> rows)
    //{
    //    var map = new Dictionary<string, string> {
    //        { "1", "1" },
    //        { "2", "2" },
    //        { "3", "3" },
    //        { "4","4" },
    //        { "5", "5" },
    //        { "6", "6" },
    //        { "7", "7" },
    //        { "8", "8" },
    //        { "9", "9" },
    //        { "one", "1" },
    //        { "two", "2" },
    //        { "three", "3" },
    //        { "four","4" },
    //        { "five", "5" },
    //        { "six", "6" },
    //        { "seven", "7" },
    //        { "eight", "8" },
    //        { "nine", "9" },
    //    };

    //    return rows
    //            .Where(row => !string.IsNullOrWhiteSpace(row))
    //            .Select(row =>
    //            {
    //                var matches = map
    //                    .Keys
    //                    .SelectMany(key => Regex.Matches(row, key).Select(item => (item.Index, Value: map[key])))
    //                    .OrderBy(x => x.Index);

    //                var first = matches.First().Value;
    //                var last = matches.Last().Value;

    //                return $"{first}{last}";
    //            })
    //            .Aggregate(0, (initial, next) =>
    //            {
    //                return initial + int.Parse(next);
    //            });
    //}
}