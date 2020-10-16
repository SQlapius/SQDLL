using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace GZIDAL002.Patienten.Models
{
    public class Patient
    {
        [JsonProperty("sedula")]
        public string Sedula { get; set; }

        [JsonProperty("naam")]
        public string Naam { get; set; }

        [JsonProperty("dob")]
        public DateTime Dob { get; set; }

        [JsonProperty("sex")]
        public string Sex { get; set; }

        [JsonProperty("startDate")]
        public string StartDate { get; set; }

        [JsonProperty("vesId")]
        public int VesId { get; set; }
    }

    public class ZoekPatientResponse
    {
        [JsonProperty("patient")]
        public List<Patient> Patienten { get; set; }
    }
}
