using AoC.Console.Interfaces;

namespace AoC.Console.Days._2024
{
    public sealed class Day5 : IDay
    {
        public int Year => 2024;

        public int DayNumber => 5;

        public object PartOne(IEnumerable<string> rows)
        {
            var fillPageNumbers = false;
            var rules = new List<(int, int)>();
            var pages = new List<List<int>>();

            foreach (var row in rows)
            {
                if (string.IsNullOrWhiteSpace(row))
                {
                    fillPageNumbers = true;
                    continue;
                }

                if (fillPageNumbers)
                {
                    pages.Add(row.Split(',').Select(int.Parse).ToList());
                }
                else
                {
                    var rule = row.Split('|');
                    rules.Add((int.Parse(rule[0]), int.Parse(rule[1])));
                }
            }

            return pages
                .Where(page =>
                    page.Select((number, index) =>
                    {
                        var pageRules = rules.Where(x => x.Item1 == number && page.Contains(x.Item2)).Select(x => x.Item2);
                        return pageRules.All(x => page.IndexOf(x) > index);
                    })
                    .All(x => x))
                .Sum(x => x[x.Count / 2]);
        }

        public object PartTwo(IEnumerable<string> rows)
        {
            var fillPageNumbers = false;
            var rules = new List<(int, int)>();
            var pages = new List<List<int>>();

            foreach (var row in rows)
            {
                if (string.IsNullOrWhiteSpace(row))
                {
                    fillPageNumbers = true;
                    continue;
                }

                if (fillPageNumbers)
                {
                    pages.Add(row.Split(',').Select(int.Parse).ToList());
                }
                else
                {
                    var rule = row.Split('|');
                    rules.Add((int.Parse(rule[0]), int.Parse(rule[1])));
                }
            }

            return pages
                .Select(page =>
                {
                    var pageRules = rules.Where(x => page.Contains(x.Item1) && page.Contains(x.Item2)).Select(x => (Current: x.Item1, Next: x.Item2)).OrderBy(x => x.Current);
                    var orderedPage = new List<int>(page);

                    var updatedPage = pageRules.Aggregate(orderedPage, (currentPage, rule) =>
                    {
                        var (Current, Next) = rule;
                        var indexOfCurrent = currentPage.IndexOf(Current);
                        var indexOfNext = currentPage.IndexOf(Next);

                        if (indexOfCurrent > indexOfNext)
                        {
                            currentPage[indexOfNext] = Current;
                            currentPage.RemoveAt(indexOfCurrent);
                            currentPage = currentPage.Take(indexOfNext + 1)
                                                     .Append(Next)
                                                     .Concat(currentPage.Skip(indexOfNext + 1))
                                                     .ToList();
                        }

                        return currentPage;
                    });

                    return (page.SequenceEqual(updatedPage), updatedPage);
                })
                .Where(x => x.Item1 == false)
                .Sum(x => x.updatedPage[x.updatedPage.Count / 2]);
        }
    }
}
