using System;
using System.Net.Http;
using System.Threading.Tasks;
using In.Auth.Identity.Server.Services.Exception;

namespace In.Auth.Identity.Server.Services.OAuth.Facilities
{
    public class LinkedinOAuthFacility : IOAuthFacility
    {
        public async Task<OAuthPayload> GetData(string oAuthToken)
        {
            var client = new HttpClient();

            var userDataUri = $"https://api.linkedin.com/v2/me?oauth2_access_token={oAuthToken}";
            var userEmailUri =
                $"https://api.linkedin.com/v2/emailAddress?q=members&projection=(elements*(handle~))&oauth2_access_token={oAuthToken}";

            var uri = new Uri(userDataUri);
            var response = await client.GetAsync(uri);

            if (!response.IsSuccessStatusCode)
            {
                throw new AuthException("LinkedIn token invalid");
            }

            var content = await response.Content.ReadAsStringAsync();
            var userObj = (Newtonsoft.Json.Linq.JObject) Newtonsoft.Json.JsonConvert.DeserializeObject(content);

            uri = new Uri(userEmailUri);
            response = await client.GetAsync(uri);

            content = await response.Content.ReadAsStringAsync();
            var emailObj =
                (Newtonsoft.Json.Linq.JObject) Newtonsoft.Json.JsonConvert.DeserializeObject(content);

            var id = userObj.GetValue("id").ToString();
            var lastName = userObj.GetValue("lastName").First.First.First.First.ToString();
            var firstName = userObj.GetValue("firstName").First.First.First.First.ToString();
            var email = emailObj.GetValue("elements").First.Last.First.First.First.ToString();

            return new OAuthPayload(email, true,
                firstName,
                lastName, id);
        }
    }
}