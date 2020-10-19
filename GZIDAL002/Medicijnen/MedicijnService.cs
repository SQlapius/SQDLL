using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using GZIDAL002.Helpers;
using GZIDAL002.Medicijnen.Models;
using Newtonsoft.Json;
using static GZIDAL002.Config;

namespace GZIDAL002.Medicijnen
{
    public class MedicijnService
    {
        APIHelper _api;

        public MedicijnService()
        {
            _api = new APIHelper();
        }

        public async Task<List<Medicijn>> ZoekMedicijn(string naam)
        {
            try
            {
                var url = $"{API_URL}/sqz-v0/medicijn/{naam}";
                var response = await _api.Get<ZoekMedicijnResponse>(url);

                return response.Medicijnen;
            }
            catch
            {
                return new List<Medicijn>();
            }
        }
    }
}
