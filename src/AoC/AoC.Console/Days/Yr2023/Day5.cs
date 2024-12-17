using AoC.Console.Interfaces;
using System.Collections.Concurrent;
using System.Security.Cryptography;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace AoC.Console.Days.Yr2023;

public sealed record MapData(long Destination, long Source, long Length);

public sealed class Day5 : IDay
{
    public int Year => 2023;

    public int DayNumber => 5;

    public object PartOne(IEnumerable<string> rows)
    {
        var seeds = Regex.Matches(rows.First().Split(":")[1], @"\d+").Select(match => long.Parse(match.Value));

        //var mapsData = new Dictionary<string, (List<MapData> MapData, Dictionary<long, long> Map)>();
        var mapsData = new Dictionary<string, List<MapData>>();
        var lastMapName = string.Empty;

        foreach (var row in rows.Skip(2))
        {
            if (string.IsNullOrWhiteSpace(row))
            {
                continue;
            }

            var mapNameGroup = Regex.Match(row, @"(.+-.+-.+) map");
            if (mapNameGroup.Success)
            {
                lastMapName = mapNameGroup.Groups[1].Value;
                mapsData.Add(lastMapName, []);
                continue;
            }

            var data = Regex.Matches(row, @"\d+").Select(match => long.Parse(match.Value)).ToArray();
            mapsData[lastMapName].Add(new MapData(data[0], data[1], data[2]));
        }

        //foreach (var (mapName, maps) in mapsData)
        //{
        //    foreach (var map in maps.MapData)
        //    {
        //        for (var i = 0; i < map.Length; i++)
        //        {
        //            maps.Map.Add(map.Source + i, map.Destination + i);
        //        }
        //    }
        //}

        var results = new List<long>();

        foreach (var seed in seeds)
        {
            long source = seed;
            long destination;

            foreach (var (mapName, maps) in mapsData)
            {
                //if (!maps.Map.TryGetValue(source, out destination))
                //{
                //     destination = source;

                //}

                // check if in range

                foreach (var item in maps)
                {
                    if (item.Source <= source && source <= item.Source + item.Length - 1)
                    {
                        source = (source - item.Source) + item.Destination;
                        break;
                    }
                }

                //source = destination;
            }

            results.Add(source);
        }

        return results.Min();
    }

    public object PartTwo(IEnumerable<string> rows)
    {
        var x = Regex.Matches(rows.First().Split(":")[1], @"\d+").Select(match => long.Parse(match.Value)).ToList();

        //var a = GetSeeds(x).ToArray();

        //var seeds = new List<long>();
        //for (var i = 0; i < x.Count; i += 2)
        //{
        //    for (var j = x[i]; j < x[i] + x[i + 1]; j++)
        //    {
        //        seeds.Add(j);
        //    }
        //}

        //var mapsData = new Dictionary<string, (List<MapData> MapData, Dictionary<long, long> Map)>();
        var mapsData = new Dictionary<string, List<MapData>>();
        var lastMapName = string.Empty;

        foreach (var row in rows.Skip(2))
        {
            if (string.IsNullOrWhiteSpace(row))
            {
                continue;
            }

            var mapNameGroup = Regex.Match(row, @"(.+-.+-.+) map");
            if (mapNameGroup.Success)
            {
                lastMapName = mapNameGroup.Groups[1].Value;
                mapsData.Add(lastMapName, []);
                continue;
            }

            var data = Regex.Matches(row, @"\d+").Select(match => long.Parse(match.Value)).ToArray();
            mapsData[lastMapName].Add(new MapData(data[0], data[1], data[2]));
        }

        //foreach (var (mapName, maps) in mapsData)
        //{
        //    foreach (var map in maps.MapData)
        //    {
        //        for (var i = 0; i < map.Length; i++)
        //        {
        //            maps.Map.Add(map.Source + i, map.Destination + i);
        //        }
        //    }
        //}

        var results = new List<long>();

        var r = GetSeeds(x).Chunk(int.MaxValue / 1000000).AsParallel().Select(x =>
        {
            var max = long.MaxValue;
            foreach (var number in x)
            {
                long source = number;
                long destination;

                foreach (var (mapName, maps) in mapsData)
                {
                    //if (!maps.Map.TryGetValue(source, out destination))
                    //{
                    //     destination = source;

                    //}

                    // check if in range

                    foreach (var item in maps)
                    {
                        if (item.Source <= source && source <= item.Source + item.Length - 1)
                        {
                            source = (source - item.Source) + item.Destination;
                            break;
                        }
                    }

                    //source = destination;
                }

                if (source < max)
                {
                    max = source;
                }
            }

            return max;
        }).ToArray();


        //var set = new HashSet<long>();
        //var max = long.MaxValue;
        //foreach (var seed in GetSeeds(x))
        //{
        //    if(set.Contains(seed))
        //    {
        //        System.Console.WriteLine("Skip");
        //        continue;
        //    }

        //    set.Add(seed);
        //    long source = seed;
        //    long destination;

        //    foreach (var (mapName, maps) in mapsData)
        //    {
        //        //if (!maps.Map.TryGetValue(source, out destination))
        //        //{
        //        //     destination = source;

        //        //}

        //        // check if in range

        //        foreach (var item in maps)
        //        {
        //            if (item.Source <= source && source <= item.Source + item.Length - 1)
        //            {
        //                source = (source - item.Source) + item.Destination;
        //                break;
        //            }
        //        }

        //        //source = destination;
        //    }

        //    if (source < max)
        //    {
        //        max = source;
        //    }
        //    //results.Add(source);
        //}

        return r.Min();
    }

    public IEnumerable<long> GetSeeds(List<long> initial)
    {
        for (var i = 0; i < initial.Count; i += 2)
        {
            for (var j = initial[i]; j < initial[i] + initial[i + 1]; j++)
            {
                yield return j;
            }
        }
    }
}
