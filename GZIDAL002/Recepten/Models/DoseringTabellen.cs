using System;
using Newtonsoft.Json;
using System.Collections.Generic;
namespace GZIDAL002.Recepten.Models
{
    public class TijdsEenheden
    {
        [JsonProperty("kode")]
        public string Kode { get; set; }

        [JsonProperty("omschrijving")]
        public string Omschrijving { get; set; }
    }

    public class GebruiksEenheden
    {
        [JsonProperty("kode")]
        public string Kode { get; set; }

        [JsonProperty("omschrijving")]
        public string Omschrijving { get; set; }
    }

    public class AanvullendeTekst
    {
        [JsonProperty("kode")]
        public string Kode { get; set; }

        [JsonProperty("omschrijving")]
        public string Omschrijving { get; set; }
    }

    public class DoseringTabellen
    {
        [JsonProperty("tijdsEenheden")]
        public List<TijdsEenheden> TijdsEenheden { get; set; }

        [JsonProperty("gebruiksEenheden")]
        public List<GebruiksEenheden> GebruiksEenheden { get; set; }

        [JsonProperty("aanvullendeTekst")]
        public List<AanvullendeTekst> AanvullendeTeksten { get; set; }
    }

    public class GetDoseringResponse
    {
        [JsonProperty("dosering")]
        public List<DoseringTabellen> DoseringTabellen { get; set; }
    }

}
