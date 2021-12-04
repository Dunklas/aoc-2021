using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

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
        var parsed = ParseInput(input);
        for (int i = 4; i < parsed.Item1.Count(); i++)
        {
            var drawedNumbers = parsed.Item1.GetRange(0, i + 1).ToHashSet();
            var winner = parsed.Item2
                .Where(board =>
                    board.AllRows.Any(row => row.IsSubsetOf(drawedNumbers))
                )
                .FirstOrDefault();
            if (winner != null)
            {
                var allNumbers = winner.AllRows
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
        var winners = new List<Board>();
        for (int i = 4; i < parsed.Item1.Count(); i++)
        {
            var drawedNumbers = parsed.Item1.GetRange(0, i + 1).ToHashSet();
            parsed.Item2
                .Where(board =>
                    board.AllRows.Any(row => row.IsSubsetOf(drawedNumbers)
                ))
                .ToList()
                .ForEach(winner =>
                {
                    if (!winners.Contains(winner))
                    {
                        winners.Add(winner);
                        lastDrawedNumbers = drawedNumbers;
                    }
                });
        }
        var lastWinner = winners.Last();
        var allNumbers = lastWinner.AllRows
            .SelectMany(row => row)
            .ToHashSet();
        var sum = allNumbers.Except(lastDrawedNumbers)
            .Sum();
        return sum * lastDrawedNumbers.Last();
    }

    private (List<int>, List<Board>) ParseInput(string input)
    {
        var parts = input.Split("\n\n");
        var numbers = parts[0].Split(",")
            .Select(x => int.Parse(x))
            .ToList();

        var boards = Enumerable.Range(1, parts.Count() - 1)
            .Select(i =>
            {
                var rows = new HashSet<List<int>>();
                var columns = new HashSet<List<int>>();
                var rawRows = parts[i].Split("\n");
                for (int j = 0; j < 5; j++)
                {
                    var rowNumbers = Regex.Split(rawRows[j].Trim(), " +");
                    var row = new List<int>();
                    for (int k = 0; k < 5; k++)
                    {
                        row.Add(int.Parse(rowNumbers[k]));
                    }
                    rows.Add(row);
                }
                for (int j = 0; j < 5; j++)
                {
                    columns.Add(rows.ToList()
                        .Select(row => row[j])
                        .ToList());
                }
                return new Board(rows, columns);
            })
            .ToList();
        return (numbers, boards);
    }
}

public class Board
{
    public List<HashSet<int>> AllRows { get; }

    public Board(HashSet<List<int>> rows, HashSet<List<int>> columns)
    {
        AllRows = rows.Union(columns)
            .Select(row => row.ToHashSet())
            .ToList();
    }
}
