using AutoCareApp.ApplicationLayer.Service;

namespace AutoCareApp.PresentationLayer.View
{
    public class ConsoleOperator
    {
        private readonly VehicleService _vehicleService;

        public ConsoleOperator(VehicleService vehicleService)
        {
            this._vehicleService = vehicleService;
        }

        public void Run()
        {
            var vehicleManagementView = new VehicleManagementView(_vehicleService);
            vehicleManagementView.RunDashBoard();
        }
    }
}
