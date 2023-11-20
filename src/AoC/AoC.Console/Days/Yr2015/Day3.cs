using AoC.Console.Interfaces;

namespace AoC.Console.Days.Yr2015;

public sealed class Day3 : IDay
{
    public int Year => 2015;

    public int DayNumber => 3;

    public object PartOne(IEnumerable<string> rows)
    {
        var x = 0;
        var y = 0;

        var map = new Dictionary<char, (int X, int Y)>
        {
            { '>', (0, 1) },
            { '^', (1, 0) },
            { 'v', (-1, 0) },
            { '<', (0, -1) }
        };

        var visitedCoordinates = new HashSet<string> { "0;0" };

        foreach (var character in rows.First())
        {
            var (X, Y) = map[character];
            x += X;
            y += Y;
            visitedCoordinates.Add($"{x};{y}");
        }

        return visitedCoordinates.Count;
    }

    public object PartTwo(IEnumerable<string> rows)
    {
        var xSanta = 0;
        var ySanta = 0;
        var xRoboSanta = 0;
        var yRoboSanta = 0;

        var map = new Dictionary<char, (int X, int Y)>
        {
            { '>', (0, 1) },
            { '^', (1, 0) },
            { 'v', (-1, 0) },
            { '<', (0, -1) }
        };

        var visitedCoordinatesSanta = new HashSet<string> { "0;0" };
        var visitedCoordinatesRoboSanta = new HashSet<string> { "0;0" };

        var santasTurn = true;
        foreach (var character in rows.First())
        {
            var (X, Y) = map[character];

            if (santasTurn)
            {
                xSanta += X;
                ySanta += Y;
                visitedCoordinatesSanta.Add($"{xSanta};{ySanta}");
            }
            else
            {
                xRoboSanta += X;
                yRoboSanta += Y;
                visitedCoordinatesRoboSanta.Add($"{xRoboSanta};{yRoboSanta}");
            }

            santasTurn = !santasTurn;
        }

        return visitedCoordinatesSanta
            .Union(visitedCoordinatesRoboSanta)
            .Count();
    }
}
