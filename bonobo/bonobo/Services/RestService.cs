using bonobo.Dtos;
using bonobo.Models;
using bonobo.ViewModel;
using bonobo.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
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

        /*******************ACCOUNT-API******************************************************/

        //-----------------LOGIN--------------------------------------------------------------
        public async Task<Token> Login(LoginView user)
        {
            var json = JsonConvert.SerializeObject(user);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            Token response = await PostResponseLogIn(Constants.LoginURL, content);
            Debug.WriteLine("RestService: LogIn token = " + response.AccessToken);
            return response;
        }

        public async Task<Token> PostResponseLogIn(string weburl, StringContent content)
        {
            HttpResponseMessage response = null;
            var uri = new Uri(string.Format(weburl, string.Empty));
            // send a POST request to the web service specified by the URI
            response = await client.PostAsync(uri, content);
            //check if response is successfull
            if (response.IsSuccessStatusCode)
            {
                var jsonResult = response.Content.ReadAsStringAsync().Result;
                Debug.WriteLine("RestService: PostResponseLogIn jsonResult = " + jsonResult);
                var msg = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonResult);
                Debug.WriteLine("RestService: PostResponseLogIn token = " + msg["token"]);

                return new Token
                {
                    AccessToken = msg["token"],
                    ExpireDate = DateTime.Today.AddDays(30),
                };
            }
            else
            {
                return null;
            }
        }


        //-----------------LOGOUT-------------------------------------------------------------
        public async Task<bool> Logout()
        {
            var uri = Constants.LogoutURL;
            var response = await client.PostAsync(uri, null);
            if (response.IsSuccessStatusCode)
                return true;
            return false;
        }

        //-----------------REGISTER-----------------------------------------------------------
        public async Task<bool> Register(RegisterView user)
        {
            HttpResponseMessage response = null;
            var uri = new Uri(string.Format(Constants.RegisterURL, string.Empty));
            var json = JsonConvert.SerializeObject(user);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            //send a POST request to the web service specified by the URI
            response = await client.PostAsync(uri, content);

            //check if response is successfull
            if (response.IsSuccessStatusCode) return true;

            return false;
        }

        //-----------------FIND-USER-BY-EMAIL-------------------------------------------------
        public async Task<UserDto> FindUserByEmail(FindUserByEmailViewModel model)
        {
            if (model.Email != null)
            {
                HttpResponseMessage Response = null;
                var Token = App.TokenDatabase.GetToken();
                client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Token.AccessToken);

                var uri = new Uri(string.Format(Constants.FindUserByEmailURL, string.Empty));
                var json = JsonConvert.SerializeObject(model);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                try
                {
                    //send a POST request to the web service specified by the URI
                    Response = await client.PostAsync(uri, content);
                    if (Response.IsSuccessStatusCode)
                    {
                        var JsonResult = Response.Content.ReadAsStringAsync().Result;
                        try
                        {
                            var msg = JsonConvert.DeserializeObject<Dictionary<string, string>>(JsonResult);
                            DateTime bday = DateTime.ParseExact(msg["birthDate"], "yyyy-MM-ddTHH:mm:ss.fff",
                                           CultureInfo.InvariantCulture);
                            UserDto userdto = new UserDto
                            {
                                RemoteId = msg["id"],
                                FirstName = msg["firstName"],
                                LastName = msg["lastName"],
                                Email = msg["email"],
                                BirthDate = bday,
                                Gender = msg["gender"]
                            };
                            return userdto;
                        }
                        catch { return null; }
                    }
                }
                catch { return null; }
                return null;
            }
            else
            {
                return null;
            }
        }

        //-----------------FIND-USER-BY-ID----------------------------------------------------
        public async Task<UserDto> FindUserById(FindUserByIdViewModel model)
        {
            if (model.Id != null)
            {
                HttpResponseMessage Response = null;
                var Token = App.TokenDatabase.GetToken();
                client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Token.AccessToken);

                var uri = new Uri(string.Format(Constants.FindUserByIdURL, string.Empty));
                var json = JsonConvert.SerializeObject(model);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                try
                {
                    //send a POST request to the web service specified by the URI
                    Response = await client.PostAsync(uri, content);
                    if (Response.IsSuccessStatusCode)
                    {
                        var JsonResult = Response.Content.ReadAsStringAsync().Result;
                        try
                        {
                            var msg = JsonConvert.DeserializeObject<Dictionary<string, string>>(JsonResult);
                            DateTime bday = DateTime.ParseExact(msg["birthDate"], "yyyy-MM-ddTHH:mm:ss.fff",
                                           CultureInfo.InvariantCulture);
                            UserDto userdto = new UserDto
                            {
                                RemoteId = msg["id"],
                                FirstName = msg["firstName"],
                                LastName = msg["lastName"],
                                Email = msg["email"],
                                BirthDate = bday,
                                Gender = msg["gender"]
                            };
                            return userdto;
                        }
                        catch { return null; }
                    }
                }
                catch { return null; }
                return null;
            }
            else
            {
                return null;
            }
        }

        /*******************ACTIVITY-API*****************************************************/

        //-----------------GET-ALL-ACTIVITIES-------------------------------------------------
        public async Task<List<ActivityDto>> GetAllActivities()
        {
            HttpResponseMessage Response = null;
            var Token = App.TokenDatabase.GetToken();
            client.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Token.AccessToken);

            var uri = new Uri(string.Format(Constants.GetAllActivitiesURL, string.Empty));

            try
            {
                Response = await client.GetAsync(uri);
                if (Response.IsSuccessStatusCode)
                {

                    var JsonResult = Response.Content.ReadAsStringAsync().Result;
                    try
                    {
                        List<ActivityDto> list = JsonConvert.DeserializeObject<List<ActivityDto>>(JsonResult);
                        foreach (ActivityDto a in list)
                        {
                            if (a.JoinedUsersIds.Count == 0)
                            {
                                a.JoinedUsersIds.Add(a.ActivityHostId);
                            }
                        }

                        return list;
                    }
                    catch { return null; }
                }
            }
            catch { return null; }
            return null;
        }


        //-----------------CREATE-ACTIVITY----------------------------------------------------
        public async Task<string> CreateActivityAsync(CreateActivityViewModel model)
        {
            if (model != null)
            {
                HttpResponseMessage Response = null;
                var Token = App.TokenDatabase.GetToken();
                client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Token.AccessToken);

                var uri = new Uri(string.Format(Constants.CreateActivityURL, string.Empty));
                var json = JsonConvert.SerializeObject(model);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                try
                {
                    //send a POST request to the web service specified by the URI
                    Response = await client.PostAsync(uri, content);
                    if (Response.IsSuccessStatusCode)
                    {
                        var JsonResult = Response.Content.ReadAsStringAsync().Result;
                        try
                        {
                            string msg = JsonConvert.DeserializeObject<string>(JsonResult);
                            return msg;
                        }
                        catch { return null; }
                    }
                }
                catch { return null; }
                return null;
            }
            else
            {
                return null;
            }
        }


        //TODO
        //-----------------GET-ACTIVITY-BY-ID-------------------------------------------------
        public async Task<ActivityDto> GetActivityById(GetActivityByIdViewModel model)
        {
            HttpResponseMessage Response = null;
            var Token = App.TokenDatabase.GetToken();
            client.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Token.AccessToken);

            var uri = new Uri(string.Format(Constants.FindActivityByIdURL, string.Empty));
            var json = JsonConvert.SerializeObject(model);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                //send a POST request to the web service specified by the URI
                Response = await client.PostAsync(uri, content);
                if (Response.IsSuccessStatusCode)
                {

                    var JsonResult = Response.Content.ReadAsStringAsync().Result;
                    try
                    {
                        ActivityDto a = JsonConvert.DeserializeObject<ActivityDto>(JsonResult);
                        
                        if (a.JoinedUsersIds.Count == 0)
                        {
                            a.JoinedUsersIds.Add(a.ActivityHostId);
                        }        
                        return a;
                    }
                    catch { return null; }
                }
            }
            catch { return null; }
            return null;
        }
    }
}