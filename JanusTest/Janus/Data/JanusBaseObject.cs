using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JanusTest.Janus.Data
{
    public class JanusError
    {
        [JsonProperty("code")]
        public int Code { get; set; }
        [JsonProperty("reason")]
        public string Reason { get; set; }
    }

    public class JanusBaseObject
    {
        [JsonProperty("janus")]
        public virtual string Janus { get; set; }
        [JsonProperty("transaction")]
        public string Transaction { get; set; } = Guid.NewGuid().ToString();
        [JsonProperty("error")]
        public JanusError Error { get; set; }
    }

    public class JanusBaseObject<T> : JanusBaseObject
    {
        [JsonProperty("data")]
        public T Data { get; set; }
    }
}
