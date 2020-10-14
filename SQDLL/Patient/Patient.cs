using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SQDLL.Patient
{
    public class Root
    {
        public List<Patient> patient { get; set; }
    }

    public class Patient
    {
        PatientService _patientService;

        public int VES_ID { get; set; }

        public int PAT_ID { get; set; }

        public string NAAM { get; set; }

        public string SEDULA { get; set; }

        public DateTime DOB { get; set; }

        public Patient(int vesId, int patId)
        {
            _patientService = new PatientService();

            VES_ID = vesId;
            PAT_ID = patId;
        }

        async public Task<Patient> GetInfo()
        {
            try
            {
                var response = await _patientService.GetPatient(VES_ID, PAT_ID);
                var count = response.patient.Count;
              
                if (!(response.patient.Count < 1))
                {
                    return response.patient[0];
                }

                throw new Exception("Geen data gevonden");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
