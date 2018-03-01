using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace XamarinDemo.Services
{
    public class LoginService
    {
        private static readonly string LoginUrl = "https://reqres.in/api/login";
        public async Task<string> Login(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentNullException();
            }

            using (HttpClient client = new HttpClient())
            {
                var content = JsonConvert.SerializeObject(new { email = username,  password });
                HttpResponseMessage response = await client.PostAsync(LoginUrl,
                    new StringContent(content, Encoding.UTF8, "application/json"));
                string responseMessage = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    string accessToken = JObject.Parse(responseMessage)["token"].ToString();
                    return accessToken;
                }

                throw new ArgumentException(responseMessage);
            }
        }
    }
}
