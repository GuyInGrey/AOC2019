using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC08
{
    class Program
    {
        static void Main(string[] args)
        {
            //Decoding
            var input = File.ReadAllText("input.txt").ToCharArray().ToList().ConvertAll(c => int.Parse(c.ToString()));
            var width = 25;
            var height = 6;
            var layerCnt = input.Count / (width * height);
            var layers = new List<int[,]>();

            var i = 0;
            for (var l = 0; l < layerCnt; l++)
            {
                layers.Add(new int[width, height]);

                for (var y = 0; y < height; y++)
                {
                    for (var x = 0; x < width; x++)
                    {
                        layers[l][x, y] = input[i];
                        i++;
                    }
                }
            }

            //Part One

            var fewestZeroes = -1;
            var fewestZeroesIndex = -1;

            i = 0;
            foreach (var l in layers)
            {
                var z = CountFor(l, 0);
                if (fewestZeroesIndex == -1 || fewestZeroes > z)
                {
                    fewestZeroes = z;
                    fewestZeroesIndex = i;
                }
                i++;
            }

            Console.WriteLine((CountFor(layers[fewestZeroesIndex], 1) * CountFor(layers[fewestZeroesIndex], 2)));

            //Part Two


            //Get Final Image
            var finalImage = new int[width, height];

            for (var x = 0; x < width; x++)
            {
                for (var y = 0; y < height; y++)
                {
                    var color = 2;
                    for (var l = layerCnt - 1; l > -1; l--)
                    {
                        var c = layers[l][x, y];
                        if (c == 1) { color = 1; }
                        if (c == 0) { color = 0; }
                    }

                    Console.SetCursorPosition(x, y);
                    finalImage[x, y] = color;
                }
            }

            //Display Image
            for (var x = 0; x < width; x++)
            {
                for (var y = 0; y < height; y++)
                {
                    var c = finalImage[x, y];
                    if (c == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Black;
                    }
                    else if (c == 1)
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else if (c == 2)
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                    }

                    Console.SetCursorPosition(x, y + 2);

                    Console.Write("▓");
                }
            }

            Console.Read();
        }

        public static int CountFor(int[,] layer, int countFor)
        {
            var toReturn = 0;
            for (var x = 0; x < layer.GetLength(0); x++)
            {
                for (var y = 0; y < layer.GetLength(1); y++)
                {
                    if (layer[x,y] == countFor) { toReturn++; }
                }
            }
            return toReturn;
        }
    }
}