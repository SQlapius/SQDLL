using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using static GZIDAL002.Config;

namespace GZIDAL002.Patient
{
    public class PatientService
    {
        HttpClient _httpClient;

        public PatientService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<int> ZoekPatient(int vesId, string sedula)
        {
            var response = await _httpClient.GetAsync($"{API_URL}{vesId}{sedula}");

            Debug.WriteLine($"{API_URL}{vesId}{sedula}");

            var content = await response.Content.ReadAsStringAsync();

            //Debug.WriteLine(JsonConvert.SerializeObject(content));

            return 2;
        }
    }
}
