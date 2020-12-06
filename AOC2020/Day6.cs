using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC2020
{
    class Day6
    {
        Utils u = new Utils();

        public void Part1()
        {
            // Puzzle input is answers collected from every group on the plane
            // Each group's answers are separated by a blank line
            // Within each group, each person's answers are on a single line

            //var input = u.GetLinesInFile(@"c:\tmp\adventofcode2020\day6example.txt");
            var input = u.GetLinesInFile(@"c:\tmp\adventofcode2020\day6.txt"); 

            int totalAnswers = 0;

            var groupedAnswers = new Dictionary<int, string>();
            var currentGroup = 0;

            // Give each group a group# (count up from 0 as we iterate)
            // Get the unique occurrences of each letter in each group, that is the # of questions answered
            foreach (var answer in input)
            {
                // Increment our group # as we pass empty lines in the input
                if (answer == "")
                {
                    currentGroup++;
                    continue;
                }

                if (groupedAnswers.ContainsKey(currentGroup))
                {
                    // If the current answer has not been noted, add it to the list of total answers for the group
                    foreach (var _char in answer)
                    {
                        if (!groupedAnswers[currentGroup].Contains(_char))
                        {
                            groupedAnswers[currentGroup] += _char;
                        }
                    }
                }
                else
                {
                    var chars = new String(answer.Distinct().ToArray());
                    groupedAnswers.Add(currentGroup, chars);
                }
            }

            foreach (KeyValuePair<int, string> kvp in groupedAnswers)
            {
                Console.WriteLine($"Group {kvp.Key} had {kvp.Value.Length} answers");
                totalAnswers += kvp.Value.Length;
            }

            Console.WriteLine($"Total answers: {totalAnswers}");
            Console.ReadKey();
        }

        public void Part2()
        {
            var input = u.GetLinesInFile(@"c:\tmp\adventofcode2020\day6.txt");
            var groupedAnswers = new Dictionary<int, Dictionary<int, string>>();
            var currentGroup = 0;
            var total = 0;

            for (int i = 0; i < input.Count; i++)
            {
                if (input[i] == "")
                {
                    currentGroup++;
                    continue;
                }

                if (groupedAnswers.ContainsKey(currentGroup))
                {
                    if (groupedAnswers[currentGroup].ContainsKey(i))
                    {
                        foreach (var _char in input[i])
                        {
                            if (!groupedAnswers[currentGroup][i].Contains(_char))
                            {
                                groupedAnswers[currentGroup][i] += _char;
                            }
                        }
                    }
                    else
                    {
                        var chars = new String(input[i].Distinct().ToArray());
                        groupedAnswers[currentGroup].Add(i, chars);
                    }

                }
                else
                {
                    var chars = new String(input[i].Distinct().ToArray());
                    var newEntry = new Dictionary<int, string>();
                    newEntry.Add(i, chars);
                    groupedAnswers.Add(currentGroup, newEntry);
                }
            }


            foreach (KeyValuePair<int, Dictionary<int, string>> group in groupedAnswers)
            {
                var peopleInGroup = group.Value.Count();
                var answers = new Dictionary<char, int>();

                foreach (KeyValuePair<int, string> kvp in group.Value)
                {
                    foreach (var c in kvp.Value)
                    {
                        if (answers.ContainsKey(c))
                        {
                            answers[c]++;
                        }
                        else
                        {
                            answers.Add(c, 1);
                        }
                    }
                }

                foreach (var answer in answers)
                {
                    if (answer.Value == peopleInGroup)
                    {
                        total++;
                    }
                }
            }

            Console.WriteLine($"Total: {total}");
            Console.ReadKey();
        }
    }
}
