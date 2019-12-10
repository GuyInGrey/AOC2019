using System;
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
            var newAsteroids = new List<(Vector2, int)>(); 
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

            Console.WriteLine(newAsteroids[0].Item2);
            Console.Read();
        }
    }

    public class Vector2
    {
        public float X = 0;
        public float Y = 0;

        public Vector2()
        {

        }

        public Vector2(float x, float y)
        {
            X = x;
            Y = y;
        }

        public float AngleTo(Vector2 b)
        {
            return AngleTo(this, b);
        }

        public static float AngleTo(Vector2 a, Vector2 b)
        {
            return (float)Math.Atan2(b.Y - a.Y, b.X - a.X);
        }

        public override bool Equals(object obj)
        {
            if (obj is Vector2 a)
            {
                return X == a.X && Y == a.Y;
            }
            return false;
        }
    }
}