using Interaction.ViewModels;
using System.Windows.Controls;

namespace Interaction.Views
{
    /// <summary>
    /// Interaction logic for ItemSelectionView.xaml
    /// </summary>
    public partial class ItemSelectionView : UserControl
    {
        public ItemSelectionView()
        {
            InitializeComponent();
            DataContext = new ItemSelectionViewModel();
        }
    }
}
