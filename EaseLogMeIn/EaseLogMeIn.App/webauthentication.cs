using CefSharp;
using CefSharp.WinForms;
using EaseLogMeIn.BusinessLayer.Desktop;
using EaseLogMeIn.Models;
using EaseMeLogIn.BusinessLayer.Desktop;
using System;
using System.Text;
using System.Windows.Forms;

namespace EaseLogMeIn.App
{
    public static class WebAuthentication
    {
        #region Chromium Browser
        private static string GoogleScript()
        {
            StringBuilder googleScript = new StringBuilder("document.getElementsByName('identifier')[0].value ='" + UserStaticData.CurrentWeb.UserId + "';");
            googleScript.AppendLine("document.getElementById('identifierNext').click();");
            googleScript.Append("setTimeout(function () {  document.getElementsByName('password')[0].value ='" + UserStaticData.CurrentWeb.Password + "';");
            googleScript.AppendLine("document.getElementById('passwordNext').click();},1500)");
            return googleScript.ToString();
        }
        private static string ButtonClickScript()
        {
            StringBuilder login = new StringBuilder();
            if (!string.IsNullOrEmpty(UserStaticData.CurrentWeb.ButtonId) && UserStaticData.CurrentWeb.ButtonId.ToLower() != "na")
            {
                login.AppendFormat("document.getElementById('{0}').click();", UserStaticData.CurrentWeb.ButtonId);
            }
            else if (!string.IsNullOrEmpty(UserStaticData.CurrentWeb.Script))
            {
                string[] v1 = UserStaticData.CurrentWeb.Script.Split(',');
                if (v1.Length > 1)
                {
                    if (v1[0].ToLower() == "button")
                    {
                        login.Append(@"var butn = document.getElementsByTagName('button');
                                            if (butn != null){
                            for (var i = 0; i < butn.length; i++)
                            {
                                if (butn[i].innerText.trim() == '" + v1[1].Trim() + "') {" +
                                @"butn[i].click();
                                }
                            }
                        }
                        ");
                    }
                   else if (v1[0].ToLower() == "input")
                    {
                        login.Append(@"var butn = document.getElementsByTagName('input');
                                            if (butn != null){
                            for (var i = 0; i < butn.length; i++)
                            {
                                if (butn[i].value.trim() == '" + v1[1].Trim() + "') {" +
                                @"butn[i].click();
                                }
                            }
                        }
                        ");
                    }
                    else
                    {
                        login.Append(@"var butn = document.getElementsByName('" + v1[0].Trim() + @"');
                                            if (butn != null){
                            for (var i = 0; i < butn.length; i++)
                            {
                                if (butn[i].value.trim() == '" + v1[1].Trim() + "') {" +
                                @"butn[i].click();
                                }
                            }
                        }
                        ");
                    }
                }
            }
            return login.ToString();
        }
        private static string LoginToWebsiteById()
        {
            StringBuilder login = new StringBuilder();
            login.AppendFormat("document.getElementById('{0}').value ='{1}';", UserStaticData.CurrentWeb.UserIdTextId, UserStaticData.CurrentWeb.UserId);
            login.AppendFormat("document.getElementById('{0}').value ='{1}';", UserStaticData.CurrentWeb.PasswordTextId, UserStaticData.CurrentWeb.Password);
            login.Append(ButtonClickScript());

            return login.ToString();
        }
        private static string LoginToWebsiteByName()
        {
            StringBuilder login = new StringBuilder();
            login.AppendFormat("document.getElementsByName('{0}')[0].value ='{1}';", UserStaticData.CurrentWeb.UserIdTextId, UserStaticData.CurrentWeb.UserId);
            login.AppendFormat("document.getElementsByName('{0}')[0].value ='{1}';", UserStaticData.CurrentWeb.PasswordTextId, UserStaticData.CurrentWeb.Password);

            login.Append(ButtonClickScript());
            return login.ToString();
        }
        #endregion


        public static async void PlugjQuiry(FrameLoadEndEventArgs browser)
        {
            string loginScript = string.Empty;
            if (browser.Url.ToString().ToLower().Contains("https://accounts.google.com/"))
            {                
                loginScript = GoogleScript();
            }
           else if (UserStaticData.CurrentWeb.ConfigurationType == ConfigurationType.ById)
            {
                loginScript = LoginToWebsiteById();
            }
            else if (UserStaticData.CurrentWeb.ConfigurationType == ConfigurationType.ByName)
            {
                loginScript = LoginToWebsiteByName();
            }
            if (!string.IsNullOrEmpty(loginScript))
            {
                loginScript = "setTimeout(function () {" + loginScript + "}, 1000);";
                var msg = await browser.Frame.EvaluateScriptAsync(loginScript);
                if (!msg.Success)
                {                    
                    
                }
            }
            else
            {
                MessageBox.Show("Invalid configuration", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
