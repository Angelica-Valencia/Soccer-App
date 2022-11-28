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
    public class VM_Live : BaseViewModel
    {
        #region: VARIABLES
       
        IList<DatumEvent> _eventList;
        #endregion

        #region: CONSTRUCTORS
        public VM_Live(INavigation navigation)
        {
            Navigation = navigation;
            Task.Run(GetLiveEvents);


        }
        #endregion


        #region: Properties

        public IList<DatumEvent> EventList
        {
            get
            {
                if (_eventList == null)
                    _eventList = new ObservableCollection<DatumEvent>();
                return _eventList;
            }
            set
            {
                SetValue(ref _eventList, value);
                OnPropertyChanged();
            }
        }


        #endregion

        #region: PROCESSES

        public async Task GetLiveEvents()
        {
            DateTime currentTime = DateTime.Now.ToUniversalTime();

            int year = currentTime.Year;
            int month = currentTime.Month;
            int day = currentTime.Day;

            string fullTime;
            int hour;
            int min;
            int sec;

            DateTime now = DateTime.Now.ToUniversalTime();
            string current;

            var eventsInfo = await API_Helper_Live.GetIncidentsLive();

            

            for(int i = 0; i < eventsInfo.Count;i++)
            {
                    
                fullTime = eventsInfo[i].start_at.Split(' ')[1];
                hour = Int32.Parse(fullTime.Split(':')[0]);
                min = Int32.Parse(fullTime.Split(':')[1]);
                sec = Int32.Parse(fullTime.Split(':')[2]);

                DateTime game_time_start = new DateTime(DateTime.Now.Year,
                                           DateTime.Now.Month,
                                           DateTime.Now.Day, hour, min, sec);

                if (eventsInfo[i].status_more == "1st half")
                {
                    current = (now - game_time_start).ToString();
                    current = current.Split(':')[1];
                    eventsInfo[i].current_time = current;
                }
                else if (eventsInfo[i].status_more == "2nd half")
                {
                    foreach(DatumIncidents item in eventsInfo[i].dataIncidents)
                    {
                        if(item.incident_type== "injuryTime")
                        {
                            game_time_start = new DateTime(DateTime.Now.Year,
                                           DateTime.Now.Month,
                                           DateTime.Now.Day, hour, min + item.length?? default(int) +15, sec);
                            current = (now - game_time_start).ToString();
                            current = current.Split(':')[1];
                            eventsInfo[i].current_time = current;
                        }
                    }
                   
                    
                }
                else
                {
                    current = "";
                    eventsInfo[i].current_time = current;
                }

                EventList.Add(eventsInfo[i]);
                EventList.OrderBy(a => a.priority);
            }
            
             


        }
        #endregion

        #region: COMMANDS
        //public ICommand ProcesoAsyncommand => new Command(async () => await ProcesoAsyncrono());
        //public ICommand SearchCommand => new Command(Search(object text));
        #endregion

    }
}

