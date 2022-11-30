﻿using System;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using Soccer_App.Model;
using System.Collections.Generic;
using System.Linq;
using Soccer_App.ViewModel;
using Xamarin.Forms;

namespace Soccer_App.Service
{
    public class API_Helper_Home
    {
        public const string Soccer_API_Key = "b106e7b2e3mshbff5a41453543e0p11afc9jsn46b94d7dc34f";
        public const string API_URL = "https://sportscore1.p.rapidapi.com/sports/1/leagues?page=1";

        public static INavigation Navigation { get; private set; }

        public static async Task<List<DatumSeason>> GetSeason(IList<Datum> leagueSettings)
        {
            //VM_Settings mySettings = new VM_Settings(Navigation);
            //List<Datum> leaguesSetttings = new List<Datum>();
            List<int> favLeagues = new List<int>();

            if (leagueSettings.Count == 0)
            {
                favLeagues.Add(251);
                favLeagues.Add(317);
                favLeagues.Add(592);
                favLeagues.Add(498);
                favLeagues.Add(512);
                favLeagues.Add(452);
            }
            else
            {
                foreach(Datum league in leagueSettings)
                {
                    favLeagues.Add(league.id ?? default(int));
                }
                
            }
            
            
            List<DatumSeason> seasonList = new List<DatumSeason>();
            Root seasonRespond = new Root();
            HttpClient ApiClient = new HttpClient();
            DateTime currentTime = DateTime.Now;

            for (int i = 0; i < favLeagues.Count; i++)
            {

                var request = new HttpRequestMessage(HttpMethod.Get, $"https://sportscore1.p.rapidapi.com/leagues/{favLeagues[i]}/seasons?page=1");

                request.Headers.Add("X-RapidAPI-Key", Soccer_API_Key);
                request.Headers.Add("X-RapidAPI-Host", "sportscore1.p.rapidapi.com");

                HttpResponseMessage response = await ApiClient.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    seasonRespond = JsonConvert.DeserializeObject<Root>(content);
                }

                foreach (DatumSeason item in seasonRespond.data)
                {
                    if (item.year_end >= currentTime.Year)
                    {
                        seasonList.Add(item);
                    }
                }
            }

            return seasonList;

        }


        public static async Task<List<DatumEvent>> GetEventId(IList<Datum> leagueSettings)
        {
            List<DatumEvent> eventList = new List<DatumEvent>();
            RootEvent eventRespond = new RootEvent();
            HttpClient ApiClient = new HttpClient();
            DateTime currentTime = DateTime.Now;

            List<DatumSeason> mySeaons = await API_Helper_Home.GetSeason(leagueSettings);

            for (int i = 0; i < mySeaons.Count; i++)
            {

                var request = new HttpRequestMessage(HttpMethod.Get, $"https://sportscore1.p.rapidapi.com/seasons/{mySeaons[i].id}/events");

                request.Headers.Add("X-RapidAPI-Key", Soccer_API_Key);
                request.Headers.Add("X-RapidAPI-Host", "sportscore1.p.rapidapi.com");

                HttpResponseMessage response = await ApiClient.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    eventRespond = JsonConvert.DeserializeObject<RootEvent>(content);
                }

                foreach (DatumEvent item in eventRespond.data)
                {
                    if (item.status == "finished")
                    {
                        eventList.Add(item);
                    }
                    else
                    {
                        continue;
                    }
                }
            }

            return eventList;
        }

        public static async Task<int?> EventsList(IList<Datum> leagueSettings)
        {
            var events = await GetEventId(leagueSettings);
            events = events.OrderBy(a => a.start_at).ToList();
            int lastEvent = events.Count;
            //return events[lastEvent - 1].id;
            return events[0].id;

        }

        public static async Task<List<DatumMedia>> GetMediaVideo(IList<Datum> leagueSettings)
        {
            List<DatumMedia> mediaVideo = new List<DatumMedia>();
            int? eventId = await API_Helper_Home.EventsList(leagueSettings);
            HttpClient ApiClient = new HttpClient();
            RootMedia mediaRespond = new RootMedia()
;
            var request = new HttpRequestMessage(HttpMethod.Get, $"https://sportscore1.p.rapidapi.com/events/{eventId}/medias");

            request.Headers.Add("X-RapidAPI-Key", Soccer_API_Key);
            request.Headers.Add("X-RapidAPI-Host", "sportscore1.p.rapidapi.com");

            HttpResponseMessage response = await ApiClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                mediaRespond = JsonConvert.DeserializeObject<RootMedia>(content);
            }

            mediaVideo = mediaRespond.data.ToList();
            return mediaVideo;
        }

        public static async Task<List<DatumEvent>> GetIncidentsLive()
        {

            List<DatumEvent> eventListFull = new List<DatumEvent>();
            List<DatumIncidents> incidentsList = new List<DatumIncidents>();
            RootIncidents incidentsRespond = new RootIncidents();
            HttpClient ApiClient2 = new HttpClient();

            List<DatumEvent> events_on_live = await API_Helper_Home.GetEventsLive();
            events_on_live.OrderBy(a => a.id);

            for (int i = 0; i < events_on_live.Count; i++)
            {
                var request = new HttpRequestMessage(HttpMethod.Get, $"https://sportscore1.p.rapidapi.com/events/{events_on_live[i].id}/incidents");

                request.Headers.Add("X-RapidAPI-Key", Soccer_API_Key);
                request.Headers.Add("X-RapidAPI-Host", "sportscore1.p.rapidapi.com");

                HttpResponseMessage response = await ApiClient2.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    incidentsRespond = JsonConvert.DeserializeObject<RootIncidents>(content);
                }

                foreach (DatumIncidents indident in incidentsRespond.data)
                {
                    incidentsList.Add(indident);
                }

                incidentsList.OrderBy(a => a.event_id);
                events_on_live[i].dataIncidents = incidentsList.ToArray();

                //foreach (DatumIncidents item2 in incidentsList)
                //{
                //    events_on_live[i].dataIncidents.ToList().Add(item2);
                //}


            }


            return events_on_live;

        }

        public static async Task<List<DatumEvent>> GetEventsLive()
        {

            List<DatumEvent> eventList = new List<DatumEvent>();
            RootEvent eventRespond = new RootEvent();
            HttpClient ApiClient = new HttpClient();


            var request = new HttpRequestMessage(HttpMethod.Get, $"https://sportscore1.p.rapidapi.com/sports/1/events/live?page=1");

            request.Headers.Add("X-RapidAPI-Key", Soccer_API_Key);
            request.Headers.Add("X-RapidAPI-Host", "sportscore1.p.rapidapi.com");

            HttpResponseMessage response = await ApiClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                eventRespond = JsonConvert.DeserializeObject<RootEvent>(content);
            }

            foreach (DatumEvent item in eventRespond.data)
            {
                eventList.Add(item);
            }

            return eventList;

        }

        public static async Task<int> GetPage()
        {
            Rootobject2 pageResponse = new Rootobject2();
            HttpClient ApiClient = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, API_URL);

            request.Headers.Add("X-RapidAPI-Key", Soccer_API_Key);
            request.Headers.Add("X-RapidAPI-Host", "sportscore1.p.rapidapi.com");

            HttpResponseMessage response = await ApiClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                try
                {
                    pageResponse = JsonConvert.DeserializeObject<Rootobject2>(content);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            return pageResponse.meta.last_page ?? default(int);
        }


        public static async Task<List<Datum>> GetMedia()
        {
            List<Datum> responseList = new List<Datum>();
            Rootobject2 pageResponse2 = new Rootobject2();
            HttpClient ApiClient = new HttpClient();
            int pageNumber;
            DateTime currentTime = DateTime.Now;

            pageNumber = await API_Helper_Home.GetPage();

            for (int i = 0; i < pageNumber; i++)
            {

                var request = new HttpRequestMessage(HttpMethod.Get, $"https://sportscore1.p.rapidapi.com/sports/1/leagues?page={i}");

                request.Headers.Add("X-RapidAPI-Key", Soccer_API_Key);
                request.Headers.Add("X-RapidAPI-Host", "sportscore1.p.rapidapi.com");

                HttpResponseMessage response = await ApiClient.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    pageResponse2 = JsonConvert.DeserializeObject<Rootobject2>(content);
                }

                foreach (Datum item in pageResponse2.data)
                {
                    if ((item.start_date == "") || item.end_date == "")
                    {
                        continue;
                    }
                    else
                    {
                        item.start_date = item.start_date.Split(' ')[0];
                        int yearStart = int.Parse(item.start_date.Split('-')[0]);
                        item.end_date = item.end_date.Split(' ')[0];
                        int yearEnd = int.Parse(item.end_date.Split('-')[0]);
                        if ((yearStart >= currentTime.Year) || (yearEnd >= currentTime.Year))
                        {
                            if (item.name_translations.en == "MLS" || item.slug == "world-world-cup")
                            {
                                item.most_count = 10;
                            }

                            if (item.host == null)
                            {
                                if (item.name_translations.en.ToUpper().Contains("CONMEBOL"))
                                {
                                    item.country = "South America";
                                }
                                else if (item.slug.ToUpper().Contains("CONCACAF"))
                                {
                                    item.country = "North and Central America";
                                }
                                else if (item.slug.ToUpper().Contains("UEFA"))
                                {
                                    item.country = "Europe";
                                }
                                else if (item.slug.ToUpper().Contains("CAF "))
                                {
                                    item.country = "Africa";
                                }
                                else if (item.slug.ToUpper().Contains("AFC"))
                                {
                                    item.country = "Asia";
                                }
                                else if (item.slug.ToUpper().Contains("WORLD"))
                                {
                                    item.country = "World";
                                }
                                item.most_count = 10;
                            }
                            if (item.host != null)
                            {
                                item.country = item.host.country;
                            }

                            if (item.most_count == null || item.most_count < 10)
                            {

                                continue;

                            }

                            else if (((item.facts[0].name == "Division level") && (item.facts[0].value == "1")) || (item.slug.Contains("world")
                                || item.slug.Contains("conmebol") || item.slug.Contains("concacaf") || item.slug.Contains("uefa") ||
                                item.slug.Contains("caf ")))
                            {
                                responseList.Add(item);
                            }
                            else if (item.slug.Contains("amateur"))
                            {
                                continue;
                            }
                            else if ((item.facts[0].name == "Division level") && (item.facts[0].value != "1"))
                            {
                                continue;
                            }

                            else
                            {
                                responseList.Add(item);
                            }



                        }
                    }
                }


            }



            return responseList;
        }

    }
}

