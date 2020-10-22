using System;
using GZIDAL002.Global;
using Newtonsoft.Json;


namespace GZIDAL002.Recepten.Models
{
    public class ContraIndicatie : BaseViewModel
    {
        [JsonProperty("medId")]
        public int MedId { get; set; }

        [JsonProperty("prKode")]
        public int PRKode { get; set; }

        [JsonProperty("medNaam")]
        public string MedNaam { get; set; }

        [JsonProperty("inaard")]
        public int InAard { get; set; }

        [JsonProperty("aard")]
        public string Aard { get; set; }

        [JsonProperty("pcaId")]
        public int PcaId { get; set; }

        [JsonProperty("ciCode")]
        public int CICode { get; set; }

        [JsonProperty("pcaFlag")]
        public int PcaFlag { get; set; }

        private string _patCIAardActie = "O";
        public string PatCIAardActie
        {
            get => _patCIAardActie;
            set
            {
                _patCIAardActie = value;
                OnPropertyChanged();
            }
        }
    }

    public class CAardPatientActieFlag
    {
        public const string Bewaken = "B";
        public const string Onderdrukken = "H";
        public const string BewakingStoppen = "S";
    }
}
