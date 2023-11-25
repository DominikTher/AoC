using AoC.Console.Interfaces;
using System.Text.RegularExpressions;

namespace AoC.Console.Days.Yr2015;

internal class Day8 : IDay
{
    public int Year => 2015;

    public int DayNumber => 8;

    public object PartOne(IEnumerable<string> rows)
        => rows
        .Where(row => !string.IsNullOrEmpty(row))
        .Sum(row => row.Length + 2 - Regex.Unescape(row).Length);

    public object PartTwo(IEnumerable<string> rows)
        => rows
        .Where(row => !string.IsNullOrEmpty(row))
        .Sum(row => 2 + row.Replace("\\", "\\\\").Replace("\"", "\\\"").Length + 2 - (row.Length + 2));
}
