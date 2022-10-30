using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Soccer_App.Model;
using Soccer_App.ViewModel;

namespace Soccer_App.View
{
    public partial class MyFlyoutPage : FlyoutPage
    {

        public MyFlyoutPage()
        {
            InitializeComponent();
            this.BindingContext = new VM_LeaguesList(Navigation);


            string[] myPageNames = { "Home", "Leagues", "Live", "Settings"};
            string[] IconSource = { "home.png", "football.png", "live.png", "settings.png" };

            //FlyoutItemPage_Model home = new FlyoutItemPage_Model("Home", "home.png");
            //FlyoutItemPage_Model leagues = new FlyoutItemPage_Model("Leagues", "leagues.png");
            //FlyoutItemPage_Model live = new FlyoutItemPage_Model("Live", "live.png");
            //FlyoutItemPage_Model settings = new FlyoutItemPage_Model("Settings", "settings.png");

            //FlyoutItemPage_Model[] myMenu = { home, leagues, live, settings };



            menu.ItemsSource = myPageNames;
            menu.ItemTapped += (sender, e) =>
            {
                ContentPage gotoPage;
                switch (e.Item.ToString())
                {
                    case "Home":
                        gotoPage = new HomePage();
                        break;
                    case "Leagues":
                        gotoPage = new LeaguesPage();
                        break;
                    case "Live":
                        gotoPage = new LivePage();
                        break;
                    default:
                        gotoPage = new SettingsPage();
                        break;
                }
                Detail = new NavigationPage(gotoPage);
                ((ListView)sender).SelectedItem = null;
                this.IsPresented = false;
            };
            Detail = new NavigationPage(new HomePage());
        }
    }
}

