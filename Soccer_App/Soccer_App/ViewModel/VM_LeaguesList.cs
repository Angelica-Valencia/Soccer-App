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

namespace Soccer_App.ViewModel
{
    internal class VM_LeaguesList : BaseViewModel
  
    {
        #region: VARIABLES
        IList<Response> _leaguesList;
        IList<Response> _leaguesBackUp;
        IList<Datum> _leaguesList2;
        IList<Datum> _leaguesBackUp2;
        #endregion

        #region: CONSTRUCTORS
        public VM_LeaguesList(INavigation navigation)
        {
            Navigation = navigation;
            Task.Run(DisplayLeagues);
            SearchCommand = new Command<TextChangedEventArgs>(Search);


        }
        #endregion

        #region: Properties

        public Command<TextChangedEventArgs> SearchCommand { get; }

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

        public IList<Response> LeaguesBackUp
        {
            get
            {
                if (_leaguesBackUp == null)
                    _leaguesBackUp = new ObservableCollection<Response>();
                return _leaguesBackUp;
            }
            set
            {
                SetValue(ref _leaguesBackUp, value);
                OnPropertyChanged();
            }
        }
        public IList<Datum> LeaguesList2
        {
            get
            {
                if (_leaguesList2 == null)
                    _leaguesList2 = new ObservableCollection<Datum>();
                return _leaguesList2;
            }
            set
            {
                SetValue(ref _leaguesList2, value);
                OnPropertyChanged();
            }
        }

        public IList<Datum> LeaguesBackUp2
        {
            get
            {
                if (_leaguesBackUp2 == null)
                    _leaguesBackUp2 = new ObservableCollection<Datum>();
                return _leaguesBackUp2;
            }
            set
            {
                SetValue(ref _leaguesBackUp2, value);
                OnPropertyChanged();
            }
        }

        #endregion

        #region: PROCESSES

        public async Task DisplayLeagues()
        {
            try
            {
                var league2 = await API_Helper2.GetMedia();

                //var league = await API_Helper.GetLeagues();
                
                for(int i = 0; i < league2.Count; i++)
                { 

                    if (league2[i].country is null && (league2[i].facts[0].name == "Division level" && league2[i].facts[0].value == "1"))
                    {
                        List<string> split_string = league2[i].slug.Split('-').ToList();

                        string new_name;

                        for (int index = 0; index < split_string.Count; index++)
                        {
                            new_name = split_string[index][0].ToString().ToUpper() + split_string[index].Substring(1);

                            if (league2[i].name_translations.en.Contains(','))
                            {
                                league2[i].name_translations.en = league2[i].name_translations.en.Split(',')[0] + league2[i].name_translations.en.Split(',')[1];
                            }
                            

                            if (league2[i].name_translations.en.Contains(new_name) is false)
                            {
                                league2[i].country = string.Concat(new_name);
                            }
                            else
                            {
                                continue;   
                            }

                        }
                    }
                    if (league2[i].country is null || league2[i].name_translations.en.ToUpper().Contains("FRIENDLY") || league2[i].slug.ToUpper().Contains("U1") || league2[i].slug.ToUpper().Contains("U2"))
                    {
                        continue;
                    }
                    else
                    {
                        LeaguesList2.Add(league2[i]);
                        LeaguesBackUp2.Add(league2[i]);
                    }
                    
                }
                
                //for(int i = 0; i < league.response.Length; i++)
                //{
                //    if (league.response[i].country.name == "World")
                //    {
                //        if (league.response[i].seasons[0].coverage.standings != true
                //            || ((league.response[i].seasons[0].coverage.fixtures.events != true) || (league.response[i].seasons[0].coverage.fixtures.lineups != true)))
                //        {
                //            continue;
                //        }
                //        else
                //        {
                //            LeaguesList.Add(league.response[i]);
                //            LeaguesBackUp.Add(league.response[i]);
                //        }
                //    }
                //    else
                //    {
                //        if (league.response[i].seasons[0].coverage.players != true || league.response[i].seasons[0].coverage.standings != true
                //            || ((league.response[i].seasons[0].coverage.fixtures.events != true) || (league.response[i].seasons[0].coverage.fixtures.lineups != true)))
                //        {
                //            continue;
                //        }
                //        else
                //        {
                //            LeaguesList.Add(league.response[i]);
                //            LeaguesBackUp.Add(league.response[i]);

                //        }
                //    }
                    
                    
                //}
                
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            
        }
        public void Search(TextChangedEventArgs text)
        {
            string word = text.NewTextValue.ToLower();
            LeaguesList2 = LeaguesBackUp2.Where(a => a.name_translations.en.ToLower().Contains(word) || a.country.ToLower().Contains(word)).ToList();


        }
        #endregion

        #region: COMMANDS
        //public ICommand ProcesoAsyncommand => new Command(async () => await ProcesoAsyncrono());
        //public ICommand SearchCommand => new Command(Search(object text));
        #endregion
    }
}

