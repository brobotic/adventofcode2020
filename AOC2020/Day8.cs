using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC2020
{
    class Day8
    {
        Utils utils = new Utils();

        private int answer = 0;
        private int position = 0;

        public void Part1()
        {
            var input = utils.GetLinesInFile(@"c:\tmp\adventofcode2020\day8.txt");
            var instructionsUsed = new List<int>();

            foreach (var i in input)
            {
                var line = input[position].Split(' ');
                var instruction = line[0];
                var rawValue = line[1];
                var value = Int32.Parse(line[1]);

                if (instruction == "nop")
                {
                    position++;
                    continue;
                }

                if (instruction != "nop" && instructionsUsed.Contains(position))
                {
                    break;
                }

                instructionsUsed.Add(position);

                if (instruction == "acc")
                {
                    answer += value;
                    position++;
                }

                if (instruction == "jmp")
                {
                    var nextInstruction = input[position];
                    position += value;
                }
            }

            Console.WriteLine($"Answer: {answer}");
            Console.ReadKey();
        }

        public void Part2()
        {
            var input = utils.GetLinesInFile(@"c:\tmp\adventofcode2020\day8.txt");
            var allInputs = new List<List<string>>();
            var instructionsChangedNop = new List<int>();
            var instructionsChangedJmp = new List<int>();
            var instructionsUsed = new List<int>();
            var completed = false;

            // Define how many lines we need to change
            var linesToChange = input.Where(x => x.Contains("nop") || x.Contains("jmp")).Count();

            // Create a variation of the input for each nop/jmp swap
            for (int l = 0; l < linesToChange; l++)
            {
                var inputCopy = new List<string>(input);

                for (int k = 0; k < input.Count(); k++)
                {
                    var line = input[k].Split(' ');
                    var instruction = line[0];

                    if (instruction == "nop" || instruction == "jmp")
                    {
                        if (!instructionsChangedNop.Contains(k))
                        {
                            if (instruction == "nop")
                            {
                                inputCopy[k] = inputCopy[k].Replace("nop", "jmp");
                            }
                            else
                            {
                                inputCopy[k] = inputCopy[k].Replace("jmp", "nop");
                            }

                            instructionsChangedNop.Add(k);
                            break;
                        }
                    }
                }

                allInputs.Add(inputCopy);
            }

            // Test each variation of the input until a program terminates succesfully
            foreach (var inp in allInputs)
            {
                if (completed)
                {
                    break;
                }

                answer = 0;
                position = 0;
                var instructionsPerformed = new List<int>();

                foreach (var i in inp)
                {
                    if (position >= inp.Count())
                    {
                        completed = true;
                        break;
                    }

                    var line = inp[position].Split(' ');
                    var instruction = line[0];
                    var rawValue = line[1];
                    var value = Int32.Parse(line[1]);

                    if (instruction == "nop")
                    {
                        position++;
                        continue;
                    }

                    if (instruction != "nop" && instructionsPerformed.Contains(position))
                    {
                        break;
                    }

                    instructionsPerformed.Add(position);

                    if (instruction == "acc")
                    {
                        answer += value;
                        position++;
                    }

                    if (instruction == "jmp")
                    {
                        position += value;
                    }
                }
            }

            Console.WriteLine($"Answer: {answer}");
            Console.ReadKey();
        }
    }
}
