using System;
using System.Collections.Generic;
using System.Linq;

namespace aoc_2021.solutions;

public class Day5
{
    public void Solve(string input)
    {
        Console.WriteLine(SolvePart1(input));
        Console.WriteLine(SolvePart2(input));
    }

    public int SolvePart1(string input)
    {
        var coordinatePairs = ParseInput(input)
            .Where(pair => pair.IsHorizontalOrVertical())
            .ToList();
        return NumberOverlaps(coordinatePairs);
    }

    public int SolvePart2(string input)
    {
        var coordinatePairs = ParseInput(input)
            .ToList();
        return NumberOverlaps(coordinatePairs);
    }

    private int NumberOverlaps(List<CoordinatePair> coordinatePairs)
    {
        var dangerZone = new HashSet<Coordinate>();
        var hydroThermalVents = new HashSet<Coordinate>();
        coordinatePairs.ForEach(pair =>
            pair.Line().ForEach(coordinate =>
            {
                if (!hydroThermalVents.Add(coordinate))
                    dangerZone.Add(coordinate);
            })
        );
        return dangerZone.Count();
    }

    private List<CoordinatePair> ParseInput(string input)
    {
        return input.Split("\n")
            .Select(line => line.Split("->"))
            .Select(parts => new CoordinatePair(Coordinate.FromString(parts[0]), Coordinate.FromString(parts[1])))
            .ToList();
    }
}

public class CoordinatePair
{
    public Coordinate First { get; }
    public Coordinate Second { get; }

    public CoordinatePair(Coordinate first, Coordinate second)
    {
        First = first;
        Second = second;
    }

    public bool IsHorizontalOrVertical()
        => IsHorizontal() || IsVertical();
    public bool IsHorizontal()
        => First.Y == Second.Y;
    public bool IsVertical()
        => First.X == Second.X;

    public List<Coordinate> Line()
    {
        var leftMost = Math.Min(First.X, Second.X) == First.X ? First : Second;
        var topMost = Math.Min(First.Y, Second.Y) == First.Y ? First : Second;
        var rightMost = leftMost.Equals(First) ? Second : First;
        var bottomMost = topMost.Equals(First) ? Second : First;
        IEnumerable<(int First, int Second)> range = null;
        if (IsHorizontal())
            range = Enumerable.Range(leftMost.X, rightMost.X - leftMost.X + 1)
                .Zip(Enumerable.Repeat(First.Y, rightMost.X - leftMost.X + 1));
        if (IsVertical())
            range = Enumerable.Repeat(First.X, bottomMost.Y - topMost.Y + 1)
                .Zip(Enumerable.Range(topMost.Y, bottomMost.Y - topMost.Y + 1));
        if (!IsHorizontalOrVertical() && leftMost.Equals(topMost))
            range = Enumerable.Range(leftMost.X, rightMost.X - leftMost.X + 1)
                .Zip(Enumerable.Range(topMost.Y, bottomMost.Y - topMost.Y + 1));
        if (!IsHorizontalOrVertical() && leftMost.Equals(bottomMost))
            range = Enumerable.Range(leftMost.X, rightMost.X - leftMost.X + 1)
                .Zip(Enumerable.Range(topMost.Y, bottomMost.Y - topMost.Y + 1).Reverse());
        return range
            .Select(pair => new Coordinate(pair.First, pair.Second))
            .ToList();
    }
}

public struct Coordinate
{
    public int X { get; }
    public int Y { get; }

    public Coordinate(int x, int y)
    {
        X = x;
        Y = y;
    }

    public static Coordinate FromString(string text)
    {
        var parts = text.Split(",");
        return new Coordinate(int.Parse(parts[0].Trim()), int.Parse(parts[1].Trim()));
    }
}
