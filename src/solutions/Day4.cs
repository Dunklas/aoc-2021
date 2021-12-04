using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace aoc_2021.solutions;
public record BingoInput(List<uint> Numbers, List<(uint, bool)[][]> Boards);
public class Day4
{
    public void Solve(string input)
    {
        Console.WriteLine(SolvePart1(input));
        Console.WriteLine(SolvePart2(input));
    }

    public int SolvePart1(string input)
    {
        var parsed = ParseInput(input);
        var firstBoard = parsed.Boards.First();
        return 42;
    }

    public int SolvePart2(string input)
    {
        return 42;
    }

    private BingoInput ParseInput(string input)
    {
        var parts = input.Split("\n\n");
        var numbers = parts[0].Split(",")
            .Select(x => uint.Parse(x))
            .ToList();

        var boards = Enumerable.Range(1, parts.Count() - 1)
            .Select(i =>
            {
                var rows = parts[i].Split("\n");
                var board = new (uint, bool)[5][];
                for (int j = 0; j < 5; j++)
                {
                    board[j] = new (uint, bool)[5];
                    var rowNumbers = Regex.Split(rows[j].Trim(), " +");
                    for (int k = 0; k < 5; k++)
                    {
                        board[j][k] = (uint.Parse(rowNumbers[k]), false);
                    }
                }
                return board;
            })
            .ToList();
        return new BingoInput(numbers, boards);
    }
}
