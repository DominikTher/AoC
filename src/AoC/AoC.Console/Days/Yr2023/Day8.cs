using AoC.Console.Extensions;
using AoC.Console.Interfaces;

namespace AoC.Console.Days.Yr2023;

public sealed class Day8 : IDay
{
    public int Year => 2023;

    public int DayNumber => 8;

    public object PartOne(IEnumerable<string> rows)
    {
        //var instructions = rows.First().Select(row => row).ToArray();
        //var nodes = new Dictionary<string, (string Left, string Right)>();

        //foreach (var row in rows.Skip(2).WithoutNullOrWhiteSpace())
        //{
        //    var individual = row.Split("=");
        //    var leftRightSplit = individual[1].Replace("(", string.Empty).Replace(")", string.Empty).Split(",");

        //    nodes.Add(individual[0].Trim(), (leftRightSplit[0].Trim(), leftRightSplit[1].Trim()));
        //}

        //var steps = 0;
        //var nextKey = "AAA";
        //var nextNode = nodes[nextKey];
        //var instructionIndex = 0;
        //while (nextKey != "ZZZ")
        //{
        //    if (instructions[instructionIndex++] == 'L')
        //    {
        //        nextKey = nextNode.Left;
        //    }
        //    else
        //    {
        //        nextKey = nextNode.Right;
        //    }

        //    nextNode = nodes[nextKey];
        //    steps++;

        //    if(instructionIndex >=  instructions.Length)
        //    {
        //        instructionIndex = 0;
        //    }
        //}

        return 0;
    }

    public object PartTwo(IEnumerable<string> rows)
    {
        var instructions = rows.First().Select(row => row).ToArray();
        var nodes = new Dictionary<string, (string Left, string Right)>();

        foreach (var row in rows.Skip(2).WithoutNullOrWhiteSpace())
        {
            var individual = row.Split("=");
            var leftRightSplit = individual[1].Replace("(", string.Empty).Replace(")", string.Empty).Split(",");

            nodes.Add(individual[0].Trim(), (leftRightSplit[0].Trim(), leftRightSplit[1].Trim()));
        }

        long steps = 0;
        //var nextKey = "AAA";
        //var nextNode = nodes[nextKey];
        //var instructionIndex = 0;
        //var nextNodes = nodes.Where(node => node.Key.EndsWith('A')).ToDictionary();

        var result = nodes.Keys.Where(key => key.EndsWith('A')).Select(key =>
        {
            long steps = 0;
            var nextKey = key;
            var nextNode = nodes[nextKey];
            var instructionIndex = 0;
            while (!nextKey.EndsWith('Z'))
            {
                if (instructions[instructionIndex++] == 'L')
                {
                    nextKey = nextNode.Left;
                }
                else
                {
                    nextKey = nextNode.Right;
                }

                nextNode = nodes[nextKey];
                steps++;

                if (instructionIndex >= instructions.Length)
                {
                    instructionIndex = 0;
                }
            }

            return steps;
        }).Aggregate(lcm);

        var a = result;

        //while (!nextNodes.Keys.All(key=> key.EndsWith('Z')))
        //{
        //    var leftIndex = instructions[instructionIndex] == 'L';
        //    var tmp = new Dictionary<string, (string Left, string Right)>();
        //    foreach (var (Left, Right) in nextNodes.Values)
        //    {
        //        if (leftIndex)
        //        {

        //            tmp.Add(Left, nodes[Left]);
        //            //nextNodes = nodes.Where(node => nextNodes.Select(x => x.Value.Left).Contains(node.Key)).ToList();
        //        }
        //        else
        //        {
        //            tmp.Add(Right, nodes[Right]);
        //            //nextNodes = nodes.Where(node => nextNodes.Select(x => x.Value.Right).Contains(node.Key)).ToList();
        //        }
        //    }

        //    nextNodes = tmp;
        //    //nextNode = nodes[nextKey];
        //    steps++;
        //    instructionIndex++;

        //    if (instructionIndex >= instructions.Length)
        //    {
        //        instructionIndex = 0;
        //    }
        //}

        return a;
    }

    static long gcf(long a, long b)
    {
        while (b != 0)
        {
            long temp = b;
            b = a % b;
            a = temp;
        }
        return a;
    }

    static long lcm(long a, long b)
    {
        return (a / gcf(a, b)) * b;
    }
}
