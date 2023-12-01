using AoC.Console.Interfaces;
using System.Text.RegularExpressions;

namespace AoC.Console.Days.Yr2015;

public sealed class Day9 : IDay
{
    public int Year => 2015;

    public int DayNumber => 9;

    public object PartOne(IEnumerable<string> rows)
    {
        //var cities = new HashSet<string> { "London", "Belfast", "Dublin" };
        //var costs = new Dictionary<(string, string), int>
        //{
        //    { ("London", "Belfast"), 464 },
        //    { ("London", "Dublin"), 518 },
        //    { ("Dublin", "Belfast"), 141 }
        //};

        var cities = new HashSet<string>();
        var costs = new Dictionary<(string, string), int>();

        foreach (var row in rows.Where(r => !string.IsNullOrWhiteSpace(r)))
        {
            var m = Regex.Match(row, "(.+) to (.+) = (.+)").Groups;
            cities.Add(m[1].Value);
            cities.Add(m[2].Value);
            costs.Add((m[1].Value, m[2].Value), int.Parse(m[3].Value));
        }

        var shortest = int.MaxValue;
        var longest = 0;
        foreach (var p in GetPermutations(cities, cities.Count).ToList())
        {
            var x = p.ToArray();
            var tmp = 0;
            for (var i = 0; i < x.Length - 1; i++)
            {
                if (!costs.TryGetValue((x[i], x[i + 1]), out var a))
                {
                    costs.TryGetValue((x[i + 1], x[i]), out a);
                }

                tmp += a;
            }

            if (tmp < shortest)
            {
                shortest = tmp;
            }

            if (tmp > longest)
            {
                longest = tmp;
            }
        }

        return longest;
    }

    static IEnumerable<IEnumerable<T>> GetPermutations<T>(IEnumerable<T> list, int length)
    {
        if (length == 1) return list.Select(t => new T[] { t });

        return GetPermutations(list, length - 1)
            .SelectMany(t => list.Where(e => !t.Contains(e)),
                (t1, t2) => t1.Concat(new T[] { t2 }));
    }

    public object PartTwo(IEnumerable<string> rows)
    {
        throw new NotImplementedException();
    }
}
