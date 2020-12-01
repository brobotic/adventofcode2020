using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC2020
{
    class Day1
    {
        Utils u = new Utils();

        public void Part1()
        {
            bool found = false;
            var input = u.GetLinesInFile(@"c:\tmp\adventofcode2020\day1.txt");
        
            for (int i = 0; i < input.Count; i++)
            {
                var num1 = Int32.Parse(input[i]);
                foreach (var j in input)
                {
                    var num2 = Int32.Parse(j);
                    if (num1 + num2 == 2020)
                    {
                        Console.WriteLine($"{input[i]} + {j} == 2020");
                        Console.WriteLine(num1 * num2);
                        found = true;
                        break;
                    }
                }

                if (found)
                {
                    break;
                }
            }

            Console.ReadLine();
        }

        public void Part2()
        {
            var found = false;
            var input = u.GetLinesInFile(@"c:\tmp\adventofcode2020\day1.txt");

            for (int i = 0; i < input.Count; i++)
            {
                var num1 = Int32.Parse(input[i]);
                foreach (var j in input)
                {
                    var num2 = Int32.Parse(j);

                    foreach (var k in input)
                    {
                        var num3 = Int32.Parse(k);
                        if (num1 + num2 + num3 == 2020)
                        {
                            Console.WriteLine($"{input[i]} + {j} + {k} == 2020");
                            Console.WriteLine(num1 * num2 * num3);
                            found = true;
                            break;
                        }
                    }

                    if (found)
                    {
                        break;
                    }
                }

                if (found)
                {
                    break;
                }
            }

            Console.ReadLine();
        }
    }
}
