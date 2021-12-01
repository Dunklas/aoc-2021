using System;
using System.Collections.Generic;
using System.Linq;

namespace aoc_2021.solutions
{
    public class Day1
    {
        public void Solve(string input)
        {
            Console.WriteLine(SolvePart1(input));
            Console.WriteLine(SolvePart2(input));
        }

        public int SolvePart1(string input)
        {
            var depthMeasurements = ParseInput(input);
            var totalIncreases = Enumerable.Range(0, depthMeasurements.Count - 1)
                .Where(x => depthMeasurements[x + 1] > depthMeasurements[x])
                .Count();
            return totalIncreases;
        }

        public int SolvePart2(string input)
        {
            var depthMeasurements = ParseInput(input);
            var measurementWindows = MeasurementWindows(depthMeasurements, 3);
            var totalIncreases = Enumerable.Range(0, measurementWindows.Count - 1)
                .Where(x => measurementWindows[x + 1] > measurementWindows[x])
                .Count();
            return totalIncreases;
        }

        private List<int> MeasurementWindows(List<int> measurements, int windowSize)
        {
            return Enumerable.Range(0, measurements.Count - windowSize + 1)
                .Select(x => measurements.GetRange(x, windowSize).Sum())
                .ToList();
        }

        private List<int> ParseInput(string input)
        {
            return input.Split("\n")
                .Select(line => int.Parse(line))
                .ToList();
        }
    }
}