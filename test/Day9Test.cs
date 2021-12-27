using aoc_2021.solutions;
using Xunit;

namespace aoc_2021_test;
public class Day9Test
{
    private string input = @"2199943210
3987894921
9856789892
8767896789
9899965678";

    [Fact]
    public void Part1()
    {
        Assert.Equal(15, new Day9().SolvePart1(input));
    }

    [Fact]
    public void Part2()
    {
        Assert.Equal(1134, new Day9().SolvePart2(input));
    }
}
