using System;

namespace MonitoringSensor
{
    class Program
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
