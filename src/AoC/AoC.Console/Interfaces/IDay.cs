namespace AoC.Console.Interfaces;

internal interface IDay
{
    public int Year { get; }
    public int DayNumber { get; }
    public object PartOne(IEnumerable<string> rows);
    public object PartTwo(IEnumerable<string> rows);
}
