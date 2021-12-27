using System;
using System.Collections.Generic;
using System.Linq;
using aoc_2021.utils;

namespace aoc_2021.solutions;

public class Day9
{
    public void Solve(string input)
    {
        Console.WriteLine(SolvePart1(input));
        Console.WriteLine(SolvePart2(input));
    }

    public long SolvePart1(string input)
    {
        var heightMap = ParseInput(input);
        return LowPoints(heightMap)
            .Select(c => heightMap[c] + 1)
            .Sum();
    }

    public long SolvePart2(string input)
    {
        var heightMap = ParseInput(input);
        return LowPoints(heightMap)
            .Select(lp => Basin(lp, heightMap, new HashSet<Coordinate>()))
            .Select(basin => basin.Count)
            .OrderByDescending(c => c)
            .Take(3)
            .Aggregate(1, (acc, n) => acc * n);
    }

    private HashSet<Coordinate> Basin(Coordinate source, Dictionary<Coordinate, int> heightMap, HashSet<Coordinate> visited)
    {
        if (heightMap[source] == 9)
            return visited;
        if (visited.Contains(source))
            return visited;
        visited.Add(source);
        Adjacent(source)
            .Where(n => heightMap.ContainsKey(n))
            .Where(n => heightMap[n] > heightMap[source])
            .ToList()
            .ForEach(n => Basin(n, heightMap, visited));
        return visited;
    }

    private IEnumerable<Coordinate> LowPoints(Dictionary<Coordinate, int> heightMap) =>
        heightMap.Keys
            .Where(c => Adjacent(c)
                .Where(n => heightMap.ContainsKey(n))
                .All(n => heightMap[n] > heightMap[c]));

    private IEnumerable<Coordinate> Adjacent(Coordinate source) =>
        new List<Coordinate>
        {
            new Coordinate(source.X, source.Y - 1),
            new Coordinate(source.X + 1, source.Y),
            new Coordinate(source.X, source.Y + 1),
            new Coordinate(source.X - 1, source.Y)
        };

    private Dictionary<Coordinate, int> ParseInput(string input)
        => input.Split("\n")
            .Select((line, y) => line.ToCharArray()
                .Select((c, x) => (new Coordinate(x, y), (int)Char.GetNumericValue(c))))
            .SelectMany(x => x)
            .ToDictionary(x => x.Item1, x => x.Item2);
}
