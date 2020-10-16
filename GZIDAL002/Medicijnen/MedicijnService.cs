using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using GZIDAL002.Medicijnen.Models;
using Newtonsoft.Json;
using static GZIDAL002.Config;

namespace GZIDAL002.Medicijnen
{
    public class MedicijnService
    {
        HttpClient _httpClient;

        public MedicijnService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<List<Medicijn>> ZoekMedicijn(string naam)
        {
            var response = await _httpClient.GetAsync($"{API_URL}/medicijn/{naam}");
            var content = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<ZoekMedicijnResponse>(content)
                .Medicijnen;
        }
    }
}
