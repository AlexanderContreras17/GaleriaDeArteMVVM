using GaleriaArtesMVVM.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GaleriaArtesMVVM
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new ArteView());
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
