using aoc_2021.solutions;
using Xunit;

namespace aoc_2021_test;
public class Day6Test
{
    private string input = @"3,4,3,1,2";

    [Fact]
    public void Part1()
    {
        Assert.Equal(26, new Day6().Solve(input, 18));
        Assert.Equal(5934, new Day6().Solve(input, 80));
    }

    [Fact]
    public void Part2()
    {
        Assert.Equal(26984457539, new Day6().Solve(input, 256));
    }
}
