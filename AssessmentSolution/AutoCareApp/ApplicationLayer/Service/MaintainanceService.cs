using AutoCareApp.ApplicationLayer.Interface;
using AutoCareApp.Domain.Model;
using System.Collections.Generic;
using System.Linq;

namespace AutoCareApp.ApplicationLayer.Service
{
    public class MaintainanceService
    {
        private readonly IMaintainanceRepository _maintainanceRepository;

        public MaintainanceService(IMaintainanceRepository maintainanceRepository)
        {
            this._maintainanceRepository = maintainanceRepository;
        }

        public bool BookService(Maintainance service)
        {

        }

        public List<Maintainance> GetAllMaintainanceService()
        {
            return this._maintainanceRepository.FetchAllServices().ToList();
        }
    }
}
