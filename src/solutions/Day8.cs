using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace aoc_2021.solutions;

public class Day8
{
    public void Solve(string input)
    {
        Console.WriteLine(SolvePart1(input));
        Console.WriteLine(SolvePart2(input));
    }

    public long SolvePart1(string input)
    {
        return input.Split("\n")
            .Select(line => new SignalPatternDeducer(line).OutputNumber())
            .Select(outputNumber => outputNumber.ToString().ToCharArray()
                .Where(digit => new int[] { '1', '4', '7', '8' }.Contains(digit))
                .Count())
            .Sum();
    }

    public long SolvePart2(string input)
    {
        return input.Split("\n")
            .Select(entry => new SignalPatternDeducer(entry).OutputNumber())
            .Sum();
    }
}

public class SignalPatternDeducer
{
    private static Dictionary<int, IEnumerable<int>> lengthToDigit = new Dictionary<int, IEnumerable<int>>
    {
        { 2, new List<int> { 1 } },
        { 3, new List<int> { 7 } },
        { 4, new List<int> { 4 } },
        { 7, new List<int> { 8 } },
    };

    private List<string> _outputValues;
    private Dictionary<int, HashSet<char>> _patternOf;

    public SignalPatternDeducer(String raw)
    {
        var parts = raw.Split("|");
        _outputValues = Normalize(parts[1].Trim().Split(" "));
        Deduce(Normalize(parts[0].Trim().Split(" ")));
    }

    public int OutputNumber()
    {
        var number = _outputValues
            .Select(digitPattern => DigitOf(digitPattern))
            .Aggregate(new StringBuilder(), (number, digit) => number.Append(digit));
        return int.Parse(number.ToString());
    }

    public int DigitOf(string pattern) =>
        _patternOf.First(x => x.Value.SetEquals(pattern.ToCharArray().ToHashSet())).Key;
    
    private void Deduce(List<string> signalPatterns)
    {
        _patternOf = signalPatterns
            .Where(pattern => lengthToDigit.ContainsKey(pattern.Count()))
            .Where(pattern => lengthToDigit[pattern.Count()].Count() == 1)
            .Select(pattern => pattern.ToCharArray().ToHashSet())
            .ToDictionary(pattern => lengthToDigit[pattern.Count()].Single());
        foreach (var pattern in signalPatterns)
        {
            var toBeDeduced = pattern.ToCharArray().ToHashSet();
            if (_patternOf.Values.Any(pattern => pattern.SetEquals(toBeDeduced)))
                continue;
                
            if (toBeDeduced.Count() == 6)
            {
                if (toBeDeduced.Except(_patternOf[7]).Count() == 4)
                    _patternOf.Add(6, toBeDeduced);
                else if (toBeDeduced.Union(_patternOf[4]).Count() == 7)
                    _patternOf.Add(0, toBeDeduced);
                else
                    _patternOf.Add(9, toBeDeduced);
            }
            if (toBeDeduced.Count() == 5)
            {
                if (toBeDeduced.Except(_patternOf[4]).Count() == 3)
                    _patternOf.Add(2, toBeDeduced);
                else if (toBeDeduced.Except(_patternOf[1]).Count() == 3)
                    _patternOf.Add(3, toBeDeduced);
                else
                    _patternOf.Add(5, toBeDeduced);
            }
        }
    }

    private List<string> Normalize(string[] patterns) =>
        patterns
            .Select(pattern => pattern.ToCharArray()
                .OrderBy(signal => signal))
            .Select(pattern => string.Join("", pattern))
            .ToList();
}
