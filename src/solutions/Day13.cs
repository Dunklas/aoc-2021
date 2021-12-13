using System;
using System.Collections.Generic;
using System.Linq;
using aoc_2021.utils;

namespace aoc_2021.solutions;

public class Day13
{
    public void Solve(string input)
    {
        Console.WriteLine(SolvePart1(input));
        Print(SolvePart2(input));
    }

    public long SolvePart1(string input)
    {
        var (coords, folds) = ParseInput(input);
        var firstFold = folds.First();
        if (firstFold.Axis.Equals("y"))
            return FoldHorizontal(coords, firstFold.Value).Distinct().Count();
        return FoldVertical(coords, firstFold.Value).Distinct().Count();
    }

    public HashSet<Coordinate> SolvePart2(string input)
    {
        var (coords, folds) = ParseInput(input);
        foreach (var fold in folds)
        {
            if (fold.Axis == "y")
                coords = FoldHorizontal(coords, fold.Value);
            if (fold.Axis == "x")
                coords = FoldVertical(coords, fold.Value);
        }
        return coords;
    }

    private void Print(HashSet<Coordinate> coordinates)
    {
        var maxX = coordinates.Select(c => c.X).Max();
        var maxY = coordinates.Select(c => c.Y).Max();
        for (int i = 0; i <= maxY; i++)
        {
            for (int j = 0; j <= maxX; j++)
            {
                Console.Write(coordinates.Contains(new Coordinate(j, i)) ? '#' : ' ');
            }
            Console.Write("\n");
        }
    }

    private HashSet<Coordinate> FoldHorizontal(IEnumerable<Coordinate> coordinates, int fold)
    {
        var maxY = coordinates
            .Select(c => c.Y)
            .Max();
        return coordinates
            .Where(c => c.Y > fold)
            .Select(c => new Coordinate(c.X, Math.Abs(c.Y - maxY)))
            .Concat(coordinates.Where(c => c.Y < fold))
            .ToHashSet();
    }

    private HashSet<Coordinate> FoldVertical(IEnumerable<Coordinate> coordinates, int fold)
    {
        var maxX = coordinates
            .Select(c => c.X)
            .Max();
        return coordinates
            .Where(c => c.X > fold)
            .Select(c => new Coordinate(Math.Abs(c.X - maxX), c.Y))
            .Concat(coordinates.Where(c => c.X < fold))
            .ToHashSet();
    }

    private (HashSet<Coordinate>, IEnumerable<(string Axis, int Value)>) ParseInput(string input)
    {
        var parts = input.Split("\n\n");
        var coordinates = parts[0].Split("\n")
            .Select(line => line.Split(","))
            .Select(lineParts => new Coordinate(int.Parse(lineParts[0]), int.Parse(lineParts[1])));
        var folds = parts[1].Split("\n")
            .Select(line => line.Split(" ")[2])
            .Select(line => line.Split("="))
            .Select(lineParts => (lineParts[0], int.Parse(lineParts[1])));
        return (coordinates.ToHashSet(), folds);
    }
}
