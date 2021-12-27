using aoc_2021.solutions;
using Xunit;

namespace aoc_2021_test;
public class Day14Test
{
    private string input = @"NNCB

CH -> B
HH -> N
CB -> H
NH -> C
HB -> C
HC -> B
HN -> C
NN -> C
BH -> H
NC -> B
NB -> B
BN -> B
BB -> N
BC -> B
CC -> N
CN -> C";

    [Fact]
    public void Part1()
    {
        Assert.Equal(1588, new Day14().Solve(input, 10));
    }

    [Fact]
    public void Part2()
    {
        Assert.Equal(2188189693529, new Day14().Solve(input, 40));
    }
}
