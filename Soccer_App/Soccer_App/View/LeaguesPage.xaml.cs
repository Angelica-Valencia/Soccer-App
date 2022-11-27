using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Soccer_App.ViewModel;
using Xamarin.Forms.Xaml;

namespace Soccer_App.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LeaguesPage : ContentPage
    {
        public LeaguesPage()
        {
            InitializeComponent();
            BindingContext = new VM_LeaguesList(Navigation);
            
        }
    }
}
