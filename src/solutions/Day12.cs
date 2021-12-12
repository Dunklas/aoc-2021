using System;
using System.Collections.Generic;
using System.Linq;

namespace aoc_2021.solutions;

public class Day12
{
    public void Solve(string input)
    {
        Console.WriteLine(SolvePart1(input));
        Console.WriteLine(SolvePart2(input));
    }

    public long SolvePart1(string input)
    {
        var cave = Cave.FromString(input);
        return cave.NumberOfPaths(true);
    }

    public long SolvePart2(string input)
    {
        var cave = Cave.FromString(input);
        return cave.NumberOfPaths(false);
    }
}

public class Cave
{
    private Dictionary<string, List<string>> _graph;

    private Cave(Dictionary<string, List<string>> graph)
    {
        _graph = graph;
    }

    public static Cave FromString(string raw) =>
        new Cave(raw.Split("\n")
            .Select(row => row.Trim().Split("-"))
            .Aggregate(new Dictionary<string, List<string>>(), (cave, edge) =>
            {
                if (cave.ContainsKey(edge[0]))
                    cave.GetValueOrDefault(edge[0]).Add(edge[1]);
                else
                    cave.Add(edge[0], new List<string>{ edge[1] });
                if (cave.ContainsKey(edge[1]))
                    cave.GetValueOrDefault(edge[1]).Add(edge[0]);
                else
                    cave.Add(edge[1], new List<string>{ edge[0] });
                return cave;
            }));
    
    public int NumberOfPaths(bool restrictive) => NumberOfPaths("start", "end", new Dictionary<string, int>(), restrictive);
    
    private int NumberOfPaths(string source, string destination, Dictionary<string, int> visited, bool restrictive)
    {
        visited[source] = visited.GetValueOrDefault(source) + 1;
        if (source == destination)
        {
            visited[source] = visited.GetValueOrDefault(source) - 1;
            return 1;
        }
        var totalPaths = _graph.GetValueOrDefault(source)
            .Where(neighbour => restrictive && AllowedToVisitRestrictive(neighbour, visited) || !restrictive && AllowedToVisit(neighbour, visited))
            .Select(neighbour => NumberOfPaths(neighbour, destination, visited, restrictive))
            .Sum();
        visited[source] = visited.GetValueOrDefault(source) - 1;
        return totalPaths;
    }

    private bool AllowedToVisitRestrictive(string node, Dictionary<string, int> visited) =>
        Large(node) || visited.GetValueOrDefault(node) == 0;

    private bool AllowedToVisit(string node, Dictionary<string, int> visited)
    {
        if (node.Equals("start"))
            return false;
        if (Large(node))
            return true;
        if (visited.GetValueOrDefault(node) == 0)
            return true;
        var visitedSmallCaveTwice = visited.Keys
            .Where(cave => !Large(cave))
            .Any(smallCave => visited.GetValueOrDefault(smallCave) == 2);
        if (visitedSmallCaveTwice)
            return false;
        return true;
    }

    private bool Large(string node) => node.Equals(node.ToUpper());
}