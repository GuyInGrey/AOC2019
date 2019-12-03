using System;
using System.IO;
using System.Linq;

namespace AOC02
{
    class Program
    {
        static void Main(string[] args)
        {
            var wantedOutput = 19690720;
            var noun = 0;
            var verb = 0;
            var output = 0;

            while (true)
            {
                var input = File.ReadAllText("input.txt").Split(',').ToList().ConvertAll(a => int.Parse(a)).ToArray();
                input[1] = noun;
                input[2] = verb;
                output = ParseIntcode(input)[0];
                if (output == wantedOutput) { break; }

                noun++;
                if (noun == 100)
                {
                    noun = 0;
                    verb++;
                }
            }

            Console.WriteLine(noun.ToString("00") + verb.ToString("00"));
            Console.Read();
        }

        static int[] ParseIntcode(int[] input)
        {
            var instructionPointer = 0;
            while (true)
            {
                var op = input[instructionPointer];
                if (op == 99) { break; }
                var in1 = input[instructionPointer + 1];
                var in2 = input[instructionPointer + 2];
                var result = (op == 1) ? input[in1] + input[in2] : input[in1] * input[in2];
                input[input[instructionPointer + 3]] = result;
                instructionPointer += 4;
            }
            return input;
        }
    }
}