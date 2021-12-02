using System;
using System.Collections.Generic;
using System.Linq;

namespace aoc_2021.solutions;

readonly record struct Instruction(string Action, int Value);

public class Day2
{
    public void Solve(string input)
    {
        Console.WriteLine(SolvePart1(input));
        Console.WriteLine(SolvePart2(input));
    }

    public int SolvePart1(string input)
    {
        var hPos = 0;
        var depth = 0;
        var instructions = ParseInput(input);
        instructions.ForEach(instruction =>
        {
            switch (instruction.Action)
            {
                case "forward":
                    hPos += instruction.Value;
                    break;
                case "down":
                    depth += instruction.Value;
                    break;
                case "up":
                    depth -= instruction.Value;
                    break;
            }
        });
        return hPos * depth;
    }

    public int SolvePart2(string input)
    {
        var hPos = 0;
        var depth = 0;
        var aim = 0;
        var instructions = ParseInput(input);
        instructions.ForEach(instruction =>
        {
            switch (instruction.Action)
            {
                case "forward":
                    hPos += instruction.Value;
                    depth += (aim * instruction.Value);
                    break;
                case "down":
                    aim += instruction.Value;
                    break;
                case "up":
                    aim -= instruction.Value;
                    break;
            }
        });
        return hPos * depth;
    }

    private List<Instruction> ParseInput(string input)
    {
        return input.Split("\n")
            .Select(line => line.Split(" "))
            .Select(parts => new Instruction(parts[0], int.Parse(parts[1])))
            .ToList();
    }
}
