using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Soccer_App.View
{
    public partial class MyNavigationShell : Shell
    {
        public MyNavigationShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("LeaguesPages", typeof(LeaguesPage));
        }

        public async Task navto()
        {
            await Shell.Current.GoToAsync("LeaguesPage");
        }
    }
}

