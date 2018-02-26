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

        public RestService()
        {
            client = new HttpClient();
            //maximum number of bytes to buffer when reading the content in the HTTP response message
            client.MaxResponseContentBufferSize = 256000;
            //set the Timeout property to a larger value to avoid TaskCanceledException
            client.Timeout = TimeSpan.FromMinutes(30);
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<Token> Login(User user)
        {
            var json = JsonConvert.SerializeObject(user);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await PostResponseLogIn<Token>(Constants.LoginURL, content);
            DateTime dt = new DateTime();
            dt = DateTime.Today;
            response.ExpireDate = dt.AddSeconds(response.ExpireIn);
            return response;
        }

        public async Task<T> PostResponseLogIn<T>(string weburl, StringContent content) where T : class
        {
            HttpResponseMessage response = null;
            var uri = new Uri(string.Format(weburl, string.Empty));
            // send a POST request to the web service specified by the URI
            response = await client.PostAsync(uri, content);
            //check if response is successfull
            if (response.IsSuccessStatusCode)
            {
                var jsonResult = response.Content.ReadAsStringAsync().Result;
                var token = JsonConvert.DeserializeObject<T>(jsonResult);
                return token;
            }
            else
            {
                return null;
            }
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
