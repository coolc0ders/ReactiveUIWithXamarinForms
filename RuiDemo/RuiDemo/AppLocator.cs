using RuiDemo.Services;
using RuiDemo.ViewModels;
using Splat;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace RuiDemo
{
    public class AppLocator
    {
        public static NewItemsViewModel NewItemsViewModel => Locator.Current.GetService<NewItemsViewModel>();
        public static ItemsViewModel ItemsViewModel => Locator.Current.GetService<ItemsViewModel>();

        static public void Init()
        {
            Locator.CurrentMutable.Register(() => new ItemsViewModel());
            Locator.CurrentMutable.Register(() => new NewItemsViewModel());

            //registered without splat... skipped this step for simplicity.
            DependencyService.Register<MockDataStore>();
        }
    }
}
