using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC2020
{
    class Day5
    {
        Utils u = new Utils();

        public void Run()
        {
            var input = u.GetLinesInFile(@"c:\tmp\adventofcode2020\day5.txt");
            var allSeatIds = new List<int>();
            var allSeats = new List<Tuple<int, int>>();

            foreach (var seat in input)
            {
                var firstSeven = seat.Substring(0, seat.Length - 3);
                var lastThree = seat.Substring(seat.Length - 3);
                var rowMin = 0;
                var rowMax = 127;
                var finalRow = 0;

                var colMin = 0;
                var colMax = 7;
                var finalCol = 0;

                foreach (var t in firstSeven)
                {
                    if (t == 'F')
                    {
                        var oldMax = rowMax;
                        rowMax = (rowMin + rowMax) / 2;
                    }
                    else
                    {
                        var oldMin = rowMin;
                        rowMin = ((rowMin + rowMax) / 2) + 1;
                    }
                }

                foreach (var l in lastThree)
                {
                    if (l == 'L')
                    {
                        var oldMax = colMax;
                        colMax = (colMin + colMax) / 2;
                    }
                    else
                    {
                        var oldMin = colMin;
                        colMin = ((colMin + colMax) / 2) + 1;

                    }
                }

                finalRow = rowMax;
                finalCol = colMax;
                allSeats.Add(Tuple.Create(finalRow, finalCol));

                var seatId = finalRow * 8 + finalCol;
                allSeatIds.Add(seatId);
            }

            var highestSeatId = allSeatIds.Aggregate((x, y) => x > y ? x : y);
            Console.WriteLine($"Highest seat ID: {highestSeatId}");

            // Part 2
            var myRow = 0;
            var myCol = 0;

            var sorted = allSeats.OrderBy(i => i.Item1).ToList();
            var firstRow = sorted[0].Item1;
            var lastRow = sorted[sorted.Count - 1].Item1;

            // All rows except the first and last
            var rows = sorted.Where(i => i.Item1 > firstRow).Where(i => i.Item1 < lastRow).ToList();

            // Create a group for every row #. Count the # of seats taken in the row, the row that 
            var myRowGroup = rows.GroupBy(x => x.Item1).Where(x => x.Count() < 8).ToList()[0];
            var seatsInRow = new List<int>();

            foreach (var seat in myRowGroup)
            {
                seatsInRow.Add(seat.Item2);
            }

            var missingCol = Enumerable.Range(0, 8).Except(seatsInRow).ToList();
            myCol = missingCol[0];
            myRow = myRowGroup.Key;

            var mySeatId = myRow * 8 + myCol;
            Console.WriteLine($"My seat ID: {myRow * 8 + myCol}");
            Console.ReadKey();
        }
    }
}
