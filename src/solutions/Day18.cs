using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace aoc_2021.solutions;

public class Day18
{
    public void Solve(string input)
    {
        Console.WriteLine(SolvePart1(input));
        Console.WriteLine(SolvePart2(input));
    }

    public long SolvePart1(string input)
    {
        var numbers = input.Split("\n")
            .Select(line => SnailfishNumber.FromString(line));
        SnailfishNumber sum = null;
        foreach (var n in numbers)
            sum = sum == null ? n : SnailfishNumber.Add(sum, n);
        return sum.Magnitude();
    }

    public long SolvePart2(string input)
    {
        var numbers = input.Split("\n");
        var largest = int.MinValue;
        for (int i = 0; i < numbers.Count() - 1; i++)
        {
            for (int j = i; j < numbers.Count(); j++)
            {
                if (i == j)
                    continue;
                var first = SnailfishNumber.Add(
                    SnailfishNumber.FromString(numbers[i]),
                    SnailfishNumber.FromString(numbers[j])
                ).Magnitude();
                if (first > largest)
                    largest = first;
                var second = SnailfishNumber.Add(
                    SnailfishNumber.FromString(numbers[j]),
                    SnailfishNumber.FromString(numbers[i])
                ).Magnitude();
                if (second > largest)
                    largest = second;
            }
        }
        return largest;
    }
}

public class SnailfishNumber
{
    public SnailfishNumber Parent { get; private set; }
    public int? Value { get; private set; }
    public SnailfishNumber Left { get; private set; }
    public SnailfishNumber Right { get; private set; }

    public SnailfishNumber(SnailfishNumber left, SnailfishNumber right, SnailfishNumber parent)
    {
        Left = left;
        Right = right;
        Parent = parent;
        Left.Parent = this;
        Right.Parent = this;
    }

    public SnailfishNumber(double value, SnailfishNumber parent)
    {
        Value = (int)value;
        Parent = parent;
    }

    public static SnailfishNumber Add(SnailfishNumber first, SnailfishNumber second)
    {
        var sum = new SnailfishNumber(first, second, null);
        first.Parent = sum;
        second.Parent = sum;
        sum.Reduce();
        return sum;
    }

    private int Depth() => this.Parent == null ? 1 : this.Parent.Depth() + 1;

    private bool Leaf() => this.Value != null;

    public void Reduce()
    {
        while (true)
        {
            var toExplode = FindExploding();
            if (toExplode != null)
            {
                toExplode.Explode();
                continue;
            }
            var toSplit = FindSplitting();
            if (toSplit != null)
            {
                toSplit.Split();
                continue;
            }
            break;
        }
    }

    public int Magnitude()
    {
        int left = Left.Leaf() ? Left.Value.Value : Left.Magnitude();
        int right = Right.Leaf() ? Right.Value.Value : Right.Magnitude();
        return (3 * left) + (2 * right);
    }

    private void Explode()
    {
        SnailfishNumber closestToLeft = FindClosestLeft(PathToRoot().Skip(1));
        SnailfishNumber closestToRight = FindClosestRight(PathToRoot().Skip(1));
        if (closestToLeft != null)
            closestToLeft.Value += Left.Value;
        if (closestToRight != null)
            closestToRight.Value += Right.Value;
        if (this.Equals(Parent.Left))
            Parent.Left.Value = 0;
        else
            Parent.Right.Value = 0;
    }

    private void Split()
    {
        var newNumber = new SnailfishNumber
        (
            new SnailfishNumber(Math.Floor((int)this.Value / 2.0), null),
            new SnailfishNumber(Math.Ceiling((int)this.Value / 2.0), null),
            this.Parent
        );
        if (this.Parent.Left.Equals(this))
            this.Parent.Left = newNumber;
        else
            this.Parent.Right = newNumber;
    }

    private SnailfishNumber FindClosestRight(IEnumerable<SnailfishNumber> pathToRoot)
    {
        var visited = new HashSet<SnailfishNumber>() { this };
        foreach (var node in pathToRoot)
        {
            if (node.Right.Leaf())
                return node.Right;
            else if (!visited.Contains(node.Right))
                return FindRegularPrioLeft(node.Right);
            visited.Add(node);
        }
        return null;
    }

    private SnailfishNumber FindRegularPrioLeft(SnailfishNumber current)
        => current.Left.Leaf() ? current.Left : FindRegularPrioLeft(current.Left);

    private SnailfishNumber FindClosestLeft(IEnumerable<SnailfishNumber> pathToRoot)
    {
        var visited = new HashSet<SnailfishNumber>() { this };
        foreach (var node in pathToRoot)
        {
            if (node.Left.Leaf())
                return node.Left;
            else if (!visited.Contains(node.Left))
                return FindRegularPrioRight(node.Left);
            visited.Add(node);
        }
        return null;
    }

    private SnailfishNumber FindRegularPrioRight(SnailfishNumber current)
        => current.Right.Leaf() ? current.Right : FindRegularPrioRight(current.Right);

    private IEnumerable<SnailfishNumber> PathToRoot()
    {
        var pathToRoot = new List<SnailfishNumber>();
        var current = this;
        while (current != null)
        {
            pathToRoot.Add(current);
            current = current.Parent;
        }
        return pathToRoot;
    }
    private SnailfishNumber FindExploding()
    {
        if (this.Leaf())
            return null;
        if (this.Depth() >= 5 && !this.Leaf())
            return this;
        return this.Left.FindExploding() ?? this.Right.FindExploding();
    }

    private SnailfishNumber FindSplitting()
    {
        if (this.Leaf() && this.Value >= 10)
            return this;
        if (this.Leaf())
            return null;
        return this.Left.FindSplitting() ?? this.Right.FindSplitting();
    }

    public override string ToString()
    {
        if (this.Leaf())
            return $"{this.Value}";
        return $"[{Left},{Right}]";
    }

    public static SnailfishNumber FromString(string raw)
    {
        if (Regex.IsMatch(raw, @"^\d$"))
            return new SnailfishNumber(Char.GetNumericValue(raw[0]), null);
        if (Regex.IsMatch(raw, @"^\[\d,\d\]$"))
            return new SnailfishNumber(SnailfishNumber.FromString(raw.Substring(1, 1)), SnailfishNumber.FromString(raw.Substring(3, 1)), null);
        if (Regex.IsMatch(raw, @"^\[\d,.*\]$"))
            return new SnailfishNumber(SnailfishNumber.FromString(raw.Substring(1, 1)), SnailfishNumber.FromString(raw.Substring(3, raw.Count() - 4)), null);
        if (Regex.IsMatch(raw, @"^.*,\d\]$"))
            return new SnailfishNumber(SnailfishNumber.FromString(raw.Substring(1, raw.Count() - 4)), SnailfishNumber.FromString(raw.Substring(raw.Count() - 2, 1)), null);

        var splitIndex = FindSplitPoint(raw);
        var left = SnailfishNumber.FromString(raw.Substring(1, splitIndex));
        var right = SnailfishNumber.FromString(raw.Substring(splitIndex + 2, raw.Count() - splitIndex - 3));
        return new SnailfishNumber(left, right, null);
    }

    private static int FindSplitPoint(string raw)
    {
        int depth = 0;
        foreach((char c, int index) in raw.ToCharArray().Select((c, i) => (c, i)))
        {
            if (c == '[')
                depth++;
            else if (c == ']')
                depth--;
            if (depth == 1 && c == ']')
                return index;
        }
        return -1;
    }
}
