using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JanusTest.Janus.Data.Session
{
    public class SessionToken
    {
        [JsonProperty("id")]
        public string Id { get; set; }
    }

    public class GetSessionTokenRequest : JanusBaseObject
    {
        [JsonProperty("plugin")]
        public string Plugin { get; set; }
    }

    public class GetSessionTokenResponse : JanusBaseObject<Session>
    {
        [JsonProperty("session_id")]
        public string SessionId { get; set; }
    }
}
