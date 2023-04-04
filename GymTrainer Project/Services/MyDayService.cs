using GymTrainer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace GymTrainer.Services
{
    /// <summary>
    /// This service will handle HTTP requests to retrieve data from the cloud
    /// </summary>
    public class MyDayService
    {
        HttpClient httpClient;
        List<Day> days = new();

        public MyDayService()
        {
            httpClient = new HttpClient();
        }

        public async Task<List<Day>> GetDays()
        {
            if (days?.Count > 0)
                return days;

            var url = "https://gist.githubusercontent.com/Gurvinder-007/f7e653666cbfe64768d6bdca4f999134/raw/214ebc55a0abecccde6799957fe7782164d9fd54/Gym.json";

            var response = await httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                days = await response.Content.ReadFromJsonAsync<List<Day>>();
            }

            return days;
        }
    }
}

