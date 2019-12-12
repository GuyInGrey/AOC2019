using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC12
{
    public class Program
    {
        public static void Main()
        {
            //Get Input
            var input = File.ReadAllText("input.txt").Split('\n').ToList();
            var planets = new List<((int, int, int), (int, int, int))>(); // Position and Velocity

            input.ForEach(a =>
            {
                var parts = a.Split(' ').ToList().ConvertAll(b => b.Trim());
                var x = int.Parse(parts[0].Substring(3, parts[0].Length - 4));
                var y = int.Parse(parts[1].Substring(2, parts[1].Length - 3));
                var z = int.Parse(parts[2].Substring(2, parts[2].Length - 3));
                planets.Add(((x, y, z), (0, 0, 0)));
            });

            //Steps
            var stepCnt = 1000;
            for (var s = 0; s < stepCnt; s++)
            {
                for (var i = 0; i < planets.Count; i++)
                {
                    for (var i2 = 0; i2 < planets.Count; i2++)
                    {
                        var p1 = planets[i];
                        var p2 = planets[i2];
                        if (p1.Item1.Item1 < p2.Item1.Item1) { p1.Item2.Item1++; }
                        if (p1.Item1.Item1 > p2.Item1.Item1) { p1.Item2.Item1--; }
                        if (p1.Item1.Item2 < p2.Item1.Item2) { p1.Item2.Item2++; }
                        if (p1.Item1.Item2 > p2.Item1.Item2) { p1.Item2.Item2--; }
                        if (p1.Item1.Item3 < p2.Item1.Item3) { p1.Item2.Item3++; }
                        if (p1.Item1.Item3 > p2.Item1.Item3) { p1.Item2.Item3--; }
                        planets[i] = p1;
                        planets[i2] = p2;
                    }
                }

                for (var i = 0; i < planets.Count; i++)
                {
                    planets[i] = (Add(planets[i]), planets[i].Item2);
                }
            }

            var energy = 0;
            for (var i = 0; i < planets.Count; i++)
            {
                var p1 = planets[i];
                energy += Sum(Abs(p1.Item1)) * Sum(Abs(p1.Item2));
            }

            Console.WriteLine("Energy: " + energy);
            Console.Read();
        }

        public static int Sum((int, int, int) a)
        {
            return a.Item1 + a.Item2 + a.Item3;
        }

        public static (int, int, int) Abs((int, int, int) a)
        {
            return (Math.Abs(a.Item1), Math.Abs(a.Item2), Math.Abs(a.Item3));
        }

        public static (int, int, int) Add(((int, int, int), (int, int, int)) a)
        {
            return Add(a.Item1, a.Item2);
        }

        public static (int, int, int) Add((int, int, int) a, (int, int, int) b)
        {
            return (a.Item1 + b.Item1, a.Item2 + b.Item2, a.Item3 + b.Item3);
        }
    }
}