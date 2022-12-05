using AoC.Console.Interfaces;
using System.Text.RegularExpressions;

namespace AoC.Console.Days;

internal class Day5 : IDay
{
    public int Year => 2022;

    public int DayNumber => 5;

    public object PartOne(IEnumerable<string> rows)
    {
        var stacks = new List<Stack<char>>();
        var index = 0;

        foreach (var row in rows.Where(r => r.Contains("[")).Reverse())
        {
            foreach (var item in row.Chunk(4))
            {
                var match = Regex.Match(new string(item), "[a-zA-Z]");

                if (stacks.ElementAtOrDefault(index) == null)
                {
                    stacks.Add(new Stack<char>());
                }

                if(match.Success)
                {
                    stacks[index].Push(match.Value[0]);
                }

                index++;
            }

            index = 0;
        }

        foreach (var row in rows.Where(r => r.Contains("move")))
        {
            var matchCollection = Regex.Matches(row, "\\d+");

            var move = int.Parse(matchCollection[0].Value);
            var from = int.Parse(matchCollection[1].Value);
            var to = int.Parse(matchCollection[2].Value);

            for (int i = 0; i < move; i++)
            {
                var toMove = stacks[from - 1].Pop();
                stacks[to - 1].Push(toMove);
            }
        }

        var items = stacks.Select(s => s.Pop()).ToArray();

        return new string(items);
    }

    public object PartTwo(IEnumerable<string> rows)
    {
        var stacks = new List<Stack<char>>();
        var index = 0;

        foreach (var row in rows.Where(r => r.Contains("[")).Reverse())
        {
            foreach (var item in row.Chunk(4))
            {
                var match = Regex.Match(new string(item), "[a-zA-Z]");

                if (stacks.ElementAtOrDefault(index) == null)
                {
                    stacks.Add(new Stack<char>());
                }

                if (match.Success)
                {
                    stacks[index].Push(match.Value[0]);
                }

                index++;
            }

            index = 0;
        }

        foreach (var row in rows.Where(r => r.Contains("move")))
        {
            var matchCollection = Regex.Matches(row, "\\d+");

            var move = int.Parse(matchCollection[0].Value);
            var from = int.Parse(matchCollection[1].Value);
            var to = int.Parse(matchCollection[2].Value);

            var toMove = new List<char>();
            for (int i = 0; i < move; i++)
            {
                toMove.Add(stacks[from - 1].Pop());
            }

            toMove.Reverse();
            foreach (var item in toMove)
            {
                stacks[to - 1].Push(item);
            }
        }

        var items = stacks.Select(s => s.Pop()).ToArray();

        return new string(items);
    }
}