using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using static GZIDAL002.Config;

namespace GZIDAL002.Medicijn
{
    public class MedicijnService
    {
        HttpClient _httpClient;

        public MedicijnService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<int> ZoekMedicijn(string naam)
        {
            var response = await _httpClient.GetAsync($"{API_URL}/medicijn/{naam}");
            var content = await response.Content.ReadAsStringAsync();

            return 20;
        }
    }
}
