using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Linq;
using System.Data;

namespace GZIDAL001.Patient
{
    public class Patient
    {
        public string Naam { get; set; }

        private PatientService _patientService = new PatientService();


        public Patient()
        {
        }

        public async Task<List<Patient>> FindPatientAsync(string parameter, int vesId)
        {
            try
            { 
                var data = await _patientService.GetUserAsync(parameter, vesId);

                if (data == null) return new List<Patient>();

                List<Patient> patients = new List<Patient>();

                foreach (DataRow item in data.Tables[0].Rows)
                {
                    Patient patient = new Patient
                    {
                        Naam = item["NAAM"].ToString()
                    };

                    patients.Add(patient);
                }

                return patients;

            }
            catch(Exception e)
            {
                Debug.WriteLine("Something went wrong", e);
                return default;
            }
        }
    }
}
