using System;

namespace AutoCareApp.PresentationLayer.Helper
{
    public class ConsoleHelper
    {
        public static void WriteColoredLine(string input, ConsoleColor colorChoice)
        {
            Console.ForegroundColor = colorChoice;
            Console.WriteLine(input);
            Console.ResetColor();
        }
    }
}
