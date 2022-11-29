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
    public class VM_Settings : BaseViewModel
    {
        #region VARIABLES
        IList<Datum> _favLeagues;
        IList<Datum> _favLeaguesBackup;

        #endregion
        #region CONSTRUCTOR
        public VM_Settings(INavigation navigation)
        {
            Navigation = navigation;
            Task.Run(GetAllLeagues);
            //SearchFavCommand = new Command<TextChangedEventArgs>(SearchFav);
        }
        #endregion
        #region PROPERTIES
        //public Command<TextChangedEventArgs> SearchFavCommand { get; }
        public IList<Datum> FavLeagues
        {
            get
            {
                if (_favLeagues == null)
                    _favLeagues = new ObservableCollection<Datum>();
                return _favLeagues;
            }
            set
            {
                SetValue(ref _favLeagues, value);
                OnPropertyChanged();
            }
        }
        public IList<Datum> FavLeaguesBackUp
        {
            get
            {
                if (_favLeaguesBackup == null)
                    _favLeaguesBackup = new ObservableCollection<Datum>();
                return _favLeaguesBackup;
            }
            set
            {
                SetValue(ref _favLeaguesBackup, value);
                OnPropertyChanged();
            }
        }
        #endregion
        #region PROCESSES
        public async Task GetAllLeagues()
        {
            var allLeagues = await API_Helper_Leagues.GetMedia();
            for (int i = 0; i < allLeagues.Count; i++)
            {

                if (allLeagues[i].country is null && (allLeagues[i].facts[0].name == "Division level" && allLeagues[i].facts[0].value == "1"))
                {
                    List<string> split_string = allLeagues[i].slug.Split('-').ToList();

                    string new_name;

                    for (int index = 0; index < split_string.Count; index++)
                    {
                        new_name = split_string[index][0].ToString().ToUpper() + split_string[index].Substring(1);

                        if (allLeagues[i].name_translations.en.Contains(','))
                        {
                            allLeagues[i].name_translations.en = allLeagues[i].name_translations.en.Split(',')[0] + allLeagues[i].name_translations.en.Split(',')[1];
                        }


                        if (allLeagues[i].name_translations.en.Contains(new_name) is false)
                        {
                            allLeagues[i].country = string.Concat(new_name);
                        }
                        else
                        {
                            continue;
                        }

                    }
                }
                if (allLeagues[i].country is null || allLeagues[i].name_translations.en.ToUpper().Contains("FRIENDLY") || allLeagues[i].slug.ToUpper().Contains("U1") || allLeagues[i].slug.ToUpper().Contains("U2"))
                {
                    continue;
                }
                else
                {
                    FavLeagues.Add(allLeagues[i]);
                    FavLeaguesBackUp.Add(allLeagues[i]);
                }

            }

        }
        public void SearchFav(TextChangedEventArgs text)
        {
            string word = text.NewTextValue.ToLower();
            FavLeagues = FavLeaguesBackUp.Where(a => a.name_translations.en.ToLower().Contains(word) || a.country.ToLower().Contains(word)).ToList();


        }
        #endregion
        #region COMMANDS
        //public ICommand ProcesoAsyncommand => new Command(async () => await ProcesoAsyncrono());
        public ICommand SearchFavCommand => new Command<TextChangedEventArgs>(SearchFav);
        #endregion
    }
}

