using AoC.Console.Interfaces;
using System.Text.Json;

namespace AoC.Console.Days;

internal sealed class Day13 : IDay
{
    public int Year => 2022;
    public int DayNumber => 13;

    public object PartOne(IEnumerable<string> rows)
    {
        var data = rows.Where(row => !string.IsNullOrWhiteSpace(row));
        var results = new List<(bool?, int)>();
        var index = 1;

        foreach (var item in data.Chunk(2))
        {
            var left = JsonSerializer.Deserialize<JsonElement[]>(item[0]);
            var right = JsonSerializer.Deserialize<JsonElement[]>(item[1]);

            var result = Compare(left!, right!);

            bool? Compare(JsonElement[] left, JsonElement[] right)
            {
                var dimension = Math.Max(left.Length, right.Length);

                for (int i = 0; i < dimension; i++)
                {
                    if (i == left.Length) return true;
                    if (i == right.Length) return false;

                    if (left[i].ValueKind == JsonValueKind.Number && right[i].ValueKind == JsonValueKind.Number)
                    {
                        if (left[i].GetInt32() > right[i].GetInt32())
                        {
                            return false;
                        }

                        if (left[i].GetInt32() < right[i].GetInt32())
                        {
                            return true;
                        }

                        continue;
                    }

                    if (left[i].ValueKind == JsonValueKind.Array || right[i].ValueKind == JsonValueKind.Array)
                    {             
                        var lA = left[i].ValueKind == JsonValueKind.Number ? new JsonElement[] { left[i] } : left[i].EnumerateArray().ToArray();
                        var rA = right[i].ValueKind == JsonValueKind.Number ? new JsonElement[] { right[i] } : right[i].EnumerateArray().ToArray();

                        var inner = Compare(lA, rA);

                        if (inner != null) return inner;
                    }
                }

                return null;
            }

            results.Add((result, index++));
        }

        return results.Where(x => x.Item1.HasValue && x.Item1.Value).Sum(x => x.Item2);
    }

    public object PartTwo(IEnumerable<string> rows)
    {
        var data = rows.Where(row => !string.IsNullOrWhiteSpace(row));
        var results = new List<(bool?, int)>();
        var index = 1;

        var packets = new List<JsonElement[]>();

        foreach (var item in data.Chunk(2))
        {
            var left = JsonSerializer.Deserialize<JsonElement[]>(item[0]);
            var right = JsonSerializer.Deserialize<JsonElement[]>(item[1]);

            var result = Compare(left!, right!);

            bool? Compare(JsonElement[] left, JsonElement[] right)
            {
                var dimension = Math.Max(left.Length, right.Length);

                for (int i = 0; i < dimension; i++)
                {
                    if (i == left.Length) return true;
                    if (i == right.Length) return false;

                    if (left[i].ValueKind == JsonValueKind.Number && right[i].ValueKind == JsonValueKind.Number)
                    {
                        if (left[i].GetInt32() > right[i].GetInt32())
                        {
                            return false;
                        }

                        if (left[i].GetInt32() < right[i].GetInt32())
                        {
                            return true;
                        }

                        continue;
                    }

                    if (left[i].ValueKind == JsonValueKind.Array || right[i].ValueKind == JsonValueKind.Array)
                    {
                        var lA = left[i].ValueKind == JsonValueKind.Number ? new JsonElement[] { left[i] } : left[i].EnumerateArray().ToArray();
                        var rA = right[i].ValueKind == JsonValueKind.Number ? new JsonElement[] { right[i] } : right[i].EnumerateArray().ToArray();

                        var inner = Compare(lA, rA);

                        if (inner != null) return inner;
                    }
                }

                return null;
            }

            if(result == true)
            {
                packets.Add(left);
                packets.Add(right);
            }
            else
            {
                packets.Add(right);
                packets.Add(left);
            }

            

            results.Add((result, index++));
        }

        var ordered = packets.OrderBy(x =>
        {
            if (x.Length == 0) return 0;

            return GetNumber(x[0]);

            int GetNumber(JsonElement jsonElement)
            {
                if (jsonElement.ValueKind == JsonValueKind.Undefined)
                    return 0;

                if (jsonElement.ValueKind == JsonValueKind.Number)
                {
                    return jsonElement.GetInt32();
                }
                else
                {
                    try
                    {
                        return GetNumber(jsonElement.EnumerateArray().ToList().FirstOrDefault());
                    }
                    catch (Exception e)
                    {

                        throw;
                    }
                }
            }
        }).ToList();

        return results.Where(x => x.Item1.HasValue && x.Item1.Value).Sum(x => x.Item2);
    }
}