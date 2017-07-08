using Prism.Interactivity.InteractionRequest;
using System.Collections.Generic;

namespace Interaction.Notifications
{
    public class ItemSelectionNotification : Confirmation
    {
        public ItemSelectionNotification()
        {
            Items = new List<string>();
            SelectedItem = null;
        }

        public ItemSelectionNotification(IEnumerable<string> items)
            : this()
        {
            foreach (string item in items)
            {
                Items.Add(item);
            }
        }

        public IList<string> Items { get; private set; }

        public string SelectedItem { get; set; }
    }
}
