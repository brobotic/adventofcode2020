using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC2020
{
    class Day3
    {
        Utils u = new Utils();

        public void Part1()
        {
            var input = u.GetLinesInFile(@"c:\tmp\adventofcode2020\day3test.txt");

            // we should know when we are at the edge of the map - what is the right most coordinate?
            // we should know when we are at the bottom of the map
            // build a coordinate system for this

            var mapWidth = input[0].Length;
            var mapHeight = input.Count;
            Console.WriteLine($"Each map is {mapWidth} coordinates wide");
            Console.WriteLine($"The map has {mapHeight} rows");

            var positionX = 0;
            var positionY = 0;
            var trees = 0;

            for (int i = 0; i < mapHeight; i++)
            {

                // If we're on the final row, stop
                /*
                if (positionY == mapHeight - 1)
                {
                    break;
                }
                */

                // If our current position + 3 is greater than the length of a row, overflow into a new row
                if (positionX + 3 > mapWidth)
                {
                    positionX = (positionX + 3) - mapWidth;
                } else
                {
                    positionX = positionX + 3;
                }

                positionY = positionY + 1;

                var check = input[positionY].ToCharArray()[positionX - 1];

                Console.WriteLine("Iteration" + i + ": " + input[positionY]);
                Console.WriteLine(positionX + ", " + positionY + ": " + check);

                if (check.Equals('#'))
                {
                    trees++;
                }

                //63 is wrong
                //Console.ReadKey();
            }

            Console.WriteLine(trees);
            Console.ReadKey();
        }

        public void Part2()
        {
            Console.ReadKey();
        }
    }
}
