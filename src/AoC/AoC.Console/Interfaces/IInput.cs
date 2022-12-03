namespace AoC.Console.Interfaces;

internal interface IInput
{
    Task<IEnumerable<string>> GetRows(int year, int dayNumber);
}