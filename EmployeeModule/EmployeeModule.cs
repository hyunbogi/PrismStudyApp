using EmployeeModule.Controllers;
using EmployeeModule.Services;
using EmployeeModule.Views;
using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Regions;

namespace EmployeeModule
{
    public class EmployeeModule : IModule
    {
        private IUnityContainer _container;
        private IRegionManager _regionManager;

        private MainRegionController _mainRegionController;

        public EmployeeModule(IUnityContainer container, IRegionManager regionManager)
        {
            _container = container;
            _regionManager = regionManager;
        }

        public void Initialize()
        {
            // Register the EmployeeDataService concrete type with the container.
            // Change this to swap in another data service implementation.
            _container.RegisterType<IEmployeeDataService, EmployeeDataService>();

            _regionManager.RegisterViewWithRegion(
                RegionNames.LeftRegion,
                () => _container.Resolve<EmployeeListView>());

            _mainRegionController = _container.Resolve<MainRegionController>();

            _regionManager.RegisterViewWithRegion(
                RegionNames.TabRegion,
                () => _container.Resolve<EmployeeDetailsView>());

            _regionManager.RegisterViewWithRegion(
                RegionNames.TabRegion,
                () => _container.Resolve<EmployeeProjectsView>());
        }
    }
}