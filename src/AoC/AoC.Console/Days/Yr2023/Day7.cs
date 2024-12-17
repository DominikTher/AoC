using AoC.Console.Extensions;
using AoC.Console.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace AoC.Console.Days.Yr2023
{
    public enum Type
    {
        FiveOfKind = 7,
        FourOfKInd = 6,
        FullHouse = 5,
        ThreeOfKind = 4,
        TwoPair = 3,
        OnePair = 2,
        HighCard = 1
    }
    public sealed class Day7 : IDay
    {
        public int Year => 2023;

        public int DayNumber => 7;

        public object PartOne(IEnumerable<string> rows)
        {
            var games = new List<(string, int, Type)>();
            foreach (var row in rows.WithoutNullOrWhiteSpace())
            {
                var split = row.Split(" ");
                games.Add((split[0], int.Parse(split[1]), GetType(split[0])));
            }

            var comparer = new CardComparer();
            var fiveOfKInd = games.Where(x => x.Item3 == Type.FiveOfKind).OrderBy(c => c.Item1, comparer).ToList();
            var fourOfKInd = games.Where(x => x.Item3 == Type.FourOfKInd).OrderBy(c => c.Item1, comparer).ToList();
            var fullHouse = games.Where(x => x.Item3 == Type.FullHouse).OrderBy(c => c.Item1, comparer).ToList();
            var threeOfKInd = games.Where(x => x.Item3 == Type.ThreeOfKind).OrderBy(c => c.Item1, comparer).ToList();
            var twoPair = games.Where(x => x.Item3 == Type.TwoPair).OrderBy(c => c.Item1, comparer).ToList();
            var onePair = games.Where(x => x.Item3 == Type.OnePair).OrderBy(c => c.Item1, comparer).ToList();
            var highCard = games.Where(x => x.Item3 == Type.HighCard).OrderBy(c => c.Item1, comparer).ToList();

            var rank = 1;
            var h = highCard.Aggregate(0, (a, b) => a + (b.Item2 * rank++));
            var o = onePair.Aggregate(0, (a, b) => a + (b.Item2 * rank++));
            var t = twoPair.Aggregate(0, (a, b) => a + (b.Item2 * rank++));
            var th = threeOfKInd.Aggregate(0, (a, b) => a + (b.Item2 * rank++));
            var f = fullHouse.Aggregate(0, (a, b) => a + (b.Item2 * rank++));
            var fo = fourOfKInd.Aggregate(0, (a, b) => a + (b.Item2 * rank++));
            var fi = fiveOfKInd.Aggregate(0, (a, b) => a + (b.Item2 * rank++));

            return h + o + t + th + f + fo + fi;
        }

        private Type GetType(string cards)
        {
            var joker = cards.Any(c => c == 'J');
            var diff = string.Join("", cards.Where(c => c != 'J').GroupBy(c => c).Select(g => g.Key));

            var type = TypeInternal(cards);

            if (type != Type.FiveOfKind && joker)
            {
                foreach (var item in diff)
                {
                    var newCards = cards.Replace('J', item);
                    var newType = TypeInternal(newCards);

                    if(newType > type)
                    {
                        type = newType;
                    }
                }
            }

            return type;
        }

        private static Type TypeInternal(string cards)
        {
            if (cards.GroupBy(c => c).Count() == 1)
                return Type.FiveOfKind;

            if (cards.GroupBy(c => c).Any(c => c.Count() == 4))
                return Type.FourOfKInd;

            if (cards.GroupBy(c => c).Any(x => x.Count() == 2) &&
                cards.GroupBy(c => c).Any(x => x.Count() == 3))
                return Type.FullHouse;

            if (cards.GroupBy(c => c).Any(c => c.Count() == 3))
                return Type.ThreeOfKind;

            if (cards.GroupBy(c => c).Count() == 3)
                return Type.TwoPair;

            if (cards.GroupBy(c => c).Count() == 4)
                return Type.OnePair;

            return Type.HighCard;
        }

        public object PartTwo(IEnumerable<string> rows)
        {
            throw new NotImplementedException();
        }
    }

    public sealed class CardComparer : IComparer<string>
    {
        public int Compare(string? x, string? y)
        {
            var cards = new char[] { 'A', 'K', 'Q', 'T', '9', '8', '7', '6', '5', '4', '3', '2', 'J', };
            for (int i = 0; i < x.Length; i++)
            {
                if (x[i] == y[i])
                {
                    continue;
                }

                if (char.IsNumber(x[i]))
                {
                    var f = Array.IndexOf(cards, x[i]);
                    var s = Array.IndexOf(cards, y[i]);
                    if (f < s)
                        return 1;
                    else
                        return -1;
                }

                var first = Array.IndexOf(cards, x[i]);
                var second = Array.IndexOf(cards, y[i]);
                if (first < second)
                    return 1;
                else
                    return -1;
            }

            return 0;
        }
    }
}
