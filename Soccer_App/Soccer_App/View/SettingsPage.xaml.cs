using System;
using System.Collections.Generic;
using Soccer_App.ViewModel;
using Xamarin.Forms.Xaml;
using Xamarin.CommunityToolkit;
using System.Threading.Tasks;
using YoutubeExplode;
using YoutubeExplode.Videos.Streams;
using Soccer_App.Model;
using Soccer_App.Themes;


using Xamarin.Forms;

namespace Soccer_App.View
{
    public partial class SettingsPage : ContentPage
    {
        public SettingsPage()
        {
            InitializeComponent();
            BindingContext = new VM_Settings(Navigation);
        }

        void theme_swhitch_Toggled(System.Object sender, Xamarin.Forms.ToggledEventArgs e)
        {
            //bool theme = switched.Value;
            Application.Current.Resources.MergedDictionaries.Clear(); //Clear any theme data
            if (e.Value)
            {
                //dark mode
                Application.Current.Resources.MergedDictionaries.Add(new DarkTheme());
            }
            else
            {
                //light mode
                Application.Current.Resources.MergedDictionaries.Add(new LightTheme());
            }
        }
    }
}

