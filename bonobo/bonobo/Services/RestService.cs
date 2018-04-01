using bonobo.Dtos;
using bonobo.Models;
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
                var msg= JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonResult);
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
            if(response.IsSuccessStatusCode)
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
            Debug.WriteLine("RestService: FindUserByEmail email = " + model.Email);
            HttpResponseMessage Response = null;
            var Token = App.TokenDatabase.GetToken();
            Debug.WriteLine("RestService: FindUserByEmail token = " + Token.AccessToken);
            client.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Token.AccessToken);

            var uri = new Uri(string.Format(Constants.FindUserByEmailURL, string.Empty));
            Debug.WriteLine("RestService: FindUserByEmail uri = " + uri);
            var json = JsonConvert.SerializeObject(model);
            Debug.WriteLine("RestService: FindUserByEmail json = " + json);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            Debug.WriteLine("RestService: FindUserByEmail content = " + content);

            try
            {
                //send a POST request to the web service specified by the URI
                Response = await client.PostAsync(uri, content);
                if (Response.IsSuccessStatusCode)
                {
                    var JsonResult = Response.Content.ReadAsStringAsync().Result;
                    Debug.WriteLine("RestService: FindUserByEmail JsonResult: " + JsonResult);
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

        /*******************ACTIVITY-API*****************************************************/

        public async Task<List<ActivityDto>> GetAllActivities()
        {
            Debug.WriteLine("RestService: GetAllActivities START");
            HttpResponseMessage Response = null;
            var Token = App.TokenDatabase.GetToken();
            Debug.WriteLine("RestService: GetAllActivities token = ", Token.AccessToken);
            client.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Token.AccessToken);

            var uri = new Uri(string.Format(Constants.GetAllActivitiesURL, string.Empty));

            Debug.WriteLine("RestService: GetAllActivities TRY");
            try
            {
                Response = await client.GetAsync(uri);
                Debug.WriteLine("RestService: GetAllActivities Response");
                if (Response.IsSuccessStatusCode)
                {
                    Debug.WriteLine("RestService: GetAllActivities Response SUCCES");
                    var JsonResult = Response.Content.ReadAsStringAsync().Result;
                    Debug.WriteLine("RestService: GetAllActivities JsonResult = ", JsonResult);
                    try
                    {
                        List<ActivityDto> list = JsonConvert.DeserializeObject<List<ActivityDto>>(JsonResult);
                        foreach(ActivityDto a in list)
                        {
                            Debug.WriteLine("RestService: GetAllActivities");
                            if (a.JoinedUsersIds.Count == 0)
                            {
                                Debug.WriteLine("RestService: GetAllActivities E NULL");
                                a.JoinedUsersIds.Add(a.ActivityHostId);
                            }
                            Debug.WriteLine("RestService: GetAllActivities" + a.JoinedUsersIds);
                            Debug.WriteLine("RestService: GetAllActivities" + a.JoinedUsersIds[0]);
                        }
                        Debug.WriteLine("RestService: GetAllActivities act1 = " + list[1].ActivityName);
                        Debug.WriteLine("RestService: GetAllActivities act1 = " + list[1].Category);
                        Debug.WriteLine("RestService: GetAllActivities act1 = " + list[1].NoPlaces);
                        Debug.WriteLine("RestService: GetAllActivities act1 = " + list[1].ShortDescription);
                        Debug.WriteLine("RestService: GetAllActivities act1 = " + list[1].Where);
                        Debug.WriteLine("RestService: GetAllActivities act1 = " + list[1].When);
                        Debug.WriteLine("RestService: GetAllActivities act1 = " + list[1].IsHost);
                        Debug.WriteLine("RestService: GetAllActivities act1 = " + list[1].IsJoined);
                    

                        return list;
                    }
                    catch { return null; }
                }
            }
            catch { return null; }
            return null;
        }
    }
}
