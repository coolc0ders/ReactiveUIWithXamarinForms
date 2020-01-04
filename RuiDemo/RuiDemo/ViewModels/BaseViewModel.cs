using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using Xamarin.Forms;

using RuiDemo.Models;
using RuiDemo.Services;
using ReactiveUI;

namespace RuiDemo.ViewModels
{
    public class BaseViewModel : ReactiveObject
    {
        public IDataStore<Item> DataStore => DependencyService.Get<IDataStore<Item>>();

        bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set { this.RaiseAndSetIfChanged(ref isBusy, value); }
        }
        /// <summary>
        /// Observes the busy state of the viewmodel 
        /// Is used to activate or not a command depending on wether 
        /// The view model is busy or not
        /// </summary>
        protected IObservable<bool> NotBusyObservable { get; private set; }

        public BaseViewModel()
        {
            NotBusyObservable = this.WhenAnyValue(vm => vm.IsBusy, isbusy => !isbusy);
        }

        string title = string.Empty;
        public string Title
        {
            get { return title; }
            set { this.RaiseAndSetIfChanged(ref title, value); }
        }
    }
}
