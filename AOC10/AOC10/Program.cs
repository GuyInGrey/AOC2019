using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace AOC10
{
    public class Program
    {
        public static void Main()
        {
            start:;
            Console.Clear();

            //Get Asteroid Position List From Input
            var asteroids = new List<Vector2>();
            var input = File.ReadAllText("input.txt").Split('\n').ToList().ConvertAll(a => a.ToCharArray()).ToArray();
            for (var y = 0; y < input.Length; y++)
            {
                for (var x = 0; x < input[y].Length; x++)
                {
                    if (input[y][x] == '#')
                    {
                        asteroids.Add(new Vector2(x, y));
                    }
                }
            }

            Console.SetWindowSize(input[0].Length + 2, input.Length + 8);

            // Get number of visible asteroids from each asteroid, get best asteroid
            var newAsteroids = new List<(Vector2, float)>(); 
            for (var i = 0; i < asteroids.Count; i++)
            {
                var a1 = asteroids[i];
                var angles = new List<float>();
                for (var i2 = 0; i2 < asteroids.Count; i2++)
                {
                    var a2 = asteroids[i2];
                    if (!a1.Equals(a2))
                    {
                        angles.Add(a1.AngleTo(a2));
                    }
                }
                newAsteroids.Add((a1, angles.Distinct().Count()));
            }
            newAsteroids.Sort((a, b) => b.Item2.CompareTo(a.Item2));
            var bestAsteroid = newAsteroids[0].Item1;
            asteroids.Remove(bestAsteroid);

            //Get angles from best asteroid to all others
            newAsteroids.Clear();
            for (var i = 0; i < asteroids.Count; i++)
            {
                var angle = bestAsteroid.AngleTo(asteroids[i]);
                //angle = asteroids[i].Y == bestAsteroid.Y && angle > 3.14f ? -3.14f : angle;
                //angle = angle < (Math.PI / 2) ? angle + (float)Math.PI : angle;
                angle = angle < 0 ? angle + ((float)Math.PI * 2) : angle;
                newAsteroids.Add((asteroids[i], angle));
            }
            newAsteroids.Sort((a, b) => b.Item2.CompareTo(a.Item2));
            newAsteroids.Reverse();

            //Correctly sort asteroids with same angle
            var changes = -1;
            while (changes != 0)
            {
                changes = 0;
                for (var i = 0; i < newAsteroids.Count - 1; i++)
                {
                    var a = newAsteroids[i];
                    var b = newAsteroids[i + 1];
                    if (a.Item2 == b.Item2)
                    {
                        if (b.Item1.DistanceFrom(bestAsteroid) < a.Item1.DistanceFrom(bestAsteroid))
                        {
                            newAsteroids = newAsteroids.Swap(i, i + 1).ToList();
                            changes++;
                        }
                    }
                }
            }

            //Visualization Stuff
            for (var y = 0; y < input.Length; y++)
            {
                for (var x = 0; x < input[y].Length; x++)
                {
                    Console.SetCursorPosition(x, y);
                    Console.Write('.');
                }
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            foreach (var a in asteroids)
            {
                Console.SetCursorPosition((int)a.X, (int)a.Y);
                Console.Write("#");
            }

            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.SetCursorPosition((int)bestAsteroid.X, (int)bestAsteroid.Y);
            Console.Write('#');

            Console.ForegroundColor = ConsoleColor.Red;

            //Vaporize Asteroids one by one :D BY GIANT LAZERZ
            var removedOrder = new List<(Vector2, float)>();
            var lastRemoved = (Vector2.Zero, 100f);
            var destroyedCnt = 0;
            while (newAsteroids.Count > 0)
            {
                var blasted = false;
                var current = newAsteroids[0];
                if (current.Item2 != lastRemoved.Item2)
                {
                    blasted = true;
                }
                else
                {
                    //if (newAsteroids[0].Item2 == newAsteroids.Last().Item2)
                    if (newAsteroids.All(a => a.Item2 == newAsteroids[0].Item2))
                    {
                        blasted = true;
                    }
                    else
                    {
                        newAsteroids.RemoveAt(0);
                        newAsteroids.Add(current);
                    }
                }

                if (blasted)
                {
                    lastRemoved = current;
                    removedOrder.Add(current);
                    newAsteroids.RemoveAt(0);
                    destroyedCnt++;
                    //More Visualization
                    Console.SetCursorPosition((int)current.Item1.X, (int)current.Item1.Y);
                    Console.Write('O');
                }

                Thread.Sleep(20);
            }

            Console.SetCursorPosition(0, input.Length + 2);
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine("Best Asteroid For Visibility: " + bestAsteroid);

            var last = removedOrder.Last();
            var num = (removedOrder[199].Item1.X * 100) + removedOrder[199].Item1.Y;
            Console.WriteLine("Part 2 Answer: " + num);
            Console.WriteLine("Grid Size: " + input.Length + "x" + input[0].Length);
            Console.WriteLine("Asteroids destroyed: " + destroyedCnt);

            Console.Read();
            //goto start;
        }
    }

    public class Vector2
    {
        public float X = 0;
        public float Y = 0;

        public Vector2() { }

        public Vector2(float x, float y)
        {
            X = x;
            Y = y;
        }

        public float AngleTo(Vector2 b) =>
            Angle(this, b);

        public float DistanceFrom(Vector2 b) =>
            Distance(this, b);

        public static float Angle(Vector2 a, Vector2 b) =>
            (float)(Math.Atan2(b.X - a.X, a.Y - b.Y));

        public static float Distance(Vector2 a, Vector2 b) =>
            (float)Math.Sqrt(Math.Pow(b.X-a.X, 2) + Math.Pow(b.Y - a.Y, 2));

        public override bool Equals(object obj) =>
            obj is Vector2 b ? X == b.X && Y == b.Y : false;

        public static Vector2 Zero => new Vector2(0, 0);

        public static Vector2 One => new Vector2(1, 1);

        public override string ToString() =>
            "(" + X + ", " + Y + ")";
    }
}