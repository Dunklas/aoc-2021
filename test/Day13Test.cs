using aoc_2021.solutions;
using Xunit;

namespace aoc_2021_test;
public class Day13Test
{
    private string input = @"6,10
0,14
9,10
0,3
10,4
4,11
6,0
6,12
4,1
0,13
10,12
3,4
3,0
8,4
1,10
2,14
8,10
9,0

fold along y=7
fold along x=5";

    [Fact]
    public void Part1()
    {
        Assert.Equal(17, new Day13().SolvePart1(input));
    }
}
