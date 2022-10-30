using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Soccer_App.Model;

namespace Soccer_App.Service
{
    public class API_Helper
    {

        public const string Soccer_API_Key = "84deb254b6msh44b574419aabc3bp1fffbfjsn68b61465f010";
        public const string API_URL = "https://api-football-v1.p.rapidapi.com/v3/leagues?season=2022&current=true";


        public static async Task<Rootobject> GetLeagues()
        {
            Rootobject leagues = new Rootobject();
            HttpClient ApiClient = new HttpClient();

            var request = new HttpRequestMessage(HttpMethod.Get, API_URL);

            request.Headers.Add("X-RapidAPI-Key", Soccer_API_Key);
            request.Headers.Add("X-RapidAPI-Host", "api-football-v1.p.rapidapi.com");

            HttpResponseMessage response = await ApiClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                leagues = JsonConvert.DeserializeObject<Rootobject>(content);               
            }

            return leagues;
        }


    }

}
