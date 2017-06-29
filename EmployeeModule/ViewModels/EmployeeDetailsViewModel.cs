using EmployeeModule.Models;
using Prism.Mvvm;

namespace EmployeeModule.ViewModels
{
    public class EmployeeDetailsViewModel : BindableBase
    {
        public string ViewName => "Employee Details";

        private Employee _currentEmployee;
        public Employee CurrentEmployee
        {
            get { return _currentEmployee; }
            set { SetProperty(ref _currentEmployee, value); }
        }
    }
}
