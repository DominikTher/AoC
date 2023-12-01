using AoC.Console.Interfaces;

namespace AoC.Console.Days.Yr2015;

public sealed class Day11 : IDay
{
    public int Year => 2015;

    public int DayNumber => 11;

    public object PartOne(IEnumerable<string> rows)
        => Solve(rows.First())
            .First(IsPasswordValid);

    public object PartTwo(IEnumerable<string> rows)
        => Solve(rows.First())
            .Where(IsPasswordValid)
            .Skip(1)
            .First();

    private static IEnumerable<string> Solve(string password)
    {
        while(true)
        {
            for (var i = password.Length - 1; i >= 0; i--)
            {
                var incremented = password[i] + 1;
                var newChar = incremented == 123 ? 'a' : (char)incremented;
                password = password.Remove(i, 1).Insert(i, newChar.ToString());

                if (newChar != 'a')
                {
                    break;
                }
            }

            yield return password;
        }
    }

    private static bool IsPasswordValid(string password)
    {
        if (!"iol".Any(password.Contains))
        {
            for (int i = 0; i < password.Length - 2; i++)
            {
                var next = 3 + i;
                var chunk = password[i..next];
                if (chunk[0] + 1 == chunk[1] && chunk[1] + 1 == chunk[2])
                {
                    var cnt = 0;
                    var lastLetter = char.MinValue;
                    for (var j = 0; j < password.Length - 1; j++)
                    {
                        if (password[j] == password[j + 1] && password[j] != lastLetter)
                        {
                            if (++cnt == 2)
                            {
                                return true;
                            }

                            j++;
                        }
                    }
                }
            }
        }

        return false;
    }
}
