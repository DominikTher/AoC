using AoC.Console.Interfaces;

namespace AoC.Console.Days._2025;

public sealed class Day4 : IDay
{
    public int Year => 2025;

    public int DayNumber => 4;

    public object PartOne(IEnumerable<string> rows)
    {
        var matrix = CreateMatrix(rows);
        var count = 0;

        for (var row = 0; row < matrix.GetLength(0); row++)
        {
            for (var col = 0; col < matrix.GetLength(1); col++)
            {
                if (matrix[row, col] == '@' && ValidAdjacents(matrix, row, col))
                {
                    count++;
                }
            }
        }

        return count;
    }

    public object PartTwo(IEnumerable<string> rows)
    {
        var matrix = CreateMatrix(rows);
        var count = 0;
        var toBeRemoved = new List<(int, int)>();

        do
        {
            toBeRemoved.Clear();

            for (var row = 0; row < matrix.GetLength(0); row++)
            {
                for (var col = 0; col < matrix.GetLength(1); col++)
                {
                    if (matrix[row, col] == '@' && ValidAdjacents(matrix, row, col))
                    {
                        toBeRemoved.Add((row, col));
                    }
                }
            }

            foreach (var (row, col) in toBeRemoved)
            {
                matrix[row, col] = '0';
            }

            count += toBeRemoved.Count;
        } while (toBeRemoved.Count > 0);


        return count;
    }

    private static char[,] CreateMatrix(IEnumerable<string> rows)
    {
        var matrix = new char[rows.Count(), rows.First().Length];
        var rowIndex = 0;

        foreach (var row in rows)
        {
            for (var colIndex = 0; colIndex < row.Length; colIndex++)
            {
                matrix[rowIndex, colIndex] = row[colIndex];
            }

            rowIndex++;
        }

        return matrix;
    }

    private static bool ValidAdjacents(char[,] matrix, int row, int col)
    {
        // Check the eight possible adjacent positions for count of same characters
        var directions = new (int RowOffset, int ColOffset)[]
        {
            (-1, -1), (-1, 0), (-1, 1),
            (0, -1),          (0, 1),
            (1, -1), (1, 0), (1, 1)
        };

        var adjacentCount = 0;

        foreach (var (rowOffset, colOffset) in directions)
        {
            var newRow = row + rowOffset;
            var newCol = col + colOffset;

            if (newRow >= 0 && newRow < matrix.GetLength(0) &&
                newCol >= 0 && newCol < matrix.GetLength(1) &&
                matrix[newRow, newCol] == '@')
            {
                if (++adjacentCount > 3)
                {
                    return false;
                }
            }
        }

        return true;
    }
}
