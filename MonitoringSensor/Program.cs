using System;

namespace MonitoringSensor
{
    public class Program
    {
        public static void Main()
        {
            //creating object of class Program
            var readDataToConsole = new SenderReadFromCsv();
            readDataToConsole.ReadValues(); // Calling method
            Console.ReadLine();
        }
    }
}
