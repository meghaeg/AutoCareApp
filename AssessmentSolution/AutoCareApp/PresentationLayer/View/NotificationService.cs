using AutoCareApp.Domain.Model;
using AutoCareApp.PresentationLayer.Helper;
using System;

namespace AutoCareApp.PresentationLayer.View
{
    public class NotificationService
    {
        private static readonly object _consoleLock = new object();

        public static void DisplayNotification(Maintainance service)
        {
            lock (_consoleLock)
            {
                int left = Console.WindowWidth - 65;
                int currentLeft = Console.CursorLeft;
                int currentTop = Console.CursorTop;
                Console.SetCursorPosition(left, 0);
                ConsoleHelper.WriteColoredLine($"Service for {service.VehicleNumber} has been completed, Ready for pick-up!".PadRight(60), ConsoleColor.Cyan);
                Console.SetCursorPosition(currentLeft, currentTop);
            }
        }
    }
}
