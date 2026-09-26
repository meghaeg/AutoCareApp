using AutoCareApp.ApplicationLayer.Interface;
using AutoCareApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AutoCareApp.InfrastructureLayer
{
    public class VehicleRepository : IVehiclerepository
    {
        private readonly object _fileLock = new object();

        public void AddVehicle(Vehicle vehicle)
        {
            lock (_fileLock)
            {
                var listOfVehicles = FileHandlingService.ReadFile<Vehicle>(FilePath.VehicleFile);
                listOfVehicles.Add(vehicle);
                FileHandlingService.WriteFile(FilePath.VehicleFile, listOfVehicles);
            }
        }

        public void DeleteVehicle(string vehicleNumber)
        {
            lock (_fileLock)
            {
                var listOfVehicles = FileHandlingService.ReadFile<Vehicle>(FilePath.VehicleFile);
                var vehicleToDelete = listOfVehicles.FirstOrDefault(x => x.VehicleNumber == vehicleNumber);
                listOfVehicles.Remove(vehicleToDelete);
                FileHandlingService.WriteFile(FilePath.VehicleFile, listOfVehicles);
            }
        }

        public IEnumerable<Vehicle> FetchAllVehicles()
        {
            lock (_fileLock)
            {
                var listOfVehicles = FileHandlingService.ReadFile<Vehicle>(FilePath.VehicleFile);
                return listOfVehicles;
            }
        }

        public void UpdateVehicle(string vehicleNumber, Vehicle newVehicle)
        {
            lock (_fileLock)
            {
                var listOfVehicles = FileHandlingService.ReadFile<Vehicle>(FilePath.VehicleFile);
                var vehicleToUpdate = listOfVehicles.FirstOrDefault(x => x.VehicleNumber == vehicleNumber);
                vehicleToUpdate.Model = newVehicle.Model;
                vehicleToUpdate.Manufacturer = newVehicle.Manufacturer;
                vehicleToUpdate.ManufacturedYear = newVehicle.ManufacturedYear;
                vehicleToUpdate.Kilometer = newVehicle.Kilometer;
                FileHandlingService.WriteFile(FilePath.VehicleFile, listOfVehicles);
            }
        }
    }
}
