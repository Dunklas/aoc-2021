using System.Diagnostics.CodeAnalysis;

namespace aoc_2021.utils;

public struct Coordinate
{
    public int X { get; }
    public int Y { get; }

    public Coordinate(int x, int y)
    {
        X = x;
        Y = y;
    }

    public static Coordinate FromString(string text)
    {
        var parts = text.Split(",");
        return new Coordinate(int.Parse(parts[0].Trim()), int.Parse(parts[1].Trim()));
    }

    public override bool Equals([NotNullWhen(true)] object other)
    {
        if (other == null || this.GetType() != other.GetType())
            return false;
        var otherC = (Coordinate)other;
        return X.Equals(otherC.X) && Y.Equals(otherC.Y);
    }

    public override int GetHashCode()
    {
        return X.GetHashCode() * 17 + Y.GetHashCode();
    }

    public override string ToString()
    {
        return $"{X},{Y}";
    }
}
