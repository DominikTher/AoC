using AoC.Console.Extensions;
using AoC.Console.Interfaces;

namespace AoC.Console.Days._2024
{
    public sealed class Day1 : IDay
    {
        public int Year => 2024;

        public int DayNumber => 1;

        public object PartOne(IEnumerable<string> rows)
        {
            var (first, second) = GetInput(rows);

            return first
                .OrderBy(x => x)
                .Zip(second.OrderBy(x => x), (a, b) => Math.Abs(a - b))
                .Sum();
        }

        public object PartTwo(IEnumerable<string> rows)
        {
            var (first, second) = GetInput(rows);

            return first.Aggregate(0, (a, b) => a + (b * second.Count(x => x == b)));
        }

        private static (IList<int> First, IList<int> Second) GetInput(IEnumerable<string> rows)
        {
            var first = new List<int>();
            var second = new List<int>();

            foreach (var row in rows.WithoutNullOrWhiteSpace())
            {
                var split = row.Split("  ");
                first.Add(int.Parse(split[0]));
                second.Add(int.Parse(split[1]));
            }

            return (first, second);
        }
    }
}