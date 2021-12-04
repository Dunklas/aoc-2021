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
        for (int round = 4; round < parsed.Numbers.Count(); round++)
        {
            var drawedNumbers = parsed.Numbers.GetRange(0, round + 1);
            var winner = parsed.Boards
                .Where(board => board.AllRows.Any(row => row.IsSubsetOf(drawedNumbers)))
                .FirstOrDefault();
            if (winner != null)
                return CalculateScore(winner, drawedNumbers);
        }
        return -1;
    }

    public int SolvePart2(string input)
    {
        var parsed = ParseInput(input);
        List<int> lastDrawedNumbers = null;
        var winners = new List<Board>();
        for (int round = 4; round < parsed.Numbers.Count(); round++)
        {
            if (winners.Count() == parsed.Boards.Count())
                break;

            var drawedNumbers = parsed.Numbers.GetRange(0, round + 1);
            parsed.Boards
                .Where(board => !winners.Contains(board))
                .Where(board => board.AllRows.Any(row => row.IsSubsetOf(drawedNumbers)))
                .ToList()
                .ForEach(newWinner =>
                {
                    winners.Add(newWinner);
                    lastDrawedNumbers = drawedNumbers;
                });
        }
        var lastWinner = winners.Last();
        return CalculateScore(lastWinner, lastDrawedNumbers);
    }

    private int CalculateScore(Board board, List<int> drawedNumbers)
    {
        var allBoardNumbers = board.AllRows
            .SelectMany(row => row)
            .ToHashSet();
        var sum = allBoardNumbers.Except(drawedNumbers)
            .Sum();
        return sum * drawedNumbers.Last(); 
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
        return new BingoInput(numbers, boards);
    }
}

public readonly record struct BingoInput(List<int> Numbers, List<Board> Boards);

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
