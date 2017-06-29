using EmployeeModule.Events;
using EmployeeModule.Models;
using EmployeeModule.Services;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.ComponentModel;
using System.Windows.Data;

namespace EmployeeModule.ViewModels
{
    public class EmployeeListViewModel : BindableBase
    {
        private readonly IEventAggregator _eventAggregator;

        public ICollectionView Employees { get; private set; }

        public EmployeeListViewModel(IEmployeeDataService dataService, IEventAggregator eventAggregator)
        {
            if (dataService == null) { throw new ArgumentNullException(nameof(dataService)); }
            _eventAggregator = eventAggregator ?? throw new ArgumentNullException(nameof(eventAggregator));

            Employees = new ListCollectionView(dataService.GetEmployees());
            Employees.CurrentChanged += new EventHandler(SelectedEmployeeChanged);
        }

        private void SelectedEmployeeChanged(object sender, EventArgs e)
        {
            if (Employees.CurrentItem is Employee employee)
            {
                _eventAggregator.GetEvent<EmployeeSelectedEvent>().Publish(employee.Id);
            }
        }
    }
}
