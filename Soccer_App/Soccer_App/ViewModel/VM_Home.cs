﻿using System;
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
using System.ComponentModel;

namespace Soccer_App.ViewModel
{
    public class VM_Home: BaseViewModel
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
                YoutubeURL = source;

            }

        }
        #endregion

        #region: COMMANDS
        //public ICommand ProcesoAsyncommand => new Command(async () => await ProcesoAsyncrono());
        //public ICommand SearchCommand => new Command(Search(object text));
        #endregion

    }
}

