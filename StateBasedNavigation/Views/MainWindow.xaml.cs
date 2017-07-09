using StateBasedNavigation.Models;
using StateBasedNavigation.ViewModels;
using System.Windows;

namespace StateBasedNavigation.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new ChatViewModel(new ChatService());
        }
    }
}
