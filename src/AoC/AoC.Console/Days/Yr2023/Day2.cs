using AoC.Console.Extensions;
using AoC.Console.Interfaces;
using System.Text.RegularExpressions;

namespace AoC.Console.Days.Yr2023;

public sealed record Cube(int Id, int Red, int Green, int Blue);

public sealed class Day2 : IDay
{
    public int Year => 2023;

    public int DayNumber => 2;

    public object PartOne(IEnumerable<string> rows)
        => rows.WithoutNullOrWhiteSpace()
            .Select(ParseCube)
            .Where(c => c.Red <= 12 && c.Green <= 13 && c.Blue <= 14)
            .Select(c => c.Id)
            .Sum();

    public object PartTwo(IEnumerable<string> rows)
        => rows.WithoutNullOrWhiteSpace()
            .Select(ParseCube)
            .Select(c => c.Red * c.Green * c.Blue)
            .Sum();

    private static Cube ParseCube(string row)
    {
        var id = int.Parse(Regex.Match(row, @"Game (\d+)").Groups[1].Value);
        var red = Regex.Matches(row, @"(\d+) red").Max(x => int.Parse(x.Groups[1].Value));
        var green = Regex.Matches(row, @"(\d+) green").Max(x => int.Parse(x.Groups[1].Value));
        var blue = Regex.Matches(row, @"(\d+) blue").Max(x => int.Parse(x.Groups[1].Value));

        return new Cube(id, red, green, blue);
    }

    //public object PartOne(IEnumerable<string> rows)
    //{
    //    var rules = new Dictionary<string, int>
    //    {
    //        { "red", 12 },
    //        { "green", 13 },
    //        { "blue", 14 }
    //    };

    //    return rows.WithoutNullOrWhiteSpace()
    //        .Select(row =>
    //        {
    //            var game = row.Split(":");
    //            var id = int.Parse(game[0].Split(" ")[1]);
    //            var cubes = game[1].Split(";");

    //            return (id, cubes);
    //        })
    //        .Where(x => !x.cubes.Any(x =>
    //        {

    //            var sets = x.Split(",");
    //            foreach (var set in sets)
    //            {
    //                var cube = set.Trim().Split(" ");
    //                (int Count, string Cube) = (int.Parse(cube[0]), cube[1]);

    //                if (rules[Cube] < Count)
    //                {
    //                    return true;
    //                }
    //            }

    //            return false;
    //        }))
    //        .Sum(x => x.id);
    //}

    //public object PartTwo(IEnumerable<string> rows)
    //    => rows.WithoutNullOrWhiteSpace()
    //       .Select(row =>
    //       {
    //           var game = row.Split(":");
    //           var id = int.Parse(game[0].Split(" ")[1]);

    //           return (id, game: game[1]);
    //       })
    //       .Select(x =>
    //       {
    //           return x.game.Split(";").SelectMany(a => a.Split(",").Select(b =>
    //           {
    //               var cube = b.Trim().Split(" ");
    //               return (int.Parse(cube[0]), cube[1]);
    //           }))
    //           .GroupBy(x => x.Item2)
    //           .Select(g => g.Max(c => c.Item1))
    //           .Aggregate(1, (a, b) => a * b);
    //       })
    //       .Sum();
}
