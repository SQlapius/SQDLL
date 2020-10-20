using System;
using System.Collections.Generic;
using GZIDAL002.Global.Models;
using GZIDAL002.Medicijnen.Models;
using Newtonsoft.Json;

namespace GZIDAL002.Recepten.Models
{
    public class ReceptRegel
    {
        public Medicijn Medicijn { get; set; }

        public int Aantal { get; set; }

        public string Dosering { get; set; }

        public List<ContraIndicatie> ContraIndicaties { get; set; }

        public List<Interactie> Interacties { get; set; }
    }

    internal class AddReceptRegelResponse
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("vesId")]
        public int VesId { get; set; }

        [JsonProperty("patId")]
        public int PatId { get; set; }

        [JsonProperty("recId")]
        public int RecId { get; set; }

        [JsonProperty("prKode")]
        public int PrKode { get; set; }

        [JsonProperty("CI")]
        public List<ContraIndicatie> ContraIndicaties { get; set; }

        [JsonProperty("IA")]
        public List<Interactie> Interacties { get; set; }

        [JsonProperty("status")]
        public List<Status> Status { get; set; }
    }

    internal class AddReceptRegelResponseRoot
    {
        [JsonProperty("receptregel")]
        public List<AddReceptRegelResponse> Regel { get; set; }
    }
}
