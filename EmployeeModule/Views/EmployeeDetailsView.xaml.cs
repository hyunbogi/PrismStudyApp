using EmployeeModule.Models;
using EmployeeModule.ViewModels;
using Prism.Regions;
using System.Windows.Controls;

namespace EmployeeModule.Views
{
    /// <summary>
    /// Interaction logic for EmployeeDetailsView.xaml
    /// </summary>
    public partial class EmployeeDetailsView : UserControl
    {
        public EmployeeDetailsView(EmployeeDetailsViewModel employeeDetailsViewModel)
        {
            InitializeComponent();

            DataContext = employeeDetailsViewModel;

            RegionContext.GetObservableContext(this).PropertyChanged += (sender, e) =>
                employeeDetailsViewModel.CurrentEmployee = RegionContext.GetObservableContext(this).Value as Employee;
        }
    }
}
