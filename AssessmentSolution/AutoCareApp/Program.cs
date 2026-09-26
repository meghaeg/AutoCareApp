using AutoCareApp.ApplicationLayer.Service;
using AutoCareApp.InfrastructureLayer;
using AutoCareApp.PresentationLayer.View;
using System;
using System.Timers;

namespace AutoCareApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            AppDomain.CurrentDomain.UnhandledException += UnhandledExceptionHandler;
            var vehicleRepository = new VehicleRepository();
            var maintainanceRepository = new MaintainanceRepository();
            var maintainanceService = new MaintainanceService(maintainanceRepository, vehicleRepository);
            maintainanceService.OnCompleted += service =>
            {
                NotificationService.DisplayNotification(service);
            };

            maintainanceService.InitializePendingServices();
            var timer = new System.Timers.Timer();
            timer.Interval = 3000;
            timer.Elapsed += ((object sender, ElapsedEventArgs e) =>
            {
                maintainanceService.InitializePendingServices();
            });

            timer.Start();
            _ = maintainanceService.DoService();
            var vehicleService = new VehicleService(vehicleRepository);
            var consoleOperator = new ConsoleOperator(vehicleService, maintainanceService);
            consoleOperator.Run();
            timer.Stop();
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
