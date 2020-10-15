using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using GZIDAL002.Patient.Models;
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

        public async Task<List<int>> ZoekPatient(int vesId, string sedula)
        {
            var response = await _httpClient.GetAsync($"{API_URL}/patient/0{vesId}{sedula}");
            var content = await response.Content.ReadAsStringAsync();

            return new List<int> {  };
        }
    }
}
