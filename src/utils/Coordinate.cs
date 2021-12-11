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
}
