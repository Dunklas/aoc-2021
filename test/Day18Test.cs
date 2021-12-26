using aoc_2021.solutions;
using Xunit;

namespace aoc_2021_test;
public class Day18Test
{
    private string input = @"[[[0,[5,8]],[[1,7],[9,6]]],[[4,[1,2]],[[1,4],2]]]
[[[5,[2,8]],4],[5,[[9,9],0]]]
[6,[[[6,2],[5,6]],[[7,6],[4,7]]]]
[[[6,[0,7]],[0,9]],[4,[9,[9,0]]]]
[[[7,[6,4]],[3,[1,3]]],[[[5,5],1],9]]
[[6,[[7,3],[3,2]]],[[[3,8],[5,7]],4]]
[[[[5,4],[7,7]],8],[[8,3],8]]
[[9,3],[[9,9],[6,[4,9]]]]
[[2,[[7,7],7]],[[5,8],[[9,3],[0,2]]]]
[[[[5,2],5],[8,[3,7]]],[[5,[7,5]],[4,4]]]";

    [Fact]
    public void Part1()
    {
        Assert.Equal(4140, new Day18().SolvePart1(input));
    }

    [Fact]
    public void Part2()
    {
        Assert.Equal(3993, new Day18().SolvePart2(input));
    }

    [Fact]
    public void Reduce()
    {
        var n = SnailfishNumber.FromString("[[[[[4,3],4],4],[7,[[8,4],9]]],[1,1]]");
        n.Reduce();
        Assert.Equal("[[[[0,7],4],[[7,8],[6,0]]],[8,1]]", n.ToString());
    }

    [Fact]
    public void Add()
    {
        Assert.Equal(
            "[[[[0,7],4],[[7,8],[6,0]]],[8,1]]",
            SnailfishNumber.Add(SnailfishNumber.FromString("[[[[4,3],4],4],[7,[[8,4],9]]]"), SnailfishNumber.FromString("[1,1]")).ToString()
        );
        Assert.Equal(
            "[[[[3,0],[5,3]],[4,4]],[5,5]]",
            SnailfishNumber.Add(SnailfishNumber.FromString("[[[[1,1],[2,2]],[3,3]],[4,4]]"), SnailfishNumber.FromString("[5,5]")).ToString()
        );
        Assert.Equal(
            "[[[[5,0],[7,4]],[5,5]],[6,6]]",
            SnailfishNumber.Add(SnailfishNumber.FromString("[[[[3,0],[5,3]],[4,4]],[5,5]]"), SnailfishNumber.FromString("[6,6]")).ToString()
        );
        Assert.Equal(
            "[[[[4,0],[5,4]],[[7,7],[6,0]]],[[8,[7,7]],[[7,9],[5,0]]]]",
            SnailfishNumber.Add(SnailfishNumber.FromString("[[[0,[4,5]],[0,0]],[[[4,5],[2,6]],[9,5]]]"), SnailfishNumber.FromString("[7,[[[3,7],[4,3]],[[6,3],[8,8]]]]")).ToString()
        );
    }

    [Fact]
    public void Magnitude()
    {
        Assert.Equal(29, SnailfishNumber.FromString("[9,1]").Magnitude());
        Assert.Equal(21, SnailfishNumber.FromString("[1,9]").Magnitude());
        Assert.Equal(129, SnailfishNumber.FromString("[[9,1],[1,9]]").Magnitude());
        Assert.Equal(143, SnailfishNumber.FromString("[[1,2],[[3,4],5]]").Magnitude());
        Assert.Equal(1384, SnailfishNumber.FromString("[[[[0,7],4],[[7,8],[6,0]]],[8,1]]").Magnitude());
        Assert.Equal(445, SnailfishNumber.FromString("[[[[1,1],[2,2]],[3,3]],[4,4]]").Magnitude());
        Assert.Equal(791, SnailfishNumber.FromString("[[[[3,0],[5,3]],[4,4]],[5,5]]").Magnitude());
        Assert.Equal(1137, SnailfishNumber.FromString("[[[[5,0],[7,4]],[5,5]],[6,6]]").Magnitude());
        Assert.Equal(3488, SnailfishNumber.FromString("[[[[8,7],[7,7]],[[8,6],[7,7]]],[[[0,7],[6,6]],[8,7]]]").Magnitude());
    }
}
