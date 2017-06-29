using EmployeeModule.Models;
using Prism.Mvvm;

namespace EmployeeModule.ViewModels
{
    public class EmployeeSummaryViewModel : BindableBase
    {
        private Employee _currentEmployee;
        public Employee CurrentEmployee
        {
            get { return _currentEmployee; }
            set { SetProperty(ref _currentEmployee, value); }
        }
    }
}
