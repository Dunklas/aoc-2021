using aoc_2021.solutions;
using Xunit;

namespace aoc_2021_test;
public class Day7Test
{
    private string input = "16,1,2,0,4,2,7,1,2,14";

    [Fact]
    public void Part1()
    {
        Assert.Equal(37, new Day7().SolvePart1(input));
    }

    [Fact]
    public void Part2()
    {
        Assert.Equal(168, new Day7().SolvePart2(input));
    }
}
