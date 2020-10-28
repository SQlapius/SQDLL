using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace GZIDAL002.Patienten.Models
{
    public class Medicatie 
    {
        [JsonProperty("medId")]
        public int MedId { get; set; }

        [JsonProperty("vasteMedicatie")]
        public int VasteMedicatie { get; set; }

        [JsonProperty("passport")]
        public int Passport { get; set; }

        [JsonProperty("naam")]
        public string Naam { get; set; }

        [JsonProperty("prKode")]
        public int PrKode { get; set; }

        [JsonProperty("gpKode")]
        public int GpKode { get; set; }

        [JsonProperty("dosering")]
        public string Dosering { get; set; }

        [JsonProperty("telrec")]
        public int Telrec { get; set; }

        [JsonProperty("lastRecDt")]
        public DateTime LastRecDt { get; set; }

        [JsonProperty("creaDt")]
        public DateTime CreatedDate { get; set; }
    }

    internal class GetPatientMedicationResponse
    {
        [JsonProperty("patmed")]
        public List<Medicatie> Medicatie { get; set; }
    }
}
