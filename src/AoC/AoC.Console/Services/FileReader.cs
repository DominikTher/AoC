using AoC.Console.Interfaces;

namespace AoC.Console.Services;

internal class FileReader : IFileReader
{
    public Task<string> ReadAllTextAsync(string path)
        => File.ReadAllTextAsync(path);
}
