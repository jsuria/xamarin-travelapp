using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TravelRecordApp.Helpers;
using TravelRecordApp.Model;

namespace TravelRecordApp.Logic
{
    public class VenueLogic
    {
        public async static Task<List<Result>> GetVenues(double latitude, double longitude)
        {
            //List<Venue> venues = new List<Venue>();
            List<Result> venues = new List<Result>();

            var url = VenueRoot.GenerateURL(latitude, longitude);           

            // UDEMY METHOD (Lionel https://www.udemy.com/user/lionel-493/)
            /**/
            using (var client = new HttpClient())
            {
                var request = new HttpRequestMessage(HttpMethod.Get, url);

                request.Headers.Add("Accept", "application/json");
                request.Headers.TryAddWithoutValidation("Authorization", Constants.VENUE_API_KEY);

                var response = await client.SendAsync(request, HttpCompletionOption.ResponseContentRead);
                var json = await response.Content.ReadAsStringAsync();

                var venueResultSet = (JsonConvert.DeserializeObject<Venue>(json)).results;

                venues = venueResultSet as List<Result>;
            }/**/

            return venues;
        }
    }
}

