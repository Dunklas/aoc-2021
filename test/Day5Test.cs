using System.Collections.Generic;
using aoc_2021.solutions;
using aoc_2021.utils;
using Xunit;

namespace aoc_2021_test;
public class Day5Test
{
    private string input = @"0,9 -> 5,9
8,0 -> 0,8
9,4 -> 3,4
2,2 -> 2,1
7,0 -> 7,4
6,4 -> 2,0
0,9 -> 2,9
3,4 -> 1,4
0,0 -> 8,8
5,5 -> 8,2";

    [Fact]
    public void Part1()
    {
        Assert.Equal(5, new Day5().SolvePart1(input));
    }

    [Fact]
    public void Part2()
    {
        Assert.Equal(12, new Day5().SolvePart2(input));
    }

    [Fact]
    public void TestCoordinatesBetween()
    {
        Assert.Equal(
            new HashSet<Coordinate> { new Coordinate(1, 1), new Coordinate(1, 2), new Coordinate(1, 3) },
            new CoordinatePair(new Coordinate(1, 1), new Coordinate(1, 3)).Line()
        );
        Assert.Equal(
            new HashSet<Coordinate> { new Coordinate(7, 7), new Coordinate(8, 7), new Coordinate(9, 7) },
            new CoordinatePair(new Coordinate(9,7), new Coordinate(7,7)).Line()
        );
        Assert.Equal(
            new HashSet<Coordinate> { new Coordinate(0, 9), new Coordinate(1, 9), new Coordinate(2, 9), new Coordinate(3, 9), new Coordinate(4, 9), new Coordinate(5, 9) },
            new CoordinatePair(new Coordinate(0, 9), new Coordinate(5, 9)).Line()
        );
        Assert.Equal(
            new HashSet<Coordinate> { new Coordinate(1, 1), new Coordinate(2, 2), new Coordinate(3, 3) },
            new CoordinatePair(new Coordinate(1, 1), new Coordinate(3, 3)).Line()
        );
        Assert.Equal(
            new HashSet<Coordinate> { new Coordinate(7, 9), new Coordinate(8, 8), new Coordinate(9, 7) },
            new CoordinatePair(new Coordinate(9, 7), new Coordinate(7, 9)).Line()
        );
    }
}
