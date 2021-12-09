using System;
using System.Collections.Generic;
using System.Linq;

namespace aoc_2021.solutions;

public class Day6
{
    public void Solve(string input)
    {
        Console.WriteLine(Solve(input, 80));
        Console.WriteLine(Solve(input, 256));
    }

    public long Solve(string input, int daysToSimulate)
    {
        var numBirthsByDayOfWeek = new long[7];
        var tooYoungToGiveBirth = new long[daysToSimulate + 2];
        Parse(input).ToList()
            .ForEach(fish => numBirthsByDayOfWeek[fish % 7] += 1);
        Enumerable.Range(1, daysToSimulate - 1)
            .ToList()
            .ForEach(day =>
            {
                var newFish = numBirthsByDayOfWeek[day % 7] - tooYoungToGiveBirth[day];
                tooYoungToGiveBirth[day + 2] = newFish;
                numBirthsByDayOfWeek[(day + 2) % 7] += newFish;
            });
        return numBirthsByDayOfWeek
            .Sum();
    }

    private IEnumerable<int> Parse(string input) =>
        input.Split(',')
            .Select(i => int.Parse(i));
}
