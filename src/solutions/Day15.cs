using System;
using System.Collections.Generic;
using System.Linq;
using aoc_2021.utils;

namespace aoc_2021.solutions;

public class Day15
{
    public void Solve(string input)
    {
        Console.WriteLine(SolvePart1(input));
        Console.WriteLine(SolvePart2(input));
    }

    public long SolvePart1(string input) =>
        new Graph(input, false).MinRisk();
    
    public long SolvePart2(string input) =>
        new Graph(input, true).MinRisk();
}

public class Graph
{
    Dictionary<Coordinate, int> riskLevels;

    public Graph(string raw, bool full)
    {
        riskLevels = raw.Split("\n")
            .Select((line, i) => line.ToCharArray()
                .Select((weight, j) => (new Coordinate(j, i), (int)Char.GetNumericValue(weight))))
            .SelectMany(pair => pair)
            .ToDictionary(pair => pair.Item1, pair => pair.Item2);
        if (!full)
            return;
        var width = riskLevels.Keys.Select(c => c.X).Max() + 1;
        var height = riskLevels.Keys.Select(c => c.Y).Max() + 1;
        for (int y = 0; y < 5 * height; y++)
            for (int x = 0; x < 5 * width; x++)
            {
                if (y < height && x < width)
                    continue;
                var originalWeight = riskLevels[y < height ? new Coordinate(x - width, y) : new Coordinate(x, y - height)];
                var newWeight = originalWeight + 1 == 10 ? 1 : originalWeight + 1;
                riskLevels.Add(new Coordinate(x, y), newWeight);
            }
    }

    public int MinRisk()
    {
        var target = new Coordinate(riskLevels.Keys.Select(c => c.X).Max(), riskLevels.Keys.Select(c => c.Y).Max());
        return MinRisk(new Coordinate(0, 0), target);
    }

    private int MinRisk(Coordinate source, Coordinate target)
    {
        var visited = new HashSet<Coordinate>();
        var distances = riskLevels.Keys
            .ToDictionary(c => c, c => int.MaxValue);
        distances[source] = 0;
        var candidates = new PriorityQueue<Coordinate, int>();
        candidates.Enqueue(source, 0);
        while (candidates.Count != 0)
        {
            var current = candidates.Dequeue();
            visited.Add(current);
            if (current.Equals(target))
                return distances[target];
            var unvisitedNeighbours = Adjacent(current)
                .Where(neighbour => !visited.Contains(neighbour));
            foreach (var n in unvisitedNeighbours)
            {
                var newDistance = distances[current] + riskLevels[n];
                if (newDistance < distances[n])
                {
                    distances[n] = newDistance;
                    candidates.Enqueue(n, newDistance);
                }
            }
        }
        return -1;
    }

    private IEnumerable<Coordinate> Adjacent(Coordinate c)
    {
        return new List<Coordinate>
        {
            new Coordinate(c.X, c.Y + 1),
            new Coordinate(c.X + 1, c.Y),
            new Coordinate(c.X, c.Y - 1),
            new Coordinate(c.X - 1, c.Y)
        }.Where(coord => riskLevels.ContainsKey(coord));
    }
}
