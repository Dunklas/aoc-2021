using Xunit;
using aoc_2021.solutions;

namespace aoc_2021_test;

public class Day2Test
{
    private string input = @"forward 5
down 5
forward 8
up 3
down 8
forward 2";

    [Fact]
    public void Part1()
    {
        Assert.Equal(150, new Day2().SolvePart1(input));
    }

    [Fact]
    public void Part2()
    {
        Assert.Equal(900, new Day2().SolvePart2(input));
    }
}
