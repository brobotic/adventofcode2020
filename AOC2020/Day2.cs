using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC2020
{
    class Day2
    {
        Utils u = new Utils();

        public void Part1()
        {
            int total = 0;
            var input = u.GetLinesInFile(@"c:\tmp\adventofcode2020\day2.txt");
            foreach (var i in input)
            {
                var s = i.Split(' ');
                var policyCount = s[0].Split('-');
                var policyCountLow = Int32.Parse(policyCount[0]);
                var policyCountHigh = Int32.Parse(policyCount[1]);
                var policyLetter = s[1].TrimEnd(':').ToCharArray()[0];
                var password = s[2];
                
               if (PolicyTest(policyCountLow, policyCountHigh, password, policyLetter))
               {
                    total++;
               }
            }

            Console.WriteLine(total);
            Console.ReadLine();
        }

        public bool PolicyTest(int low, int high, string password, char letter)
        {
            var count = password.Count(x => x == letter);
            if (count < low || count > high)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool PolicyTestPart2(int posOne, int posTwo, string password, char letter)
        {
            var indexOne = posOne - 1;
            var indexTwo = posTwo - 1;
            var matches = 0;
            if (password[indexOne] == letter)
            {
                matches++;
            }

            if (password[indexTwo] == letter)
            {
                matches++;
            }

            if (matches == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Part2()
        {
            int total = 0;
            var input = u.GetLinesInFile(@"c:\tmp\adventofcode2020\day2.txt");
            foreach (var i in input)
            {
                var s = i.Split(' ');
                var policyCount = s[0].Split('-');
                var positionOne = Int32.Parse(policyCount[0]);
                var positionTwo = Int32.Parse(policyCount[1]);
                var policyLetter = s[1].TrimEnd(':').ToCharArray()[0];
                var password = s[2];

                if (PolicyTestPart2(positionOne, positionTwo, password, policyLetter))
                {
                    total++;
                }
            }

            Console.WriteLine(total);
            Console.ReadLine();
        }
    }
}
