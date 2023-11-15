using AoC.Console.Interfaces;
using System;
using System.Data;

namespace AoC.Console.Days;

internal sealed class Day9 : IDay
{
    public int Year => 2022;

    public int DayNumber => 9;

    public object PartOne(IEnumerable<string> rows)
    {
        var bridge = new Bridge();

        foreach (var moves in rows.Where(row => !string.IsNullOrWhiteSpace(row)))
        {
            var split = moves.Split(' ');
            var move = split[0];
            var steps = int.Parse(split[1]);

            switch (move)
            {
                case "R":
                    MoveRight(steps, bridge);
                    break;
                case "L":
                    MoveLeft(steps, bridge);
                    break;
                case "U":
                    MoveUp(steps, bridge);
                    break;
                case "D":
                    MoveDown(steps, bridge);
                    break;
            }
        }

        return bridge.VisitedCount;
    }

    private (int Row, int Column) CheckTail((int Row, int Column) head, (int Row, int Column) tail)
    {
        if ((head.Row - 2, head.Column) == tail)
        {
            return (head.Row - 1, head.Column);
        }

        if ((head.Row + 2, head.Column) == tail)
        {
            return (head.Row + 1, head.Column);
        }

        if ((head.Row, head.Column + 2) == tail)
        {
            return (head.Row, head.Column + 1);
        }

        if ((head.Row, head.Column - 2) == tail)
        {
            return (head.Row, head.Column - 1);
        }

        if ((head.Row - 2, head.Column - 1) == tail || (head.Row - 2, head.Column + 1) == tail)
        {
            return (head.Row - 1, head.Column);
        }

        if ((head.Row + 2, head.Column - 1) == tail || (head.Row + 2, head.Column + 1) == tail)
        {
            return (head.Row + 1, head.Column);
        }

        if ((head.Row - 1, head.Column - 2) == tail || (head.Row + 1, head.Column - 2) == tail)
        {
            return (head.Row, head.Column - 1);
        }

        if ((head.Row - 1, head.Column + 2) == tail || (head.Row + 1, head.Column + 2) == tail)
        {
            return (head.Row, head.Column + 1);
        }




        if ((head.Row - 2, head.Column - 2) == tail)
        {
            return (head.Row - 1, head.Column - 1);
        }

        if ((head.Row + 2, head.Column - 2) == tail)
        {
            return (head.Row + 1, head.Column - 1);
        }

        if ((head.Row + 2, head.Column + 2) == tail)
        {
            return (head.Row + 1, head.Column + 1);
        }

        if ((head.Row - 2, head.Column + 2) == tail)
        {
            return (head.Row - 1, head.Column + 1);
        }

        return tail;
    }

    private void MoveDown(int steps, Bridge bridge)
    {
        for (int i = 0; i < steps; i++)
        {
            bridge.Head = (bridge.Head.Row - 1, bridge.Head.Column);
            bridge.Tail = CheckTail(bridge.Head, bridge.Tail);
            bridge.AddTailVisit();

            var head = bridge.Head;
            for (int j = 0; j < bridge.Rope.Count; j++)
            {
                if (j > 0) head = bridge.Rope[j - 1];
                bridge.Rope[j] = CheckTail(head, bridge.Rope[j]);
            }
            bridge.AddNineTailVisit();
            //if (Math.Abs(bridge.Tail.Row - bridge.Head.Row) == 2)
            //{
            //    bridge.Tail = (bridge.Head.Row + 1, bridge.Head.Column);
            //    bridge.AddTailVisit();
            //}
        }
    }

    private void MoveUp(int steps, Bridge bridge)
    {
        for (int i = 0; i < steps; i++)
        {
            bridge.Head = (bridge.Head.Row + 1, bridge.Head.Column);

            bridge.Tail = CheckTail(bridge.Head, bridge.Tail);
            bridge.AddTailVisit();

            var head = bridge.Head;
            for (int j = 0; j < bridge.Rope.Count; j++)
            {
                if (j > 0) head = bridge.Rope[j - 1];
                bridge.Rope[j] = CheckTail(head, bridge.Rope[j]);
            }
            bridge.AddNineTailVisit();
            //if (Math.Abs(bridge.Tail.Row - bridge.Head.Row) == 2)
            //{
            //    bridge.Tail = (bridge.Head.Row - 1, bridge.Head.Column);
            //    bridge.AddTailVisit();
            //}
        }
    }

    private void MoveLeft(int steps, Bridge bridge)
    {
        for (int i = 0; i < steps; i++)
        {
            bridge.Head = (bridge.Head.Row, bridge.Head.Column - 1);
            bridge.Tail = CheckTail(bridge.Head, bridge.Tail);
            bridge.AddTailVisit();

            var head = bridge.Head;
            for (int j = 0; j < bridge.Rope.Count; j++)
            {
                if (j > 0) head = bridge.Rope[j - 1];
                bridge.Rope[j] = CheckTail(head, bridge.Rope[j]);
            }
            bridge.AddNineTailVisit();
            //if (Math.Abs(bridge.Tail.Column - bridge.Head.Column) == 2)
            //{
            //    bridge.Tail = (bridge.Head.Row, bridge.Head.Column + 1);
            //    bridge.AddTailVisit();
            //}
        }
    }

    private void MoveRight(int steps, Bridge bridge)
    {
        for (int i = 0; i < steps; i++)
        {
            bridge.Head = (bridge.Head.Row, bridge.Head.Column + 1);

            bridge.Tail = CheckTail(bridge.Head, bridge.Tail);
            bridge.AddTailVisit();

            var head = bridge.Head;
            for (int j = 0; j < bridge.Rope.Count; j++)
            {
                if (j > 0) head = bridge.Rope[j - 1];
                bridge.Rope[j] = CheckTail(head, bridge.Rope[j]);
            }
            bridge.AddNineTailVisit();
            //if (Math.Abs(bridge.Tail.Column - bridge.Head.Column) == 2)
            //{
            //    bridge.Tail = (bridge.Head.Row, bridge.Head.Column - 1);
            //    bridge.AddTailVisit();
            //}
        }
    }

    public object PartTwo(IEnumerable<string> rows)
    {
        return 0;
    }

    private sealed class Bridge
    {
        public (int Row, int Column) Head { get; set; } = (Row: 0, Column: 0);

        public (int Row, int Column) Tail { get; set; } = (Row: 0, Column: 0);

        public List<(int Row, int Column)> Visited { get; set; } = new List<(int Row, int Column)> { (Row: 0, Column: 0) };

        public int VisitedCount { get; set; } = 1;

        public int NineTailVisit { get; set; } = 1;

        public List<(int Row, int Column)> Rope { get; set; } = Enumerable.Repeat((Row: 0, Column: 0), 9).ToList();

        public List<(int Row, int Column)> NineVisited { get; set; } = new List<(int Row, int Column)> { (Row: 0, Column: 0) };

        public void AddTailVisit()
        {
            if (!NineVisited.Exists(visited => visited.Row == Tail.Row && visited.Column == Tail.Column))
            {
                VisitedCount++;
                Visited.Add(Tail);
            }
        }

        public void AddNineTailVisit()
        {
            var tail = Rope.ElementAt(8);
            if (!NineVisited.Exists(visited => visited.Row == tail.Row && visited.Column == tail.Column))
            {
                NineTailVisit++;
                NineVisited.Add(tail);
            }
        }
    }
}

