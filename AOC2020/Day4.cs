using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AOC2020
{
    class Day4
    {
        Utils u = new Utils();
        static List<string> fields = new List<string>() {
            "byr",
            "iyr",
            "eyr",
            "hgt",
            "hcl",
            "ecl",
            "pid",
            "cid"
        };

        public void Part1()
        {
            var input = u.GetLinesInFile(@"c:\tmp\adventofcode2020\day4.txt");
            var passports = new Dictionary<int, Dictionary<string, string>>();
            var currentPassport = 0;
            var validPassports = 0;

            foreach (var line in input)
            {
                if (line == "")
                {
                    currentPassport++;
                    continue;
                }

                var passport = new Dictionary<string, string>();
                var s = line.Split(' ');

                for (int j = 0; j < s.Length; j++)
                {
                    var info = s[j].Split(':');
                    if (passports.ContainsKey(currentPassport))
                    {
                        passports[currentPassport].Add(info[0], info[1]);

                    }
                    else
                    {
                        var newDict = new Dictionary<string, string>() { { info[0], info[1] } };
                        passports.Add(currentPassport, newDict);
                    }
                }
            }
            
            foreach (KeyValuePair<int, Dictionary<string, string>> d in passports)
            {
                var isValid = IsValidPassportOne(d.Value);
                if (isValid)
                {
                    validPassports++;
                }
            }

            Console.WriteLine(validPassports);
            Console.ReadKey();
        }

        public bool IsValidPassportOne(Dictionary<string, string> passport )
        {
            if (passport.Count == fields.Count)
            {
                return true;
            }

            if (passport.Count == fields.Count - 1 && !passport.ContainsKey("cid"))
            {
                return true;
            }

            return false;
        }

        public bool IsValidPassportTwo(Dictionary<string, string> passport)
        {
            var birthYear = Int32.Parse(passport["byr"]);
            var issueYear = Int32.Parse(passport["iyr"]);
            var expirationYear = Int32.Parse(passport["eyr"]);
            var height = passport["hgt"];
            var hairColor = passport["hcl"];
            var eyeColor = passport["ecl"];
            var passportId = passport["pid"];

            if (birthYear < 1920 || birthYear > 2002)
            {
                return false;
            }

            if (issueYear < 2010 || issueYear > 2020)
            {
                return false;
            }


            if (expirationYear < 2020 || expirationYear > 2030)
            {
                return false;
            }

            var heightCmInCheck = Regex.Matches(height, @"[a-zA-Z]").Count;
            if (heightCmInCheck < 1)
            {
                return false;
            }

            var heightString = new string(height.Where(Char.IsDigit).ToArray());
            int heightNumber = Int32.Parse(heightString);
            var heightStyle = new string(height.Where(Char.IsLetter).ToArray());

            if (heightStyle == "cm")
            {
                if (heightNumber < 150 || heightNumber > 193)
                {
                    return false;
                }
            }

            if (heightStyle == "in")
            {
                if (heightNumber < 59 || heightNumber > 76)
                {
                    return false;
                }
            }

            if (!hairColor.StartsWith("#"))
            {
                return false;
            }

            if (!EyeColorCheck(eyeColor))
            {
                return false;
            }

            var passportIdCheck = new string(passportId.Where(Char.IsDigit).ToArray());
            if (passportIdCheck.Length != 9)
            {
                return false;
            }

            return true;
        }

        public bool EyeColorCheck(string eyeColor)
        {
            List<Regex> filters = new List<Regex>();
            filters.Add(new Regex("amb"));
            filters.Add(new Regex("blu"));
            filters.Add(new Regex("brn"));
            filters.Add(new Regex("gry"));
            filters.Add(new Regex("grn"));
            filters.Add(new Regex("hzl"));
            filters.Add(new Regex("oth"));

            if(filters.Any(x => x.IsMatch(eyeColor)))
            {
                return true;
            }

            return false;
        }

        public void Part2()
        {
            var input = u.GetLinesInFile(@"c:\tmp\adventofcode2020\day4.txt");
            var passports = new Dictionary<int, Dictionary<string, string>>();
            var currentPassport = 0;
            var validPassports = 0;

            foreach (var line in input)
            {
                if (line == "")
                {
                    currentPassport++;
                    continue;
                }

                var passport = new Dictionary<string, string>();
                var s = line.Split(' ');

                for (int j = 0; j < s.Length; j++)
                {
                    var info = s[j].Split(':');
                    if (passports.ContainsKey(currentPassport))
                    {
                        passports[currentPassport].Add(info[0], info[1]);

                    }
                    else
                    {
                        var newDict = new Dictionary<string, string>() { { info[0], info[1] } };
                        passports.Add(currentPassport, newDict);
                    }
                }
            }

            foreach (KeyValuePair<int, Dictionary<string, string>> d in passports)
            {
                if (d.Value.Count == fields.Count || d.Value.Count == fields.Count - 1 && !d.Value.ContainsKey("cid"))
                {
                    var isValid = IsValidPassportTwo(d.Value);
                    if (isValid)
                    {
                        validPassports++;
                    }
                }
            }

            Console.WriteLine(validPassports);
            Console.ReadKey();
        }
    }
}
