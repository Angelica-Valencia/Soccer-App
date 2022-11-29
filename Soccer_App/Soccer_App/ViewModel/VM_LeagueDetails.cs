using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using Soccer_App.Model;
using Soccer_App.Service;
using Xamarin.CommunityToolkit;
using System.Linq;
using System.Linq.Expressions;
using YoutubeExplode;
using YoutubeExplode.Videos.Streams;
using Xamarin.CommunityToolkit.Core;
using System.ComponentModel;
namespace Soccer_App.ViewModel
{
	public class VM_LeagueDetails:BaseViewModel
	{
        #region: VARIABLES
        public Datum paraBrhought { get; set; }
        #endregion

        #region: CONSTRUCTORS
        public VM_LeagueDetails(INavigation navigation, Datum paraReceive)
        {
            Navigation = navigation;
            //Task.Run(DisplayMedia);
            paraBrhought = paraReceive;


        }

        #endregion

        #region: Properties

        //public Datum League
        //{
        //    get
        //    {
        //        return league;
        //    }
        //    set
        //    {
        //        SetValue(ref league, value);
        //    }

        //}




        #endregion

        #region: PROCESSES


        //public async Task GotoLeaguedetails(Datum league)
        //{

        //    League = league;
        //}


        #endregion

        #region: COMMANDS
        //public ICommand ProcesoAsyncommand => new Command(async () => await ProcesoAsyncrono());
        //public ICommand SearchCommand => new Command(Search(object text));
        #endregion
    }
}

