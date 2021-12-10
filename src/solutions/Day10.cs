using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace aoc_2021.solutions;

public class Day10
{
    private Dictionary<char, int> _syntaxPoints = new Dictionary<char, int>()
    {
        { ')', 3 },
        { ']', 57 },
        { '}', 1197 },
        { '>', 25137 }
    };

    private Dictionary<char, int> _autocompletePoints = new Dictionary<char, int>()
    {
        { ')', 1 },
        { ']', 2 },
        { '}', 3 },
        { '>', 4 }
    };

    private Dictionary<char, char> _expectedClosings = new Dictionary<char, char>()
    {
        { '(', ')' },
        { '[', ']' },
        { '{', '}' },
        { '<', '>' }
    };

    public void Solve(string input)
    {
        Console.WriteLine(SolvePart1(input));
        Console.WriteLine(SolvePart2(input));
    }

    public long SolvePart1(string input)
    {
        return Parse(input)
            .Select(chunk => ParseChunk(chunk))
            .Where(res => res.Result == Result.INVALID)
            .Select(res => _syntaxPoints.GetValueOrDefault(res.Characters[0]))
            .Sum();
    }

    public long SolvePart2(string input)
    {
        var results = Parse(input)
            .Select(chunk => ParseChunk(chunk))
            .Where(res => res.Result == Result.INCOMPLETE)
            .Select(res => res.Characters
                .Select(character => _autocompletePoints.GetValueOrDefault(character))
                .Aggregate(0L, (total, point) => 5 * total + point))
            .OrderBy(score => score)
            .ToArray();
        return results[results.Count() / 2];
    }

    public ParseResult ParseChunk(string line, int index = 0)
    {
        var openings = new Stack<char>();
        for (int i = 0; i < line.Count(); i++)
        {
            if (IsOpening(line[i]))
            {
                openings.Push(line[i]);
                continue;
            }
            if (_expectedClosings.GetValueOrDefault(openings.Peek()) != line[i])
                return new ParseResult(Result.INVALID, line[i].ToString());
            openings.Pop();
        }
        if (openings.Count() == 0)
            return new ParseResult(Result.SUCCESS, "");
        return new ParseResult(Result.INCOMPLETE, openings
            .Select(c => _expectedClosings.GetValueOrDefault(c))
            .Aggregate(new StringBuilder(), (acc, c) => acc.Append(c))
            .ToString());
    }

    private bool IsOpening(char c)
        => new char[] { '(', '[', '{', '<'}.Contains(c);
    
    private IEnumerable<string> Parse(string input) =>
        input.Split('\n');
}

public enum Result
{
    SUCCESS,
    INVALID,
    INCOMPLETE
}

public struct ParseResult
{
    public Result Result { get; }
    public string Characters { get; }

    public ParseResult(Result result, string characters)
    {
        Result = result;
        Characters = characters;
    }
}
