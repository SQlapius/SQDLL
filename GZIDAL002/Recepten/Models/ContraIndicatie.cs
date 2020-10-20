using System;
using Newtonsoft.Json;

namespace GZIDAL002.Recepten.Models
{
    public class ContraIndicatie
    {
        [JsonProperty("medId")]
        public int MedId { get; set; }

        [JsonProperty("cicode")]
        public int CICode { get; set; }

        [JsonProperty("ciaard_nr")]
        public int CIAardNr { get; set; }

        [JsonProperty("op_Scherm")]
        public int OpScherm { get; set; }

        [JsonProperty("op_lijst")]
        public int OpLijst { get; set; }

        [JsonProperty("bs")]
        public int Bs { get; set; }

        [JsonProperty("b")]
        public string B { get; set; }

        [JsonProperty("ciaard")]
        public string CIAard { get; set; }
    }
}
