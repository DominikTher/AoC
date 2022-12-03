namespace AoC.Console.Interfaces;

internal interface IDay
{
    public int Year { get; }
    public int DayNumber { get; }
    public int PartOne(IEnumerable<string> rows);
    public int PartTwo(IEnumerable<string> rows);
}
