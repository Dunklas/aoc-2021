using aoc_2021.solutions;
using Xunit;

namespace aoc_2021_test;
public class Day15Test
{
    private string input = @"1163751742
1381373672
2136511328
3694931569
7463417111
1319128137
1359912421
3125421639
1293138521
2311944581";

    [Fact]
    public void Part1()
    {
        Assert.Equal(40, new Day15().SolvePart1(input));
    }

    [Fact]
    public void Part2()
    {
        Assert.Equal(315, new Day15().SolvePart2(input));
    }
}
