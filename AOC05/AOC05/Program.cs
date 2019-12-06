using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace AOC02
{
    class Program
    {
        static void Main(string[] args)
        {
            var inputOriginal = File.ReadAllText("input.txt").Split(',').ToList().ConvertAll(a => int.Parse(a)).ToArray();

            var counts = new List<long>();

            for (var i = 0; i < 10000; i++)
            {
                var s = Stopwatch.StartNew();
                ParseIntCode2((int[])inputOriginal.Clone());
                s.Stop();
                counts.Add(s.ElapsedMilliseconds);
            }

            Console.WriteLine("Finished. Took " + counts.Average() + " milliseconds.");
            Console.Read();
        }

        static (int, int)[] OpCodeParameters = new (int, int)[]
        {
            (1, 3),
            (2, 3),
            (3, 1),
            (4, 1),
        };

        public static int[] ParseIntCode2(int[] input)
        {
            var instructionPointer = 0;
            while (true)
            {
                var current = input[instructionPointer];
                var currentS = current.ToString("D5");
                var opcode = current % 100;
                var modes = currentS.Substring(0, currentS.Length - 2).Select(d => int.Parse(d.ToString())).ToList().ConvertAll(a => a == 1 ? true : false); // true = immediate
                modes.Reverse();

                switch (opcode)
                {
                    case 1: // a * b at c
                        var a = GetParameter(0);
                        var b = GetParameter(1);
                        var c = input[instructionPointer + 3];
                        input[c] = a + b;
                        instructionPointer += 4;
                        break;
                    case 2:
                        a = GetParameter(0);
                        b = GetParameter(1);
                        c = input[instructionPointer + 3];
                        input[c] = a * b;
                        instructionPointer += 4;
                        break;
                    case 3:
                        //input[input[instructionPointer + 1]] = int.Parse(Console.ReadLine());
                        input[input[instructionPointer + 1]] = int.Parse("1");
                        instructionPointer += 2;
                        break;
                    case 4:
                        //Console.WriteLine("Output: " + GetParameter(0));
                        instructionPointer += 2;
                        break;
                    case 5:
                        if (GetParameter(0) != 0)
                        {
                            instructionPointer = GetParameter(1);
                        }
                        else
                        {
                            instructionPointer += 3;
                        }
                        break;
                    case 6:
                        if (GetParameter(0) == 0)
                        {
                            instructionPointer = GetParameter(1);
                        }
                        else
                        {
                            instructionPointer += 3;
                        }
                        break;
                    case 7:
                        input[input[instructionPointer + 3]] = GetParameter(0) < GetParameter(1) ? 1 : 0;
                        instructionPointer++;
                        break;
                    case 8:
                        input[input[instructionPointer + 3]] = GetParameter(0) == GetParameter(1) ? 1 : 0;
                        instructionPointer++;
                        break;
                    case 99:
                        goto exit;
                    default:
                        instructionPointer++;
                        break;
                }

                int GetParameter(int i)
                {
                    return modes[i] ? input[instructionPointer + i + 1] : input[input[instructionPointer + i + 1]];
                }
            }

            exit:;
            return input;
        }
    }
}