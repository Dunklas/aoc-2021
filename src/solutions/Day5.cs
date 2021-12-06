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
        var xValues = Enumerable.Range(Math.Min(First.X, Second.X), Math.Max(First.X, Second.X) - Math.Min(First.X, Second.X) + 1);
        var yValues = Enumerable.Range(Math.Min(First.Y, Second.Y), Math.Max(First.Y, Second.Y) - Math.Min(First.Y, Second.Y) + 1);
        if (First.X > Second.X)
            xValues = xValues.Reverse();
        if (First.Y > Second.Y)
            yValues = yValues.Reverse();
        IEnumerable<(int First, int Second)> range = null;
        if (IsHorizontal())
            range = xValues.Zip(Enumerable.Repeat(First.Y, xValues.Count()));
        if (IsVertical())
            range = Enumerable.Repeat(First.X, yValues.Count()).Zip(yValues);
        if (!IsHorizontalOrVertical())
            range = xValues.Zip(yValues);
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
