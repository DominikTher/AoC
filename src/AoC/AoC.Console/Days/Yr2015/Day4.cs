using AoC.Console.Interfaces;
using System.Security.Cryptography;

namespace AoC.Console.Days.Yr2015;

internal class Day4 : IDay
{
    public int Year => 2015;

    public int DayNumber => 4;

    public object PartOne(IEnumerable<string> rows)
    {
        var number = 0;
        for (;number < int.MaxValue; number++)
        {
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(rows.First() + number);
            byte[] hashBytes = MD5.HashData(inputBytes);

            var hash = Convert.ToHexString(hashBytes);

            if (hash.StartsWith("00000"))
            {
                break;
            }
        }

        return number;
    }

    public object PartTwo(IEnumerable<string> rows)
    {
        var number = 0;
        for (; number < int.MaxValue; number++)
        {
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(rows.First() + number);
            byte[] hashBytes = MD5.HashData(inputBytes);

            var hash = Convert.ToHexString(hashBytes);

            if (hash.StartsWith("000000"))
            {
                break;
            }
        }

        return number;
    }
}
