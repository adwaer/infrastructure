using System;
using System.Net.Http;
using System.Threading.Tasks;
using In.Auth.Identity.Server.Services.Exception;

namespace In.Auth.Identity.Server.Services.OAuth.Facilities
{
    public class FacebookOAuthFacility : IOAuthFacility
    {
        public async Task<OAuthPayload> GetData(string oAuthToken)
        {
            var client = new HttpClient();

            var verifyTokenEndPoint = $"https://graph.facebook.com/me?access_token={oAuthToken}&fields=email,name";

            var uri = new Uri(verifyTokenEndPoint);
            var response = await client.GetAsync(uri);

            if (!response.IsSuccessStatusCode)
            {
                throw new AuthException("Facebook token invalid");
            }

            var content = await response.Content.ReadAsStringAsync();
                var userObj = (Newtonsoft.Json.Linq.JObject) Newtonsoft.Json.JsonConvert.DeserializeObject(content);

                return new OAuthPayload(userObj.Value<string>("email"), true,
                    userObj.Value<string>("name"),
                    null, userObj.Value<string>("id"));

        }
    }
}