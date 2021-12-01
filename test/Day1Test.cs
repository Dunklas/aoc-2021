using Xunit;
using aoc_2021.solutions;

namespace aoc_2021_test;

public class Day1Test
{
    private string input = @"199
200
208
210
200
207
240
269
260
263";

    [Fact]
    public void Part1()
    {
        Assert.Equal(7, new Day1().SolvePart1(input));
    }

    [Fact]
    public void Part2()
    {
        Assert.Equal(5, new Day1().SolvePart2(input));
    }
}
