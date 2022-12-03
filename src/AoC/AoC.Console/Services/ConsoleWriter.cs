using AoC.Console.Interfaces;

namespace AoC.Console;

internal sealed class ConsoleWriter : IConsoleWriter
{
    public void WriteLine(string message)
    {
        System.Console.WriteLine(message);
    }
}
