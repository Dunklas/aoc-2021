using System;
using System.Collections.Generic;
using System.Linq;

namespace aoc_2021.solutions;

public class Day7
{
    public void Solve(string input)
    {
        Console.WriteLine(SolvePart1(input));
        Console.WriteLine(SolvePart2(input));
    }

    public long SolvePart1(string input)
    {
        var crabs = ParseInput(input);
        return Enumerable.Range(0, crabs.Count())
            .Select(pos => crabs
                .Select(crab => Math.Abs(crab - pos))
                .Sum())
            .Min();
    }

    public long SolvePart2(string input)
    {
        var crabs = ParseInput(input);
        return Enumerable.Range(0, crabs.Count())
            .Select(pos => crabs
                .Select(crab => Math.Abs(crab - pos))
                .Select(distance => Enumerable.Range(1, distance)
                    .Aggregate(0, (acc, i) => acc + i))
                .Sum())
            .Min();
    }

    private IEnumerable<int> ParseInput(string input)
        => input.Split(",")
            .Select(i => int.Parse(i));
}
