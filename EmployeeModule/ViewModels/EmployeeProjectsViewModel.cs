using EmployeeModule.Models;
using EmployeeModule.Services;
using Prism.Mvvm;
using System;
using System.ComponentModel;
using System.Windows.Data;

namespace EmployeeModule.ViewModels
{
    public class EmployeeProjectsViewModel : BindableBase
    {
        public string ViewName => "Employee Projects";

        public ICollectionView Projects { get; private set; }

        private Employee _currentEmployee;
        public Employee CurrentEmployee
        {
            get { return _currentEmployee; }
            set
            {
                _currentEmployee = value;
                if (CurrentEmployee != null)
                {
                    Projects.Filter = obj => ((Project)obj).Id == CurrentEmployee.Id;
                }
                Projects.Refresh();

                RaisePropertyChanged(nameof(CurrentEmployee));
                RaisePropertyChanged(nameof(Projects));
            }
        }

        public EmployeeProjectsViewModel(IEmployeeDataService dataService)
        {
            if (dataService == null) { throw new ArgumentNullException(nameof(dataService)); }

            Projects = new ListCollectionView(dataService.GetProjects());
        }
    }
}
