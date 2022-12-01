using System;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using Soccer_App.Model;
using System.Collections.Generic;
using System.Linq;

namespace Soccer_App.Service
{
	public class API_Helper_Settings
	{
        public const string Soccer_API_Key = "b106e7b2e3mshbff5a41453543e0p11afc9jsn46b94d7dc34f";
        public const string API_URL = "https://sportscore1.p.rapidapi.com/sports/1/leagues";

        public static async Task<List<Datum>> GetMedia()
        {
            List<Datum> responseList = new List<Datum>();
            Rootobject2 pageResponse2 = new Rootobject2();
            HttpClient ApiClient = new HttpClient();
            int pageNumber = 1;
            bool isLastPage = true;
            DateTime currentTime = DateTime.Now;

            while (isLastPage)
            {
                var request = new HttpRequestMessage(HttpMethod.Get, $"https://sportscore1.p.rapidapi.com/sports/1/leagues?page={pageNumber}");

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


                pageNumber += 1;

                if (pageNumber == pageResponse2.meta.last_page)
                {
                    isLastPage = false;
                }

            }



            return responseList;
        }
    }
}

