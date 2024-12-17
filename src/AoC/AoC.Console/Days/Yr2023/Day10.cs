using AoC.Console.Extensions;
using AoC.Console.Interfaces;

namespace AoC.Console.Days.Yr2023;

public sealed class Day10 : IDay
{
    public int Year => 2023;

    public int DayNumber => 10;

    public object PartOne(IEnumerable<string> rows)
    {
        var tiles = rows.WithoutNullOrWhiteSpace().ToArray();
        var area = new char[tiles.Length, tiles.First().Length];
        (int x, int y) start = (0, 0);

        for (var i = 0; i < tiles.Length; i++)
        {
            for (var j = 0; j < tiles.First().Length; j++)
            {
                var tile = tiles[i][j];
                area[i, j] = tile;

                if (tile == 'S')
                {
                    start = (i, j);
                }
            }
        }

        var graph = new Dictionary<Tile, List<Tile>>();
        var t = new Tile(start.x, start.y, 'S');
        graph[t] = GetNeigbours(start.x, start.y, area).ToList();

        BreadthFirstSearch(graph, area);
        return 0;
    }

    public IEnumerable<Tile> GetNeigbours(int x, int y, char[,] area)
    {
        yield return new Tile(x, y + 1, area[x, y + 1]);
        yield return new Tile(x, y - 1, area[x, y - 1]);
        yield return new Tile(x + 1, y, area[x + 1, y]);
        yield return new Tile(x - 1, y, area[x - 1, y]);
    }

    public record Tile(int X, int Y, char Label)
    {
        public string GetKey() => $"{X},{Y}";
    }

    public void BreadthFirstSearch(Dictionary<Tile, List<Tile>> graph, char[,] area)
    {
        var searchQueue = new Queue<Tile>(graph.First().Value);
        var searched = new HashSet<string>();

        while (searchQueue.Count > 0)
        {
            var tile = searchQueue.Dequeue();
            if (!searched.Contains(tile.GetKey()))
            {
                if (IsLoop(tile.GetKey()))
                {
                    break;
                }
                else
                {
                    foreach (var item in GetNeigbours(tile.X, tile.Y, area))
                    {
                        searchQueue.Enqueue(item);
                    }
                }
            }

            searched.Add(tile.GetKey());
        }

        static bool IsLoop(string person)
            => person[^1] == 'm';
    }

    public object PartTwo(IEnumerable<string> rows)
    {
        throw new NotImplementedException();
    }
}
