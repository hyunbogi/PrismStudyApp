using EmployeeModule.ViewModels;
using System.Windows.Controls;

namespace EmployeeModule.Views
{
    /// <summary>
    /// Interaction logic for EmployeeSummaryView.xaml
    /// </summary>
    public partial class EmployeeSummaryView : UserControl
    {
        public EmployeeSummaryView(EmployeeSummaryViewModel viewModel)
        {
            InitializeComponent();

            DataContext = viewModel;
        }
    }
}
