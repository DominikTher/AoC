using AoC.Console.Extensions;
using AoC.Console.Interfaces;

namespace AoC.Console.Days._2024;

public sealed class Day4 : IDay
{
    public int Year => 2024;

    public int DayNumber => 4;

    public object PartOne(IEnumerable<string> rows)
    {
        var data = PrepareData(rows);
        var numberOfXmas = 0;

        for (var row = 0; row < data.GetLength(0); row++)
        {
            for (var column = 0; column < data.GetLength(1); column++)
            {
                var searchable = new[] {
                    ContainsPartOne(data, row, column, "XMAS", 0, r => r, c => c + 1) || ContainsPartOne(data, row, column, "SAMX", 0, r => r, c => c + 1),
                    ContainsPartOne(data, row, column, "XMAS", 0, r => r + 1, c => c + 1) || ContainsPartOne(data, row, column, "SAMX", 0, r => r + 1, c => c + 1),
                    ContainsPartOne(data, row, column, "XMAS", 0, r => r - 1, c => c + 1) || ContainsPartOne(data, row, column, "SAMX", 0, r => r - 1, c => c + 1),
                    ContainsPartOne(data, row, column, "XMAS", 0, r => r + 1, c => c) || ContainsPartOne(data, row, column, "SAMX", 0, r => r + 1, c => c)
                };

                numberOfXmas += searchable.Count(x => x);
            }
        }

        return numberOfXmas;
    }

    private static bool ContainsPartOne(char[,] data, int row, int column, string value, int index, Func<int, int> rowIndex, Func<int, int> columnIndex)
    {
        var maxRow = data.GetLength(0);
        var maxColumn = data.GetLength(1);
        var result = false;

        if (value.Length == index)
        {
            return true;
        }

        if (maxRow > row && maxColumn > column && row >= 0 && column >= 0 && index < value.Length && data[row, column] == value[index])
        {
            result = ContainsPartOne(data, rowIndex(row), columnIndex(column), value, ++index, rowIndex, columnIndex);
        }

        return result;
    }

    public object PartTwo(IEnumerable<string> rows)
    {
        var data = PrepareData(rows);
        var numberOfXmas = 0;

        for (var row = 1; row < data.GetLength(0) - 1; row++)
        {
            for (var column = 1; column < data.GetLength(1) - 1; column++)
            {
                if (ContainsPartTwo(data, row, column))
                {
                    numberOfXmas++;
                }
            }
        }

        return numberOfXmas;
    }

    private static bool ContainsPartTwo(char[,] data, int row, int column)
    {
        if (data[row, column] == 'A')
        {
            if (data[row + 1, column - 1] == 'M' && data[row + 1, column + 1] == 'M' && data[row - 1, column + 1] == 'S' && data[row - 1, column - 1] == 'S')
            {
                return true;
            }

            if (data[row + 1, column - 1] == 'S' && data[row + 1, column + 1] == 'M' && data[row - 1, column + 1] == 'M' && data[row - 1, column - 1] == 'S')
            {
                return true;
            }

            if (data[row + 1, column - 1] == 'S' && data[row + 1, column + 1] == 'S' && data[row - 1, column + 1] == 'M' && data[row - 1, column - 1] == 'M')
            {
                return true;
            }

            if (data[row + 1, column - 1] == 'M' && data[row + 1, column + 1] == 'S' && data[row - 1, column + 1] == 'S' && data[row - 1, column - 1] == 'M')
            {
                return true;
            }
        }

        return false;
    }

    private static char[,] PrepareData(IEnumerable<string> rows)
    {
        var lines = rows.WithoutNullOrWhiteSpace().ToList();
        var data = new char[lines.Count, lines[0].Length];

        for (var row = 0; row < lines.Count; row++)
        {
            for (var column = 0; column < lines[row].Length; column++)
            {
                data[row, column] = lines[row][column];
            }
        }

        return data;
    }
}
