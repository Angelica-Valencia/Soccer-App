using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Soccer_App.ViewModel;
using Xamarin.Forms.Xaml;

namespace Soccer_App.View
{
    public partial class LivePage : ContentPage
    {
        public LivePage()
        {
            InitializeComponent();
            BindingContext = new VM_Live(Navigation);
        }

        //protected async override void OnAppearing()
        //{
        //    await (BindingContext as VM_Live).GetLiveEvents();
        //}
    }
}

