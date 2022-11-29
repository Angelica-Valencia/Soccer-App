using System;
using System.Collections.Generic;
using Soccer_App.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.CommunityToolkit;
using System.Threading.Tasks;
using YoutubeExplode;
using YoutubeExplode.Videos.Streams;
using Soccer_App.Model;

namespace Soccer_App.View
{
    public partial class LeagueDetails : ContentPage
    {
        public LeagueDetails(Datum league)
        {
            InitializeComponent();
            BindingContext = new VM_LeagueDetails(Navigation, league);
        }
    }
}

