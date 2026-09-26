using AutoCareApp.ApplicationLayer.Interface;
using AutoCareApp.Domain.Model;
using AutoCareApp.InfrastructureLayer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AutoCareApp.ApplicationLayer.Service
{
    public class VehicleService
    {
        private readonly IVehiclerepository _vehicleRepository;

        public VehicleService(IVehiclerepository vehiclerepository)
        {
            this._vehicleRepository = vehiclerepository;
        }

        public bool AddVehicleDetails(Vehicle vehicle)
        {
            if (this.CheckVehicleExists(vehicle.VehicleNumber))
            {
                return false;
            }

            this._vehicleRepository.AddVehicle(vehicle);
            return true;
        }

        public bool RemoveVehicle(string vehicleNumber)
        {
            if (!this.CheckVehicleExists(vehicleNumber))
            {
                return false;
            }

            this._vehicleRepository.DeleteVehicle(vehicleNumber);
            return true;
        }

        public IEnumerable<Vehicle> GetAllVehicles()
        {
            return this._vehicleRepository.FetchAllVehicles();
        }

        public bool UpdateVehicleDetails(string vehicleNumber, Vehicle newVehicle)
        {
            if (!this.CheckVehicleExists(vehicleNumber))
            {
                return false;
            }

            this._vehicleRepository.UpdateVehicle(vehicleNumber, newVehicle);
            return true;
        }

        private bool CheckVehicleExists(string vehicleNumber)
        {
            return this.GetAllVehicles().FirstOrDefault(x => string.Equals(x.VehicleNumber, vehicleNumber)) != null;
        }
    }
}
