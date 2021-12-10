using aoc_2021.solutions;
using Xunit;

namespace aoc_2021_test;
public class Day10Test
{
    private string input = @"[({(<(())[]>[[{[]{<()<>>
[(()[<>])]({[<{<<[]>>(
{([(<{}[<>[]}>{[]{[(<()>
(((({<>}<{<{<>}{[]{[]{}
[[<[([]))<([[{}[[()]]]
[{[{({}]{}}([{[{{{}}([]
{<[[]]>}<{[{[{[]{()[[[]
[<(<(<(<{}))><([]([]()
<{([([[(<>()){}]>(<<{{
<{([{{}}[<[[[<>{}]]]>[]]";

    [Fact]
    public void Part1()
    {
        Assert.Equal(26397, new Day10().SolvePart1(input));
    }

    [Fact]
    public void Part2()
    {
        Assert.Equal(288957, new Day10().SolvePart2(input));
    }

    [Fact]
    public void TestValid()
    {
        Assert.Equal(Result.SUCCESS, new Day10().ParseChunk("()").Result);
        Assert.Equal(Result.SUCCESS, new Day10().ParseChunk("([])").Result);
        Assert.Equal(Result.SUCCESS, new Day10().ParseChunk("{()()()}").Result);
        Assert.Equal(Result.SUCCESS, new Day10().ParseChunk("<([{}])>").Result);
        Assert.Equal(Result.SUCCESS, new Day10().ParseChunk("[<>({}){}[([])<>]]").Result);
        Assert.Equal(Result.SUCCESS, new Day10().ParseChunk("(((((((((())))))))))").Result);
    }

    [Fact]
    public void TestInvalid()
    {
        Assert.Equal(new ParseResult(Result.INVALID, "}"), new Day10().ParseChunk("{([(<{}[<>[]}>{[]{[(<()>"));
        Assert.Equal(new ParseResult(Result.INVALID, ")"), new Day10().ParseChunk("[[<[([]))<([[{}[[()]]]"));
        Assert.Equal(new ParseResult(Result.INVALID, "]"), new Day10().ParseChunk("[{[{({}]{}}([{[{{{}}([]"));
        Assert.Equal(new ParseResult(Result.INVALID, ")"), new Day10().ParseChunk("[<(<(<(<{}))><([]([]()"));
        Assert.Equal(new ParseResult(Result.INVALID, ">"), new Day10().ParseChunk("<{([([[(<>()){}]>(<<{{"));
    }

    [Fact]
    public void TestIncomplete()
    {
        Assert.Equal(new ParseResult(Result.INCOMPLETE, "}}]])})]"), new Day10().ParseChunk("[({(<(())[]>[[{[]{<()<>>"));
        Assert.Equal(new ParseResult(Result.INCOMPLETE, ")}>]})"), new Day10().ParseChunk("[(()[<>])]({[<{<<[]>>("));
        Assert.Equal(new ParseResult(Result.INCOMPLETE, "}}>}>))))"), new Day10().ParseChunk("(((({<>}<{<{<>}{[]{[]{}"));
        Assert.Equal(new ParseResult(Result.INCOMPLETE, "]]}}]}]}>"), new Day10().ParseChunk("{<[[]]>}<{[{[{[]{()[[[]"));
        Assert.Equal(new ParseResult(Result.INCOMPLETE, "])}>"), new Day10().ParseChunk("<{([{{}}[<[[[<>{}]]]>[]]"));
    }
}
