using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using GZIDAL002.Helpers;
using GZIDAL002.Medicijnen.Models;
using GZIDAL002.Patienten.Models;
using GZIDAL002.Recepten.Models;
using Newtonsoft.Json;
using static GZIDAL002.Config;

namespace GZIDAL002.Recepten
{
    public class ReceptService
    {
        APIHelper _api;

        public ReceptService()
        {
            _api = new APIHelper();
        }

        public async Task<Recept> MaakRecept(
            Patient patient,
            Medicijn medicijn,
            int aantal,
            string dosering,
            string medewerker,
            int recId = 0
        )
        {
            var url = $"{API_URL}/zi-v0/receptline";
            var data = new Dictionary<string, dynamic>
            {
                { "vesId", patient.VesId},
                { "patId", patient.PatId },
                { "prKode", medicijn.PRKode },
                { "aantal", aantal },
                { "dosering", dosering },
                { "recId", recId },
            };

            var response = await _api.Post<Root>(url, data);
            var recept = new Recept(
                patient,
                medicijn,
                medewerker,
                aantal,
                dosering
            );

            Debug.WriteLine(JsonConvert.SerializeObject(response));

            return recept;
        }
    }
}
