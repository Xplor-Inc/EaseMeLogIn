using EaseLogMeIn.BusinessLayer.Desktop;
using EaseLogMeIn.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace EaseMeLogIn.BusinessLayer.Desktop
{
    public static class APIRequestHandler
    {

        static readonly CookieContainer cookieContainer = new CookieContainer();
        static readonly Uri APIBaseUrl = new Uri(ReadOnlyKeys.APIBaseUrl);
        static readonly HttpClientHandler handler = new HttpClientHandler
        {
            CookieContainer = cookieContainer
        };
        static readonly HttpClient client = new HttpClient(handler)
        {
            BaseAddress = APIBaseUrl
        };
        
        static APIRequestHandler()
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public static bool IssueToken()
        {
            Users users = new Users { UserId = ReadOnlyKeys.APIUserId, Password = ReadOnlyKeys.APIUserPassword };
            try
            {
                var token = client.PostAsJsonAsync("Account/LoginToAPI", users).Result;
                if (token.IsSuccessStatusCode)
                {
                    var issuedToken = token.Content.ReadAsAsync<AuthResponse>().Result;
                    if (issuedToken.Status)
                    {
                        Cookie responseCookies = cookieContainer.GetCookies(APIBaseUrl).Cast<Cookie>().FirstOrDefault(c => c.Name == ".ASPXAUTH");
                        if (responseCookies != null)
                        {
                            cookieContainer.Add(responseCookies);
                            UserStaticData.IPAddress = issuedToken.IPAddress;
                            return true;
                        }
                        else
                        {
                           return false;
                        }
                    }
                }
            }
            catch
            {
                return false;
            }
            return false;
        }
        public static bool IsValidEmployee(string UserId)
        {
            string path = string.Format(ReadOnlyKeys.IsValidEmployee, UserId);
            bool IsValidEmployee = false;
            HttpResponseMessage response = client.GetAsync(path).Result;
            DesktopAppLoginHistory history = new DesktopAppLoginHistory
            {
                IPAddress = UserStaticData.IPAddress,
                MACAddress = SystemConfiguration.MACAddress,
                UserId = Environment.UserName
            };
            if (response.IsSuccessStatusCode)
            {
                UserStaticData.Employee = response.Content.ReadAsAsync<Employee>().Result;
                IsValidEmployee = UserStaticData.Employee != null;
                if (IsValidEmployee)
                {
                    history.UniqueId = UserStaticData.Employee.UniqueId;
                    history.Name = UserStaticData.Employee.Name;
                }
            }
            history.IsSuccessed = IsValidEmployee;
            AddLogin(history);
            return IsValidEmployee;
        }
        public static void GetWebsites()
        {
            string path = string.Format(ReadOnlyKeys.GetWebsiteList, UserStaticData.Employee.UserId);
            HttpResponseMessage response = client.GetAsync(path).Result;
            if (response.IsSuccessStatusCode)
            {
                UserStaticData.Websites = response.Content.ReadAsAsync<List<Website>>().Result;
            }
        }
        public static bool IsValidWebsite(Guid WebsiteId, Guid UserId)
        {
            WebsiteAccessLog data = new WebsiteAccessLog { UserId = UserId, WebsiteId = WebsiteId, IPAddress = UserStaticData.IPAddress, Id = UserStaticData.LoginId };
            HttpResponseMessage response = client.PostAsJsonAsync(ReadOnlyKeys.IsValidWebsite, data).Result;
            bool IsValidWebsite = false;
            if (response.IsSuccessStatusCode)
            {
                UserStaticData.CurrentWeb = response.Content.ReadAsAsync<Website>().Result;
                IsValidWebsite = UserStaticData.CurrentWeb != null;
            }
            return IsValidWebsite;
        }

        public static void UpdateClipboardData(UserActionData data)
        {
            HttpResponseMessage response = client.PostAsJsonAsync(ReadOnlyKeys.AddUserActionData, data).Result;
            if (response.IsSuccessStatusCode)
            {
                var resp = response.Content.ReadAsAsync<bool>().Result;
            }
        }

        public static void AddLogin(DesktopAppLoginHistory data)
        {
            try
            {
                data.OSName = SystemConfiguration.OSName;
                data.OSVersion = SystemConfiguration.OSVersion;
                data.OSArchitecture = SystemConfiguration.OSArchitecture;
                data.MachineName = SystemConfiguration.MachineName;
                HttpResponseMessage response = client.PostAsJsonAsync(ReadOnlyKeys.AddLoginHistory, data).Result;
                if (response.IsSuccessStatusCode)
                {
                    UserStaticData.LoginId = response.Content.ReadAsAsync<int>().Result;
                }
            }
            catch (Exception)
            {
            }
        }
        public static void UpdateLogOut()
        {
            try
            {
                DesktopAppLoginHistory history = new DesktopAppLoginHistory
                {
                    UniqueId = UserStaticData.Employee.UniqueId,
                    Id = UserStaticData.LoginId
                };
                HttpResponseMessage response = client.PostAsJsonAsync(ReadOnlyKeys.UpdateLoginHistory, history).Result;
                if (response.IsSuccessStatusCode)
                {
                    bool id = response.Content.ReadAsAsync<bool>().Result;
                }
            }
            catch (Exception)
            {
            }
        }
    }
}
