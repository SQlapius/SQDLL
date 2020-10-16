using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace GZIDAL002.Medicijnen.Models
{
    public class Medicijn
    {
        [JsonProperty("HPKODE")]
        public int HPKode { get; set; }

        [JsonProperty("PRKODE")]
        public int PRKode { get; set; }

        [JsonProperty("GPKODE")]
        public int GPKode { get; set; }

        [JsonProperty("NMMEMO")]
        public string NMMEMO { get; set; }

        [JsonProperty("NMMEMO_050")]
        public string NMMEMO050 { get; set; }

        [JsonProperty("ATCODE")]
        public string ATCode { get; set; }

        [JsonProperty("NMNAAM")]
        public string NMNaam { get; set; }

        [JsonProperty("NMNAAM_050")]
        public string NMNaam050 { get; set; }

        [JsonProperty("NAAM")]
        public string Naam { get; set; }
    }

    public class ZoekMedicijnResponse
    {
        [JsonProperty("medicijn")]
        public List<Medicijn> Medicijnen { get; set; }
    }
}
