using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AOC07
{
    class Part2
    {
        public static void Main2()
        {
            var currentAmp = 0;
            var first = true;

            var input = File.ReadAllText("input.txt").Split(',').ToList().ConvertAll(t => int.Parse(t)).ToArray();

            var possibleCombos = new List<int[]>();

            for (var a = 5; a < 10; a++)
            {
                for (var b = 5; b < 10; b++)
                {
                    for (var c = 5; c < 10; c++)
                    {
                        for (var d = 5; d < 10; d++)
                        {
                            for (var e = 5; e < 10; e++)
                            {
                                var combo = (new int[] { a, b, c, d, e }).ToList();
                                if (combo.Distinct().Count() == combo.Count())
                                {
                                    possibleCombos.Add(combo.ToArray());
                                }
                            }
                        }
                    }
                }
            }

            var finalOutputs = new List<int>();

            foreach (var combo in possibleCombos)
            {
                first = true;
                var amps = new List<Amplifier>();
                for (var i = 0; i < 5; i++)
                {
                    amps.Add(new Amplifier((int[])input.Clone(), combo[i]));
                }

                var lastOutput = 0;

                while (true)
                {
                    if (first)
                    {
                        amps[0].Inputs.Add(0);
                        first = false;
                    }

                    var current = amps[currentAmp];
                    var output = current.ParseIntCode2();
                    if (output == int.MaxValue)
                    {
                        goto exit;
                    }
                    if (currentAmp == amps.Count - 1)
                    {
                        lastOutput = output;
                        currentAmp = -1;
                    }

                    currentAmp++;
                    amps[currentAmp].Inputs.Add(output);
                }
                exit:;
                finalOutputs.Add(lastOutput);
            }

            finalOutputs.Sort();
            finalOutputs.Reverse();
            Console.WriteLine("Last Final Output: " + finalOutputs[0]);
            Console.Read();
        }
    }
}