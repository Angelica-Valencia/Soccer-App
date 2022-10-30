using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Soccer_App.ViewModel;
namespace Soccer_App.View
{
    public partial class LeaguesPage : ContentPage
    {
        public LeaguesPage()
        {
            InitializeComponent();
            this.BindingContext = new VM_LeaguesList(Navigation);
        }
    }
}

