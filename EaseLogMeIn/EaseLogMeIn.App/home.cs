using CefSharp;
using CefSharp.WinForms;
using EaseLogMeIn.BusinessLayer.Desktop;
using EaseLogMeIn.Models;
using EaseMeLogIn.BusinessLayer.Desktop;
using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EaseLogMeIn.App
{
    public partial class Home : Form
    {
        #region Private Methods
        public ChromiumWebBrowser chromeBrowser = null;
        public string ClipboardValue = null;
        private int Clicked = 0;
        private void InitializeChromium()
        {
            CefSettings settings = new CefSettings();
            Cef.Initialize(settings);
            chromeBrowser = new ChromiumWebBrowser("");
            chromeBrowser.LoadingStateChanged += new EventHandler<LoadingStateChangedEventArgs>(ChromeBrowser_LoadingStateChanged);
            chromeBrowser.FrameLoadEnd += new EventHandler<FrameLoadEndEventArgs>(ChromeBrowser_FrameLoadEnd);
            Controls.Add(chromeBrowser);
            chromeBrowser.Dock = DockStyle.Fill;
        }

        private void ChromeBrowser_FrameLoadEnd(object sender, FrameLoadEndEventArgs e)
        {
            if (e.Frame.IsMain)
            {
                if (Clicked == 0)
                {
                    WebAuthentication.PlugjQuiry(e);
                    Clicked++;
                }
            }
        }

        private void BindDropDown()
        {
            lstAirlines.DataSource = null;
            UserStaticData.Websites.OrderBy(w => w.Name);
            Website temp = UserStaticData.Websites.Find(w => w.UniqueId == Guid.Empty);
            if (temp == null)
            {
                UserStaticData.Websites.Insert(0, new Website { Name = "Select Website" });
            }
            lstAirlines.DataSource = UserStaticData.Websites;
            lstAirlines.DisplayMember = "Name";
            lstAirlines.ValueMember = "UniqueId";
        }
        delegate void MyDelegate(bool show);
        public void ShowProgressBarChrome(bool show)
        {
            chromeBrowser.Enabled = !show;
            picLoader.Visible = show;
        }
        public void ShowProgressBar(bool show)
        {
            picLoader.Visible = show;
        }
        public Home()
        {
            InitializeComponent();
            this.Width = Screen.PrimaryScreen.Bounds.Width;
            this.Height = Screen.PrimaryScreen.Bounds.Height-50;
            panel1.Width = Width;
            picLoader.Left = (Width / 2) - 50;
            picLoader.Top = (Height / 2) - 100;
            lblUserName.Text = "Welcome " + UserStaticData.Employee.Name;
            int wdth = lblUserName.Width;
            lblUserName.Left = Width - wdth - 20;
            toolTip1.SetToolTip(lblUserName, "Click to close Application");
        }
        #endregion

        #region Form Events
        private void Home_Load(object sender, EventArgs e)
        {

            InitializeChromium();
            WebManager.GetWebsites();
            if (UserStaticData.Websites == null || UserStaticData.Websites.Count == 0)
            {
                MessageBox.Show("You are not assigned any website!!", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            BindDropDown();
        }
        protected override void OnActivated(EventArgs e)
        {
            string clipData = Clipboard.GetText(TextDataFormat.Text);
            if (!string.IsNullOrEmpty(clipData))
            {
                if (clipData != ClipboardValue)
                {
                    WebManager.UpdateClipboardData(clipData, ActionType.Clipboard);
                    ClipboardValue = clipData;
                }
            }
            base.OnActivated(e);
        }
        private void Home_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason != CloseReason.ApplicationExitCall)
            {
                var result = MessageBox.Show("Are you sure to logout exit application", "Exit Application", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                e.Cancel = (result == DialogResult.No);
                if (result == DialogResult.Yes)
                {
                    chromeBrowser.Dispose();
                    WebManager.UpdateLogOut();
                    Application.Exit();
                }
            }
        }
        #endregion

        #region Form Button Events
        private void BtnLoginWebsite_Click(object sender, EventArgs e)
        {
            Clicked = 0;
            if (lstAirlines.SelectedIndex == 0)
            {
                MessageBox.Show("Please select a airline first!!", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string id = lstAirlines.SelectedValue.ToString();
            if (Guid.TryParse(id, out Guid WebsiteId))
            {
                bool IsValidWebsite = WebManager.IsValidWebsite(WebsiteId, UserStaticData.Employee.UniqueId);
                Website exist = UserStaticData.Websites.Find(w => w.UniqueId == WebsiteId);
                if (!IsValidWebsite)
                {
                    UserStaticData.Websites.Remove(exist);
                    UserStaticData.CurrentWeb = null;
                    BindDropDown();
                    lstAirlines.SelectedIndex = 0;
                    MessageBox.Show("You are not authorized to access this website", "No Access", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }
                UserStaticData.Websites.Add(UserStaticData.CurrentWeb);
                UserStaticData.CurrentWeb.UserId = Security.Decrypt(UserStaticData.CurrentWeb.UserId, UserStaticData.CurrentWeb.Salt);
                UserStaticData.CurrentWeb.Password = Security.Decrypt(UserStaticData.CurrentWeb.Password, UserStaticData.CurrentWeb.Salt);
                chromeBrowser.Load(UserStaticData.CurrentWeb.URL);
            }

        }
        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            Invoke(new MyDelegate(ShowProgressBar), true);
            WebManager.GetWebsites();
            BindDropDown();
            Invoke(new MyDelegate(ShowProgressBar), false);
        }
        #endregion

        #region Chrome Browser Events
        public void ChromeBrowser_LoadingStateChanged(object sender, LoadingStateChangedEventArgs args)
        {
            try
            {
                Invoke(new MyDelegate(ShowProgressBarChrome), args.IsLoading);
            }
            catch (Exception) { }
        }
        #endregion

        private void LblUserName_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Are you sure to logout exit application", "Exit Application", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                chromeBrowser.Dispose();
                WebManager.UpdateLogOut();
                Application.Exit();
            }
        }
    }
}


