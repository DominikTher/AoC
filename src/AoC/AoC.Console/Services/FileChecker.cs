using AoC.Console.Interfaces;

namespace AoC.Console.Services;

internal class FileChecker : IFileChecker
{
    public bool FileExist(string path)
        => File.Exists(path);
}
