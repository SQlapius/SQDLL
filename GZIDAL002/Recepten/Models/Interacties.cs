using System;
using Newtonsoft.Json;

namespace GZIDAL002.Recepten.Models
{
    public class Interactie
    {
        [JsonProperty("idA")]
        public int IdA { get; set; }

        [JsonProperty("dB")]
        public int DB { get; set; }

        [JsonProperty("Naam_a")]
        public string NaamA { get; set; }

        [JsonProperty("naam_b")]
        public string NaamB { get; set; }

        [JsonProperty("w")]
        public string W { get; set; }

        [JsonProperty("ia")]
        public string IA { get; set; }

        [JsonProperty("av")]
        public string AV { get; set; }

        [JsonProperty("prio")]
        public int Prio { get; set; }

        [JsonProperty("iakode")]
        public int IAKode { get; set; }
    }
}
