using AoC.Console.Interfaces;

namespace AoC.Console.Days;

internal sealed class Day10 : IDay
{
    public int Year => 2022;

    public int DayNumber => 10;

    public object PartOne(IEnumerable<string> rows)
    {
        var cycle = 1;
        var x = 1;
        var spin = 1;
        var period = 20;
        var sum = 0;

        foreach (var row in rows)
        {
            var split = row.Split(" ");

            if (split[0].StartsWith("addx"))
            {
                spin = 2;
            }

            for (int i = 0; i < spin; i++)
            {
                if (cycle % period == 0)
                {
                    sum += cycle * x;
                    period += 40;
                }

                if (spin == 2 && i == spin - 1)
                {
                    x += int.Parse(split[1]);
                }

                cycle++;
            }

            spin = 1;
        }

        return 0;
    }

    public object PartTwo(IEnumerable<string> rows)
    {
        var cycle = 1;
        var x = 1;
        var spin = 1;
        var part = 0;
        var sprite =    "###.....................................";
        var crt =       "........................................";

        foreach (var row in rows)
        {
            var position = cycle - 1;

            var split = row.Split(" ");

            if (split[0].StartsWith("addx"))
            {
                spin = 2;
            }

            for (int i = 0; i < spin; i++)
            {
                

                //for (int j = 0; j < part; j++)
                //{
                if (sprite[part] == '#')
                {
                    crt = crt.Remove(part, 1).Insert(part, "#");
                }
                //else if (sprite[j] == '.' && crt[j] != '#')
                //{
                //    crt = crt.Remove(j, 1).Insert(j, ".");
                //}
                //}

                if (spin == 2 && i == spin - 1)
                {
                    x += int.Parse(split[1]);

                    sprite = string.Empty;
                    for (int j = 1; j <= 40; j++)
                    {
                        if (j >= x && j <= x + 2)
                        {
                            sprite += '#';
                        }
                        else
                        {
                            sprite += '.';
                        }
                    }
                }

                if (cycle % 40 == 0)
                {
                    System.Console.WriteLine(crt);
                    crt = "........................................";
                    //sprite = "###.....................................";
                    part = 0;
                }
                else
                {
                    part++;
                }

                cycle++;
               
            }

            spin = 1;
        }

        return 0;
    }
}

