using AoC.Console.Interfaces;
using System.Text;

namespace AoC.Console.Days.Yr2015;

public sealed class Day10 : IDay
{
    public int Year => 2015;

    public int DayNumber => 10;

    public object PartOne(IEnumerable<string> rows)
    {
        var number = new StringBuilder("1321131112");
        char lastDigit = '1';
        var counter = 0;
        
        for (var i = 0; i < 50; i++)
        {
            var tmp = new StringBuilder();
            foreach (var digit in number.ToString())
            {
                if (digit == lastDigit)
                {
                    counter++;
                }
                else
                {
                    tmp.Append($"{counter}{lastDigit}");
                    counter = 1;
                }

                lastDigit = digit;
            }
            number.Clear();
            number.Append(tmp + $"{counter}{lastDigit}");
            lastDigit = number[0];
            counter = 0;
            System.Console.WriteLine(i);
        }

        return number.Length;
    }

    public object PartTwo(IEnumerable<string> rows)
    {
        throw new NotImplementedException();
    }
}
