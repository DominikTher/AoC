using AoC.Console.Extensions;
using AoC.Console.Interfaces;

namespace AoC.Console.Days._2024
{
    public sealed class Day6 : IDay
    {
        public int Year => 2024;

        public int DayNumber => 6;

        // Directions: 0 = up, 1 = right, 2 = down, 3 = left.
        static int[][] directions = new int[][] {
            new int[] {-1, 0},  // up
            new int[] {0, 1},   // right
            new int[] {1, 0},   // down
            new int[] {0, -1}   // left
        };

        public object PartOne(IEnumerable<string> r)
        {
            // Read input map from file "task.txt"
            string[] lines = r.WithoutNullOrWhiteSpace().ToArray();

            int rows = lines.Length;
            int cols = lines[0].Length;
            char[][] grid = new char[rows][];
            for (int i = 0; i < rows; i++)
            {
                grid[i] = lines[i].ToCharArray();
            }

            // Locate the guard's starting position and its facing direction (indicated by '^', '>', 'v', or '<')
            int guardRow = -1, guardCol = -1, guardDir = -1;
            bool found = false;
            for (int i = 0; i < rows && !found; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    char ch = grid[i][j];
                    if (ch == '^' || ch == '>' || ch == 'v' || ch == '<')
                    {
                        guardRow = i;
                        guardCol = j;
                        if (ch == '^')
                            guardDir = 0;
                        else if (ch == '>')
                            guardDir = 1;
                        else if (ch == 'v')
                            guardDir = 2;
                        else if (ch == '<')
                            guardDir = 3;

                        // Replace the guard symbol with an empty cell.
                        grid[i][j] = '.';
                        found = true;
                        break;
                    }
                }
            }

            if (!found)
            {
                return 0;
            }

            // PART 2: Try placing an extra obstruction on every eligible empty cell
            int loopObstructionCount = 0;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    // Skip the guard’s starting cell.
                    if (i == guardRow && j == guardCol)
                        continue;

                    // Only consider cells that are empty (i.e. '.')
                    if (grid[i][j] == '.')
                    {
                        // Place a temporary obstruction.
                        grid[i][j] = '#';
                        // If this obstruction causes the guard to loop, count it.
                        if (DoesGuardLoop(grid, guardRow, guardCol, guardDir))
                        {
                            loopObstructionCount++;
                        }
                        // Revert the cell to the original state.
                        grid[i][j] = '.';
                    }
                }
            }

            return loopObstructionCount;
        }


        // Simulates the guard’s patrol using loop detection.
        // Returns true if a loop is detected, false if the guard eventually leaves the grid.
        static bool DoesGuardLoop(char[][] grid, int startRow, int startCol, int startDir)
        {
            int rows = grid.Length, cols = grid[0].Length;
            int r = startRow, c = startCol, d = startDir;
            var visitedStates = new HashSet<string>();

            while (true)
            {
                // Create a unique key for the current state: position (r, c) and direction d.
                string state = $"{r},{c},{d}";
                if (visitedStates.Contains(state))
                {
                    // Loop detected.
                    return true;
                }
                visitedStates.Add(state);

                // Compute the next cell in the current direction.
                int nr = r + directions[d][0];
                int nc = c + directions[d][1];

                // If stepping off the grid, the guard escapes without looping.
                if (nr < 0 || nr >= rows || nc < 0 || nc >= cols)
                    return false;

                // If there is an obstruction ahead, turn right 90°.
                if (grid[nr][nc] == '#')
                {
                    d = (d + 1) % 4;
                }
                else
                {
                    // Otherwise, take a step forward.
                    r = nr;
                    c = nc;
                }
            }
        }

        //public object PartOne(IEnumerable<string> rows)
        //{
        //    var startPositions = new Dictionary<char, (Func<int, int> rowIncrement, Func<int, int> colIncrement, char next)>
        //    {
        //        { '^', (_ => _ - 1, _ => _, '>') },
        //        { '>', (_ => _, _ => _ + 1, 'v') },
        //        { 'v', (_ => _ + 1, _ => _, '<') },
        //        { '<', (_ => _, _ => _ - 1, '^') },
        //    };

        //    var data = rows.WithoutNullOrWhiteSpace().ToList();
        //    var map = new char[data.Count, data.First().Length];
        //    var startPosition = (-1, -1);
        //    var incrementFunctions = startPositions['^'];

        //    for (int row = 0; row < data.Count; row++)
        //    {
        //        for (int column = 0; column < data[row].Length; column++)
        //        {
        //            map[row, column] = data[row][column];

        //            if (startPositions.TryGetValue(map[row, column], out var value))
        //            {
        //                startPosition = (row, column);
        //                incrementFunctions = value;
        //            }
        //        }
        //    }

        //    var visitedPlaces = new HashSet<(int, int)>();

        //    while (true)
        //    {
        //        if (map[startPosition.Item1, startPosition.Item2] != '#')
        //        {
        //            visitedPlaces.Add(startPosition);
        //        }

        //        var nextPosition = (incrementFunctions.rowIncrement(startPosition.Item1), incrementFunctions.colIncrement(startPosition.Item2));

        //        if (nextPosition.Item1 < 0 || nextPosition.Item2 < 0 || nextPosition.Item1 > map.GetLength(0) - 1 || nextPosition.Item2 > map.GetLength(1) - 1)
        //        {
        //            break;
        //        }

        //        if (map[nextPosition.Item1, nextPosition.Item2] == '#')
        //        {
        //            incrementFunctions = startPositions[incrementFunctions.next];
        //            startPosition = (incrementFunctions.rowIncrement(startPosition.Item1), incrementFunctions.colIncrement(startPosition.Item2));
        //        }
        //        else
        //        {
        //            startPosition = nextPosition;
        //        }
        //    }

        //    return visitedPlaces.Count;
        //}

        public object PartTwo(IEnumerable<string> r)
        {
            var map = r.WithoutNullOrWhiteSpace().ToArray();
            int rows = map.Length;
            int cols = map[0].Length;
            char[,] grid = new char[rows, cols];
            (int x, int y) start = (0, 0);
            int startDx = 0, startDy = 0;

            // Parse map and find guard's start
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    grid[i, j] = map[i][j];
                    if ("^>v<".Contains(map[i][j]))
                    {
                        start = (i, j);
                        switch (map[i][j])
                        {
                            case '^': startDx = -1; startDy = 0; break;
                            case '>': startDx = 0; startDy = 1; break;
                            case 'v': startDx = 1; startDy = 0; break;
                            case '<': startDx = 0; startDy = -1; break;
                        }
                    }
                }
            }

            int loopPositions = 0;

            // Test each position
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if ((i == start.x && j == start.y) || grid[i, j] == '#') continue; // Skip start and existing obstacles

                    // Temporarily place obstruction
                    char original = grid[i, j];
                    grid[i, j] = '#';

                    // Simulate patrol
                    if (DoesGuardLoop(grid, start, startDx, startDy))
                        loopPositions++;

                    // Restore original
                    grid[i, j] = original;
                }
            }

            return loopPositions;
        }

        static bool DoesGuardLoop(char[,] grid, (int x, int y) start, int startDx, int startDy)
        {
            int rows = grid.GetLength(0);
            int cols = grid.GetLength(1);
            HashSet<(int x, int y, int dx, int dy)> states = new HashSet<(int x, int y, int dx, int dy)>();
            (int x, int y) pos = start;
            int dx = startDx, dy = startDy;

            while (true)
            {
                var state = (pos.x, pos.y, dx, dy);
                if (!states.Add(state)) // Loop detected
                    return true;

                int nextX = pos.x + dx;
                int nextY = pos.y + dy;

                // Exit map = no loop
                if (nextX < 0 || nextX >= rows || nextY < 0 || nextY >= cols)
                    return false;

                if (grid[nextX, nextY] == '#')
                {
                    // Turn right: (dx, dy) -> (dy, -dx)
                    int temp = dx;
                    dx = dy;
                    dy = -temp;
                }
                else
                {
                    pos = (nextX, nextY);
                }
            }
        }

        //public object PartTwo(IEnumerable<string> rows)
        //{
        //    var startPositions = new Dictionary<char, (Func<int, int> rowIncrement, Func<int, int> colIncrement, char next)>
        //    {
        //        { '^', (_ => _ - 1, _ => _, '>') },
        //        { '>', (_ => _, _ => _ + 1, 'v') },
        //        { 'v', (_ => _ + 1, _ => _, '<') },
        //        { '<', (_ => _, _ => _ - 1, '^') },
        //    };

        //    var data = rows.WithoutNullOrWhiteSpace().ToList();
        //    var map = new char[data.Count, data.First().Length];
        //    var startPosition = (-1, -1);
        //    var incrementFunctions = startPositions['^'];

        //    for (int row = 0; row < data.Count; row++)
        //    {
        //        for (int column = 0; column < data[row].Length; column++)
        //        {
        //            map[row, column] = data[row][column];

        //            if (startPositions.TryGetValue(map[row, column], out var value))
        //            {
        //                startPosition = (row, column);
        //                incrementFunctions = value;
        //            }
        //        }
        //    }

        //    var visitedPlaces = new HashSet<(int, int)>();
        //    map[6, 3] = 'O';
        //    var obstructionPosition = (6, 3);
        //    var originalPosition = startPosition;
        //    var originalInstructions = incrementFunctions;
        //    var c = 0;
        //    var timesOfVisited = 0;
        //    var tmpVisited = new List<(int, int)>();
        //    var f = false;
        //    var res = new List<(int, int)>();

        //    while (map[data.Count - 1, data.First().Length - 1] != 'O')
        //    {
        //        if (map[startPosition.Item1, startPosition.Item2] != '#' && f)
        //        {
        //            if (!visitedPlaces.Add(startPosition) && startPosition != originalPosition)
        //            {
        //                timesOfVisited++;
        //                tmpVisited.Add((startPosition.Item1, startPosition.Item2));
        //                // todo temp visited, check next or insert difference between positions, must be one max
        //                // if all last group by 2 
        //                // or

        //                var tmp = tmpVisited.IndexOf(startPosition);
        //                var x = tmpVisited.SkipWhile(x => x != startPosition).Skip(1).ToList();
        //                var y = tmpVisited.Skip(tmp - x.Count - 1).Take(x.Count).ToList();


        //                if (x.Count > 0 && y.Count > 0 && x.SequenceEqual(y))
        //                {

        //                }



        //                if (tmpVisited.GroupBy(x => x).Select(x => x.Count()).All(x => x == 2))
        //                {
        //                    res.Add(obstructionPosition);
        //                    c++;
        //                    var prev = obstructionPosition;


        //                    try
        //                    {
        //                        while (map[obstructionPosition.Item1, obstructionPosition.Item2] != '.')
        //                        {
        //                            obstructionPosition.Item2++;

        //                            if (obstructionPosition.Item2 == map.GetLength(0) - 1)
        //                            {
        //                                obstructionPosition = (++obstructionPosition.Item1, 0);
        //                            }
        //                        }
        //                    }
        //                    catch (Exception)
        //                    {

        //                        break;
        //                    }

        //                    map[prev.Item1, prev.Item2] = '.';
        //                    map[obstructionPosition.Item1, obstructionPosition.Item2] = 'O';
        //                    visitedPlaces.Clear();
        //                    tmpVisited.Clear();
        //                    timesOfVisited = 0;
        //                    startPosition = originalPosition;
        //                    incrementFunctions = originalInstructions;
        //                    f = false;
        //                    continue;
        //                }


        //            }
        //        }

        //        var nextPosition = (incrementFunctions.rowIncrement(startPosition.Item1), incrementFunctions.colIncrement(startPosition.Item2));

        //        if (nextPosition.Item1 < 0 || nextPosition.Item2 < 0 || nextPosition.Item1 > map.GetLength(0) - 1 || nextPosition.Item2 > map.GetLength(1) - 1)
        //        {
        //            if (obstructionPosition.Item1 >= map.GetLength(0) || obstructionPosition.Item2 >= map.GetLength(1))
        //            {
        //                break;
        //            }

        //            var prev = obstructionPosition;
        //            try
        //            {
        //                while (map[obstructionPosition.Item1, obstructionPosition.Item2] != '.')
        //                {
        //                    obstructionPosition.Item2++;

        //                    if (obstructionPosition.Item2 == map.GetLength(0) - 1)
        //                    {
        //                        obstructionPosition = (++obstructionPosition.Item1, 0);
        //                    }
        //                }
        //            }
        //            catch (Exception)
        //            {

        //                break;
        //            }
        //            map[prev.Item1, prev.Item2] = '.';
        //            map[obstructionPosition.Item1, obstructionPosition.Item2] = 'O';
        //            visitedPlaces.Clear();
        //            tmpVisited.Clear();
        //            timesOfVisited = 0;
        //            startPosition = originalPosition;
        //            incrementFunctions = originalInstructions;
        //            f = false;
        //            continue;
        //        }

        //        if (map[nextPosition.Item1, nextPosition.Item2] == '#' || map[nextPosition.Item1, nextPosition.Item2] == 'O')
        //        {
        //            // spatne?
        //            incrementFunctions = startPositions[incrementFunctions.next];
        //            startPosition = (incrementFunctions.rowIncrement(startPosition.Item1), incrementFunctions.colIncrement(startPosition.Item2));
        //        }
        //        else
        //        {
        //            startPosition = nextPosition;
        //        }

        //        f = true;
        //    }

        //    return c;
        //}
    }
}
