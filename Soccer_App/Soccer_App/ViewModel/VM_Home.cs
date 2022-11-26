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
    internal class VM_Home: BaseViewModel
    {
        #region: VARIABLES
        IList<Datum> _mediaList;
        IList<Datum> _mediaBackUp;
        #endregion

        #region: CONSTRUCTORS
        public VM_Home(INavigation navigation)
        {
            Navigation = navigation;
            Task.Run(DisplayMedia);


        }
        #endregion

        #region: Properties


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
            //try
            //{
            //    API_Helper2 MyInfo = new API_Helper2();
            //    var media = await MyInfo.GetMedia();

            //    for (int i = 0; i < media.Count; i++)
            //    {
            //        if (media[i].section.name.source.sport_id == 1)
            //        {
            //            MediaList.Add(media.data[i]);
            //        }

            //    }

            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine(e.Message);
            //}

        }
        #endregion

        #region: COMMANDS
        //public ICommand ProcesoAsyncommand => new Command(async () => await ProcesoAsyncrono());
        //public ICommand SearchCommand => new Command(Search(object text));
        #endregion

    }
}

