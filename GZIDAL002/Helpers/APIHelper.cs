using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GZIDAL002.Helpers
{
    internal class APIHelper
    {
        static HttpClient _httpClient = new HttpClient();

        public async Task<TResponse> Get<TResponse>(string url)
        {
            var response = await _httpClient.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<TResponse>(content);
        }

        public async Task<TResponse> Post<TResponse>(string url, Dictionary<string, dynamic> body, object headers = null)
        {
            var content = new StringContent(JsonConvert.SerializeObject(body));
            var response = await _httpClient.PostAsync(url, content);
            var responseString = await response.Content.ReadAsStringAsync();

            Debug.WriteLine(responseString);

            return JsonConvert.DeserializeObject<TResponse>(responseString);
        }
    }
}
