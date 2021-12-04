using aoc_2021.solutions;
using Xunit;

namespace aoc_2021_test;
public class Day4Test
{
    private string input = @"";

    [Fact]
    public void Part1()
    {
        Assert.Equal(42, new Day4().SolvePart1(input));
    }

    [Fact]
    public void Part2()
    {
        Assert.Equal(42, new Day4().SolvePart2(input));
    }
}
