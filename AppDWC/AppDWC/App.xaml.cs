using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppDWC
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
        }

        public static bool IsUserLoggedIn = false;
        private DateTime? SleepTime = null;

        protected override void OnSleep()
        {
            SleepTime = DateTime.Now;
        }

        protected override void OnResume()
        {
            if (SleepTime != null)
            {
                if (DateTime.Now.Subtract((DateTime)SleepTime) > TimeSpan.FromSeconds(60))
                {
                    LogOutUser();
                }
            }
        }

        public static void LogOutUser()
        {
            if (!IsUserLoggedIn)
                return;
            Application.Current.MainPage = new MainPage();
        }

    }
}
