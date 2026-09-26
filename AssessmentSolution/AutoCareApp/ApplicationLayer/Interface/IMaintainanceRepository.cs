using AutoCareApp.Domain.Model;
using System.Collections.Generic;

namespace AutoCareApp.ApplicationLayer.Interface
{
    public interface IMaintainanceRepository
    {
        void AddService(Maintainance service);

        IEnumerable<Maintainance> FetchAllServices();

        void UpdateService(Maintainance serviceOrder);
    }
}
