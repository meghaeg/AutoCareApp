using AutoCareApp.ApplicationLayer.Interface;
using AutoCareApp.Domain.Model;
using System.Collections.Generic;
using System.Linq;

namespace AutoCareApp.InfrastructureLayer
{
    public class MaintainanceRepository : IMaintainanceRepository
    {
        private readonly object _fileLock = new object();

        public void AddService(Maintainance service)
        {
            lock (_fileLock)
            {
                var listOfService = FileHandlingService.ReadFile<Maintainance>(FilePath.MaintainanceFile);
                listOfService.Add(service);
                FileHandlingService.WriteFile(FilePath.MaintainanceFile, listOfService);
            }
        }

        public IEnumerable<Maintainance> FetchAllServices()
        {
            lock (_fileLock)
            {
                var listOfService = FileHandlingService.ReadFile<Maintainance>(FilePath.MaintainanceFile);
                return listOfService;
            }
        }

        public void UpdateService(Maintainance serviceOrder)
        {
            lock (_fileLock)
            {
                var listOfService = FileHandlingService.ReadFile<Maintainance>(FilePath.MaintainanceFile);
                var serviceToUpdate = listOfService.FirstOrDefault(x => x.ServiceId == serviceOrder.ServiceId);
                serviceToUpdate.ServiceCurrentStatus = serviceOrder.ServiceCurrentStatus;
                FileHandlingService.WriteFile(FilePath.MaintainanceFile, listOfService);
            }
        }
    }
}
