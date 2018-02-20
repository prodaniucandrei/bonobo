using bonobo.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace bonobo.Data
{
    public class RestService
    {
        HttpClient client;
        string GrantType = "password";

        public RestService()
        {
            client = new HttpClient();
            client.MaxResponseContentBufferSize = 256000;
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded' "));
        }

        public async Task<Token> Login(User user)
        {
            var PostData = new List<KeyValuePair<string, string>>();
            PostData.Add(new KeyValuePair<string, string>("GrantType", GrantType));
            PostData.Add(new KeyValuePair<string, string>("username", user.Username));
            PostData.Add(new KeyValuePair<string, string>("username", user.Password));
            var content = new FormUrlEncodedContent(PostData);
            var response = await PostResponseLogIn<Token>(Constants.LoginURL, content);
            DateTime dt = new DateTime();
            dt = DateTime.Today;
            response.ExpireDate = dt.AddSeconds(response.ExpireIn);
            return response;
        }

        public async Task<T> PostResponseLogIn<T>(string weburl, FormUrlEncodedContent content) where T : class
        {
            var response = await client.PostAsync(weburl, content);
            var jsonResult = response.Content.ReadAsStringAsync().Result;
            var token = JsonConvert.DeserializeObject<T>(jsonResult);
            return token;
        }

        public async Task<T> PostResponse<T>(string weburl, string jsonstring) where T : class
        {
            var Token = App.TokenDatabase.GetToken();
            string ContentType = "application/json";
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Token.AccessToken);
            try
            {
                var Result = await client.PostAsync(weburl, new StringContent(jsonstring, Encoding.UTF8, ContentType));
                if (Result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var JsonResult = Result.Content.ReadAsStringAsync().Result;
                    try
                    {
                        var ContentResp = JsonConvert.DeserializeObject<T>(JsonResult);
                        return ContentResp;
                    }
                    catch { return null; }
                }
            }
            catch { return null; }
            return null;
        }

        public async Task<T> GetResponse<T>(string weburl) where T : class
        {
            var Token = App.TokenDatabase.GetToken();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Token.AccessToken);
            try
            {
                var Response = await client.GetAsync(weburl);
                if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var JsonResult = Response.Content.ReadAsStringAsync().Result;
                    Debug.WriteLine("JsonResult: " + JsonResult);
                    try
                    {
                        var ContentResp = JsonConvert.DeserializeObject<T>(JsonResult);
                        return ContentResp;
                    }
                    catch { return null; }
                }
            }
            catch { return null; }
            return null;
        }
    }
}
