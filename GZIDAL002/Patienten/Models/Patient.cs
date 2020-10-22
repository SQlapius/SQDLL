using System;
using System.Collections.Generic;
using GZIDAL002.Global.Models;
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

        [JsonProperty("patId")]
        public int PatId { get; set; }
    }

    internal class ZoekPatientResponse
    {
        [JsonProperty("patient")]
        public List<Patient> Patienten { get; set; }
    }

    internal class PatCIAardRe
    {
        [JsonProperty("status")]
        public List<Status> Status { get; set; }
    }

    internal class SavePatientCAardResponse
    {
        [JsonProperty("patciAard")]
        public List<PatCIAardRe> Aard { get; set; }
    }
}
