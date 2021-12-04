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
        var parsedInput = ParseInput(input);
        for (int round = 4; round < parsedInput.Numbers.Count(); round++)
        {
            var drawedNumbers = parsedInput.Numbers.GetRange(0, round + 1);
            var winner = parsedInput.Boards
                .Where(board => board.AllRows.Any(row => row.IsSubsetOf(drawedNumbers)))
                .FirstOrDefault();
            if (winner != null)
                return CalculateScore(winner, drawedNumbers);
        }
        return -1;
    }

    public int SolvePart2(string input)
    {
        var parsedInput = ParseInput(input);
        List<int> lastDrawedNumbers = null;
        var winners = new List<Board>();
        for (int round = 4; round < parsedInput.Numbers.Count(); round++)
        {
            if (winners.Count() == parsedInput.Boards.Count())
                break;

            var drawedNumbers = parsedInput.Numbers.GetRange(0, round + 1);
            parsedInput.Boards
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
            .Select(rawBoardIndex =>
            {
                var rawRows = parts[rawBoardIndex].Split("\n");
                var rows = rawRows.ToList()
                    .Select(rawRow => Regex.Split(rawRow.Trim(), " +"))
                    .Select(row => row.Select(n => int.Parse(n)).ToList())
                    .ToList();
                var columns = Enumerable.Range(0, 5)
                    .Select(i => rows.Select(row => row[i]).ToList())
                    .ToList();
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

    public Board(List<List<int>> rows, List<List<int>> columns)
    {
        AllRows = rows.Concat(columns)
            .Select(row => row.ToHashSet())
            .ToList();
    }
}
