using AutoCareApp.Domain.Model;
using System;
using System.Collections.Generic;

namespace AutoCareApp.ApplicationLayer.Interface
{
    public interface IVehiclerepository
    {
        void AddVehicle(Vehicle vehicle);

        void UpdateVehicle(string vehicleNumber, Vehicle newVehicle);

        void DeleteVehicle(string vehicleNumber);

        IEnumerable<Vehicle> FetchAllVehicles();
    }
}
