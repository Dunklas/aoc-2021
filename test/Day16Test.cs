using System.Collections.Generic;
using aoc_2021.solutions;
using Xunit;

namespace aoc_2021_test;
public class Day16Test
{
    [Fact]
    public void Part1()
    {
        Assert.Equal(16, new Day16().SolvePart1("8A004A801A8002F478"));
        Assert.Equal(12, new Day16().SolvePart1("620080001611562C8802118E34"));
        Assert.Equal(23, new Day16().SolvePart1("C0015000016115A2E0802F182340"));
        Assert.Equal(31, new Day16().SolvePart1("A0016C880162017C3686B18A3D4780"));
    }

    [Fact]
    public void Part2()
    {
        Assert.Equal(3, new Day16().SolvePart2("C200B40A82"));
        Assert.Equal(54, new Day16().SolvePart2("04005AC33890"));
        Assert.Equal(7, new Day16().SolvePart2("880086C3E88112"));
        Assert.Equal(9, new Day16().SolvePart2("CE00C43D881120"));
        Assert.Equal(1, new Day16().SolvePart2("D8005AC2A8F0"));
        Assert.Equal(0, new Day16().SolvePart2("F600BC2D8F"));
        Assert.Equal(0, new Day16().SolvePart2("9C005AC2F8F0"));
        Assert.Equal(1, new Day16().SolvePart2("9C0141080250320F1802104A08"));
    }
}
