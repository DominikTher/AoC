using AoC.Console.Extensions;
using AoC.Console.Interfaces;

namespace AoC.Console.Days._2025;

public class Day1 : IDay
{
    public int Year => 2025;

    public int DayNumber => 1;

    public object PartOne(IEnumerable<string> rows)
    {
        var actualIndex = 50;
        var counter = 0;

        foreach (var row in rows.WithoutNullOrWhiteSpace())
        {
            var (direction, steps) = (row[0], int.Parse(row[1..]));
            var correctedSteps = steps % 100;

            if (direction == 'L')
            {
                actualIndex = (0 - correctedSteps + actualIndex) % 100;
            }

            if (direction == 'R')
            {
                actualIndex = (0 + correctedSteps + actualIndex) % 100;
            }

            if (actualIndex == 0)
            {
                counter++;
            }
        }

        return counter;
    }

    public object PartTwo(IEnumerable<string> rows)
    {
        var actualIndex = 50;
        var counter = 0;

        foreach (var row in rows.WithoutNullOrWhiteSpace())
        {
            var (direction, steps) = (row[0], int.Parse(row[1..]));

            var remainder = steps % 100;
            counter += steps / 100;

            for (int step = 0; step < remainder; step++)
            {
                actualIndex = direction switch
                {
                    'L' => ProcessStep(Move.Left, actualIndex),
                    'R' => ProcessStep(Move.Right, actualIndex),
                    _ => throw new NotSupportedException()
                };

                if (actualIndex == 0)
                {
                    counter++;
                }
            }
        }

        return counter;

        static int ProcessStep(Move move, int actualIndex)
        {
            var step = Step(move, actualIndex);

            if (step.actualIndex == step.stop)
            {
                return step.reset;
            }

            return step.actualIndex;
        }

        static (int stop, int reset, int actualIndex) Step(Move move, int actualIndex) =>
            move switch
            {
                Move.Left => (-1, 99, --actualIndex),
                Move.Right => (100, 0, ++actualIndex),
                _ => throw new NotSupportedException()
            };
    }
}

public enum Move
{
    Left,
    Right,
}