using EaseLogMeIn.BusinessLayer.Desktop;
using EaseMeLogIn.BusinessLayer.Desktop;
using System;
using System.DirectoryServices.Protocols;
using System.Windows.Forms;

namespace EaseLogMeIn.App
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            if (SystemUtility.IsNetworkAvailable)
            {
                if (AuthManager.IssueToken())
                {
                    bool IsValidEmployee = AuthManager.IsValidEmployee(Environment.UserName);
                    if (IsValidEmployee)
                    {
                        if (UserStaticData.Employee.IsNonADUser)
                        {
                            if (string.Equals(SystemConfiguration.MACAddress, UserStaticData.Employee.MACId, StringComparison.OrdinalIgnoreCase))
                            {
                                pnlADUsers.Visible = false;
                                pnlNonADUsers.Visible = true;
                            }
                            else
                            {
                                MessageBox.Show("You are not authorized to access this app!!", "UnAuthorized Access", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                                Application.Exit();
                            }
                        }
                        else
                        {
                            pnlADUsers.Visible = true;
                            pnlNonADUsers.Visible = false;
                            lblUserName.Text = "Hi " + Environment.UserName.ToUpper();
                        }
                    }
                    else
                    {
                        MessageBox.Show("You are not authorized to access this app!!", "UnAuthorized Access", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        Application.Exit();
                    }
                }
                else
                {
                    MessageBox.Show("Unable to connect to remote server!!", "UnAuthorized Access", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    Application.Exit();
                }
            }
            else
            {
                MessageBox.Show("Please check your network connection!!", "No Network", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                Application.Exit();
            }
        }

        private void BtnLoginAD_Click(object sender, EventArgs e)
        {
            OpenHomeForm();
        }

        private void TxtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                OpenHomeForm();
            }
        }
      
        private void OpenHomeForm()
        {
            if (txtPassword.Text.Length == 0)
            {
                MessageBox.Show("Enter Valid Password", "Password Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            try
            {
                if (SystemUtility.IsValidADUser(UserStaticData.Employee.UserId, txtPassword.Text))
                {
                    Home home = new Home();
                    this.Hide();
                    home.Show();
                }
            }
            catch (LdapException ex)
            {
                MessageBox.Show("Error: "+ex.Message +"\n Ldap Error: "+ex.ServerErrorMessage, "Password Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                //MessageBox.Show("Invalid Credentials!!", "Password Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
        }

        private void BtnNonADUser_Click(object sender, EventArgs e)
        {

        }
    }
}
