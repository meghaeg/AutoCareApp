using AutoCareApp.Domain.Model;
using System.Collections.Generic;

namespace AutoCareApp.ApplicationLayer.Interface
{
    public interface IMaintainanceRepository
    {
        void AddService(MaintainanceService service);

        IEnumerable<MaintainanceService> FetchAllServices();
    }
}
