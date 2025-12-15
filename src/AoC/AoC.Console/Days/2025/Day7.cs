using AoC.Console.Extensions;
using AoC.Console.Interfaces;

namespace AoC.Console.Days._2025;

internal class Day7 : IDay
{
    public int Year => 2025;

    public int DayNumber => 7;

    public object PartOne(IEnumerable<string> rows)
    {
        string[] lines = [.. rows.WithoutNullOrWhiteSpace()];
        int r = lines.Length;
        var c = lines[0].Length;
        char[][] grid = new char[r][];

        for (int i = 0; i < r; i++)
        {
            grid[i] = lines[i].ToCharArray();
        }

        var split = 0;

        try
        {
            for (int i = 0; i < r; i++)
            {
                for (int j = 0; j < c; j++)
                {
                    var cell = grid[i][j];

                    if (cell == 'S')
                    {
                        grid[i + 1][j] = '|';
                        break;
                    }
                    else if (cell == '|' && grid[i + 1][j] == '.')
                    {
                        grid[i + 1][j] = '|';
                    }
                    else if (cell == '|' && grid[i + 1][j] == '^')
                    {
                        split++;
                        grid[i + 1][j - 1] = '|';
                        grid[i + 1][j + 1] = '|';
                    }
                }


            }
        }
        catch (Exception)
        {
        }

        //PrintGrid(grid);

        return split;
    }

    public object PartTwo(IEnumerable<string> rows)
    {
        string[] lines = [.. rows.WithoutNullOrWhiteSpace()];
        int r = lines.Length;
        var c = lines[0].Length;
        char[][] grid = new char[r][];
        var s = 0;

        for (int i = 0; i < r; i++)
        {
            grid[i] = lines[i].ToCharArray();
            if (lines[i].Contains('S'))
            {
                s = lines[0].IndexOf('S');
            }
        }

        var ways = new long[r][];

        for (int i = 0; i < r; i++)
        {
            ways[i] = new long[c];

            if (i == 0)
            {
                ways[i][s] = 1;
            }
        }

        for (int i = 0; i < r - 1; i++)
        {
            for (int j = 0; j < c; j++)
            {
                var cell = grid[i + 1][j];

                if (cell == '.')
                {
                    ways[i + 1][j] += ways[i][j];
                }

                if (cell == '^')
                {
                    var left = j - 1;
                    var right = j + 1;

                    if (left >= 0)
                    {
                        ways[i + 1][left] += ways[i][j];
                    }

                    if (right < c)
                    {
                        ways[i + 1][right] += ways[i][j];
                    }
                }
            }

            //PrintWays(ways);
        }

        return ways[r - 1].Sum();
    }

    static void PrintGrid(char[][] grid)
    {
        for (int i = 0; i < grid.Length; i++)
        {
            System.Console.WriteLine(new string(grid[i]));
        }
    }

    static void PrintWays(long[][] ways)
    {
        for (int i = 0; i < ways.Length; i++)
        {
            System.Console.WriteLine(string.Join(',', ways[i].Select(w => w.ToString().PadLeft(2, '0'))));
        }

        System.Console.WriteLine();
    }
}
