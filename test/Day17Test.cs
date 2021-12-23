using aoc_2021.solutions;
using Xunit;

namespace aoc_2021_test;
public class Day17Test
{
    private string input = "target area: x=20..30, y=-10..-5";

    [Fact]
    public void Part1()
    {
        Assert.Equal(45, new Day17().SolvePart1(input));
    }

    [Fact]
    public void Part2()
    {
        Assert.Equal(112, new Day17().SolvePart2(input));
    }
}
