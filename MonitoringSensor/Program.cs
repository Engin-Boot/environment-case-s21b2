using System;

namespace MonitoringSensor
{
    public class Program
    {
        static void Main(string[] args)
        {
            //creating object of class Program
            var readDataToConsole = new SenderReadFromCsv();
            readDataToConsole.ReadValues(); // Calling method
            Console.ReadLine();
        }
    }
}
