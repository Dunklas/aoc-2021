using System;
using System.Collections.Generic;
using System.Linq;

namespace aoc_2021.solutions;

public class Day14
{
    public void Solve(string input)
    {
        Console.WriteLine(Solve(input, 10));
        Console.WriteLine(Solve(input, 40));
    }

    public long Solve(string input, int steps)
    {
        var (template, rules) = ParseInput(input);
        var counts = RunPairInsertation(template, rules, steps);
        var orderedCounts = counts
            .OrderByDescending(pair => pair.Value)
            .Select(pair => pair.Value);
        return orderedCounts.First() - orderedCounts.Last();
    }

    private Dictionary<char, long> RunPairInsertation(string template, Dictionary<string, char> rules, int steps)
    {
        var cache = rules.ToDictionary
        (
            rule => rule.Key,
            rule => (Value: rule.Value, NewPairs: new List<string>{ $"{rule.Key[0]}{rule.Value}", $"{rule.Value}{rule.Key[1]}"})   
        );
        var counts = template.ToCharArray()
            .GroupBy(c => c)
            .ToDictionary(c => c.Key, c => (long)c.Count());
        var pairs = Enumerable.Range(0, template.Count() - 1)
            .Select(i => template.Substring(i, 2))
            .GroupBy(pair => pair)
            .Select(byPair => (byPair.Key, Count: byPair.Count()))
            .ToDictionary(x => x.Key, x => (long)x.Count);
        for (int i = 0; i < steps; i++)
        {
            var newPairs = new Dictionary<string, long>();
            foreach (var pair in pairs)
            {
                var newValue = cache[pair.Key].Value;
                var numOcurrences = pair.Value;
                counts[newValue] = counts.GetValueOrDefault(newValue) + numOcurrences;

                foreach (var newPair in cache[pair.Key].NewPairs)
                    newPairs[newPair] = newPairs.GetValueOrDefault(newPair) + numOcurrences;
            }
            pairs = newPairs;
        }
        return counts;
    }

    private (string, Dictionary<string, char>) ParseInput(string input)
    {
        var parts = input.Split("\n\n");
        var rules = parts[1].Split("\n")
            .Select(line => line.Split(" -> "))
            .ToDictionary(ruleParts => ruleParts[0], ruleParts => ruleParts[1].ToCharArray().Single());
        return (parts[0], rules);
    }
}
