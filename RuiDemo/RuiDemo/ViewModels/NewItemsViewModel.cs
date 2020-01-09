using ReactiveUI;
using RuiDemo.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace RuiDemo.ViewModels
{
    public class NewItemsViewModel : BaseViewModel
    {
        public Item Item { get; set; }
        public ICommand SaveCommand { get; private set; }
        public ICommand CancelCommand { get; private set; }

        public NewItemsViewModel()
        {
            CancelCommand = ReactiveCommand.Create(OnCancelCommand);
            SaveCommand = ReactiveCommand.Create(OnSave);
            Item = new Item
            {
                Text = "Item name",
                Description = "This is an item description."
            };
        }

        public void OnSave()
        {
            MessagingCenter.Send(this, "AddItem", Item);
            MessagingCenter.Send(this, "PopModal");
        }

        public void OnCancelCommand()
        {
            MessagingCenter.Send(this, "PopModal");
        }
    }
}
