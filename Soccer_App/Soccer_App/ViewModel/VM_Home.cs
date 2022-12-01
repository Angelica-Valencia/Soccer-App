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
using Xamarin.Essentials;

namespace Soccer_App.ViewModel
{
    public class VM_Home: BaseViewModel
    {
        #region: VARIABLES
        IList<Datum> _mediaList;
        IList<Datum> _mediaBackUp;
        string youtubeURL;
        string videoTitle;
        string videoSubTitle;
        IList<Datum> _leaguesListSeason;
        IList<Datum> _leaguesSettings;
        DatumEvent _eventSingle;
        IList<DatumEvent> _listIncidents;
        #endregion

        #region: CONSTRUCTORS
        public VM_Home(INavigation navigation)
        {
            Navigation = navigation;
            Task.Run(GetListUser);
            Task.Run(GetLiveSingleEvent);
            

        }

        #endregion


        #region: Properties

        public string YoutubeURL
        {
            get
            {   
                return youtubeURL;
            }
            set
            {
                SetValue(ref youtubeURL, value);
            }
                
         }
        public string VideoTitle
        {
            get
            {
                return videoTitle;
            }
            set
            {
                SetValue(ref videoTitle, value);
            }

        }
        public string VideoSubTitle
        {
            get
            {
                return videoSubTitle;
            }
            set
            {
                SetValue(ref videoSubTitle, value);
            }

        }
        public IList<Datum> LeaguesListSeason
        {
            get
            {
                if (_leaguesListSeason == null)
                    _leaguesListSeason = new ObservableCollection<Datum>();
                return _leaguesListSeason;
            }
            set
            {
                SetValue(ref _leaguesListSeason, value);
                OnPropertyChanged();
            }
        }
        public IList<Datum> LeaguesSettings
        {
            get
            {
                if (_leaguesSettings == null)
                    _leaguesSettings = new ObservableCollection<Datum>();
                return _leaguesSettings;
            }
            set
            {
                SetValue(ref _leaguesSettings, value);
                OnPropertyChanged();
            }
        }

        public DatumEvent EventSingle
        {
            get
            { 
                return _eventSingle;
            }
            set
            {
                SetValue(ref _eventSingle, value);
            }
        }
        public IList<DatumEvent> ListIncidents
        {
            get
            {
                if (_listIncidents == null)
                    _listIncidents = new ObservableCollection<DatumEvent>();
                return _listIncidents;
            }
            set
            {
                SetValue(ref _listIncidents, value);
                OnPropertyChanged();
            }
        }



        #endregion

        #region: PROCESSES


        public async Task DisplayMedia()
        {
            var media = await API_Helper_Home.GetMediaVideo(LeaguesSettings);
            if(media.Count() == 0)
            {
                IList<Datum> defaultLeague = new ObservableCollection<Datum>();
                media = await API_Helper_Home.GetMediaVideo(defaultLeague);
            }
            string videoURL = media[0].url;
            VideoTitle = media[0].title.en;
            VideoSubTitle = media[0].sub_title;
            var youtube = new YoutubeClient();
            var streamManifest = await youtube.Videos.Streams.GetManifestAsync(videoURL);
            var streamInfo = streamManifest.GetMuxedStreams().GetWithHighestVideoQuality();

            if (streamInfo != null)
            {
                // Get the actual stream
                var stream = await youtube.Videos.Streams.GetAsync(streamInfo);
                var source = streamInfo.Url;
                YoutubeURL = source;

            }

        }

        public async Task GetLeaguesHome()
        {
            var leaguesHome = await API_Helper_Home.GetSeason(LeaguesSettings);
            leaguesHome.OrderBy(a => a.league_id);
            List<int?> league_id = new List<int?>();
            var leaguesFull = await API_Helper_Home.GetMedia();
            List<int?> leagues_selection = new List<int?>();

            for (int i = 0; i < leaguesHome.Count; i++)
            {
                league_id.Add(leaguesHome[i].league_id);

                if (i == 0)
                {
                    leagues_selection.Add(leaguesHome[i].league_id);
                }
                else if (league_id[i-1] == leaguesHome[i].league_id)
                {
                    continue;
                }
                else
                {
                    leagues_selection.Add(leaguesHome[i].league_id);    
                }
            }


            for(int i = 0; i < leaguesFull.Count; i++)
            {
                foreach(int? item in leagues_selection)
                {
                    if (leaguesFull[i].id == item)
                    {
                        LeaguesListSeason.Add(leaguesFull[i]);
                    }
                }
                
            }

            await DisplayMedia();
        }

        public async Task GetLiveSingleEvent()
        {
            DateTime currentTime = DateTime.Now.ToUniversalTime();

            int year = currentTime.Year;
            int month = currentTime.Month;
            int day = currentTime.Day;

            string fullTime;
            string fullDate;
            int year_game;
            int month_game;
            int day_game;
            int hour;
            int min;
            int sec;

            DateTime now = DateTime.Now.ToUniversalTime();
            string current;

            ListIncidents = await API_Helper_Home.GetIncidentsLive();



            for (int i = 0; i < ListIncidents.Count; i++)
            {
                fullDate = ListIncidents[i].start_at.Split(' ')[0];
                year_game = Int32.Parse(fullDate.Split('-')[0]);
                month_game = Int32.Parse(fullDate.Split('-')[1]);
                day_game = Int32.Parse(fullDate.Split('-')[2]);
                fullTime = ListIncidents[i].start_at.Split(' ')[1];
                hour = Int32.Parse(fullTime.Split(':')[0]);
                min = Int32.Parse(fullTime.Split(':')[1]);
                sec = Int32.Parse(fullTime.Split(':')[2]);

                DateTime game_time_start = new DateTime(year_game,
                                           month_game,
                                           day_game, hour, min, sec);

                if (ListIncidents[i].status_more == "1st half")
                {
                    current = (now - game_time_start).ToString();
                    current = current.Split(':')[1];
                    ListIncidents[i].current_time = current;
                }
                else if (ListIncidents[i].status_more == "2nd half")
                {
                    foreach (DatumIncidents item in ListIncidents[i].dataIncidents)
                    {
                        if (item.incident_type == "injuryTime")
                        {
                            game_time_start = new DateTime(year_game,
                                           month_game,
                                           day_game, hour, min + item.length ?? default(int) + 15, sec);
                            current = (now - game_time_start).ToString();
                            current = current.Split(':')[1];
                            ListIncidents[i].current_time = current;
                        }
                    }


                }
                else
                {
                    current = "";
                    ListIncidents[i].current_time = current;
                }

                ListIncidents.OrderBy(a => a.section.priority);

                EventSingle = ListIncidents[0];
                

            }
        }

        public async Task GetListUser()
        {
            LeaguesSettings = App.LeaguesPreferences;

            if(LeaguesSettings.Count == 0)
            {
                await GetLeaguesHome();
            }
            else
            {
                LeaguesListSeason = LeaguesSettings;
                await DisplayMedia();
                await GetListUser();
            }
            
            
        }

            #endregion

            #region: COMMANDS
            //public ICommand ProcesoAsyncommand => new Command(async () => await ProcesoAsyncrono());
            //public ICommand SearchCommand => new Command(Search(object text));
            #endregion

    }
}

