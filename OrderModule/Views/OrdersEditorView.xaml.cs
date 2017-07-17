using OrderModule.ViewModels;
using System.Windows.Controls;

namespace OrderModule.Views
{
    /// <summary>
    /// Interaction logic for OrdersEditorView.xaml
    /// </summary>
    public partial class OrdersEditorView : UserControl
    {
        public OrdersEditorView(OrdersEditorViewModel viewModel)
        {
            InitializeComponent();

            DataContext = viewModel;
        }
    }
}
