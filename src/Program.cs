using System;
using System.IO;
using aoc_2021.solutions;

namespace aoc_2021;
class Program
{
    static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.Error.WriteLine("No day provided");
            Environment.Exit(1);
        }
        var day = args[0];

        string input = null;
        try
        {
            input = ReadInput(day);
        }
        catch (FileNotFoundException e)
        {
            Console.Error.WriteLine(e.Message);
            Environment.Exit(1);
        }

        switch (day)
        {
            case "1":
                new Day1().Solve(input);
                break;
            case "2":
                new Day2().Solve(input);
                break;
            case "3":
                new Day3().Solve(input);
                break;
            case "4":
                new Day4().Solve(input);
                break;
            case "5":
                new Day5().Solve(input);
                break;
            case "6":
                new Day6().Solve(input);
                break;
            case "7":
                new Day7().Solve(input);
                break;
            case "8":
                new Day8().Solve(input);
                break;
            case "9":
                new Day9().Solve(input);
                break;
            case "10":
                new Day10().Solve(input);
                break;
            case "11":
                new Day11().Solve(input);
                break;
            case "12":
                new Day12().Solve(input);
                break;
            case "13":
                new Day13().Solve(input);
                break;
            case "14":
                new Day14().Solve(input);
                break;
            case "15":
                new Day15().Solve(input);
                break;
            case "16":
                new Day16().Solve(input);
                break;
            case "17":
                new Day17().Solve(input);
                break;
            case "18":
                new Day18().Solve(input);
                break;
            default:
                throw new NotImplementedException();
        }
    }

    private static String ReadInput(string dayNr)
    {
        var path = Path.Combine(Directory.GetCurrentDirectory(), "solutions", $"day{dayNr}.input");
        return File.ReadAllText(path);
    }
}
