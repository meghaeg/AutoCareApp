using AutoCareApp.ApplicationLayer.Interface;
using AutoCareApp.Domain.Model;
using System.Collections.Generic;

namespace AutoCareApp.InfrastructureLayer
{
    public class MaintainanceRepository : IMaintainanceRepository
    {
        private readonly object _fileLock = new object();

        public void AddService(MaintainanceService service)
        {
            lock (_fileLock)
            {
                var listOfService = FileHandlingService.ReadFile<MaintainanceService>(FilePath.MaintainanceFile);
                listOfService.Add(service);
                FileHandlingService.WriteFile(FilePath.MaintainanceFile, listOfService);
            }
        }

        public IEnumerable<MaintainanceService> FetchAllServices()
        {
            lock (_fileLock)
            {
                var listOfService = FileHandlingService.ReadFile<MaintainanceService>(FilePath.MaintainanceFile);
                return listOfService;
            }
        }
    }
}
