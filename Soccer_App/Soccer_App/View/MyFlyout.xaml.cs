using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Soccer_App.View
{
    public partial class MyFlyout : FlyoutPage
    {
        public MyFlyout()
        {
            InitializeComponent();
            flyout.listView.ItemSelected += OnSelectedItem;

        }

        private void OnSelectedItem(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as FlyoutItemPage_Model;
            if(item != null)
            {
                Detail = new NavigationPage((Page)Activator.CreateInstance(item.TargetPage));
                flyout.listView.SelectedItem = null;
                IsPresented = false;
            }
        }
    }
}

