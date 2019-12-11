using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC11
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllText("input.txt").Split(',').ToList().ConvertAll(a => long.Parse(a)).ToArray();

            var comp = new IntcodeComputer(input);
            var paintedTiles = new SortedDictionary<(int, int), int>(); // Position, color (0 = black, 1 = white)
            var robotPos = (0, 0);
            var robotRot = 0;

            paintedTiles.Add((0, 0), 1);
            while (true)
            {
                var currentColor = GetColor(robotPos);
                comp.Inputs.Add(currentColor);

                var val1 = comp.Parse();
                if (val1 == long.MaxValue) { break; }
                var val2 = comp.Parse();
                if (val2 == long.MaxValue) { break; }

                SetColor(robotPos, (int)val1);
                robotRot = val2 == 0 ? robotRot - 1 : robotRot + 1;
                robotRot = robotRot < 0 ? 3 : robotRot > 3 ? 0 : robotRot;
                var movements = new int[,]
                {
                    { 0, 1 },
                    { 1, 0 },
                    { 0, -1 },
                    { -1, 0 },
                };
                robotPos = (
                    robotPos.Item1 + movements[robotRot, 0], 
                    robotPos.Item2 + movements[robotRot, 1]);
            }

            var xMin = paintedTiles.OrderBy(a => a.Key.Item1).First().Key.Item1;
            var yMin = paintedTiles.OrderBy(a => a.Key.Item2).First().Key.Item2;

            foreach (var t in paintedTiles)
            {
                var tCol = t.Value;
                if (tCol == 1)
                {
                    var tPos = t.Key;
                    Console.SetCursorPosition(tPos.Item1 - xMin + 1, -(tPos.Item2 - yMin + 1) - (yMin * 2));
                    Console.Write("#");
                }
            }

            Console.Read();

            int GetColor((int, int) pos) =>
                paintedTiles.ContainsKey(pos) ? paintedTiles[pos] : 0;

            void SetColor((int, int) pos, int col)
            {
                if (paintedTiles.ContainsKey(pos)) { paintedTiles[pos] = col; }
                else { paintedTiles.Add(pos, col); }
            }
        }
    }
}
