using AoC.Console.Interfaces;
using System.Text.RegularExpressions;

namespace AoC.Console.Days._2025;

public sealed class Day6 : IDay
{
    public int Year => 2025;

    public int DayNumber => 6;

    public object PartOne(IEnumerable<string> rows)
    {
        var input = rows
            .SkipLast(1)
            .Select(row => Regex.Matches(row, @"\d+"))
            .SelectMany(pair => Enumerable
                .Range(0, pair.Count)
                .Select(col =>
                {
                    var data = pair[col];
                    return (Column: col, Number: long.Parse(data.Value));
                })
            )
            .ToArray();

        return Regex
            .Matches(rows.Last(), @"\S+")
            .Select(match => match.Value)
            .Select((operation, index) =>
            {
                return operation switch
                {
                    "+" => input.Where(i => i.Column == index).Sum(i => i.Number),
                    "*" => input.Where(i => i.Column == index).Aggregate(1L, (current, next) => current * next.Number),
                    _ => throw new NotImplementedException("Operation is not supported")
                };
            })
            .Sum();
    }

    public object PartTwo(IEnumerable<string> rows)
    {
        // HAHA

        var inputs = rows
            .SkipLast(1)
            .ToArray();

        var operations = Regex
            .Matches(rows.Last(), @"\S+")
            .Select(match => (match.Index, match.Value))
            .ToArray();

        var operationIndex = 0;
        var operation = operations[operationIndex];
        var result = 0L;
        var sum = 0L;

        for (int i = 0; i < inputs[0].Length; i++)
        {
            var tmp = string.Empty;

            for (int j = 0; j < inputs.Length; j++)
            {
                if (long.TryParse($"{inputs[j][i]}", out var num))
                    tmp += num;
            }

            if (long.TryParse(tmp, out var number))
            {
                result = operation.Value switch
                {
                    "+" => result + number,
                    "*" => (result == 0 ? 1 * number : result * number),
                    _ => throw new NotImplementedException("Operation is not supported")
                };
            }

            if (operations.Any(op => op.Index == i + 1))
            {
                operation = operations[++operationIndex];
                sum += result;
                result = 0;
            }
        }

        sum += result;

        return sum;
    }
}
