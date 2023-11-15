using AoC.Console.Interfaces;

namespace AoC.Console.Days;

internal class Day8 : IDay
{
    public int Year => 2022;

    public int DayNumber => 8;

    public object PartOne(IEnumerable<string> rows)
    {
        var data = rows.Where(row => !string.IsNullOrWhiteSpace(row)).ToList();
        var length = data[0].Length;
        var count = data.Count;
        var matrix = new int[count, length];

        for (int i = 0; i < count; i++)
        {
            var numbers = data[i].Select(character => character - '0').ToArray();
            for (int j = 0; j < length; j++)
            {
                matrix[i, j] = numbers[j];
            }
        }

        var result = 0;
        for (int i = 0; i < count; i++)
        {

            for (int j = 0; j < length; j++)
            {
                if (i == 0 || i == count - 1 || j == 0 || j == length - 1)
                {
                    result++;
                }
                else
                {
                    if (IsVisible(i, j, matrix))
                    {
                        result++;
                    }
                }
            }
        }

        return 0;
    }

    private bool IsVisible(int i, int j, int[,] matrix)
    {
        var number = matrix[i, j];
        var visibleRow = true;
        var visibleColumn = true;

        for (int a = 0; a < matrix.GetLength(0); a++)
        {
            for (int b = 0; b < matrix.GetLength(1); b++)
            {
                if (a == i && b == j)
                {
                    if (visibleRow || visibleColumn)
                    {
                        return true;
                    }

                    visibleRow = visibleColumn = true;
                    continue;
                }

                var current = matrix[a, b];

                if (a == i)
                {


                    if (number <= current)
                    {
                        visibleRow = false;
                    }
                }

                if (b == j)
                {
                    if (number <= current)
                    {
                        visibleColumn = false;
                    }
                }
            }
        }

        return visibleRow || visibleColumn;
    }

    public object PartTwo(IEnumerable<string> rows)
    {
        var data = rows.Where(row => !string.IsNullOrWhiteSpace(row)).ToList();
        var length = data[0].Length;
        var count = data.Count;
        var matrix = new int[count, length];

        for (int i = 0; i < count; i++)
        {
            var numbers = data[i].Select(character => character - '0').ToArray();
            for (int j = 0; j < length; j++)
            {
                matrix[i, j] = numbers[j];
            }
        }

        var treeLeft = 0;
        var treeRight = 0;
        var treeTop = 0;
        var treeBottom = 0;

        var result = 0;
        for (int i = 0; i < count; i++)
        {
            for (int j = 0; j < length; j++)
            {
                var number = matrix[i, j];

                var r = CheckRight(i, j, matrix);
                var l = CheckLeft(i, j, matrix);
                var b = CheckBottom(i, j, matrix);
                var t = CheckTop(i, j, matrix);

                var score = r * l * b * t;
                result = score > result ? score : result;
            }
        }

        return 0;
    }

    private int CheckTop(int i, int j, int[,] matrix)
    {
        var number = matrix[i, j];

        var result = 0;

        for (int a = i - 1; a >= 0; a--)
        {
            var n = matrix[a, j];
            if (n < number)
            {
                result++;
            }

            if (n >= number)
            {
                result++;
                break;
            }
        }

        return result;
    }

    private int CheckBottom(int i, int j, int[,] matrix)
    {
        var number = matrix[i, j];
        var length = matrix.GetLength(0);

        var result = 0;

        for (int a = i + 1; a < length; a++)
        {
            var n = matrix[a, j];
            if (n < number)
            {
                result++;
            }

            if (n >= number)
            {
                result++;
                break;
            }
        }

        return result;
    }

    private int CheckLeft(int i, int j, int[,] matrix)
    {
        var number = matrix[i, j];
        var result = 0;

        for (int a = j - 1; a >= 0; a--)
        {
            var n = matrix[i, a];
            if (n < number)
            {
                result++;
            }

            if (n >= number)
            {
                result++;
                break;
            }
        }

        return result;
    }

    private int CheckRight(int i, int j, int[,] matrix)
    {
        var number = matrix[i, j];
        var length = matrix.GetLength(0);

        var result = 0;

        for (int a = j + 1; a < length; a++)
        {
            var n = matrix[i, a];
            if(n < number)
            {
                result++;
            }

            if(n >= number)
            {
                result++;
                break;
            }
        }

        return result;
    }
}