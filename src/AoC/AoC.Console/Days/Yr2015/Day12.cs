using AoC.Console.Interfaces;
using System.Text.Json;

namespace AoC.Console.Days.Yr2015;

public sealed class Day12 : IDay
{
    public int Year => 2015;

    public int DayNumber => 12;

    public object PartOne(IEnumerable<string> rows)
    {
        int Traverse(JsonElement jsonElement)
            => jsonElement.ValueKind switch
            {
                JsonValueKind.Object when jsonElement.EnumerateObject().Any(
                    p => p.Value.ValueKind == JsonValueKind.String && p.Value.GetString() == "red") => 0,
                JsonValueKind.Object => jsonElement.EnumerateObject().Select(o => Traverse(o.Value)).Sum(),
                JsonValueKind.Array => jsonElement.EnumerateArray().Select(Traverse).Sum(),
                JsonValueKind.Number => jsonElement.GetInt32(),
                _ => 0
            };
        var x = Traverse(JsonDocument.Parse(rows.First()).RootElement);
        var jsonString = JsonSerializer.Deserialize<List<Dictionary<string, object>>>(rows.First());
        return 0;
    }

    public object PartTwo(IEnumerable<string> rows)
    {
        throw new NotImplementedException();
    }
}
