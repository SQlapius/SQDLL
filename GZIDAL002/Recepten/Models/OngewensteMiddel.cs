using System;
using Newtonsoft.Json;

namespace GZIDAL002.Recepten.Models
{
    public class OngewensteMiddel
    {
        [JsonProperty("medId")]
        public int MedId { get; set; }

        [JsonProperty("prKode")]
        public int PrKode { get; set; }

        [JsonProperty("medNaam")]
        public string MedNaam { get; set; }

        [JsonProperty("pomId")]
        public int PomId { get; set; }

        [JsonProperty("lomId")]
        public int LomId { get; set; }

        [JsonProperty("naam")]
        public string Naam { get; set; }

        [JsonProperty("pomFlag")]
        public int PomFlag { get; set; }
    }
}
