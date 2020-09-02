using JanusTest.Janus.Data;
using JanusTest.Janus.Data.Room;
using JanusTest.Janus.Data.Session;
using JanusTest.Janus.Data.UserToken;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace JanusTest.Janus
{
    public class JanusClient
    {
        private HttpClient _httpClient;
        private JanusOptions _janusOptions;

        public JanusClient(HttpClient httpClient, JanusOptions janusOptions)
        {
            _httpClient = httpClient;
            _janusOptions = janusOptions;
        }

        private async Task<GetSessionResponse> GetSessionAsync()
        {
            var request = new JanusBaseObject
            {
                Janus = "create",
                Transaction = Guid.NewGuid().ToString()
            };

            var content = new StringContent(JsonConvert.SerializeObject(request));

            var response = await _httpClient.PostAsync($"{_janusOptions.JanusApiUrl}", content);

            if (!response.IsSuccessStatusCode)
            {
                var errorMessage = await response.Content.ReadAsStringAsync();

                throw new Exception(errorMessage);
            }

            var getSessionResult = JsonConvert.DeserializeObject<GetSessionResponse>(await response.Content.ReadAsStringAsync());

            if (!getSessionResult.IsSucceded())
            {
                throw new Exception(JsonConvert.SerializeObject(getSessionResult.Error));
            }

            return getSessionResult;
        }

        private async Task<GetSessionTokenResponse> GetSessionTokenAsync()
        {
            string sessionId = (await GetSessionAsync()).Data.Id;

            var request = new GetSessionTokenRequest
            {
                Janus = "attach",
                Transaction = Guid.NewGuid().ToString(),
                Plugin = "janus.plugin.audiobridge"
            };

            var content = new StringContent(JsonConvert.SerializeObject(request));

            var response = await _httpClient.PostAsync($"{_janusOptions.JanusApiUrl}/{sessionId}", content);

            var responseContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {

                throw new Exception(responseContent);
            }

            var getSessionTokenResult = JsonConvert.DeserializeObject<GetSessionTokenResponse>(responseContent);

            if (!getSessionTokenResult.IsSucceded())
            {
                throw new Exception(JsonConvert.SerializeObject(getSessionTokenResult.Error));
            }

            return getSessionTokenResult;
        }

        private async Task<string> CreateToken()
        {
            var sessionToken = await GetSessionTokenAsync();

            var request = new CreateUserTokenRequest(_janusOptions.AdminSecret);

            var content = new StringContent(JsonConvert.SerializeObject(request));

            var response = await _httpClient.PostAsync($"{_janusOptions.JanusApiUrl}/{sessionToken.SessionId}/{sessionToken.Data.Id}", content);

            var responseContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(responseContent);
            }

            return request.Token;
        }

        public async Task<CreateRoomResult> CreateRoomAsync(string id, string description)
        {
            var token = await CreateToken();

            var request = new CreateRoomRequest(id, description, _janusOptions.AdminKey, token);

            var sessionToken = await GetSessionTokenAsync();

            var content = new StringContent(JsonConvert.SerializeObject(request));

            var response = await _httpClient.PostAsync($"{_janusOptions.JanusApiUrl}/{sessionToken.SessionId}/{sessionToken.Data.Id}", content);

            var responseContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(responseContent);
            }

            var roomResponse = JsonConvert.DeserializeObject<CreateRoomResponse>(responseContent);

            if (!roomResponse.IsSucceded())
            {
                throw new Exception(JsonConvert.SerializeObject(roomResponse.Error.Reason));
            }

            return new CreateRoomResult
            {
                RoomId = id,
                Token = token
            };
        }
    }
}
