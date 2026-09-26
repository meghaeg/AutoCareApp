using System;

namespace AutoCareApp.Domain.Model
{
    public class Vehicle
    {
        public Vehicle()
        {
        }

        public Vehicle(Guid vehicleId, string vehicleNumber, string model, string manufacturer, int manufacturedYear, double kilometer)
        {
            this.VehicleId = vehicleId;
            this.VehicleNumber = vehicleNumber;
            this.Model = model;
            this.Manufacturer = manufacturer;
            this.ManufacturedYear = manufacturedYear;
            this.Kilometer = kilometer;
        }

        public Guid VehicleId { get; set; }

        public string VehicleNumber { get; set; }

        public string Model { get; set; }

        public string Manufacturer { get; set; }

        public int ManufacturedYear { get; set; }

        public double Kilometer { get; set; }

    }
}
