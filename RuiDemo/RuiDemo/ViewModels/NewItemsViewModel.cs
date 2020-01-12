using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using ReactiveUI.Validation.Extensions;
using RuiDemo.Models;
using RuiDemo.Validations;
using RuiDemo.Validations.Rules;
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

        //string _itemTitle;
        //[Reactive]
        //public string ItemTitle {

        //    get => _itemTitle;
        //    set => this.RaiseAndSetIfChanged(ref _itemTitle, value);
        //}
        //private string _itemDescription;
        //[Reactive]
        //public string ItemDescription
        //{
        //    get { return _itemDescription; }
        //    set { this.RaiseAndSetIfChanged(ref _itemDescription, value); }
        //}

        private ValidatableObject<string> _itemTitle;
        [Reactive]
        public ValidatableObject<string> ItemTitle
        {
            get { return _itemTitle; }
            set { this.RaiseAndSetIfChanged(ref _itemTitle, value); }
        }
        private ValidatableObject<string> _itemDescription;
        [Reactive]
        public ValidatableObject<string> ItemDescription
        {
            get { return _itemDescription; }
            set { this.RaiseAndSetIfChanged(ref _itemDescription, value); }
        }

        public NewItemsViewModel()
        {
            //NB: You could do validation with this default reactive ui method, OR Use a better way
            //this.ValidationRule(vm => vm.ItemDescription, desc => !string.IsNullOrEmpty(desc), "Description should not be empty");

            ItemDescription = new ValidatableObject<string>();
            ItemDescription.Validations.Add(new IsNotNullOrEmptyRule("Description should not be empty"));
            ItemTitle = new ValidatableObject<string>();
            ItemTitle.Validations.Add(new IsNotNullOrEmptyRule("Title should not be empty"));

            //Create an observable stating if we can save this item or not.
            var canSaveObservable = this.WhenAnyValue(vm => vm.ItemDescription.Value, vm => vm.ItemTitle.Value, (desc, title) =>
            {
                return ItemDescription.TryValidate(desc) && ItemTitle.TryValidate(title);
            });

            CancelCommand = ReactiveCommand.Create(OnCancelCommand);
            SaveCommand = ReactiveCommand.Create(OnSave, canSaveObservable);
        }

        public void OnSave()
        {
            Item = new Item
            {
                Text = ItemTitle.Value,
                Description = ItemDescription.Value
            };
            MessagingCenter.Send(this, "AddItem", Item);
            MessagingCenter.Send(this, "PopModal");
        }

        public void OnCancelCommand()
        {
            MessagingCenter.Send(this, "PopModal");
        }
    }
}
