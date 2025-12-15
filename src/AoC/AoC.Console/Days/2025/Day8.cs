using AoC.Console.Interfaces;

namespace AoC.Console.Days._2025;

public sealed class Day8 : IDay
{
    public int Year => 2025;

    public int DayNumber => 8;

    public object PartOne(IEnumerable<string> rows)
    {
        var junctionBoxes = rows
            .Select(row => row.Split(','))
            .Select((parts, index) => new JunctionBox(
                index,
                int.Parse(parts[0]),
                int.Parse(parts[1]),
                int.Parse(parts[2])))
            .ToList();

        var distances = junctionBoxes
            .SelectMany((box, index) => junctionBoxes
                .Skip(index + 1)
                .Select(other => (FromId: box.Id, ToId: other.Id, Distance: box.EuclideanDistanceTo(other))))
            .OrderBy(x => x.Distance)
            .ThenBy(x => x.FromId)
            .ToList();

        var circuits = new List<HashSet<int>>();

        foreach (var item in junctionBoxes)
        {
            circuits.Add([item.Id]);
        }

        var cnt = 0;
        foreach (var junctionBoxDistance in distances)
        {
            if (cnt++ == 1000)
            {
                break;
            }

            if (circuits.Any(circuit => circuit.Contains(junctionBoxDistance.FromId) && circuit.Contains(junctionBoxDistance.ToId)))
            {
                continue;
            }

            var combine = circuits
                .Select((circuit, index) => (index, circuit.Contains(junctionBoxDistance.FromId) || circuit.Contains(junctionBoxDistance.ToId)))
                .Where(x => x.Item2)
                .ToArray();

            if (combine.Length == 0)
            {
                circuits.Add([junctionBoxDistance.FromId, junctionBoxDistance.ToId]);
            }
            else if (combine.Length == 1)
            {
                circuits[combine[0].index].Add(junctionBoxDistance.FromId);
                circuits[combine[0].index].Add(junctionBoxDistance.ToId);
            }
            else
            {
                var firstIndex = combine[0].index;
                var firstCircuit = circuits[firstIndex];

                for (int i = 1; i < combine.Length; i++)
                {
                    var currentIndex = combine[i].index;
                    var currentCircuit = circuits[currentIndex];

                    foreach (var boxId in currentCircuit)
                    {
                        firstCircuit.Add(boxId);
                    }
                }

                firstCircuit.Add(junctionBoxDistance.FromId);
                firstCircuit.Add(junctionBoxDistance.ToId);

                circuits.RemoveAt(combine[1].index);
            }
        }

        return circuits
            .OrderByDescending(x => x.Count)
            .Take(3)
            .Aggregate(1, (c, n) => c * n.Count);
    }

    public object PartTwo(IEnumerable<string> rows)
    {
        var junctionBoxes = rows
           .Select(row => row.Split(','))
           .Select((parts, index) => new JunctionBox(
               index,
               int.Parse(parts[0]),
               int.Parse(parts[1]),
               int.Parse(parts[2])))
           .ToList();

        var distances = junctionBoxes
            .SelectMany((box, index) => junctionBoxes
                .Skip(index + 1)
                .Select(other => (FromId: box.Id, ToId: other.Id, Distance: box.EuclideanDistanceTo(other))))
            .OrderBy(x => x.Distance)
            .ThenBy(x => x.FromId)
            .ToList();

        var circuits = new List<HashSet<int>>();
        var result = 0;

        foreach (var item in junctionBoxes)
        {
            circuits.Add([item.Id]);
        }

        foreach (var junctionBoxDistance in distances)
        {
            if (circuits.Any(circuit => circuit.Contains(junctionBoxDistance.FromId) && circuit.Contains(junctionBoxDistance.ToId)))
            {
                continue;
            }

            var combine = circuits
                .Select((circuit, index) => (index, circuit.Contains(junctionBoxDistance.FromId) || circuit.Contains(junctionBoxDistance.ToId)))
                .Where(x => x.Item2)
                .ToArray();

            if (combine.Length == 0)
            {
                circuits.Add([junctionBoxDistance.FromId, junctionBoxDistance.ToId]);
            }
            else if (combine.Length == 1)
            {
                circuits[combine[0].index].Add(junctionBoxDistance.FromId);
                circuits[combine[0].index].Add(junctionBoxDistance.ToId);
            }
            else
            {
                var firstIndex = combine[0].index;
                var firstCircuit = circuits[firstIndex];

                for (int i = 1; i < combine.Length; i++)
                {
                    var currentIndex = combine[i].index;
                    var currentCircuit = circuits[currentIndex];
                    foreach (var boxId in currentCircuit)
                    {
                        firstCircuit.Add(boxId);
                    }
                }

                firstCircuit.Add(junctionBoxDistance.FromId);
                firstCircuit.Add(junctionBoxDistance.ToId);

                circuits.RemoveAt(combine[1].index);

                if (circuits.Count == 1)
                {
                    result = junctionBoxes[junctionBoxDistance.FromId].X * junctionBoxes[junctionBoxDistance.ToId].X;
                    break;
                }
            }
        }

        return result;
    }
}

public readonly record struct JunctionBox(int Id, int X, int Y, int Z)
{
    public double EuclideanDistanceTo(JunctionBox other)
        => Math.Sqrt(
            Math.Pow(X - other.X, 2) +
            Math.Pow(Y - other.Y, 2) +
            Math.Pow(Z - other.Z, 2));
}
