using EmployeeModule.ViewModels;
using System.Windows.Controls;

namespace EmployeeModule.Views
{
    /// <summary>
    /// Interaction logic for EmployeeListView.xaml
    /// </summary>
    public partial class EmployeeListView : UserControl
    {
        public EmployeeListView(EmployeeListViewModel viewModel)
        {
            InitializeComponent();

            DataContext = viewModel;
        }
    }
}
