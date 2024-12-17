using AoC.Console.Extensions;
using AoC.Console.Interfaces;

namespace AoC.Console.Days.Yr2023;

public sealed class Day9 : IDay
{
    public int Year => 2023;

    public int DayNumber => 9;

    public object PartOne(IEnumerable<string> rows)
        => Solve(rows.WithoutNullOrWhiteSpace(), (a, b) => a + b.Last());
   
    public object PartTwo(IEnumerable<string> rows)
        => Solve(rows.WithoutNullOrWhiteSpace(), (a, b) => b.First() - a);

    private static object Solve(IEnumerable<string> rows, Func<int, List<int>, int> aggregate)
        => rows.Select(row =>
        {
            var numbers = row.Split(' ').Select(int.Parse);
            var sequences = new List<List<int>> { new(numbers) };
            var lastSequence = numbers;
            while (!sequences.Last().All(number => number == 0))
            {
                var nextSequence = lastSequence.Skip(1).Select((number, index) => number - lastSequence.ElementAt(index));
                sequences.Add(new(nextSequence));
                lastSequence = sequences.Last();
            }

            sequences.Reverse();

            return sequences.Aggregate(0, aggregate);
        })
        .Sum();
}
