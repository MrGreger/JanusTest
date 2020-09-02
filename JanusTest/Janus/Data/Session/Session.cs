using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JanusTest.Janus.Data.Session
{
    public class Session
    {
        [JsonProperty("id")]
        public string Id { get; set; }
    }

    public class GetSessionResponse : JanusBaseObject<Session>
    {

    }
}
