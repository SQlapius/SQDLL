using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SQDLL.Patient
{
    public class PatientService
    {
        HttpClient _httpClient;

        public PatientService()
        {
            _httpClient = new HttpClient();
        }

        async public Task<Root> GetPatient(int vesId, int patId, bool Force = false)
        {
            Debug.WriteLine("hit");
            try
            {
                var data = new Dictionary<string, int?>
                {
                    { "vesId", vesId},
                    { "patId", patId },
                };

                //var test = new HttpClient();
                var response = await
                     _httpClient.PostAsync("https://fatum.sqlapius.net/ords/api/zi-v0/patient",
                     new StringContent(JsonConvert.SerializeObject(data)));
                var responseString = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Root>(responseString);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
                //return "errror";
            }
        }
    }
}
