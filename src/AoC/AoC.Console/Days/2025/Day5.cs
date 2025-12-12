using AoC.Console.Extensions;
using AoC.Console.Interfaces;

namespace AoC.Console.Days._2025;

public sealed class Day5 : IDay
{
    public int Year => 2025;

    public int DayNumber => 5;

    public object PartOne(IEnumerable<string> rows)
    {
        var input = Parse(rows.WithoutNullOrWhiteSpace()).ToList();
        var ranges = input.Where(x => x.End != null);
        var ids = input.Where(x => x.End == null).Select(x => x.Start);

        return ids.Where(id => ranges.Any(range => id >= range.Start && id <= range.End)).Count();
    }

    public object PartTwo(IEnumerable<string> rows)
    {
        // TODO: Optimize

        var ranges = Parse(rows.WithoutNullOrWhiteSpace())
            .Where(x => x.End != null)
            .Select(x => (x.Start, End: x.End!.Value));

        var result = new List<(long Start, long End)>();
        foreach (var range in ranges)
        {
            var overlaps = result
                .Select((r, index) => (index, r,
                    (range.Start >= r.Start && range.Start <= r.End) ||
                    (range.End >= r.Start && range.End <= r.End) ||
                    (range.Start <= r.Start && range.End >= r.End)))
                .Where(x => x.Item3)
                .ToList();

            if (overlaps.Count == 0)
                result.Add(range);
            else
            {
                var start = overlaps.Min(o => o.r.Start);
                var end = overlaps.Max(o => o.r.End);
                result.Add((range.Start < start ? range.Start : start, range.End > end ? range.End : end));
            }

            for (var i = 0; i < overlaps.Count; i++)
            {
                var index = overlaps[i].index - i;
                result.RemoveAt(index);
            }
        }

        return result.Aggregate(0L, (total, next) => total + next.End - next.Start + 1);
    }

    static IEnumerable<(long Start, long? End)> Parse(IEnumerable<string> rows)
    {
        var enumerator = rows.GetEnumerator();

        while (enumerator.MoveNext())
        {
            var parts = enumerator.Current.Split('-');
            var start = long.Parse(parts[0]);
            var end = parts.Length == 2 ? long.Parse(parts[1]) : (long?)null;

            yield return (start, end);
        }
    }
}
