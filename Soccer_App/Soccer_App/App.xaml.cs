using System;
using Xamarin.Forms;
using Xamarin.Forms.Svg;
using Xamarin.Forms.Xaml;
using Soccer_App.View;
using System.Collections.Generic;
using Soccer_App.Model;
using System.Collections.ObjectModel;

namespace Soccer_App
{
    public partial class App : Application
    {
        static IList<Datum> _leaguesPreferences;
        public static IList<Datum> LeaguesPreferences
        {
            get
            {
                if (_leaguesPreferences == null)
                {
                    _leaguesPreferences = new ObservableCollection<Datum>();
                }
                return _leaguesPreferences;
            }
        }
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
