using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace GZIDAL002.Recepten.Models
{
    public class LOV
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("lovId")]
        public int LovId { get; set; }

        [JsonProperty("naam")]
        public string Naam { get; set; }
    }

    public class LOVResponse
    {
        [JsonProperty("lov")]
        public List<LOV> LOVs { get; set; }
    }
}
