using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using RuiDemo.Services;
using RuiDemo.Views;

namespace RuiDemo
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();
            AppLocator.Init();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
