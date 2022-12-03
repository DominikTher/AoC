namespace AoC.Console.Interfaces;

internal interface IFileWriter
{
    Task WriteAllTextAsync(string path, string content);
}