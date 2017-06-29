using EmployeeModule.Models;
using EmployeeModule.ViewModels;
using Prism.Regions;
using System.Windows.Controls;

namespace EmployeeModule.Views
{
    /// <summary>
    /// Interaction logic for EmployeeProjectsView.xaml
    /// </summary>
    public partial class EmployeeProjectsView : UserControl
    {
        public EmployeeProjectsView(EmployeeProjectsViewModel employProjectsViewModel)
        {
            InitializeComponent();

            DataContext = employProjectsViewModel;

            RegionContext.GetObservableContext(this).PropertyChanged += (sender, e) =>
                employProjectsViewModel.CurrentEmployee = RegionContext.GetObservableContext(this).Value as Employee;
        }
    }
}
