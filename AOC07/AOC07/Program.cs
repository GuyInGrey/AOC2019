using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC07
{
    class Program
    {
        public static void Main()
        {
            Part2.Main2();
            return;

            var combos = new List<(int[], int)>();

            for (var a = 0; a < 5; a++)
            {
                for (var b = 0; b < 5; b++)
                {
                    for (var c = 0; c < 5; c++)
                    {
                        for (var d = 0; d < 5; d++)
                        {
                            for (var e = 0; e < 5; e++)
                            {
                                var input = File.ReadAllText("input.txt").Split(',').ToList().ConvertAll(t => int.Parse(t)).ToArray();
                                var aResult = ParseIntCode2((int[])input.Clone(), new int[] { a, 0 });
                                var bResult = ParseIntCode2((int[])input.Clone(), new int[] { b, aResult.Item2[0] });
                                var cResult = ParseIntCode2((int[])input.Clone(), new int[] { c, bResult.Item2[0] });
                                var dResult = ParseIntCode2((int[])input.Clone(), new int[] { d, cResult.Item2[0] });
                                var eResult = ParseIntCode2((int[])input.Clone(), new int[] { e, dResult.Item2[0] });
                                combos.Add((new int[] { a, b, c, d, e },eResult.Item2[0]));
                            }
                        }
                    }
                }
            }

            var newCombos = new List<(int[], int)>();
            foreach (var c in combos)
            {
                if (c.Item1.Distinct().Count() == c.Item1.Count())
                {
                    newCombos.Add(c);
                }
            }

            newCombos.Sort((a, b) => a.Item2.CompareTo(b.Item2));
            newCombos.Reverse();

            Console.WriteLine(newCombos[0].Item2);
            Console.Read();
        }

        public static (int[], List<int>) ParseIntCode2(int[] input, int[] input2)
        {
            var output = new List<int>();
            var inputIndex = 0;

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
                        input[input[instructionPointer + 1]] = input2[inputIndex];
                        inputIndex++;
                        instructionPointer += 2;
                        break;
                    case 4:
                        //Console.WriteLine("Output: " + GetParameter(0));
                        output.Add(GetParameter(0));
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
            return (input, output);
        }
    }
}