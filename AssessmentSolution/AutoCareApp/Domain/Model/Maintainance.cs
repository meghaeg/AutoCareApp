using AutoCareApp.Domain.Enums;
using System;

namespace AutoCareApp.Domain.Model
{
    public class Maintainance
    {
        public Maintainance()
        {
        }

        public Maintainance(Guid serviceId, Guid vehicleId, ServiceType serviceType, DateTime serviceDate, TimeSpan startTime, TimeSpan endTime)
        {
            this.ServiceId = serviceId;
            this.VehicleId = vehicleId;
            this.ServiceTypeChosen = serviceType;
            this.ServiceDate = serviceDate;
            this.ServiceStartTime = startTime;
            this.ServiceEndTime = endTime;
        }

        public Guid ServiceId { get; set; }

        public Guid VehicleId { get; set; }

        public ServiceType ServiceTypeChosen { get; set; }

        public DateTime ServiceDate { get; set; }

        public TimeSpan ServiceStartTime { get; set; }

        public TimeSpan ServiceEndTime { get; set; }
    }
}
