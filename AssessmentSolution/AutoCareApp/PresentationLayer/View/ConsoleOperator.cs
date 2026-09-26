using AutoCareApp.ApplicationLayer.Service;
using AutoCareApp.PresentationLayer.Helper;
using System;

namespace AutoCareApp.PresentationLayer.View
{
    public class ConsoleOperator
    {
        private readonly VehicleService _vehicleService;

        private readonly MaintainanceService _maintainanceService;

        public ConsoleOperator(VehicleService vehicleService, MaintainanceService maintainanceService)
        {
            this._vehicleService = vehicleService;
            this._maintainanceService = maintainanceService;
        }

        public void Run()
        {
            var vehicleManagementView = new VehicleManagementView(_vehicleService);
            var serviceMaintainanceView = new ServiceMaintainanceDashboard(_maintainanceService);

            int choice;
            do
            {
                Console.WriteLine("1. Vehicle Management");
                Console.WriteLine("2. Vehicle maintainance Service");
                Console.WriteLine("3. Exit");
                Console.WriteLine("Enter your choice (1 to 3): ");
                var isvalidchoice = InputValidator.ValidateInteger(Console.ReadLine(), out choice);
                if (!isvalidchoice)
                {
                    Console.WriteLine("Invalid Integer format!");
                }

                switch (choice)
                {
                    case 1:
                        vehicleManagementView.RunDashBoard();
                        break;
                    case 2:
                        serviceMaintainanceView.RunServiceDashBoard();
                        break;
                    case 3:
                        Console.WriteLine("Exiting the Dashboard, Bye!");
                        break;
                    default:
                        Console.WriteLine("Invalid Menu choice, Menu choice should be between 1 to 3");
                        break;
                }
            }
            while (choice != 3);
        }
    }
}
