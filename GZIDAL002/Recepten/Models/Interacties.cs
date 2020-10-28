using System;
using Newtonsoft.Json;

namespace GZIDAL002.Recepten.Models
{
    public class Interactie
    {
        [JsonProperty("piaId")]
        public int PIAId { get; set; }

        [JsonProperty("pmeAId")]
        public int PMedAId { get; set; }

        [JsonProperty("pmeBId")]
        public int PMedBId { get; set; }

        [JsonProperty("iaOms")]
        public string IAOms { get; set; }

        [JsonProperty("flag")]
        public int Flag { get; set; }

        [JsonProperty("prio")]
        public int Prio { get; set; }

        [JsonProperty("iakode")]
        public int IAKode { get; set; }

        [JsonProperty("aantal")]
        public int Aantal { get; set; }

    }
}
