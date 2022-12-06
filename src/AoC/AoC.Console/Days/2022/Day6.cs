using AoC.Console.Interfaces;

namespace AoC.Console.Days;

internal class Day6 : IDay
{
    public int Year => 2022;

    public int DayNumber => 6;

    public object PartOne(IEnumerable<string> rows)
    {
        var dataStream = rows.First();
        var result = 0;

        for (int i = 0; i < dataStream.Length - 3; i++)
        {
            var marker = dataStream.Substring(i, 4);

            if(marker.Distinct().Count() == 4)
            {
                result = i + 4;
                break;
            }
        }

        return result;
    }

    public object PartTwo(IEnumerable<string> rows)
    {
        var dataStream = rows.First();
        var result = 0;

        for (int i = 0; i < dataStream.Length - 13; i++)
        {
            var marker = dataStream.Substring(i, 14);

            if (marker.Distinct().Count() == 14)
            {
                result = i + 14;
                break;
            }
        }

        return result;
    }
}