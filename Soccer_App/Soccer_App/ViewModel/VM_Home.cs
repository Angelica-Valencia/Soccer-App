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
using YoutubeExplode;
using YoutubeExplode.Videos.Streams;
using Xamarin.CommunityToolkit.Core;


namespace Soccer_App.ViewModel
{
    internal class VM_Home: BaseViewModel
    {
        #region: VARIABLES
        IList<Datum> _mediaList;
        IList<Datum> _mediaBackUp;
        string youtubeURL;
        #endregion

        #region: CONSTRUCTORS
        public VM_Home(INavigation navigation)
        {
            Navigation = navigation;
            Task.Run(DisplayMedia);


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
                OnPropertyChanged();
            }
        }


        public IList<Datum> MediaList
        {
            get
            {
                if (_mediaList == null)
                    _mediaList = new ObservableCollection<Datum>();
                return _mediaList;
            }
            set
            {
                SetValue(ref _mediaList, value);
                OnPropertyChanged();
            }
        }

        public IList<Datum> MediaBackUp
        {
            get
            {
                if (_mediaBackUp == null)
                    _mediaBackUp = new ObservableCollection<Datum>();
                return _mediaBackUp;
            }
            set
            {
                SetValue(ref _mediaBackUp, value);
                OnPropertyChanged();
            }
        }

        #endregion

        #region: PROCESSES

        public async Task DisplayMedia()
        {
            var youtube = new YoutubeClient();
            var streamManifest = await youtube.Videos.Streams.GetManifestAsync("https://www.youtube.com/watch?v=PLZo8rCYfuY&ab_channel=LaLigaSantander");
            var streamInfo = streamManifest.GetMuxedStreams().GetWithHighestVideoQuality();

            if (streamInfo != null)
            {
                // Get the actual stream
                var stream = await youtube.Videos.Streams.GetAsync(streamInfo);
                var source = streamInfo.Url;
                youtubeURL = source.Replace("&","&amp;");

            }

        }
        #endregion

        #region: COMMANDS
        //public ICommand ProcesoAsyncommand => new Command(async () => await ProcesoAsyncrono());
        //public ICommand SearchCommand => new Command(Search(object text));
        #endregion

    }
}

