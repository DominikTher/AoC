using AoC.Console.Interfaces;
using System.Text.RegularExpressions;

namespace AoC.Console.Days.Yr2015;

public sealed class Day7 : IDay
{
    public int Year => 2015;

    public int DayNumber => 7;

    public object PartOne(IEnumerable<string> rows)
    {
        var wireValues = new Dictionary<string, int>();
        var operations = new Dictionary<(string, string, string), string>();
        
        foreach (var row in rows.Where(row => !string.IsNullOrWhiteSpace(row)))
        {
            _ = FillOperations(row, @"^(\d+) -> (.+)$", (GroupCollection groupCollection) =>
            {
                wireValues.Add(groupCollection[2].ToString(), int.Parse(groupCollection[1].ToString()));
            }) ??
            FillOperations(row, @"^(.+) AND (.+) -> (.+)$", (GroupCollection groupCollection) =>
            {
                operations.Add((groupCollection[1].ToString(), groupCollection[2].ToString(), "AND"), groupCollection[3].ToString());
            }) ??
            FillOperations(row, @"^(.+) OR (.+) -> (.+)$", (GroupCollection groupCollection) =>
            {
                operations.Add((groupCollection[1].ToString(), groupCollection[2].ToString(), "OR"), groupCollection[3].ToString());
            }) ??
            FillOperations(row, @"^(.+) LSHIFT (\d+) -> (.+)$", (GroupCollection groupCollection) =>
            {
                operations.Add((groupCollection[1].ToString(), groupCollection[1].ToString(), $"LSHIFT {groupCollection[2]}"), groupCollection[3].ToString());
            }) ??
            FillOperations(row, @"^(.+) RSHIFT (\d+) -> (.+)$", (GroupCollection groupCollection) =>
            {
                operations.Add((groupCollection[1].ToString(), groupCollection[1].ToString(), $"RSHIFT {groupCollection[2]}"), groupCollection[3].ToString());
            }) ??
            FillOperations(row, @"^NOT (.+) -> (.+)$", (GroupCollection groupCollection) =>
            {
                operations.Add((groupCollection[1].ToString(), groupCollection[1].ToString(), "NOT"), groupCollection[2].ToString());
            });
        }

        var operationsCopy = new Dictionary<(string, string, string), string>(operations);
        var wireValuesCopy = new Dictionary<string, int>(wireValues);

        while (operations.Count > 0)
        {
            var num = 0;
            var op = operations.Where(x => (wireValues.ContainsKey(x.Key.Item1) || int.TryParse(x.Key.Item1, out num)) && wireValues.ContainsKey(x.Key.Item2)).ToList();

            foreach (var operation in op)
            {
                var ops = operation.Key.Item3;
                var shift = 0;
                if (ops.Contains("SHIFT"))
                {
                    var split = ops.Split(" ");
                    ops = split[0];
                    shift = int.Parse(split[1]);
                }

                if(wireValues.TryGetValue(operation.Key.Item1, out var val))
                {
                    num = val;
                }
                else
                {
                    num = int.Parse(operation.Key.Item1);
                }

                switch (ops)
                {
                    case "AND":
                        wireValues.Add(operation.Value, And(num, wireValues[operation.Key.Item2]));
                        break;
                    case "OR":
                        wireValues.Add(operation.Value, Or(num, wireValues[operation.Key.Item2]));
                        break;
                    case "LSHIFT":
                        wireValues.Add(operation.Value, LShift(wireValues[operation.Key.Item1], shift));
                        break;
                    case "RSHIFT":
                        wireValues.Add(operation.Value, RShift(wireValues[operation.Key.Item1], shift));
                        break;
                    case "NOT":
                        wireValues.Add(operation.Value, Not(wireValues[operation.Key.Item1]));
                        break;
                    default:
                        break;
                }

                operations.Remove(operation.Key);
            }
        }

        var a = wireValues["lx"];
        wireValuesCopy["b"] = a;
        while (operationsCopy.Count > 0)
        {
            var num = 0;
            var op = operationsCopy.Where(x => (wireValuesCopy.ContainsKey(x.Key.Item1) || int.TryParse(x.Key.Item1, out num)) && wireValuesCopy.ContainsKey(x.Key.Item2)).ToList();

            foreach (var operation in op)
            {
                var ops = operation.Key.Item3;
                var shift = 0;
                if (ops.Contains("SHIFT"))
                {
                    var split = ops.Split(" ");
                    ops = split[0];
                    shift = int.Parse(split[1]);
                }

                if (wireValuesCopy.TryGetValue(operation.Key.Item1, out var val))
                {
                    num = val;
                }
                else
                {
                    num = int.Parse(operation.Key.Item1);
                }

                if(operation.Value == "b")
                {
                    wireValuesCopy.Add(operation.Value, a);
                    continue;
                }

                switch (ops)
                {
                    case "AND":
                        wireValuesCopy.Add(operation.Value, And(num, wireValuesCopy[operation.Key.Item2]));
                        break;
                    case "OR":
                        wireValuesCopy.Add(operation.Value, Or(num, wireValuesCopy[operation.Key.Item2]));
                        break;
                    case "LSHIFT":
                        wireValuesCopy.Add(operation.Value, LShift(wireValuesCopy[operation.Key.Item1], shift));
                        break;
                    case "RSHIFT":
                        wireValuesCopy.Add(operation.Value, RShift(wireValuesCopy[operation.Key.Item1], shift));
                        break;
                    case "NOT":
                        wireValuesCopy.Add(operation.Value, Not(wireValuesCopy[operation.Key.Item1]));
                        break;
                    default:
                        break;
                }

                operationsCopy.Remove(operation.Key);
            }
        }

        return 0;
    }

    private static int And(int a, int b) => a & b;
    private static int Or(int a, int b) => a | b;
    private static int Not(int a) => ~a;
    private static int LShift(int a, int b) => a << b;
    private static int RShift(int a, int b) => a >> b;

    private object? FillOperations(string row, string v, Action<GroupCollection> action)
    {
        var matches = Regex.Match(row, v);

        if (matches.Success)
        {
            action(matches.Groups);

            return 1;
        }

        return null;
    }

    public object PartTwo(IEnumerable<string> rows)
    {
        throw new NotImplementedException();
    }
}
