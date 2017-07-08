using Prism.Commands;
using Prism.Mvvm;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace Interaction.ViewModels
{
    public class InvokeCommandActionViewModel : BindableBase
    {
        private string _selectedItemText;

        public InvokeCommandActionViewModel()
        {
            Items = new List<string> { "Item1", "Item2", "Item3", "Item4", "Item5" };
            SelectedCommand = new DelegateCommand<object[]>(OnItemSelected);
        }

        public IList<string> Items { get; private set; }

        public ICommand SelectedCommand { get; private set; }

        public string SelectedItemText
        {
            get => _selectedItemText;
            set => SetProperty(ref _selectedItemText, value);
        }

        private void OnItemSelected(object[] objs)
        {
            if (objs != null && objs.Count() > 0)
            {
                SelectedItemText = objs.FirstOrDefault().ToString();
            }
        }
    }
}
