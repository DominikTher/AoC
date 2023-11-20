using AoC.Console.Interfaces;

namespace AoC.Console.Days.Yr2015;

public sealed class Day1 : IDay
{
    public int Year => 2015;

    public int DayNumber => 1;

    public object PartOne(IEnumerable<string> rows)
    {
        var result = 0;

        foreach (var character in rows.First())
        {
            if (character == '(')
            {
                result++;
            }
            else
            {
                result--;
            }
        }

        return result;
    }

    public object PartTwo(IEnumerable<string> rows)
    {
        var result = 0;
        var position = 0;

        foreach (var character in rows.First())
        {
            position++;

            if (character == '(')
            {
                result++;
            }
            else
            {
                result--;
            }

            if(result == -1)
            {
                break;
            }
        }

        return position;
    }
}
