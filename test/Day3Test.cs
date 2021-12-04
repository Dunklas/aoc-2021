using aoc_2021.solutions;
using Xunit;

namespace aoc_2021_test;

public class Day3Test
{
    private string input = @"00100
11110
10110
10111
10101
01111
00111
11100
10000
11001
00010
01010";

    [Fact]
    public void Part1()
    {
        Assert.Equal(198u, new Day3().SolvePart1(input));
    }

    [Fact]
    public void Part2()
    {
        Assert.Equal(230, new Day3().SolvePart2(input));
    }
}
