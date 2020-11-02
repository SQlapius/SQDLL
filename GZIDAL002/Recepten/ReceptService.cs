using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using GZIDAL002.Global.Models;
using GZIDAL002.Helpers;
using GZIDAL002.Medicijnen.Models;
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

        public async Task<Recept> AddReceptRegel(
            Recept recept,
            Medicijn medicijn,
            int aantal,
            string dosering
        )
        {
            var url = $"{API_URL}/zi-v0/receptline";
            var data = new Dictionary<string, dynamic>
            {
                { "vesId", recept.Patient.VesId},
                { "patId", recept.Patient.PatId },
                { "prKode", medicijn.PRKode },
                { "aantal", aantal },
                { "dosering", dosering },
                { "recId", recept.RecId },
            };

            var response = await _api.Post<AddReceptRegelResponseRoot>(url, data);
            var regel = response.Regel[0];

            if(regel.Status[0].StatusCode >= 0)
            {
                recept.RecId = regel.RecId;
                recept.Id = regel.Id;

                recept.AddRegel(new ReceptRegel()
                {
                    Medicijn = medicijn,
                    Aantal = aantal,
                    Dosering = dosering,
                    ContraIndicaties = regel.ContraIndicaties ?? new List<ContraIndicatie>(),
                    Interacties = regel.Interacties ?? new List<Interactie>(),
                    OngewensteMiddelen = regel.OngewensteMiddelen ?? new List<OngewensteMiddel>()
                });
            }

            return recept;
        }

        public async Task<bool> AddBestaandeMedicatieToRecept(Recept recept, List<int> medIds)
        {
            try
            {
                var body = new Dictionary<string, dynamic>
                {
                    { "recId", recept.RecId },
                    { "patId", recept.Patient.PatId },
                    { "vesId", recept.Patient.VesId },
                    { "medIds", medIds },
                };

                var url = $"{API_URL}/zi-v0/herhaalmed";
                var response = await _api.Post<MakeHerhaalReceptResponse>(url, body);

                Debug.WriteLine(JsonConvert.SerializeObject(response));

                return false;
            }
            catch(Exception e)
            {
                Debug.WriteLine("ok");

                throw new Exception(e.ToString());
            }
        }

        public async Task<string> GetCIInfoTekst(int CICode)
        {
            try
            {
                var url = $"{API_URL}/zi-v0/getcitekst";
                var body = new Dictionary<string, dynamic>
                {
                    { "ciCode", CICode }
                };

                var response = await _api.Post<GetCIInfoTekstResponse>(url, body);

                return response.Infos[0].Info;
            }
            catch
            {
                return "No Info Found";
            }
        }

        public async Task<string> GetAIInfoTekst(int IAKode)
        {
            try
            {
                var url = $"{API_URL}/zi-v0/getiatekst";
                var body = new Dictionary<string, dynamic>
                {
                    { "iaKode", IAKode }
                };

                var response = await _api.Post<GetAIInfoTekstResponse>(url, body);

                return response.Infos[0].Info;
            }
            catch
            {
                return "No Info Found";
            }
        }

        public async Task<Recept> SaveRecept(Recept recept)
        {
            try
            {
                var url = $"{API_URL}/zi-v0/receptcommit";
                var body = new Dictionary<string, dynamic>
                {
                    { "recId", recept.Id }
                };
                var response = await _api.Post<Recept>(url, body);

                return recept;

            }
            catch
            {
                return null;
            }
        }
    }
}
