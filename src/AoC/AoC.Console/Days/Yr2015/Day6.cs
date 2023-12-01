using AoC.Console.Interfaces;

namespace AoC.Console.Days.Yr2015;

public sealed class Day6 : IDay
{
    public int Year => 2015;

    public int DayNumber => 6;

    public object PartOne(IEnumerable<string> rows)
    {
        var grid = new bool[1000, 1000];
        var commands = new[] { "turn on", "turn off", "toggle" };

        foreach (var row in rows.Where(r => !string.IsNullOrWhiteSpace(r)))
        {
            var command = commands.First(row.Contains);
            var ranges = row.Replace(command, string.Empty).Split("through").Select(range =>
            {
                var split = range.Split(",");

                return (int.Parse(split[0]),int.Parse(split[1]));
            }).ToArray();

            switch (command)
            {
                case "turn on":
                    ChangeGrid(grid, ranges, true);
                    break;
                case "toggle":
                    ChangeGrid(grid, ranges, null);
                    break;
                case "turn off":
                    ChangeGrid(grid, ranges, false);
                    break;
            }
        }

        var lit = 0;
        foreach (var item in grid)
        {
            if(item) lit++;
        }

        return lit;
    }

    private static void ChangeGrid(bool[,] grid, (int Row, int Column)[] ranges, bool? value)
    {
        for (var i = ranges[0].Row; i <= ranges[1].Row; i++)
        {
            for (int j = ranges[0].Column; j <= ranges[1].Column; j++)
            {
                grid[i, j] = value ?? !grid[i, j];
            }
        }
    }

    public object PartTwo(IEnumerable<string> rows)
    {
        var grid = new int[1000, 1000];
        var commands = new[] { "turn on", "turn off", "toggle" };

        foreach (var row in rows.Where(r => !string.IsNullOrWhiteSpace(r)))
        {
            var command = commands.First(row.Contains);
            var ranges = row.Replace(command, string.Empty).Split("through").Select(range =>
            {
                var split = range.Split(",");

                return (int.Parse(split[0]), int.Parse(split[1]));
            }).ToArray();

            switch (command)
            {
                case "turn on":
                    ChangeGrid(grid, ranges, 1);
                    break;
                case "toggle":
                    ChangeGrid(grid, ranges, 2);
                    break;
                case "turn off":
                    ChangeGrid(grid, ranges, -1);
                    break;
            }
        }

        var lit = 0;
        foreach (var item in grid)
        {
            lit += item;
        }

        return lit;
    }

    private static void ChangeGrid(int[,] grid, (int Row, int Column)[] ranges, int value)
    {
        for (var i = ranges[0].Row; i <= ranges[1].Row; i++)
        {
            for (int j = ranges[0].Column; j <= ranges[1].Column; j++)
            {
                var nextValue = grid[i, j] + value;
                grid[i, j] = nextValue >= 0 ? nextValue : grid[i, j];
            }
        }
    }
}
