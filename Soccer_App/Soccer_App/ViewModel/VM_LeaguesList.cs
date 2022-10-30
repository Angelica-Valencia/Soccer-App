using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using Soccer_App.Model;
using Soccer_App.Service;

namespace Soccer_App.ViewModel
{
    internal class VM_LeaguesList : BaseViewModel
  
    {
        #region: VARIABLES
        string _Texto;
        IList<Response> _leaguesList;
        IList<FlyoutItemPage_Model> _myflymenu;
        #endregion

        #region: CONSTRUCTORS
        public VM_LeaguesList(INavigation navigation)
        {
            Navigation = navigation;
            Task.Run(DisplayLeagues);
            GetMenuItems();
        }
        #endregion

        #region: Properties

        public IList<FlyoutItemPage_Model> MyFlyOutMenu
        {
            get
            {
                if (_myflymenu == null)
                    _myflymenu = new ObservableCollection<FlyoutItemPage_Model>();
                return _myflymenu;
            }
            set
            {
                SetValue(ref _myflymenu, value);
                OnPropertyChanged();
            }
        }

        public IList<Response> LeaguesList
        {
            get
            {
                if (_leaguesList == null)
                    _leaguesList = new ObservableCollection<Response>();
                    return _leaguesList; }
            set
            {
                SetValue(ref _leaguesList, value);
                OnPropertyChanged();
            }
        }
        public string Texto
        {
            get { return _Texto; }
            set { SetValue(ref _Texto, value); }
        }
        #endregion

        #region: PROCESSES

        public void  GetMenuItems()
        {
            FlyoutItemPage_Model home = new FlyoutItemPage_Model();
            FlyoutItemPage_Model leagues = new FlyoutItemPage_Model();
            FlyoutItemPage_Model live = new FlyoutItemPage_Model();
            FlyoutItemPage_Model settings = new FlyoutItemPage_Model();

            home.Title = "Home";
            home.IconSource = "home.png";
            MyFlyOutMenu.Add(home);
            leagues.Title = "Leagues";
            leagues.IconSource = "leagues.png";
            MyFlyOutMenu.Add(leagues);
            live.Title = "Live";
            live.IconSource = "live.png";
            MyFlyOutMenu.Add(live);
            settings.Title = "Settings";
            settings.IconSource = "settings.png";
            MyFlyOutMenu.Add(settings);

            FlyoutItemPage_Model[] myMenu = { home, leagues, live, settings };

        }

        public async Task DisplayLeagues()
        {
            try
            {
                var league = await API_Helper.GetLeagues();

                
                for(int i = 0; i < league.response.Length; i++)
                {
                    if (league.response[i].country.name == "World")
                    {
                        if (league.response[i].seasons[0].coverage.standings != true
                            || ((league.response[i].seasons[0].coverage.fixtures.events != true) || (league.response[i].seasons[0].coverage.fixtures.lineups != true)))
                        {
                            continue;
                        }
                        else
                        {
                            LeaguesList.Add(league.response[i]);
                        }
                    }
                    else
                    {
                        if (league.response[i].seasons[0].coverage.players != true || league.response[i].seasons[0].coverage.standings != true
                            || ((league.response[i].seasons[0].coverage.fixtures.events != true) || (league.response[i].seasons[0].coverage.fixtures.lineups != true)))
                        {
                            continue;
                        }
                        else
                        {
                            LeaguesList.Add(league.response[i]);
                        }
                    }
                    
                    
                }
                
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            
        }
        public void ProcesoSimple()
        {

        }
        #endregion

        #region: COMMANDS
        //public ICommand ProcesoAsyncommand => new Command(async () => await ProcesoAsyncrono());
        public ICommand ProcesoSimpcommand => new Command(ProcesoSimple);
        #endregion
    }
}

