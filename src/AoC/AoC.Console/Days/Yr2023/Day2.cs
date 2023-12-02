using AoC.Console.Extensions;
using AoC.Console.Interfaces;

namespace AoC.Console.Days.Yr2023;

public sealed class Day2 : IDay
{
    public int Year => 2023;

    public int DayNumber => 2;

    public object PartOne(IEnumerable<string> rows)
    {
        var rules = new Dictionary<string, int>
        {
            { "red", 12 },
            { "green", 13 },
            { "blue", 14 }
        };

        return rows.WithoutNullOrWhiteSpace()
            .Select(row =>
            {
                var game = row.Split(":");
                var id = int.Parse(game[0].Split(" ")[1]);
                var cubes = game[1].Split(";");

                return (id, cubes);
            })
            .Where(x => !x.cubes.Any(x =>
            {

                var sets = x.Split(",");
                foreach (var set in sets)
                {
                    var cube = set.Trim().Split(" ");
                    (int Count, string Cube) items = (int.Parse(cube[0]), cube[1]);

                    if (rules[items.Cube] < items.Count)
                    {
                        return true;
                    }
                }

                return false;
            }))
            .Sum(x => x.id);
    }

    public object PartTwo(IEnumerable<string> rows)
        => rows.WithoutNullOrWhiteSpace()
           .Select(row =>
           {
               var game = row.Split(":");
               var id = int.Parse(game[0].Split(" ")[1]);

               return (id, game: game[1]);
           })
           .Select(x =>
           {
               return x.game.Split(";").SelectMany(a => a.Split(",").Select(b =>
               {
                   var cube = b.Trim().Split(" ");
                   return (int.Parse(cube[0]), cube[1]);
               }))
               .GroupBy(x => x.Item2)
               .Select(g => g.Max(c => c.Item1))
               .Aggregate(1, (a, b) => a * b);
           })
           .Sum();
}
