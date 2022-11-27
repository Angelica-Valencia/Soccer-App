using System;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using Soccer_App.Model;
using System.Collections.Generic;
using System.Linq;

namespace Soccer_App.Service
{
    public class API_Helper_Home
    {
        public const string Soccer_API_Key = "b106e7b2e3mshbff5a41453543e0p11afc9jsn46b94d7dc34f";
        public const string API_URL = "https://sportscore1.p.rapidapi.com/sports/1/leagues?page=1";


        public static async Task<List<DatumSeason>> GetSeason()
        {
            List<int> favLeagues = new List<int>();
            favLeagues.Add(251);
            favLeagues.Add(317);
            favLeagues.Add(592);
            favLeagues.Add(498);
            favLeagues.Add(512);
            favLeagues.Add(452);
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


        public static async Task<List<DatumEvent>> GetEventId()
        {
            List<DatumEvent> eventList = new List<DatumEvent>();
            RootEvent eventRespond = new RootEvent();
            HttpClient ApiClient = new HttpClient();
            DateTime currentTime = DateTime.Now;

            List<DatumSeason> mySeaons = await API_Helper_Home.GetSeason();

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

        public static async Task<int?> EventsList()
        {
            var events = await GetEventId();
            events = events.OrderBy(a => a.start_at).ToList();
            int lastEvent = events.Count;
            return events[lastEvent - 1].id;

        }

        public static async Task<List<DatumMedia>> GetMediaVideo()
        {
            List<DatumMedia> mediaVideo = new List<DatumMedia>();
            int? eventId = await API_Helper_Home.EventsList();
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

    }
}

