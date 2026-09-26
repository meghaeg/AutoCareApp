using AutoCareApp.ApplicationLayer.Interface;
using AutoCareApp.Domain.Enums;
using AutoCareApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoCareApp.ApplicationLayer.Service
{
    public class MaintainanceService
    {
        private readonly IMaintainanceRepository _maintainanceRepository;

        private readonly IVehiclerepository _vehicleRepository;

        private List<Maintainance> _pendingServices = new List<Maintainance>();

        private readonly Object _pendingServiceLock = new Object();

        public event Action<Maintainance> OnCompleted;

        public MaintainanceService(IMaintainanceRepository maintainanceRepository, IVehiclerepository vehiclerepository)
        {
            this._maintainanceRepository = maintainanceRepository;
            this._vehicleRepository = vehiclerepository;
        }

        public void InitializePendingServices()
        {
            lock (_pendingServiceLock)
            {
                this._pendingServices = this._maintainanceRepository.FetchAllServices().Where(x => x.ServiceCurrentStatus == ServiceStatus.Confirmed || x.ServiceCurrentStatus == ServiceStatus.InProgress).ToList();
            }
        }

        public async Task DoService()
        {
            while (true)
            {
                List<Maintainance> services;
                lock (this._pendingServiceLock)
                {
                    services = this._pendingServices.ToList();
                }

                DateTime now = DateTime.Now;
                foreach (var serviceOrder in services)
                {
                    DateTime endTime = serviceOrder.ServiceDate.Value.Date + serviceOrder.ServiceEndTime.Value;
                    DateTime startTime = serviceOrder.ServiceDate.Value.Date + serviceOrder.ServiceStartTime.Value;
                    ServiceStatus oldStatus = serviceOrder.ServiceCurrentStatus;
                    if (now >= endTime)
                    {
                        serviceOrder.ServiceCurrentStatus = ServiceStatus.Completed;
                    }
                    else if (now >= startTime)
                    {
                        serviceOrder.ServiceCurrentStatus = ServiceStatus.InProgress;
                    }

                    if (oldStatus != serviceOrder.ServiceCurrentStatus)
                    {
                        this._maintainanceRepository.UpdateService(serviceOrder);
                        if (serviceOrder.ServiceCurrentStatus == ServiceStatus.Completed)
                        {
                            OnCompleted?.Invoke(serviceOrder);
                        }
                    }
                }

                await Task.Delay(1000);
            }
        }

        public Result BookService(Maintainance service)
        {
            if (!this._vehicleRepository.FetchAllVehicles().Any(x => string.Equals(x.VehicleNumber, service.VehicleNumber)))
            {
                return new Result(false, "No Vehicles registered to book service");
            }

            if (service.ServiceStartTime >= service.ServiceEndTime)
            {
                return new Result(false, "Start time should not be ahead of end time");
            }

            var existingServices = this.GetAllMaintainanceService();
            foreach (var existingService in existingServices)
            {
                if (existingService.ServiceDate.Value.Date != service.ServiceDate.Value.Date)
                {
                    continue;
                }

                bool isOverLapping = service.ServiceStartTime < existingService.ServiceEndTime && service.ServiceEndTime > existingService.ServiceStartTime;
                if (isOverLapping)
                {
                    return new Result(false, "Already a service has been booked in this time slot");
                }
            }

            service.ServiceCurrentStatus = ServiceStatus.Confirmed;
            this._maintainanceRepository.AddService(service);
            return new Result(true, "Successfully Slot is booked!");
        }

        public List<Maintainance> GetAllMaintainanceService()
        {
            return this._maintainanceRepository.FetchAllServices().ToList();
        }
    }
}
