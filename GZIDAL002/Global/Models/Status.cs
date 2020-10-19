using System;
using Newtonsoft.Json;

namespace GZIDAL002.Global.Models
{
    public class Status
    {
        [JsonProperty("status")]
        public int StatusCode { get; set; }

        [JsonProperty("msgid")]
        public int MsgId { get; set; }

        [JsonProperty("msg")]
        public string Msg { get; set; }
    }
}
