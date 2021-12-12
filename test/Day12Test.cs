using aoc_2021.solutions;
using Xunit;

namespace aoc_2021_test;
public class Day12Test
{
    private string small = @"start-A
start-b
A-c
A-b
b-d
A-end
b-end";

    private string medium = @"dc-end
HN-start
start-kj
dc-start
dc-HN
LN-dc
HN-end
kj-sa
kj-HN
kj-dc";

    private string large = @"fs-end
he-DX
fs-he
start-DX
pj-DX
end-zg
zg-sl
zg-pj
pj-he
RW-he
fs-DX
pj-RW
zg-RW
start-pj
he-WI
zg-he
pj-fs
start-RW";

    [Fact]
    public void Part1()
    {
        Assert.Equal(10, new Day12().SolvePart1(small));
        Assert.Equal(19, new Day12().SolvePart1(medium));
        Assert.Equal(226, new Day12().SolvePart1(large));
    }

    [Fact]
    public void Part2()
    {
        Assert.Equal(36, new Day12().SolvePart2(small));
        Assert.Equal(103, new Day12().SolvePart2(medium));
        Assert.Equal(3509, new Day12().SolvePart2(large));
    }
}
