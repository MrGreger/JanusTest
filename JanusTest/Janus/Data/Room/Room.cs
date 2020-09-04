using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace JanusTest.Janus.Data.Room
{
    public class RoomPayload
    {
        [JsonProperty("room")]
        public string Room { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("request")]
        public string Request => "create";
        [JsonProperty("permanent")]
        public bool Permanent => false;
        [JsonProperty("is_private")]
        public bool IsPrivate => true;
        [JsonProperty("admin_key")]
        public string AdminKey { get; set; }
        [JsonProperty("allowed", NullValueHandling = NullValueHandling.Ignore)]
        public string[] Allowed { get; set; }
    }

    public class CreateRoomRequest : JanusBaseObject
    {
        [JsonProperty("janus")]
        public override string Janus => "message";

        [JsonProperty("body")]
        public RoomPayload Body { get; set; }

        public CreateRoomRequest(string id, string description, string adminKey, string token)
        {
            Body = new RoomPayload
            {
                AdminKey = adminKey,
                Room = id,
                Description = description,
                Allowed = new string[] { token }
            };
        }
    }

    public class CreateRoomResponse : JanusBaseObject
    {
    }
}
