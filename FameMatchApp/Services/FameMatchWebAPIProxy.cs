using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using FameMatchApp.Models;
using Microsoft.Maui.ApplicationModel.Communication;

namespace FameMatchApp.Services
{
    public class FameMatchWebAPIProxy
    {
        #region without tunnel
        /*
        //Define the serevr IP address! (should be realIP address if you are using a device that is not running on the same machine as the server)
        private static string serverIP = "localhost";
        private HttpClient client;
        private string baseUrl;
        public static string BaseAddress = (DeviceInfo.Platform == DevicePlatform.Android &&
            DeviceInfo.DeviceType == DeviceType.Virtual) ? "http://10.0.2.2:5110/api/" : $"http://{serverIP}:5110/api/";
        public static string ImageBaseAddress = (DeviceInfo.Platform == DevicePlatform.Android &&
            DeviceInfo.DeviceType == DeviceType.Virtual) ? "http://10.0.2.2:5110/UserImages/" : $"http://{serverIP}:5110/UserImages";
        */
        #endregion

        #region with tunnel
        //Define the serevr IP address! (should be realIP address if you are using a device that is not running on the same machine as the server)
        private static string serverIP = "dm6s68f2-5144.euw.devtunnels.ms";
        private HttpClient client;
        private string baseUrl;
        public static string BaseAddress = "https://dm6s68f2-5144.euw.devtunnels.ms/api/";
        public static string ImageBaseAddress = "https://dm6s68f2-5144.euw.devtunnels.ms/UserImages/";
        #endregion

        public FameMatchWebAPIProxy()
        {
            //Set client handler to support cookies!!
            HttpClientHandler handler = new HttpClientHandler();
            handler.CookieContainer = new System.Net.CookieContainer();

            this.client = new HttpClient(handler);
            this.baseUrl = BaseAddress;
        }

        public string GetImagesBaseAddress()
        {
            return FameMatchWebAPIProxy.ImageBaseAddress;
        }

        public string GetDefaultProfilePhotoUrl()
        {
            return $"{FameMatchWebAPIProxy.ImageBaseAddress}/profileImages/default.png";
        }
        public async Task<User?> LoginAsync(Loginfo userInfo)
        {
            //Set URI to the specific function API
            string url = $"{this.baseUrl}login";
            try
            {
            
                //Call the server API
                string json = JsonSerializer.Serialize(userInfo);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(url, content);
                //Check status
                if (response.IsSuccessStatusCode)
                {
                    //Extract the content as string
                    string resContent = await response.Content.ReadAsStringAsync();
                    //Desrialize result
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    Casted? resultCasted = JsonSerializer.Deserialize<Casted>(resContent, options);
                    if (resultCasted == null || resultCasted.UserHair == null)
                    {
                        Castor? resultCastor = JsonSerializer.Deserialize<Castor>(resContent, options);
                        return resultCastor;
                    }
                    else
                        return resultCasted;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        //This methos call the Register web API on the server and return the AppUser object with the given ID
        //or null if the call fails
        public async Task<Castor?> RegisterCastor(Castor c)
        {
            //Set URI to the specific function API
            string url = $"{this.baseUrl}registerCastor";
            try
            {
                //Call the server API
                string json = JsonSerializer.Serialize(c);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(url, content);
                //Check status
                if (response.IsSuccessStatusCode)
                {
                    //Extract the content as string
                    string resContent = await response.Content.ReadAsStringAsync();
                    //Desrialize result
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    Castor? result = JsonSerializer.Deserialize<Castor>(resContent, options);
                    return result;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<Casted?> RegisterCasted(Casted c)
        {
            //Set URI to the specific function API
            string url = $"{this.baseUrl}registerCasted";
            try
            {
                //Call the server API
                string json = JsonSerializer.Serialize(c);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(url, content);
                //Check status
                if (response.IsSuccessStatusCode)
                {
                    //Extract the content as string
                    string resContent = await response.Content.ReadAsStringAsync();
                    //Desrialize result
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    Casted? result = JsonSerializer.Deserialize<Casted>(resContent, options);
                    return result;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<User?> UploadProfileImage(string imagePath)
        {
            //Set URI to the specific function API
            string url = $"{this.baseUrl}uploadprofileimage";
            try
            {
                
                //Create the form data
                MultipartFormDataContent form = new MultipartFormDataContent();
                var fileContent = new ByteArrayContent(System.IO.File.ReadAllBytes(imagePath));
                form.Add(fileContent, "file", imagePath);
                //Call the server API
                HttpResponseMessage response = await client.PostAsync(url, form);
                //Check status
                if (response.IsSuccessStatusCode)
                {
                    //Extract the content as string
                    string resContent = await response.Content.ReadAsStringAsync();
                    //Desrialize result
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    User? result = JsonSerializer.Deserialize<User>(resContent, options);
                    return result;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<bool> UpdateCastor(Castor user)
        {
            //Set URI to the specific function API
            string url = $"{this.baseUrl}UpdateCastor";
            try
            {
                //Call the server API
                string json = JsonSerializer.Serialize(user);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(url, content);
                //Check status
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<bool> UpdateCasted(Casted user)
        {
            //Set URI to the specific function API
            string url = $"{this.baseUrl}UpdateCasted";
            try
            {
                //Call the server API
                string json = JsonSerializer.Serialize(user);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(url, content);
                //Check status
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        //This method call the GetAllCasteds web API and return a list of Casteds or null if it fails.
        public async Task<List<Casted>?> GetAllCasteds()
        {
            //Set URI to the specific function API
            string url = $"{this.baseUrl}GetAllCasteds";
            try
            {
                //Call the server API
                HttpResponseMessage response = await client.GetAsync(url);
                //Extract the content as string
                string resContent = await response.Content.ReadAsStringAsync();
                //Check status
                if (response.IsSuccessStatusCode)
                {
                    //Desrialize result
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    List<Casted>? result = JsonSerializer.Deserialize<List<Casted>>(resContent, options);
                    return result;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<List<User>> GetUsers()
        {

            //Set URI to the specific function API
            string url = $"{this.baseUrl}GetUsers";
            try
            {
                HttpResponseMessage response = await client.GetAsync(url);
                //Check status
                if (response.IsSuccessStatusCode)
                {
                    //Extract the content as string
                    string resContent = await response.Content.ReadAsStringAsync();
                    //Desrialize result
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    List<User>? result = JsonSerializer.Deserialize<List<User>>(resContent, options);
                    return result;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<bool> Block(User user)
        {
            //Set URI to the specific function API
            string url = $"{this.baseUrl}Block";
            try
            {
                //Call the server API
                string json = JsonSerializer.Serialize(user);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(url, content);
                //Check status
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<Audition?> AddAudition(Audition audition)
        {
            //Set URI to the specific function API
            string url = $"{this.baseUrl}AddAudition";
            try
            {
                //Call the server API
                string json = JsonSerializer.Serialize(audition);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(url, content);
                //Check status
                if (response.IsSuccessStatusCode)
                {
                    //Extract the content as string
                    string resContent = await response.Content.ReadAsStringAsync();
                    //Desrialize result
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    Audition? result = JsonSerializer.Deserialize<Audition>(resContent, options);
                    return result;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<List<Audition>?> GetAllAuditions()
        {
            //Set URI to the specific function API
            string url = $"{this.baseUrl}GetAllAuditions";
            try
            {
                //Call the server API
                HttpResponseMessage response = await client.GetAsync(url);
                //Extract the content as string
                string resContent = await response.Content.ReadAsStringAsync();
                //Check status
                if (response.IsSuccessStatusCode)
                {
                    //Desrialize result
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    List<Audition>? result = JsonSerializer.Deserialize<List<Audition>>(resContent, options);
                    return result;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<List<string>> GetAllEmails()
        {

            //Set URI to the specific function API
            string url = $"{this.baseUrl}GetAllEmails";
            try
            {
                HttpResponseMessage response = await client.GetAsync(url);
                //Check status
                if (response.IsSuccessStatusCode)
                {
                    //Extract the content as string
                    string resContent = await response.Content.ReadAsStringAsync();
                    //Desrialize result
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    List<string> result = JsonSerializer.Deserialize<List<string>>(resContent, options);
                    return result;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<User> GetUserByEmail(string email)
        {
            //Set URI to the specific function API
            string url = $"{this.baseUrl}GetUserByEmail?email={email}";
            try
            {
                HttpResponseMessage response = await client.GetAsync(url);
                //Check status
                if (response.IsSuccessStatusCode)
                {
                    //Extract the content as string
                    string resContent = await response.Content.ReadAsStringAsync();
                    //Desrialize result
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    User result = JsonSerializer.Deserialize<User>(resContent, options);
                    return result;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<bool> UpdateUserPassword(User user)
        {
            //Set URI to the specific function API
            string url = $"{this.baseUrl}UpdateUserPassword";
            try
            {
                //Call the server API
                string json = JsonSerializer.Serialize(user);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(url, content);
                //Check status
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<bool> Accept(Castor castor)
        {
            //Set URI to the specific function API
            string url = $"{this.baseUrl}Accept";
            try
            {
                //Call the server API
                string json = JsonSerializer.Serialize(castor);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(url, content);
                //Check status
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<bool> Declaine(Castor castor)
        {
            //Set URI to the specific function API
            string url = $"{this.baseUrl}Declaine";
            try
            {
                //Call the server API
                string json = JsonSerializer.Serialize(castor);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(url, content);
                //Check status
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<List<Castor>?> GetAllCastors()
        {
            //Set URI to the specific function API
            string url = $"{this.baseUrl}GetAllCastors";
            try
            {
                //Call the server API
                HttpResponseMessage response = await client.GetAsync(url);
                //Extract the content as string
                string resContent = await response.Content.ReadAsStringAsync();
                //Check status
                if (response.IsSuccessStatusCode)
                {
                    //Desrialize result
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    List<Castor>? result = JsonSerializer.Deserialize<List<Castor>>(resContent, options);
                    return result;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<Castor> GetUserByAudition(int AudId)
        {
            //Set URI to the specific function API
            string url = $"{this.baseUrl}GetUserByAudition?id={AudId}";
            try
            {
                HttpResponseMessage response = await client.GetAsync(url);
                //Check status
                if (response.IsSuccessStatusCode)
                {
                    //Extract the content as string
                    string resContent = await response.Content.ReadAsStringAsync();
                    //Desrialize result
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    Castor result = JsonSerializer.Deserialize<Castor>(resContent, options);
                    return result;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<Audition>?> GetUserAudition(int id)
        {
            //Set URI to the specific function API
            string url = $"{this.baseUrl}GetUserAuditions?id={id}";
            try
            {
                //Call the server API
                HttpResponseMessage response = await client.GetAsync(url);
                //Extract the content as string
                string resContent = await response.Content.ReadAsStringAsync();
                //Check status
                if (response.IsSuccessStatusCode)
                {
                    //Desrialize result
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    List<Audition>? result = JsonSerializer.Deserialize<List<Audition>>(resContent, options);
                    return result;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<bool> UpdateAudition(Audition Aud)
        {
            //Set URI to the specific function API
            string url = $"{this.baseUrl}UpdateAudition?audDto={Aud.AudId}";
            try
            {
                //Call the server API
                string json = JsonSerializer.Serialize(Aud);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(url, content);
                //Check status
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}

