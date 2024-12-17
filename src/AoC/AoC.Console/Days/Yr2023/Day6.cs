using AoC.Console.Interfaces;
using System.Text.RegularExpressions;

namespace AoC.Console.Days.Yr2023;

public sealed class Day6 : IDay
{
    public int Year => 2023;

    public int DayNumber => 6;

    public object PartOne(IEnumerable<string> rows)
    {
        var times = Regex.Matches(rows.First(), @"\d+").Select(match => int.Parse(match.Groups[0].Value)).ToList();
        var distances = Regex.Matches(rows.Skip(1).First(), @"\d+").Select(match => int.Parse(match.Groups[0].Value)).ToList();

        var recordsBeaten = new List<int>();
        for (var i = 0; i < times.Count; i++)
        {
            var recordBeaten = 0;
            var time = times[i];
            for (var speed = 0; speed < time; speed++)
            {
                var distance = speed * (time - speed);
                if (distance > distances[i])
                {
                    recordBeaten++;
                }
            }

            recordsBeaten.Add(recordBeaten);
        }

        return recordsBeaten.Aggregate((a, b) => a * b);
    }

    public object PartTwo(IEnumerable<string> rows)
    {
        var times = Regex.Matches(rows.First().Split(":")[1].Replace(" ", string.Empty), @"\d+").Select(match => long.Parse(match.Groups[0].Value)).ToList();
        var distances = Regex.Matches(rows.Skip(1).First().Split(":")[1].Replace(" ", string.Empty), @"\d+").Select(match => long.Parse(match.Groups[0].Value)).ToList();

        var recordsBeaten = new List<long>();
        for (var i = 0; i < times.Count; i++)
        {
            long recordBeaten = 0;
            var time = times[i];
            for (long speed = 0; speed < time; speed++)
            {
                var distance = speed * (time - speed);
                if (distance > distances[i])
                {
                    recordBeaten++;
                }
            }

            recordsBeaten.Add(recordBeaten);
        }

        return recordsBeaten.Aggregate((a, b) => a * b);
    }
}
