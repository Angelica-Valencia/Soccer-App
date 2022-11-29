﻿using System;
using System.Collections.Generic;
using Soccer_App.ViewModel;
using Xamarin.Forms.Xaml;
using Xamarin.CommunityToolkit;
using System.Threading.Tasks;
using YoutubeExplode;
using YoutubeExplode.Videos.Streams;
using Soccer_App.Model;


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
    }
}

