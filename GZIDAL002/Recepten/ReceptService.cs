using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using GZIDAL002.Global.Models;
using GZIDAL002.Helpers;
using GZIDAL002.Medicijnen.Models;
using GZIDAL002.Patienten.Models;
using GZIDAL002.Recepten.Models;
using Newtonsoft.Json;
using System.Linq;
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
                    Id = recept.ReceptRegels.Count() + 1,
                    ContraIndicaties = regel.ContraIndicaties ?? new List<ContraIndicatie>(),
                    Interacties = regel.Interacties ?? new List<Interactie>(),
                    OngewensteMiddelen = regel.OngewensteMiddelen ?? new List<OngewensteMiddel>()
                }); ;
            }

            return recept;
        }

        public async Task<Recept> AddBestaandeMedicatieToRecept(Recept recept, List<Medicatie> medicaties)
        {
            try
            {
                var medIds = medicaties
                    .Select(x => x.MedId)
                    .ToList();

                var body = new Dictionary<string, dynamic>
                {
                    { "recId", recept.RecId },
                    { "patId", recept.Patient.PatId },
                    { "vesId", recept.Patient.VesId },
                    { "medIds", medIds },
                };

                var url = $"{API_URL}/zi-v0/herhaalmed";
                var response = await _api.Post<MakeHerhaalReceptResponse>(url, body);
                var resRecept = response.Recept[0].Regels[0];

                if (response.Recept[0].Status[0].StatusCode >= 0)
                {
                    recept.RecId = resRecept.RecId;
                    recept.Id = resRecept.Id;

                    for (int i = 0; i < response.Recept[0].Regels.Count(); i++)
                    {
                        var receptRegel = response.Recept[0].Regels[i];
                        var medicijn = response.Recept[0].Regels[i].Medicijn[0];
                        var medicatie = medicaties
                            .Where(x => x.MedId == receptRegel.PMeId)
                            .FirstOrDefault();

                        recept.AddRegel(new ReceptRegel()
                        {
                            Medicijn = medicijn,
                            Aantal = 2,
                            Dosering = medicatie?.Dosering,
                            ContraIndicaties = receptRegel.ContraIndicaties ?? new List<ContraIndicatie>(),
                            Interacties = receptRegel.Interacties ?? new List<Interactie>(),
                            OngewensteMiddelen = receptRegel.OngewensteMiddelen ?? new List<OngewensteMiddel>()
                        });
                    }
                }

                return recept;
            }
            catch(Exception e)
            {
                throw new Exception(e.ToString());
            }
        }

        public async Task<List<LOV>> GetAllCI()
        {
            try
            {
                var url = $"{API_URL}/sqz-v2/lov";
                var body = new Dictionary<string, dynamic>
                {
                    { "soort", 1 }
                };

                var response = await _api.Post<LOVResponse>(url, body);

                return response.LOVs;
            }
            catch
            {
                return default;
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

                return response.Infos[0].Info ?? "No Info Found";
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
                    { "recId", recept.Id },
                };
                var response = await _api.Post<Recept>(url, body);

                return recept;

            }
            catch
            {
                return null;
            }
        }

        public async Task<DoseringTabellen> GetDoseringTables()
        {
            try
            {
                var body = new Dictionary<string, dynamic>
                {
                    { "recId", "lol" },
                };
                var url = $"{API_URL}/zi-v0/doseringlov";
                var response = await _api.Post<GetDoseringResponse>(url, body);

                return response.DoseringTabellen[0];
            }
            catch
            {
                return default;
            }
        }
    }
}
