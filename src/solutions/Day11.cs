using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using aoc_2021.utils;

namespace aoc_2021.solutions;

public class Day11
{
    public void Solve(string input)
    {
        Console.WriteLine(SolvePart1(input, 100));
        Console.WriteLine(SolvePart2(input));
    }

    public long SolvePart1(string input, int steps)
    {
        var octopuses = ParseInput(input);
        var x = Enumerable.Range(0, steps).ToList()
            .Select(step => Simulate(octopuses))
            .Sum();
        return x;
    }

    public long SolvePart2(string input)
    {
        var octopuses = ParseInput(input);
        var step = 1;
        while (true)
        {
            var numFlashers = Simulate(octopuses);
            if (numFlashers == octopuses.Keys.Count())
                return step;
            step++;
        }
    }

    private int Simulate(Dictionary<Coordinate, Octopus> octopuses)
    {
        IncrementEnergy(octopuses);
        var flashersThisStep = Flash(octopuses);
        ResetFlashers(flashersThisStep);
        return flashersThisStep.Count();
    }

    private void IncrementEnergy(Dictionary<Coordinate, Octopus> octopuses)
        => octopuses.Values.ToList().ForEach(o => o.Energy++);

    private HashSet<Octopus> Flash(Dictionary<Coordinate, Octopus> octopuses)
    {
        var priorFlashers = new HashSet<Octopus>();
        while (true)
        {
            var newFlashers = octopuses.Values
                .Where(potentialFlasher => !priorFlashers.Contains(potentialFlasher))
                .Where(potentialFlasher => potentialFlasher.Energy > 9)
                .ToList();
            if (newFlashers.Count() == 0)
                break;
            newFlashers
                .Select(flasher => AdjacentTo(flasher.Coordinate))
                .SelectMany(coordinates => coordinates)
                .Where(coordinate => octopuses.TryGetValue(coordinate, out _))
                .ToList()
                .ForEach(octopus => octopuses.GetValueOrDefault(octopus).Energy++);
            priorFlashers.UnionWith(newFlashers);
        }
        return priorFlashers;
    }

    private void ResetFlashers(HashSet<Octopus> flashers)
        => flashers.ToList().ForEach(flasher => flasher.Energy = 0);

    private List<Coordinate> AdjacentTo(Coordinate source)
    {
        return new List<Coordinate>
        {
            new Coordinate(source.X - 1, source.Y - 1),
            new Coordinate(source.X, source.Y - 1),
            new Coordinate(source.X + 1, source.Y - 1),
            new Coordinate(source.X + 1, source.Y),
            new Coordinate(source.X + 1, source.Y + 1),
            new Coordinate(source.X, source.Y + 1),
            new Coordinate(source.X - 1, source.Y + 1),
            new Coordinate(source.X - 1, source.Y)
        };
    }

    private Dictionary<Coordinate, Octopus> ParseInput(string input)
    {
        return input.Split('\n')
            .Select((row, y) => row.ToCharArray()
                .Select((energy, x) => new Octopus(new Coordinate(x, y), int.Parse(energy.ToString())))
            )
            .SelectMany(octopus => octopus)
            .ToDictionary(octopus => octopus.Coordinate);
    }
}

public class Octopus
{
    public Coordinate Coordinate { get; }
    public int Energy { get; set; }

    public Octopus(Coordinate coordinate, int energy)
    {
        this.Coordinate = coordinate;
        this.Energy = energy;
    }
}
