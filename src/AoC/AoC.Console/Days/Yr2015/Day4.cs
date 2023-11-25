using AoC.Console.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace AoC.Console.Days.Yr2015;

public sealed class Day4 : IDay
{
    public int Year => 2015;

    public int DayNumber => 4;

    public object PartOne(IEnumerable<string> rows) 
        => CalculateHash(rows.First(), "00000");

    public object PartTwo(IEnumerable<string> rows)
        => CalculateHash(rows.First(), "000000");

    private static object CalculateHash(string value, string check)
    {
        var number = 0;
        for (; number < int.MaxValue; number++)
        {
            byte[] inputBytes = Encoding.ASCII.GetBytes(value + number);
            byte[] hashBytes = MD5.HashData(inputBytes);

            var hash = Convert.ToHexString(hashBytes);

            if (hash.StartsWith(check))
            {
                break;
            }
        }

        return number;
    }
}
