using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Linq;
using Newtonsoft.Json;
using System.Collections;
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
                var data = await _patientService.GetPatientAsync(parameter, vesId);
                var patients = (
                    from DataRow dr in data.Tables[0].Rows
                        select new Patient()
                        {
                            Naam = dr["NAAM"].ToString()
                        }
                ).ToList();

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
