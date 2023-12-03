using AoC.Console.Extensions;
using AoC.Console.Interfaces;
using System.Data;

namespace AoC.Console.Days.Yr2023;

public sealed record AdjacentCell(bool Valid, char Character, int Row, int Column);

public sealed class Day3 : IDay
{
    public int Year => 2023;

    public int DayNumber => 3;

    public object PartOne(IEnumerable<string> rows)
        => Solve(rows, adjacentCell => adjacentCell.Valid)
            .Sum(x => x.Number);

    public object PartTwo(IEnumerable<string> rows)
        => Solve(rows, adjacentCell => adjacentCell.Valid && adjacentCell.Character == '*')
            .GroupBy(x => x.Coordinates)
            .Where(x => x.Count() == 2)
            .Select(x => x.Select(a => a.Number).First() * x.Select(a => a.Number).Last())
            .Sum();

    private static List<(int Number, string Coordinates)> Solve(IEnumerable<string> rows, Func<AdjacentCell, bool> predicate)
    {
        var data = rows.WithoutNullOrWhiteSpace().ToArray();
        var engineSchema = new char[data.Length, data.Max(d => d.Length)];

        for (var row = 0; row < data.Length; row++)
        {
            for (var col = 0; col < data[row].Length; col++)
            {
                engineSchema[row, col] = data[row][col];
            }
        }

        var result = new List<(int, string)>();
        var number = new List<char>();

        for (var row = 0; row < data.Length; row++)
        {
            var coordinates = string.Empty;

            for (var column = 0; column < data[row].Length; column++)
            {
                if (char.IsNumber(engineSchema[row, column]))
                {
                    number.Add(engineSchema[row, column]);
                    var adjacentCell = Adjacent(engineSchema, row, column).SingleOrDefault(predicate);

                    if (adjacentCell != null)
                    {
                        coordinates = $"{adjacentCell.Character};{adjacentCell.Row};{adjacentCell.Column}";
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(coordinates))
                    {
                        result.Add((int.Parse(string.Join("", number)), coordinates));
                    }

                    number.Clear();
                    coordinates = string.Empty;
                }
            }

            if (number.Count > 0 && !string.IsNullOrEmpty(coordinates))
            {
                result.Add((int.Parse(string.Join("", number)), coordinates));
                number.Clear();
            }
        }

        return result;
    }

    private static IEnumerable<AdjacentCell> Adjacent(char[,] engineSchema, int row, int col)
    {
        yield return Evaluate(engineSchema, row + 1, col - 1);
        yield return Evaluate(engineSchema, row + 1, col);
        yield return Evaluate(engineSchema, row + 1, col + 1);
        yield return Evaluate(engineSchema, row, col - 1);
        yield return Evaluate(engineSchema, row, col + 1);
        yield return Evaluate(engineSchema, row - 1, col - 1);
        yield return Evaluate(engineSchema, row - 1, col);
        yield return Evaluate(engineSchema, row - 1, col + 1);

        static AdjacentCell Evaluate(char[,] engineSchema, int row, int col)
        {
            try
            {
                return new(engineSchema[row, col] != '.' && !char.IsNumber(engineSchema[row, col]), engineSchema[row, col], row, col);
            }
            catch (Exception)
            {
                return new(false, char.MinValue, -1, -1);
            }
        }
    }
}
