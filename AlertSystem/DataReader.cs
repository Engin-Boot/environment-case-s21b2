using System;

namespace AlertSystem
{
    public class DataReader
    {
        public static bool ReadInput(out string data)
        {
            Console.WriteLine("Enter temperature and humidity (exit to break)");
            data = Console.ReadLine();
            return data != null && data != "exit";
        }
    }
}
