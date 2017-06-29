using EmployeeModule.Events;
using EmployeeModule.Models;
using EmployeeModule.Services;
using EmployeeModule.ViewModels;
using EmployeeModule.Views;
using Microsoft.Practices.Unity;
using Prism.Events;
using Prism.Regions;
using System;
using System.Linq;

namespace EmployeeModule.Controllers
{
    public class MainRegionController
    {
        private readonly IUnityContainer _container;
        private readonly IRegionManager _regionManager;
        private readonly IEventAggregator _eventAggregator;
        private readonly IEmployeeDataService _dataService;

        public MainRegionController(
            IUnityContainer container,
            IRegionManager regionManager,
            IEventAggregator eventAggregator,
            IEmployeeDataService dataService)
        {
            _container = container ?? throw new ArgumentNullException(nameof(container));
            _regionManager = regionManager ?? throw new ArgumentNullException(nameof(regionManager));
            _eventAggregator = eventAggregator ?? throw new ArgumentNullException(nameof(eventAggregator));
            _dataService = dataService ?? throw new ArgumentNullException(nameof(dataService));

            _eventAggregator.GetEvent<EmployeeSelectedEvent>().Subscribe(EmployeeSelected, true);
        }

        private void EmployeeSelected(string id)
        {
            if (string.IsNullOrEmpty(id)) { return; }

            Employee selectedEmployee = _dataService.GetEmployees().FirstOrDefault(item => item.Id == id);

            IRegion mainRegion = _regionManager.Regions[RegionNames.MainRegion];
            if (mainRegion == null) { return; }

            EmployeeSummaryView view = mainRegion.GetView(nameof(EmployeeSummaryView)) as EmployeeSummaryView;
            if (view == null)
            {
                view = _container.Resolve<EmployeeSummaryView>();
                mainRegion.Add(view, nameof(EmployeeSummaryView));
            }
            else
            {
                mainRegion.Activate(view);
            }

            if (view.DataContext is EmployeeSummaryViewModel viewModel)
            {
                viewModel.CurrentEmployee = selectedEmployee;
            }
        }
    }
}
