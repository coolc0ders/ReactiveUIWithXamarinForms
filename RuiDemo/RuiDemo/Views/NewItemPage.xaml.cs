using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using RuiDemo.Models;
using RuiDemo.ViewModels;

namespace RuiDemo.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class NewItemPage : ContentPage
    {
        NewItemsViewModel _viewModel;


        public NewItemPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = AppLocator.NewItemsViewModel;
        }

        protected override void OnAppearing()
        {
            MessagingCenter.Subscribe<NewItemsViewModel>(this, "PopModal", async (sender) => 
                await Navigation.PopModalAsync());
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            MessagingCenter.Unsubscribe<NewItemsViewModel>(this, "PopModal");
            base.OnDisappearing();
        }
    }
}