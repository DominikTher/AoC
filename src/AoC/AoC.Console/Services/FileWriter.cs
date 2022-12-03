using AoC.Console.Interfaces;

namespace AoC.Console;

internal sealed class FileWriter : IFileWriter
{
    public async Task WriteAllTextAsync(string path, string content)
    {
        var directoryPath = Path.GetDirectoryName(path) ?? throw new ArgumentException(nameof(path));

        if(!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }

        await File.WriteAllTextAsync(path, content);
    }
}
