using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace aoc_2021.solutions;

public record BingoInput(List<int> Numbers, List<HashSet<List<int>>> Boards);

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
        for (int i = 4; i < parsed.Numbers.Count(); i++)
        {
            var drawedNumbers = parsed.Numbers.GetRange(0, i + 1).ToHashSet();
            var winner = parsed.Boards
                .Where(board =>
                    board.Any(row =>
                        row.ToHashSet().IsSubsetOf(drawedNumbers)
                    )
                )
                .FirstOrDefault();
            if (winner != null)
            {
                var allNumbers = winner
                    .SelectMany(row => row)
                    .ToHashSet();
                var sum = allNumbers.Except(drawedNumbers)
                    .Sum();
                return sum * drawedNumbers.Last();
            }
        }
        return 42;
    }

    public int SolvePart2(string input)
    {
        var parsed = ParseInput(input);
        HashSet<int> lastDrawedNumbers = null;
        List<HashSet<List<int>>> winners = new List<HashSet<List<int>>>();
        for (int i = 4; i < parsed.Numbers.Count(); i++)
        {
            var drawedNumbers = parsed.Numbers.GetRange(0, i + 1).ToHashSet();
            parsed.Boards
                .Where(board =>
                    board.Any(row =>
                        row.ToHashSet().IsSubsetOf(drawedNumbers)
                    )
                )
                .ToList()
                .ForEach(winner =>
                {
                    if (!winners.Contains(winner))
                    {
                        Console.WriteLine("Board won");
                        winners.Add(winner);
                        lastDrawedNumbers = drawedNumbers;
                    }
                });
        }
        Console.WriteLine(winners.Count());
        var lastWinner = winners.Last();
        var allNumbers = lastWinner
            .SelectMany(row => row)
            .ToHashSet();
        var sum = allNumbers.Except(lastDrawedNumbers)
            .Sum();
        Console.WriteLine("Sum " + sum);
        return sum * lastDrawedNumbers.Last();
    }

    private BingoInput ParseInput(string input)
    {
        var parts = input.Split("\n\n");
        var numbers = parts[0].Split(",")
            .Select(x => int.Parse(x))
            .ToList();

        var boards = Enumerable.Range(1, parts.Count() - 1)
            .Select(i =>
            {
                var allRows = new HashSet<List<int>>();
                var allColumns = new HashSet<List<int>>();
                var rows = parts[i].Split("\n");
                for (int j = 0; j < 5; j++)
                {
                    var rowNumbers = Regex.Split(rows[j].Trim(), " +");
                    var row = new List<int>();
                    for (int k = 0; k < 5; k++)
                    {
                        row.Add(int.Parse(rowNumbers[k]));
                    }
                    allRows.Add(row);
                }
                for (int j = 0; j < 5; j++)
                {
                    allColumns.Add(allRows.ToList()
                        .Select(row => row[j])
                        .ToList());
                }
                return allRows.Union(allColumns).ToHashSet();
            })
            .ToList();
        return new BingoInput(numbers, boards);
    }
}
