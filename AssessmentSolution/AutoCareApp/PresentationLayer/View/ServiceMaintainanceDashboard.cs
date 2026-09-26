using AutoCareApp.ApplicationLayer.Service;
using AutoCareApp.Domain.Enums;
using AutoCareApp.Domain.Model;
using AutoCareApp.PresentationLayer.Helper;
using System;
using System.Linq;

namespace AutoCareApp.PresentationLayer.View
{
    public class ServiceMaintainanceDashboard
    {
        private readonly MaintainanceService _maintainanceService;

        public ServiceMaintainanceDashboard(MaintainanceService maintainanceService)
        {
            this._maintainanceService = maintainanceService;
        }

        public void RunServiceDashBoard()
        {
            int choice;
            do
            {
                Console.WriteLine("1. Book Service");
                Console.WriteLine("2. View all Service");
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
                        this.BookVehicleService();
                        break;
                    case 2:
                        this.SeeAllServices();
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

        private void BookVehicleService()
        {
            var service = this.GetServiceDetails();
            if (service == null)
            {
                return;
            }

            var bookingResult = this._maintainanceService.BookService(service);
            Console.WriteLine(bookingResult.Message);
        }

        private Maintainance GetServiceDetails()
        {
            string vehicleNumberToService = this.GetVehicleNumber();
            if (vehicleNumberToService == null)
            {
                return null;
            }

            var serviceType = this.GetServiceType();
            if (serviceType == ServiceType.Invalid)
            {
                Console.WriteLine("Invalid Service Type chosen!");
                return null;
            }

            var serviceDate = this.GetServiceDate();
            if (serviceDate == null)
            {
                return null;
            }

            Console.Write("Service start time, ");
            var startTime = this.GetTimeSpan();
            if (startTime == null)
            {
                return null;
            }

            Console.Write("Service end time, ");
            var endTime = this.GetTimeSpan();
            if (endTime == null)
            {
                return null;
            }

            return new Maintainance(Guid.NewGuid(), vehicleNumberToService, serviceType, ServiceStatus.Requested, serviceDate, startTime, endTime);
        }

        private DateTime? GetServiceDate()
        {
            Console.WriteLine("Enter Service Date (dd-mm-yyyy): ");
            string input = Console.ReadLine();
            if (!DateTime.TryParse(input, out DateTime serviceDate))
            {
                Console.WriteLine("Invalid Date format");
                return null;
            }

            if (serviceDate.Date < DateTime.Now.Date)
            {
                Console.WriteLine("Service date should be a future date");
                return null;
            }

            return serviceDate;
        }

        private TimeSpan? GetTimeSpan()
        {
            Console.WriteLine("Enter Time Slot (Hour:Min): ");
            string input = Console.ReadLine();
            if (!TimeSpan.TryParse(input, out TimeSpan time))
            {
                Console.WriteLine("Invalid time Span format!");
                return null;
            }

            return time;
        }

        private ServiceType GetServiceType()
        {
            Console.WriteLine("Enter service type (1 to 7):  ");
            Console.WriteLine("1. GeneralService");
            Console.WriteLine("2. OilChange");
            Console.WriteLine("3. BrakeService");
            Console.WriteLine("4. TyreService");
            Console.WriteLine("5. EngineCheck");
            Console.WriteLine("6. BatteryCheck");
            Console.WriteLine("7. Other");
            bool isValidChoice = InputValidator.ValidateInteger(Console.ReadLine(), out int choice);
            if (!isValidChoice)
            {
                choice = 0;
            }
            if (choice < 0 || choice >7)
            {
                choice = 0;
            }
            return (ServiceType)choice;
        }

        private string GetVehicleNumber()
        {
            Console.WriteLine("Enter Vehicle Number (FORMAT: TN33CD2705) : ");
            var vehicleNumber = Console.ReadLine();
            if (!InputValidator.ValidateVehicleNumber(vehicleNumber))
            {
                Console.WriteLine("Invalid Vehicle Number format!");
                return null;
            }

            return vehicleNumber;
        }

        private void SeeAllServices()
        {
            var listOfService = this._maintainanceService.GetAllMaintainanceService();
            if (!listOfService.Any())
            {
                Console.WriteLine("No services");
                return;
            }

            foreach (var service in listOfService)
            {
                Console.WriteLine($"Vehicle Number: {service.VehicleNumber}, Service Type chosen: {service.ServiceTypeChosen}, Service Data: {service.ServiceDate}, Slot: {service.ServiceStartTime} - {service.ServiceEndTime}, Service Status: {service.ServiceCurrentStatus}");
            }
        }
    }
}
