using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace AOC03
{
    class Program
    {
        static void Main()
        {
            var input = File.ReadAllText("input.txt").Split('\n').ToList().ConvertAll(a => a.Split(','));
            var s = Stopwatch.StartNew();
            var pathCoords = new List<List<(int, int)>>();

            for (var i = 0; i < input.Count; i++)
            {
                var pathCoord = new List<(int, int)>();
                var path = input[i];
                var currentX = 0;
                var currentY = 0;
                foreach (var part in path)
                {
                    var instruction = part.Substring(0, 1);
                    var num = int.Parse(part.Substring(1, part.Length - 1));

                    if (instruction == "R")
                    {
                        for (var j = 0; j < num; j++)
                        {
                            currentX++;
                            pathCoord.Add((currentX, currentY));
                        }
                    }
                    if (instruction == "L")
                    {
                        for (var j = 0; j < num; j++)
                        {
                            currentX--;
                            pathCoord.Add((currentX, currentY));
                        }
                    }
                    if (instruction == "D")
                    {
                        for (var j = 0; j < num; j++)
                        {
                            currentY--;
                            pathCoord.Add((currentX, currentY));
                        }
                    }
                    if (instruction == "U")
                    {
                        for (var j = 0; j < num; j++)
                        {
                            currentY++;
                            pathCoord.Add((currentX, currentY));
                        }
                    }
                }
                pathCoords.Add(pathCoord);
            }

            var intersections = pathCoords[0].Intersect(pathCoords[1]);
            var intersectionsDistance = new List<int>();
            foreach (var i in intersections)
            {
                var num = pathCoords[0].FindIndex(i.Equals) + 1;
                var num2 = pathCoords[1].FindIndex(i.Equals) + 1;
                intersectionsDistance.Add(num + num2);
            }

            intersectionsDistance.Sort();
            s.Stop();

            //var intersect2 = intersections.ToList().ConvertAll(a => Math.Abs(a.Item1) + Math.Abs(a.Item2));
            //intersect2.Sort();
            Console.WriteLine(intersectionsDistance[0] + "\nTook " + s.ElapsedMilliseconds + " milliseconds.");
            Console.Read();
        }
    }
}