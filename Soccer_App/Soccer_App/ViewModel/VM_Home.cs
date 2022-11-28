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
    public class VM_Home: BaseViewModel
    {
        #region: VARIABLES
        IList<Datum> _mediaList;
        IList<Datum> _mediaBackUp;
        string youtubeURL;
        string videoTitle;
        string videoSubTitle;
        IList<Datum> _leaguesListSeason;
        #endregion

        #region: CONSTRUCTORS
        public VM_Home(INavigation navigation)
        {
            Navigation = navigation;
            Task.Run(DisplayMedia);
            Task.Run(GetLeaguesHome);


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


        #endregion

        #region: PROCESSES


        public async Task DisplayMedia()
        {
            var media = await API_Helper_Home.GetMediaVideo();
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
            var leaguesHome = await API_Helper_Home.GetSeason();
            leaguesHome.OrderBy(a => a.league_id);
            List<int?> league_id = new List<int?>();
            var leagueFull = await API_Helper_Leagues.GetMedia();
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

            LeaguesListSeason = leagueFull.Join(leagues_selection,
                                                 full => full.id,
                                                 season => season,
                                                 (full, season) => new Datum
                                                 {
                                                     id = full.id,
                                                     logo = full.logo,
                                                     name_translations = full.name_translations
                                                 }).ToList();

            

        }
        #endregion

        #region: COMMANDS
        //public ICommand ProcesoAsyncommand => new Command(async () => await ProcesoAsyncrono());
        //public ICommand SearchCommand => new Command(Search(object text));
        #endregion

    }
}

