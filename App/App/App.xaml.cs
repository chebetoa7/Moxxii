﻿using App.Views.Acount;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App
{
    public partial class App : Application
    {
        [Obsolete]
        public App()
        {
            InitializeComponent();

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
