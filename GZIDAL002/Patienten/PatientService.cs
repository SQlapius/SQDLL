using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using GZIDAL002.Global.Models;
using GZIDAL002.Helpers;
using GZIDAL002.Patienten.Models;
using GZIDAL002.Recepten.Models;
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

        public async Task<List<Patient>> ZoekPatient(int vesId, string zoek, int soort)
        {
            try
            {
                var url = $"{API_URL}/sqz-v2/patient";
                var data = new Dictionary<string, dynamic>()
                {
                    { "vesId", vesId },
                    { "zoek", zoek },
                    { "soort", soort }
                };
                var response = await _api.Post<ZoekPatientResponse>(
                    url,
                    data
                );

                return response.Patienten;
            }
            catch
            {
                return new List<Patient>(); ;
            }
        }

        public async Task<Patient> GetPatientDetailed(int vesId, int patId)
        {
            try 
            { 
                var url = $"{API_URL}/zi-v0/patall";
                var data = new Dictionary<string, dynamic>()
                {
                    { "vesId", vesId },
                    { "patId", patId }
                };
                var response = await _api.Post<DetailedPatientResponse>(
                    url,
                    data
                );

                return response.Patient[0];
            }
            catch(Exception e)
            {
                Debug.WriteLine(e.ToString());
                return default; 
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

        public async Task<bool> GCPatient(Patient patient, bool fullRemove = false)
        {
            try
            {
                var url = $"{API_URL}/zi-v0/gc";
                var data = new Dictionary<string, dynamic>()
                {
                    { "vesId", patient.VesId},
                    { "patId", patient.PatId },
                    { "action", fullRemove ? "DESTROOI" : null } 
                };
                var response = await _api.Post<Status>(
                    url,
                    data
                );

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<Status> SavePatientCIAardFlag(int pcaId, string actie)
        {
            var url = $"{API_URL}/zi-v0/savepcaflag";
            var data = new Dictionary<string, dynamic>()
            {
                { "pcaId", pcaId },
                { "aktie", actie }
            };
            var response = await _api.Post<SavePatientCAardResponse>(
                url,
                data
            );

            return response.Aard[0].Status[0];
        }
    }
}
