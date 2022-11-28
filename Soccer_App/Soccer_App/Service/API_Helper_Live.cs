using System;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using Soccer_App.Model;
using System.Collections.Generic;
using System.Linq;
using AngleSharp.Dom;

namespace Soccer_App.Service
{
	public class API_Helper_Live
	{
        public const string Soccer_API_Key = "b106e7b2e3mshbff5a41453543e0p11afc9jsn46b94d7dc34f";


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

        public static async Task<List<DatumEvent>> GetIncidentsLive()
        {

            List<DatumEvent> eventListFull = new List<DatumEvent>();
            List<DatumIncidents> incidentsList = new List<DatumIncidents>();
            RootIncidents incidentsRespond = new RootIncidents();
            HttpClient ApiClient = new HttpClient();

            List<DatumEvent> events_on_live = await API_Helper_Live.GetEventsLive();
            events_on_live.OrderBy(a => a.id);

            for (int i = 0; i < events_on_live.Count; i++)
            {
                var request = new HttpRequestMessage(HttpMethod.Get, $"https://sportscore1.p.rapidapi.com/events/{events_on_live[i].id}/incidents");

                request.Headers.Add("X-RapidAPI-Key", Soccer_API_Key);
                request.Headers.Add("X-RapidAPI-Host", "sportscore1.p.rapidapi.com");

                HttpResponseMessage response = await ApiClient.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    incidentsRespond = JsonConvert.DeserializeObject<RootIncidents>(content);
                }

                foreach(  DatumIncidents indident in incidentsRespond.data)
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

        

    }
}

