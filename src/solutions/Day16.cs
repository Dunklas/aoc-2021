using System;
using System.Collections.Generic;
using System.Linq;

namespace aoc_2021.solutions;

public class Day16
{
    public void Solve(string input)
    {
        Console.WriteLine(SolvePart1(input));
        Console.WriteLine(SolvePart2(input));
    }

    public long SolvePart1(string input)
    {
        var outerPacket = ParseInput(input);
        var packetVersions = new List<long>();
        var i = 0;
        PacketValue(outerPacket, packetVersions, ref i);
        return packetVersions.Sum();
    }
    
    public long SolvePart2(string input)
    {
        var outerPacket = ParseInput(input);
        var i = 0;
        return PacketValue(outerPacket, new List<long>(), ref i);
    }
    private List<char> ParseInput(string input) =>
        input.ToCharArray()
            .Select(hex => Convert.ToUInt16(hex.ToString(), 16))
            .Select(i => Convert.ToString(i, 2).PadLeft(4, '0'))
            .SelectMany(b => b)
            .ToList();

    public long PacketValue(List<char> packet, List<long> packetVersions, ref int i)
    {
        var (PacketVersion, Type) = ParseHeader(packet, ref i); 
        packetVersions.Add(PacketVersion);
        
        var values = new List<long>();
        if (Type != Type.Literal)
        {
            var lengthTypeId = packet.GetRange(i++, 1).Single();
            if (lengthTypeId == '0')
            {
                var subpacketsLength = ToInt64(packet.GetRange(i, 15));
                var cap = (i += 15) + subpacketsLength;
                while (i < cap)
                    values.Add(PacketValue(packet, packetVersions, ref i));
            }
            else
            {
                var numSubPackets = ToInt64(packet.GetRange(i, 11));
                i += 11;
                for (int j = 0; j < numSubPackets; j++)
                    values.Add(PacketValue(packet, packetVersions, ref i));
            }
        }

        return Type switch
        {
            Type.Sum => values.Sum(),
            Type.Product => values.Aggregate(1L, (acc, i) => acc * i),
            Type.Minimum => values.Min(),
            Type.Maximum => values.Max(),
            Type.Literal => LiteralValue(packet, ref i),
            Type.GreaterThan => values[0] > values[1] ? 1 : 0,
            Type.LessThan => values[0] < values[1] ? 1 : 0,
            Type.Equal => values[0] == values[1] ? 1 : 0,
            _ => throw new ArgumentOutOfRangeException("Unexpected packet type")
        };
    }

    private long LiteralValue(List<char> packet, ref int i)
    {
        var number = new List<char>();
        while (true)
        {
            var group = packet.GetRange(i, 5);
            number.AddRange(group.Skip(1));
            i += 5;
            if (group.First() == '0')
                break;
        }
        return ToInt64(number);
    }

    private (short PackageVersion, Type Type) ParseHeader(List<char> packet, ref int i)
    {
        var header = packet.GetRange(i, 6);
        var packageVersion = ToInt16(header.GetRange(0, 3));
        var type = ToInt16(header.GetRange(3, 3)) switch
        {
            0 => Type.Sum,
            1 => Type.Product,
            2 => Type.Minimum,
            3 => Type.Maximum,
            4 => Type.Literal,
            5 => Type.GreaterThan,
            6 => Type.LessThan,
            7 => Type.Equal,
            _ => throw new ArgumentOutOfRangeException("Unexpected packet type")
        };
        i += 6;
        return (packageVersion, type);
    }

    private long ToInt64(IEnumerable<char> binary) =>
        Convert.ToInt64(string.Join("", binary), 2);
    private short ToInt16(IEnumerable<char> binary) =>
        Convert.ToInt16(string.Join("", binary), 2);
    
    private enum Type
    {
        Literal,
        Sum,
        Product,
        Minimum,
        Maximum,
        GreaterThan,
        LessThan,
        Equal
    }
}
