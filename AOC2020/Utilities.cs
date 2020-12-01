using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC2020
{
    public class Utils
    {
        public List<string> GetLinesInFile(string path)
        {
            List<string> _list = new List<string>();
            string lineInFile;
            System.IO.StreamReader file = new System.IO.StreamReader(path);
            while ((lineInFile = file.ReadLine()) != null)
            {
                _list.Add(lineInFile);
            }

            file.Close();
            return _list;
        }
    }
}
