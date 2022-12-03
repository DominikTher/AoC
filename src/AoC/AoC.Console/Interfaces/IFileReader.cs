namespace AoC.Console.Interfaces;

internal interface IFileReader
{
    public Task<string> ReadAllTextAsync(string path);
}