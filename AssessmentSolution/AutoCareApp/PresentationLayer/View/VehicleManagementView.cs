using AutoCareApp.ApplicationLayer.Service;
using AutoCareApp.Domain.Model;
using AutoCareApp.PresentationLayer.Helper;
using System;
using System.Linq;

namespace AutoCareApp.PresentationLayer.View
{
    public class VehicleManagementView
    {
        private readonly VehicleService _vehicleService;

        public VehicleManagementView(VehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        public void RunDashBoard()
        {
            int choice;
            do
            {
                Console.WriteLine(DisplayResource.VehicleDashBoardOptions);
                var isvalidchoice = InputValidator.ValidateInteger(Console.ReadLine(), out choice);
                if (!isvalidchoice)
                {
                    Console.WriteLine("Invalid Integer format!");
                }

                switch (choice)
                {
                    case 1:
                        this.RegisterVehicle();
                        break;
                    case 2:
                        this.RemoveVehicle();
                        break;
                    case 3:
                        this.UpdateVehicel();
                        break;
                    case 4:
                        this.DisplayAllVehicles();
                        break;
                    case 5:
                        Console.WriteLine("Exiting the Dashboard, Bye!");
                        break;
                    default:
                        Console.WriteLine("Invalid Menu choice, Menu choice should be between 1 to 5");
                        break;
                }
            }
            while (choice != 5);
        }

        private void RegisterVehicle()
        {
            Console.WriteLine("Enter vehicle details:");
            var vehicle = this.GetVehicleDetails();
            if (vehicle == null)
            {
                return;
            }

            if (!this._vehicleService.AddVehicleDetails(vehicle))
            {
                Console.WriteLine("Vehicle Number already exists, Can't Register Vehicle");
                return;
            }

            Console.WriteLine("Vehicle registered successfully!");
        }

        private Vehicle GetVehicleDetails()
        {
            string vehicleNumber = this.GetVehicleNumber();
            if (vehicleNumber == null)
            {
                return null;
            }

            string model = this.GetVehicleModel();
            if (model == null)
            {
                return null;
            }

            string manufacturer = this.GetManufacturer();
            if (manufacturer == null)
            {
                return null;
            }

            int manufacturedYear = this.GetManufacturedYear();
            if (manufacturedYear == -1)
            {
                return null;
            }

            double kilometer = this.GetKiloMeter();
            if (kilometer == -1)
            {
                return null;
            }

            return new Vehicle(Guid.NewGuid(), CurrentSessionManager.UserId, vehicleNumber, model, manufacturer, manufacturedYear, kilometer);
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

        private string GetVehicleModel()
        {
            Console.WriteLine("Enter Vehicle Model : ");
            var model = Console.ReadLine();
            if (!InputValidator.ValidateString(model))
            {
                Console.WriteLine("vehicle Model should contain only characters!");
                return null;
            }

            return model;
        }

        private string GetManufacturer()
        {
            Console.WriteLine("Enter Vehicle Manufacturer : ");
            var manufacturer = Console.ReadLine();
            if (!InputValidator.ValidateString(manufacturer))
            {
                Console.WriteLine("vehicle Manufacturer should contain only characters!");
                return null;
            }

            return manufacturer;
        }

        private int GetManufacturedYear()
        {
            Console.WriteLine("Enter Vehicle Manufactured Year : ");
            var isValidYear = InputValidator.ValidateInteger(Console.ReadLine(), out int year);
            if (!isValidYear)
            {
                Console.WriteLine("Invalid Manufactured year format!");
                return -1;
            }
            if (year > DateTime.Now.Year)
            {
                Console.WriteLine("Year should not be ahead of current year!");
                return -1;
            }

            return year;
        }

        private double GetKiloMeter()
        {
            Console.WriteLine("Enter Kilometers: ");
            var isValidKilometer = InputValidator.ValidateDouble(Console.ReadLine(), out double kilometer);
            if (!isValidKilometer)
            {
                Console.WriteLine("Invalid Double format format!");
                return -1;
            }
            if (kilometer < 0)
            {
                Console.WriteLine("Kilometer can't be less than 0");
                return -1;
            }

            return kilometer;
        }

        private void RemoveVehicle()
        {
            Console.WriteLine("Remove your registered vehicle:");
            var vehicleNumber = this.GetVehicleNumber();
            if (vehicleNumber == null)
            {
                return;
            }

            if (!this._vehicleService.RemoveVehicle(vehicleNumber))
            {
                Console.WriteLine("You don't have any vehicles registered with this number!");
                return;
            }

            Console.WriteLine("Vehicle Deleted Successfully!");
        }

        private void UpdateVehicel()
        {
            Console.WriteLine("Update your registered vehicle:");
            var vehicleDetails = this.GetVehicleDetails();
            if (vehicleDetails == null)
            {
                return;
            }

            if (!this._vehicleService.UpdateVehicleDetails(vehicleDetails.VehicleNumber, vehicleDetails))
            {
                Console.WriteLine("You don't have any registered vehicles to update their details");
                return;
            }

            Console.WriteLine("Vehicle Details updated successfully!");
        }

        private void DisplayAllVehicles()
        {
            var vehicles = this._vehicleService.GetAllVehicles();
            if (!vehicles.Any())
            {
                Console.WriteLine("No registered vehicles");
                return;
            }

            foreach (var vehicle in vehicles)
            {
                Console.WriteLine($"Vehicle Number: {vehicle.VehicleNumber}, Model: {vehicle.Model}, Manufacturer: {vehicle.Manufacturer}, Manufactured Year: {vehicle.ManufacturedYear}, Kilometers: {vehicle.Kilometer}");
            }
        }
    }
}
