using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TravelRecordApp
{
    public partial class App : Application
    {
        public static string DatabaseLocation = string.Empty;

        /*
        public App()
        {
            InitializeComponent();
            
            // create a navigation page
            MainPage = new NavigationPage(new MainPage());
        }
        */   

        public App(string databaseLocation)
        {
            InitializeComponent();

            // Material
            XF.Material.Forms.Material.Init(this);


            // create a navigation page
            MainPage = new NavigationPage(new MainPage());

            // sqlite db instance
            DatabaseLocation = databaseLocation;
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
