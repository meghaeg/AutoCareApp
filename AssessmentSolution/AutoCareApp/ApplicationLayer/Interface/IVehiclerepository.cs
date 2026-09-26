using AutoCareApp.Domain.Model;
using System;
using System.Collections.Generic;

namespace AutoCareApp.ApplicationLayer.Interface
{
    public interface IVehiclerepository
    {
        void AddVehicle(Vehicle vehicle);

        void UpdateVehicle(Guid vehicleId, Vehicle newVehicle);

        void DeleteVehicle(Guid vehicleId);

        IEnumerable<Vehicle> FetchAllVehicles();
    }
}
