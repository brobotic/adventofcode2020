using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC2020
{
    class Day7
    {
        Utils u = new Utils();
        public int totalBags = 0;
        public int _totalChildBags = 0;
        public Dictionary<int, Bag> bagOfDicts = new Dictionary<int, Bag>();

        public class Bag
        {
            public string name;
            public Dictionary<string, int> children;

            public Bag(string name, Dictionary<string, int> children) {
                this.name = name;
                this.children = children;
            }
        }

        public void Part1()
        {
            var rules = u.GetLinesInFile(@"c:\tmp\adventofcode2020\day7.txt");
            var allBags = new Dictionary<string, Dictionary<string, int>>();
            var answer = 0;

            foreach (var r in rules)
            {
                var line = r.Split(' ');
                var bag = $"{line[0]} {line[1]}"; // First two strings of each line define the bag
                var bagContents = new Dictionary<string, int>();

                // Add bags that contain no other bags to the dict of all bags so we can reference them easily later. TODO: may have been easier to see if you can add null dictionaries, but w/e
                if (r.Contains("no"))
                {
                    var none = new Dictionary<string, int>();
                    none.Add("none", 0);
                    allBags.Add(bag, none);
                    continue;
                }

                // This input line has multiple bags that need to be added to the master dict
                if (r.Contains(','))
                {
                    var bagContentsRaw = r.Split(new string[] { "contain" }, StringSplitOptions.None)[1].TrimStart();
                    var bags = bagContentsRaw.Split(',');

                    foreach (var b in bags)
                    {
                        var _line = b.TrimStart().Split(' ');
                        var _bagCount = _line[0];
                        var _bag = $"{_line[1]} {_line[2]}";
                        bagContents.Add(_bag, Int32.Parse(_bagCount));
                    }
                }
                else
                {
                    // There is only one bag that can be held in this bag
                    var newBag = $"{line[line.Length - 3]} {line[line.Length - 2]}";
                    var newBagCount = line[line.Length - 4];
                    bagContents.Add(newBag, Int32.Parse(newBagCount));
                }

                allBags.Add(bag, bagContents);
            }

            var bagsThatCannotContainGoldBag = new List<string>();
            var bagsThatCanContainGoldBags = new List<string>();
            var bagsRemaining = allBags.Count();

            while(bagsRemaining > 0)
            {
                foreach (KeyValuePair<string, Dictionary<string, int>> _rules in allBags)
                {
                    // To start, bags that are contained in a shiny gold bags cannot contain a gold bag. Remove all of these bags and their children
                    if (_rules.Key == "shiny gold")
                    {
                        foreach (var kvp in _rules.Value)
                        {
                            bagsThatCannotContainGoldBag.Add(kvp.Key);

                            foreach (var _kvp in allBags[kvp.Key])
                            {
                                bagsThatCannotContainGoldBag.Add(_kvp.Key);
                            }
                        }

                        bagsThatCannotContainGoldBag.Add(_rules.Key); // Add the shiny gold bag to list of bags to be removed
                    }

                    // Does this bag contain at least one bag that is known to contain gold bags?
                    var goldHolders = _rules.Value.Keys.Where(x => bagsThatCanContainGoldBags.Any(y => y == x));
                    if (goldHolders.Count() > 0)
                    {
                        //Console.WriteLine($"{_rules.Key}: {goldHolders.Count()}");
                        if (!bagsThatCanContainGoldBags.Contains(_rules.Key))
                        {
                            bagsThatCanContainGoldBags.Add(_rules.Key);
                        }
                    }

                    // Remove this bag if it only contains bags that cannot contain gold bags
                    var bad = _rules.Value.Keys.Where(x => bagsThatCannotContainGoldBag.Any(y => y == x)).ToList();
                    if (bad.Count() == _rules.Value.Keys.Count())
                    {
                        bagsThatCannotContainGoldBag.Add(_rules.Key);
                    }

                    foreach (KeyValuePair<string, int> kvp in _rules.Value)
                    {
                        if (kvp.Key == "shiny gold")
                        {
                            if (!bagsThatCanContainGoldBags.Contains(_rules.Key))
                            {
                                bagsThatCanContainGoldBags.Add(_rules.Key);
                            }
                        }

                        // Remove bags that do not contain other bags
                        if (kvp.Key == "none")
                        {
                            bagsThatCannotContainGoldBag.Add(_rules.Key);
                        }

                    }
                }

                foreach (var bag in bagsThatCannotContainGoldBag)
                {
                    if (allBags.ContainsKey(bag))
                    {
                        allBags.Remove(bag);
                        bagsRemaining--;
                    }
                }

                foreach (var bag in bagsThatCanContainGoldBags)
                {
                    if (allBags.ContainsKey(bag))
                    {
                        allBags.Remove(bag);
                        answer++;
                        bagsRemaining--;
                    }
                }
            }

            // Part 1 answer: 103
            Console.WriteLine($"Answer: {answer}");
            Console.ReadKey();
        }

        public void Part2()
        {
            var rules = u.GetLinesInFile(@"c:\tmp\adventofcode2020\day7.txt");
            var stupidFuckingBags = new List<Bag>();

            foreach (var r in rules)
            {
                var line = r.Split(' ');
                var bag = $"{line[0]} {line[1]}";
                var bagContents = new Dictionary<string, int>();

                if (r.Contains("no"))
                {
                    bagContents.Add(bag, 0);
                }
                else if (r.Contains(','))
                {
                    var bagContentsRaw = r.Split(new string[] { "contain" }, StringSplitOptions.None)[1].TrimStart();
                    var bags = bagContentsRaw.Split(',');

                    foreach (var b in bags)
                    {
                        var _line = b.TrimStart().Split(' ');
                        var _bagCount = _line[0];
                        var _bag = $"{_line[1]} {_line[2]}";
                        bagContents.Add(_bag, Int32.Parse(_bagCount));
                    }
                }
                else
                {
                    var newBag = $"{line[line.Length - 3]} {line[line.Length - 2]}";
                    var newBagCount = line[line.Length - 4];
                    bagContents.Add(newBag, Int32.Parse(newBagCount));
                }

                var ba = new Bag(bag, bagContents);
                stupidFuckingBags.Add(ba);
            }

            var bagToCheck = stupidFuckingBags.Where(x => x.name == "shiny gold").ToList()[0];

            foreach (var _b in stupidFuckingBags)
            {
                if (_b.name == "shiny gold")
                {
                    totalBags += GetBagCount(_b.name, stupidFuckingBags);
                }
            }

            // Wrong: 5822438 (too high)
            // Wrong: 1662 (too high)
            // Wrong: 1661
            // Wrong: 1006 (too low)
            // Wrong: 1010 (?)
            // Wrong: 1017 (?)
            // Wrong: 1054 (?)
            // Wrong: 1276 (?)
            // Wrong: 1324 (?)
            Console.WriteLine($"Answer: {totalBags}");
            Console.ReadKey();
        }

        public int GetBagCount(string bagName, List<Bag> bags)
        {
            int count = 0;
            var bag = bags.Where(x => x.name == bagName).ToList()[0];

            foreach (var b in bag.children)
            {

                if (b.Value == 0)
                {
                    return count;
                }

                count += b.Value + b.Value * GetBagCount(b.Key, bags);
            }

            return count;
        }
    }
}
