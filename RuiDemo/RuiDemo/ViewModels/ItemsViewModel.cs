using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using RuiDemo.Models;
using RuiDemo.Views;
using System.Windows.Input;
using ReactiveUI;
using System.Reactive.Linq;

namespace RuiDemo.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        public ObservableCollection<Item> Items { get; set; }
        public ICommand LoadItemsCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        public ICommand AddNewItemCommand { get; private set; }
        private readonly Interaction<ItemsViewModel, bool> _deleteItem;
        public Interaction<ItemsViewModel, bool> DeleteItemInteraction => _deleteItem;

        public ItemsViewModel()
        {
            Title = "Browse";
            Items = new ObservableCollection<Item>();
            LoadItemsCommand = ReactiveCommand.CreateFromTask(ExecuteLoadItemsCommand, NotBusyObservable);
            DeleteCommand = ReactiveCommand.CreateFromTask<Item>(ExecuteDeleteItem, NotBusyObservable);
            AddNewItemCommand = ReactiveCommand.CreateFromTask(ExecuteAddNewItem);
            _deleteItem = new Interaction<ItemsViewModel, bool>();

            MessagingCenter.Subscribe<NewItemsViewModel, Item>(this, "AddItem", async (obj, item) =>
            {
                var newItem = item as Item;
                Items.Add(newItem);
                await DataStore.AddItemAsync(newItem);
            });
        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await DataStore.GetItemsAsync(true);
                foreach (var item in items)
                {
                    Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        async Task ExecuteDeleteItem(Item item)
        {
            //Call an interaction to confirm item deletion
            var result = await DeleteItemInteraction.Handle(this);
            if (result)
            {
                IsBusy = true;
                await Task.Delay(TimeSpan.FromSeconds(2));
                Items.Remove(item);
            }

            IsBusy = false;
        }

        async Task ExecuteAddNewItem()
        {

        }
    }
}