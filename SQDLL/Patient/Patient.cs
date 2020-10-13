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

        //public List<MedicineModel> Medikatie { get; set; }

        public DateTime DOB { get; set; }

        public Patient(int vesid, int patid)
        {
            _patientService = new PatientService();
            VES_ID = vesid;
            PAT_ID = patid;

        }

        async public Task<Patient> GetInfo()
        {
            try
            {
                var Response = await _patientService.GetPatient(VES_ID, PAT_ID);
                var count = Response.patient.Count;
              
                if (!(Response.patient.Count < 1))
                {
                    return Response.patient[0];
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
