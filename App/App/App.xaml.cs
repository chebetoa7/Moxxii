using App.Views.Acount;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App
{
    public partial class App : Application
    {
        public static bool ActionPush_;
        [Obsolete]
        public App(string configBD, bool ActionPush)
        {
            InitializeComponent();
            
            DB.ConfigRepository.Inicializador(configBD);
            ActionPush_ = ActionPush;

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
