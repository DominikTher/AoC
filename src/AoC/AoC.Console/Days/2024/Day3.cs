using AoC.Console.Extensions;
using AoC.Console.Interfaces;
using System.Text.RegularExpressions;

namespace AoC.Console.Days._2024
{
    public sealed class Day3 : IDay
    {
        public int Year => 2024;

        public int DayNumber => 3;

        public object PartOne(IEnumerable<string> rows)
        {
            return Regex
                .Matches(string.Join(string.Empty, rows.WithoutNullOrWhiteSpace()), "mul\\(\\d+,\\d+\\)")
                .Aggregate(0, (a, x) => Regex.Matches(x.Value, "\\d+").Select(x => int.Parse(x.Value)).Aggregate((a, b) => a * b) + a);
        }

        public object PartTwo(IEnumerable<string> rows)
        {
            var data = Regex
               .Matches(string.Join(string.Empty, rows.WithoutNullOrWhiteSpace()), "mul\\(\\d+,\\d+\\)|do\\(\\)|don't\\(\\)")
               .Select(x => x.Value)
               .Prepend("do()")
               .ToList();

            var sum = 0;
            var enabled = false;
            for (var i = 1; i < data.Count; i++)
            {
                if (data[i - 1] == "don't()") enabled = false;
                if (data[i - 1] == "do()") enabled = true;

                if (enabled && !data[i].StartsWith("do"))
                {
                    sum += Regex.Matches(data[i], "\\d+").Select(x => int.Parse(x.Value)).Aggregate((a, b) => a * b);
                }

            }

            return sum;
        }
    }
}
