using AoC.Console.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC.Console.Days;

internal class Day3 : IDay
{
    public int Year => 2022;

    public int DayNumber => 3;

    public int PartOne(IEnumerable<string> rows)
    {
        var result = 0;

        foreach (var row in rows.Where(r => !string.IsNullOrWhiteSpace(r)))
        {
            int half = row.Length / 2;
            var firstCompartment = row[..half];
            var secondCompartment = row[half..];

            var item = firstCompartment
                .Intersect(secondCompartment)
                .First();

            if(char.IsLower(item))
            {
                result += item - 96;
            }
            else
            {
                result += item - 38;
            }
        }

        return result;
    }

    public int PartTwo(IEnumerable<string> rows)
    {
        var result = 0;

        foreach (var chunk in rows.Where(r => !string.IsNullOrWhiteSpace(r)).Chunk(3))
        {
            var item = chunk[0]
                .Intersect(chunk[1])
                .Intersect(chunk[2])
                .First();

            if (char.IsLower(item))
            {
                result += item - 96;
            }
            else
            {
                result += item - 38;
            }
        }

        return result;
    }
}