using aoc_2021.solutions;
using Xunit;

namespace aoc_2021_test;
public class Day11Test
{
    private string input = @"5483143223
2745854711
5264556173
6141336146
6357385478
4167524645
2176841721
6882881134
4846848554
5283751526";

    [Fact]
    public void Part1()
    {
        Assert.Equal(0, new Day11().SolvePart1(input, 1));
        Assert.Equal(35, new Day11().SolvePart1(input, 2));
        Assert.Equal(80, new Day11().SolvePart1(input, 3));
        Assert.Equal(96, new Day11().SolvePart1(input, 4));
        Assert.Equal(204, new Day11().SolvePart1(input, 10));
        Assert.Equal(1656, new Day11().SolvePart1(input, 100));
    }

    [Fact]
    public void Part2()
    {
        Assert.Equal(195, new Day11().SolvePart2(input));
    }
}
