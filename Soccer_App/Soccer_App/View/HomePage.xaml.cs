using System;
using System.Collections.Generic;
using Soccer_App.ViewModel;
using Xamarin.Forms;

namespace Soccer_App.View
{
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
            this.BindingContext = new VM_Home(Navigation);
            this.BindingContext = new VM_LeaguesList(Navigation);

        }
    }
}

