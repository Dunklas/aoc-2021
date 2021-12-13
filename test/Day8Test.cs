using aoc_2021.solutions;
using Xunit;

namespace aoc_2021_test;
public class Day8Test
{
    private string large = @"be cfbegad cbdgef fgaecd cgeb fdcge agebfd fecdb fabcd edb | fdgacbe cefdb cefbgd gcbe
edbfga begcd cbg gc gcadebf fbgde acbgfd abcde gfcbed gfec | fcgedb cgb dgebacf gc
fgaebd cg bdaec gdafb agbcfd gdcbef bgcad gfac gcb cdgabef | cg cg fdcagb cbg
fbegcd cbd adcefb dageb afcb bc aefdc ecdab fgdeca fcdbega | efabcd cedba gadfec cb
aecbfdg fbg gf bafeg dbefa fcge gcbea fcaegb dgceab fcbdga | gecf egdcabf bgf bfgea
fgeab ca afcebg bdacfeg cfaedg gcfdb baec bfadeg bafgc acf | gebdcfa ecba ca fadegcb
dbcfg fgd bdegcaf fgec aegbdf ecdfab fbedc dacgb gdcebf gf | cefg dcbef fcge gbcadfe
bdfegc cbegaf gecbf dfcage bdacg ed bedf ced adcbefg gebcd | ed bcgafe cdgba cbgef
egadfb cdbfeg cegd fecab cgb gbdefca cg fgcdab egfdb bfceg | gbdfcae bgc cg cgb
gcafb gcf dcaebfg ecagb gf abcdeg gaef cafbge fdbac fegbdc | fgae cfgab fg bagce";

    [Fact]
    public void Part1()
    {
        Assert.Equal(26, new Day8().SolvePart1(large));
    }

    [Fact]
    public void Part2()
    {
        Assert.Equal(61229, new Day8().SolvePart2(large));
    }

    [Fact]
    public void FullDeduction()
    {
        var deducer = new SignalPatternDeducer("acedgfb cdfbe gcdfa fbcad dab cefabd cdfgeb eafb cagedb ab | cdfeb fcadb cdfeb cdbaf");
        Assert.Equal(0, deducer.DigitOf("abcdeg"));
        Assert.Equal(1, deducer.DigitOf("ab"));
        Assert.Equal(2, deducer.DigitOf("acdfg"));
        Assert.Equal(3, deducer.DigitOf("abcdf"));
        Assert.Equal(4, deducer.DigitOf("abef"));
        Assert.Equal(5, deducer.DigitOf("bcdef"));
        Assert.Equal(6, deducer.DigitOf("bcdefg"));
        Assert.Equal(7, deducer.DigitOf("abd"));
        Assert.Equal(8, deducer.DigitOf("abcdefg"));
        Assert.Equal(9, deducer.DigitOf("abcdef"));
    }
}
