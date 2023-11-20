using AoC.Console.Interfaces;

namespace AoC.Console;

internal sealed class Input : IInput
{
    private readonly HttpClient httpClient;
    private readonly IFileWriter fileWriter;
    private readonly IFileChecker fileChecker;
    private readonly IFileReader fileReader;

    private static readonly string[] separator = ["\r\n", "\r", "\n"];

    public Input(
        HttpClient httpClient, 
        IFileWriter fileWriter, 
        IFileChecker fileChecker,
        IFileReader fileReader)
    {
        this.httpClient = httpClient;
        this.fileWriter = fileWriter;
        this.fileChecker = fileChecker;
        this.fileReader = fileReader;
    }

    public async Task<IEnumerable<string>> GetRows(int year, int dayNumber)
    {
        var path = $"inputs/{year}/day{dayNumber}.txt";
        string content;

        if (!fileChecker.FileExist(path))
        {
            content = await httpClient.GetStringAsync($"{year}/day/{dayNumber}/input");

            await fileWriter.WriteAllTextAsync(path, content);
        }
        else
        {
            content = await fileReader.ReadAllTextAsync(path);
        }

        return content
            .Split(separator, StringSplitOptions.None)
            .AsEnumerable();
    }
}
