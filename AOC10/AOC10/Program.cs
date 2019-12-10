﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AOC10
{
    public class Program
    {
        public static void Main()
        {
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
            // Get number of visible asteroids from each asteroid
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
            Console.WriteLine("Best Asteroid: " + bestAsteroid);

            //Get angles from best asteroid to all others
            newAsteroids.Clear();
            for (var i = 0; i < asteroids.Count; i++)
            {
                var angle = bestAsteroid.AngleTo(asteroids[i]);
                newAsteroids.Add((asteroids[i], asteroids[i].Y == bestAsteroid.Y && angle > 6.2f ? 0 : angle));
            }
            newAsteroids.Sort((a, b) => b.Item2.CompareTo(a.Item2));
            newAsteroids.Reverse();

            //Vaporize Asteroids one by one :D BY GIANT LAZERZ
            var removedOrder = new List<(Vector2, float)>();
            var lastRemoved = (Vector2.Zero, 0f);
            while (newAsteroids.Count > 0)
            {
                var current = newAsteroids[0];
                if (current.Item2 != lastRemoved.Item2)
                {
                    a:;
                    Console.WriteLine("Blasted " + current.Item1);
                    lastRemoved = current;
                    removedOrder.Add(current);
                    newAsteroids.RemoveAt(0);
                }
                else
                {
                    if (newAsteroids[0].Item2 == newAsteroids.Last().Item2)
                    {
                        Console.WriteLine("Blasted " + current.Item1);
                        lastRemoved = current;
                        removedOrder.Add(current);
                        newAsteroids.RemoveAt(0);
                    }
                    else
                    {
                        newAsteroids.RemoveAt(0);
                        newAsteroids.Add(current);
                    }
                }
            }
            var last = removedOrder.Last();
            //var num = (removedOrder[199].Item1.X * 100) + removedOrder[199].Item1.Y;
            //Console.WriteLine(num);

            Console.Read();
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
            AngleTo(this, b);

        public static float AngleTo(Vector2 a, Vector2 b) =>
            (float)(Math.Atan2(a.Y - b.Y, b.X - a.X) + Math.PI);

        public override bool Equals(object obj) =>
            obj is Vector2 b ? X == b.X && Y == b.Y : false;

        public static Vector2 Zero => new Vector2(0, 0);

        public static Vector2 One => new Vector2(1, 1);

        public override string ToString() =>
            "(" + X + ", " + Y + ")";
    }
}