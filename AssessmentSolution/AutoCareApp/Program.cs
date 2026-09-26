using AutoCareApp.ApplicationLayer.Service;
using AutoCareApp.InfrastructureLayer;
using AutoCareApp.PresentationLayer.View;
using System;

namespace AutoCareApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            AppDomain.CurrentDomain.UnhandledException += UnhandledExceptionHandler;
            var vehicleRepository = new VehicleRepository();
            var vehicleService = new VehicleService(vehicleRepository);
            var consoleOperator = new ConsoleOperator(vehicleService);
            consoleOperator.Run();
        }

        private static void UnhandledExceptionHandler(object sender, UnhandledExceptionEventArgs e)
        {
            if (e.ExceptionObject is Exception ex)
            {
                Console.WriteLine($"Exception caught: {ex.Message}");
            }    
        }
    }
}
