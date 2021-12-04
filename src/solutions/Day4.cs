using System;
using System.Collections.Generic;
using System.Linq;

namespace aoc_2021.solutions;
public class Day4
{
    public void Solve(string input)
    {
        Console.WriteLine(SolvePart1(input));
        Console.WriteLine(SolvePart2(input));
    }

    public int SolvePart1(string input)
    {
        return 42;
    }

    public int SolvePart2(string input)
    {
        return 42;
    }

    private List<string> ParseInput(string input)
    {
        return input.Split("\n")
            .ToList();
    }
}
