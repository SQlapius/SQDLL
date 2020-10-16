using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using GZIDAL002.Patienten.Models;
using Newtonsoft.Json;
using static GZIDAL002.Config;

namespace GZIDAL002.Patienten
{
    public class PatientService
    {
        HttpClient _httpClient;

        public PatientService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<List<Patient>> ZoekPatient(int vesId, string sedula)
        {
            var response = await _httpClient.GetAsync($"{API_URL}/patient/{vesId:D4}{sedula}");
            var content = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<ZoekPatientResponse>(content)
                .Patienten;
        }
    }
}
