using AutoCareApp.Domain.Enums;
using System;

namespace AutoCareApp.Domain.Model
{
    public class Maintainance
    {
        public Maintainance()
        {
        }

        public Maintainance(Guid serviceId, string vehicleNumber, ServiceType serviceType, ServiceStatus serviceCurrentStatus, DateTime? serviceDate, TimeSpan? startTime, TimeSpan? endTime)
        {
            this.ServiceId = serviceId;
            this.VehicleNumber = vehicleNumber;
            this.ServiceTypeChosen = serviceType;
            this.ServiceCurrentStatus = serviceCurrentStatus;
            this.ServiceDate = serviceDate;
            this.ServiceStartTime = startTime;
            this.ServiceEndTime = endTime;
        }

        public Guid ServiceId { get; set; }

        public string VehicleNumber { get; set; }

        public ServiceType ServiceTypeChosen { get; set; }

        public ServiceStatus ServiceCurrentStatus { get; set; }

        public DateTime? ServiceDate { get; set; }

        public TimeSpan? ServiceStartTime { get; set; }

        public TimeSpan? ServiceEndTime { get; set; }
    }
}
