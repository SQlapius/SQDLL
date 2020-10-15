using System;
using Newtonsoft.Json;

namespace GZIDAL002.Patient.Models
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
}
