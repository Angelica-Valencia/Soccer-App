using System;
using Xamarin.Forms;
using Xamarin.Forms.Svg;
using Xamarin.Forms.Xaml;
using Soccer_App.View;

namespace Soccer_App
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            SvgImageSource.RegisterAssembly();

            MainPage = new MyNavigationShell();
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
