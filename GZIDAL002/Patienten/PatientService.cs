using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using GZIDAL002.Helpers;
using GZIDAL002.Patienten.Models;
using Newtonsoft.Json;
using static GZIDAL002.Config;

namespace GZIDAL002.Patienten
{
    public class PatientService
    {
        APIHelper _api;

        public PatientService()
        {
            _api = new APIHelper();
        }

        public async Task<List<Patient>> ZoekPatient(int vesId, string sedula)
        {
            try
            {
                var url = $"{API_URL}/sqz-v0/patient/{vesId:D4}{sedula}";
                var response = await _api.Get<ZoekPatientResponse>(url);

                return response.Patienten;
            }
            catch
            {
                return new List<Patient>(); ;
            }
        }

        public async Task<List<Medicatie>> GetPatientMedicatie(Patient patient)
        {
            try
            {
                var url = $"{API_URL}/zi-v0/patmed";
                var data = new Dictionary<string, dynamic>()
                {
                    { "vesId", patient.VesId},
                    { "patId", patient.PatId },
                };
                var response = await _api.Post<GetPatientMedicationResponse>(
                    url,
                    data
                );

                return response.Medicatie;
            }
            catch
            {
                return new List<Medicatie>();
            }
        }
    }
}
