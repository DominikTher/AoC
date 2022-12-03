namespace AoC.Console.Interfaces;

internal interface IDayExecuter
{
    Task Execute(int year, int dayNumber);
}