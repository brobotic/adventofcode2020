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
            var input = u.GetLinesInFile(@"c:\tmp\adventofcode2020\day3.txt");
            var mapWidth = input[0].Length;
            var mapHeight = input.Count;
            var positionX = 0;
            var positionY = 0;
            var trees = 0;

            for (int i = 0; i < mapHeight; i++)
            {
                if (positionY == mapHeight - 1)
                {
                    break;
                }

                if (positionX + 3 >= mapWidth)
                {
                    positionX = (positionX + 3) - mapWidth;
                }
                else
                {
                    positionX = positionX + 3;
                }

                positionY = positionY + 1;

                var check = input[positionY].ToCharArray()[positionX];

                if (check.Equals('#'))
                {
                    trees++;
                }
            }

            Console.WriteLine(trees);
            Console.ReadKey();
        }

        public void Part2()
        {
            var input = u.GetLinesInFile(@"c:\tmp\adventofcode2020\day3.txt");

            var slopes = new List<Tuple<int, int>>()
            {
                Tuple.Create(1,1),
                Tuple.Create(3,1),
                Tuple.Create(5,1),
                Tuple.Create(7,1),
                Tuple.Create(1,2),
            };

            var allTrees = new List<long>();

            foreach (var slope in slopes)
            {
                var trees = 0;
                var right = slope.Item1;
                var down = slope.Item2;
                var mapWidth = input[0].Length;
                var mapHeight = input.Count;
                var positionX = 0;
                var positionY = 0;

                for (int i = 0; i < mapHeight; i++)
                {
                    if (positionY == mapHeight - 1)
                    {
                        break;
                    }

                    if (positionX + right >= mapWidth)
                    {
                        positionX = (positionX + right) - mapWidth;
                    }
                    else
                    {
                        positionX = positionX + right;
                    }

                    positionY = positionY + down;

                    var check = input[positionY].ToCharArray()[positionX];

                    if (check.Equals('#'))
                    {
                        trees++;
                    }
                }

                allTrees.Add(trees);
            }

            long total = allTrees.Aggregate((x, y) => x * y);
            Console.WriteLine(total);
            Console.ReadKey();
        }
    }
}
