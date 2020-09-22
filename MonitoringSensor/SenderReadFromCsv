using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace MonitoringSensor
{
    public class SenderReadFromCsv
    {
        public void ReadValues()
        {
            var columns = new List<string>();
            const string path = "MonitoringSensor/SenderSampleData.csv";

            if (!File.Exists(path))
            {
                Console.WriteLine("File does not exist");
                return;
            }

            using (var reader = new CsvFileReader(path))
            {
                while (reader.ReadRow(columns))
                {
                    ReadDataToConsole(columns);
                    Console.WriteLine();
                  
                }
            }

            IsFileEmpty(columns);
        }

        public void ReadDataToConsole(List<string> columns)
        {

            foreach (var s in columns)
            {
                Console.Write(s);
                Console.Write(" ");
                Thread.Sleep(1000);
                        
            }
        }

        public void IsFileEmpty(List<string> columns)
        {
            if (columns.Count == 0)
            {
                Console.WriteLine("CSV FILE IS EMPTY");
            }
        }
    }
}
