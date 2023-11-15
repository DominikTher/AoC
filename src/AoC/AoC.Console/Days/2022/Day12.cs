using AoC.Console.Interfaces;
using System.Security.Cryptography.X509Certificates;

namespace AoC.Console.Days;

internal sealed class Day12 : IDay
{
    public int Year => 2022;

    public int DayNumber => 12;

    public object PartOne(IEnumerable<string> rows)
    {
        var data = rows
            .Where(row => !string.IsNullOrEmpty(row))
            .Select(row => row)
            .ToList();

        var d = data.First().Length;
        var graph = new char[data.Count, d];

        var start = (x: 0, y: 0);
        var end = (x: 0, y: 0);

        for (int row = 0; row < data.Count; row++)
        {
            for (int column = 0; column < d; column++)
            {
                var c = data[row].ElementAt(column);

                if (c == 'E')
                {
                    c = 'z';
                    end.x = row;
                    end.y = column;
                }

                if (c == 'S')
                {
                    c = 'a';
                    start.x = row;
                    start.y = column;
                }

                graph[row, column] = c;
            }
        }

        var dimension = graph.GetLength(0) - 1;
        var queue = new Queue<(int x, int y)>();
        queue.Enqueue((start.x, start.y));

        var visited = new List<(int x, int y)>
        {
            //{ (0, 0) }
        };

        var distance = new Dictionary<(int x, int y), int>
        {
            { (start.x, start.y), 0 }
        };

        var dx = new int[] { -1, 0, 1, 0 };
        var dy = new int[] { 0, 1, 0, -1 };

        while (queue.Count > 0)
        {
            var (x, y) = queue.Dequeue();

            if (visited.Contains((x, y)))
            {
                continue;
            }

            visited.Add((x, y));

            for (int i = 0; i < 4; i++)
            {
                if (IsValid(x + dx[i], y + dy[i]))
                {
                    var newX = x + dx[i];
                    var newY = y + dy[i];

                    try
                    {
                        var a = (graph[newX, newY] - '0');
                        var b = (graph[x, y] - '0');

                        if (a - b <= 1)
                        {
                            if (distance.ContainsKey((newX, newY)))
                            {
                                var dst = distance[(x, y)];
                                var curr = distance[(newX, newY)];

                                if (curr < dst)
                                {
                                    distance[(newX, newY)] += 1;
                                }
                            }
                            else
                            {
                                distance.Add((newX, newY), distance[(x, y)] + 1);
                            }

                            //visited.Add((x, y));
                            queue.Enqueue((newX, newY));
                        }
                    }
                    catch (Exception exception)
                    {

                        //throw;
                    }
                }
            }
        }

        bool IsValid(int x, int y)
        {
            if (x < 0 || x > data.Count || y < 0 || y > d)
            {
                return false;
            }

            if (visited.Contains((x, y)))
            {
                return false;
            }

            return true;
        }

        var result = distance[(end.x, end.y)];

        return 0;
    }



    public object PartTwo(IEnumerable<string> rows)
    {
        var data = rows
            .Where(row => !string.IsNullOrEmpty(row))
            .Select(row => row)
            .ToList();

        var d = data.First().Length;
        var graph = new char[data.Count, d];

        var startingPoints = new List<(int x, int y)>();

        //var start = (x: 0, y: 0);
        var end = (x: 0, y: 0);

        for (int row = 0; row < data.Count; row++)
        {
            for (int column = 0; column < d; column++)
            {
                var c = data[row].ElementAt(column);

                if (c == 'E')
                {
                    c = 'z';
                    end.x = row;
                    end.y = column;
                }

                if (c == 'S' || c == 'a')
                {
                    c = 'a';
                    //start.x = row;
                    //start.y = column;
                    startingPoints.Add((row, column));
                }


                graph[row, column] = c;
            }
        }

        var dimension = graph.GetLength(0) - 1;

        var dx = new int[] { -1, 0, 1, 0 };
        var dy = new int[] { 0, 1, 0, -1 };

        var r = new List<int>();

        foreach (var startPoint in startingPoints)
        {
            var queue = new Queue<(int x, int y)>();
            queue.Enqueue((startPoint.x, startPoint.y));

            var visited = new List<(int x, int y)>
            {
                //{ (0, 0) }
            };

            var distance = new Dictionary<(int x, int y), int>
            {
                { (startPoint.x, startPoint.y), 0 }
            };

            while (queue.Count > 0)
            {
                var (x, y) = queue.Dequeue();

                if (visited.Contains((x, y)))
                {
                    continue;
                }

                visited.Add((x, y));

                for (int i = 0; i < 4; i++)
                {
                    if (IsValid(x + dx[i], y + dy[i]))
                    {
                        var newX = x + dx[i];
                        var newY = y + dy[i];

                        try
                        {
                            var a = (graph[newX, newY] - '0');
                            var b = (graph[x, y] - '0');

                            if (a - b <= 1)
                            {
                                if (distance.ContainsKey((newX, newY)))
                                {
                                    var dst = distance[(x, y)];
                                    var curr = distance[(newX, newY)];

                                    if (curr < dst)
                                    {
                                        distance[(newX, newY)] += 1;
                                    }
                                }
                                else
                                {
                                    distance.Add((newX, newY), distance[(x, y)] + 1);
                                }

                                //visited.Add((x, y));
                                queue.Enqueue((newX, newY));
                            }
                        }
                        catch (Exception exception)
                        {

                            //throw;
                        }
                    }
                }
            }

            try
            {
                var result = distance[(end.x, end.y)];
                r.Add(result);
            }
            catch (Exception exception)
            {

                //throw;
            }

            bool IsValid(int x, int y)
            {
                if (x < 0 || x > data.Count || y < 0 || y > d)
                {
                    return false;
                }

                if (visited.Contains((x, y)))
                {
                    return false;
                }

                return true;
            }
        }

        return 0;
    }
}

