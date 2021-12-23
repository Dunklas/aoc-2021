using System;
using System.Collections.Generic;
using System.Linq;
using aoc_2021.utils;

namespace aoc_2021.solutions;

public class Day17
{
    public void Solve(string input)
    {
        Console.WriteLine(SolvePart1(input));
        Console.WriteLine(SolvePart2(input));
    }

    public long SolvePart1(string input)
    {
        var targetArea = ParseInput(input);
        var maxX = targetArea.Select(c => c.X).Max();
        var minY = targetArea.Select(c => c.Y).Min();
        return PotentialVelocities(targetArea, maxX, minY)
            .Where(v => Fire(v.X, v.Y, new Coordinate(0, 0), targetArea, maxX, minY))
            .Select(v => HighestYPos(v))
            .Max();
    }

    public long SolvePart2(string input)
    {
        var targetArea = ParseInput(input);
        var maxX = targetArea.Select(c => c.X).Max();
        var minY = targetArea.Select(c => c.Y).Min();
        return PotentialVelocities(targetArea, maxX, minY)
            .Where(v => Fire(v.X, v.Y, new Coordinate(0, 0), targetArea, maxX, minY))
            .Count();
    }

    private IEnumerable<(int X, int Y)> PotentialVelocities(HashSet<Coordinate> targetArea, int maxX, int minY)
    {
        var velocities = new List<(int x, int y)>();
        for (var y = minY; y < Math.Abs(minY); y++)
            for (var x = 1; x <= maxX; x++)
                velocities.Add((x, y));
        return velocities;
    }

    private bool Fire(int xVel, int yVel, Coordinate pos, HashSet<Coordinate> targetArea, int maxX, int minY)
    {
        var newPos = new Coordinate(pos.X + xVel, pos.Y + yVel);
        if (targetArea.Contains(newPos))
            return true;
        if (newPos.X > maxX || newPos.Y < minY)
            return false;
        var newXVel = xVel > 0 ? xVel - 1 : 0;
        var newYVel = yVel - 1;
        return Fire(newXVel, newYVel, newPos, targetArea, maxX, minY);
    }

    private int HighestYPos((int X, int Y) vel) => vel.Y * (vel.Y + 1) / 2; 

    private HashSet<Coordinate> ParseInput(string input)
    {
        var parts = input.Substring(13, input.Count() - 13).Split(", ");
        var xRange = parts[0].Substring(2, parts[0].Count() - 2).Split("..")
            .Select(x => int.Parse(x))
            .OrderBy(x => x);
        var yRange = parts[1].Substring(2, parts[1].Count() - 2).Split("..")
            .Select(y => int.Parse(y))
            .OrderBy(y => y);
        var coordinates = new HashSet<Coordinate>();
        for (int y = yRange.ElementAt(0); y <= yRange.ElementAt(1); y++)
            for (int x = xRange.ElementAt(0); x <= xRange.ElementAt(1); x++)
                coordinates.Add(new Coordinate(x, y));
        return coordinates;
    }
}
