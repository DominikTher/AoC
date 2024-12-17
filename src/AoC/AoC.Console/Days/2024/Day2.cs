using AoC.Console.Extensions;
using AoC.Console.Interfaces;
using System.Text.RegularExpressions;

namespace AoC.Console.Days._2024
{
    public sealed partial class Day2 : IDay
    {
        public int Year => 2024;

        public int DayNumber => 2;

        public object PartOne(IEnumerable<string> rows)
        {
            var numberOfSafe = 0;
            foreach (var row in rows.WithoutNullOrWhiteSpace())
            {

                var numbers = MyRegex().Matches(row).Select(x => int.Parse(x.Value)).ToList();

                var sx = Enumerable.Zip(numbers, numbers.Skip(1)).ToList();

                var decrease = numbers.Zip(numbers.Skip(1), (x, y) =>
                {
                    return Math.Sign(x - y) == -1;
                }).All(x => x);

                var increase = numbers.Zip(numbers.Skip(1), (x, y) =>
                {
                    return Math.Sign(x - y) == 1;
                }).All(x => x);

                if (increase || decrease)
                {
                    var ss = numbers.Zip(numbers.Skip(1), (x, y) =>
                    {
                        return x - y;
                    }).All(x => Math.Abs(x) >= 1 && Math.Abs(x) <= 3);

                    if (ss) numberOfSafe++;
                }
            }

            return numberOfSafe;
        }

        public object PartTwo(IEnumerable<string> rows)
        {
            var numberOfSafe = 0;
            foreach (var row in rows.WithoutNullOrWhiteSpace())
            {

                var numbers = MyRegex().Matches(row).Select(x => int.Parse(x.Value)).ToList();

                var res = Enumerable.Range(0, numbers.Count).Select(x => Enumerable.Concat(numbers.Take(x), numbers.Skip(x + 1))).ToList();

                var input = res.Prepend(numbers);

                foreach (var item in input)
                {
                    var decrease = item.Zip(item.Skip(1), (x, y) =>
                    {
                        return Math.Sign(x - y) == -1;
                    }).All(x => x);

                    var increase = item.Zip(item.Skip(1), (x, y) =>
                    {
                        return Math.Sign(x - y) == 1;
                    }).All(x => x);

                    if (increase || decrease)
                    {
                        var ss = item.Zip(item.Skip(1), (x, y) =>
                        {
                            return x - y;
                        }).All(x => Math.Abs(x) >= 1 && Math.Abs(x) <= 3);

                        if (ss)
                        {
                            numberOfSafe++;
                            break;
                        };
                    }
                }
            }

            return numberOfSafe;
        }

        [GeneratedRegex("\\d+")]
        private static partial Regex MyRegex();
    }
}
