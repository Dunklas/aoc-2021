using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace aoc_2021.solutions;

public class Day3
{
    public void Solve(string input)
    {
        Console.WriteLine(SolvePart1(input));
        Console.WriteLine(SolvePart2(input));
    }

    public long SolvePart1(string input)
    {
        var lines = ParseInput(input);
        var maxBitLength = MaxBitLength(lines);
        var gammaRate = Enumerable.Range(0, MaxBitLength(lines) + 1).Reverse()
            .Aggregate(0L, (acc, pos) => acc + MostCommon(lines, pos) * (int) Math.Pow(2, pos));
        var epsilonRate = ~gammaRate & (1<<maxBitLength+1)-1;
        return gammaRate * epsilonRate;
    }

    
    public long SolvePart2(string input)
    {
        var lines = ParseInput(input);
        var oxygenGeneratorRatings = lines.ToHashSet();
        var co2ScrubberRatings = lines.ToHashSet();
        Enumerable.Range(0, MaxBitLength(lines) + 1).Reverse()
            .ToList().ForEach(i =>
            {
                var mostCommon = MostCommon(oxygenGeneratorRatings.ToArray(), i);
                var leastCommon = LeastCommon(co2ScrubberRatings.ToArray(), i);
                var oxygenToKeep = oxygenGeneratorRatings
                    .Where(binary => BitInPos(binary, i) == mostCommon);
                var co2ToKeep = co2ScrubberRatings
                    .Where(binary => BitInPos(binary, i) == leastCommon);
                oxygenGeneratorRatings.RemoveWhere(n => oxygenGeneratorRatings.Count() > 1 && !oxygenToKeep.Contains(n));
                co2ScrubberRatings.RemoveWhere(n => co2ScrubberRatings.Count() > 1 && !co2ToKeep.Contains(n));
            });
        return oxygenGeneratorRatings.Single() * co2ScrubberRatings.Single();
    }

    private int MaxBitLength(uint[] binaryNumbers) => (int) binaryNumbers
        .Select(x => Math.Log2(x))
        .Max();

    private long MostCommon(uint[] numbers, int pos)
         => NumOneBits(numbers, pos) >= (numbers.Count() / 2.0) ? 1 : 0;
    
    private long LeastCommon(uint[] numbers, int pos)
        => NumOneBits(numbers, pos) < (numbers.Count() / 2.0) ? 1 : 0;

    private uint NumOneBits(uint[] numbers, int pos) => Enumerable.Range(0, numbers.Count())
        .Aggregate(0u, (acc, i) => acc + BitInPos(numbers[i], pos));
    
    private uint BitInPos(uint number, int pos)
        => (number & (uint) Math.Pow(2, pos)) >> pos;

    private uint[] ParseInput(string input)
        => input.Split("\n")
            .Select(line => Convert.ToUInt32(line, 2))
            .ToArray();
}
