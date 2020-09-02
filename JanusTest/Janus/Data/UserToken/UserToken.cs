using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JanusTest.Janus.Data.UserToken
{
    public class CreateUserTokenRequest : JanusBaseObject
    {
        [JsonProperty("janus")]
        public override string Janus => "add_token";
        [JsonProperty("plugins")]
        public string[] Plugins => new string[]
        {
            "janus.plugin.audiobridge"
        };
        [JsonProperty("token")]
        public string Token { get; set; } = Guid.NewGuid().ToString();
        [JsonProperty("admin_secret")]
        public string AdminSecret { get; private set; }

        public CreateUserTokenRequest(string adminSecret)
        {
            AdminSecret = adminSecret;
        }
    }
}
