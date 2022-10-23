using System;
using System.Collections.Generic;
using System.DirectoryServices.Protocols;
using System.Linq;
using System.Management;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace EaseLogMeIn.BusinessLayer.Desktop
{
    public static class SystemUtility
    {
        public static bool IsNetworkAvailable
        {
            get
            {
                return NetworkInterface.GetIsNetworkAvailable();
            }
        }
        public static bool IsValidADUser(string UserName, string Password)
        {
            try
            {
                NetworkCredential nc = new NetworkCredential(UserName, Password, Environment.UserDomainName);
                LdapConnection lcon = new LdapConnection(new LdapDirectoryIdentifier((string)null, false, false))
                {
                    Credential = nc,
                    AuthType = AuthType.Negotiate
                };
                lcon.Bind(nc);
                lcon.Dispose();
            }
            catch (LdapException)
            {
                throw;
            }
            return true;
        }

    }
    public static class SystemConfiguration
    {
        static SystemConfiguration()
        {
            using (ManagementObjectSearcher searcher = new ManagementObjectSearcher("Select CSDVersion, Caption, OSArchitecture from Win32_OperatingSystem"))
            {
                ManagementObjectCollection collections = searcher.Get();
                foreach (var collection in collections)
                {
                    OSName = collection.Properties["Caption"].Value?.ToString();
                    OSVersion = collection.Properties["CSDVersion"].Value?.ToString();
                    OSArchitecture = collection.Properties["OSArchitecture"].Value?.ToString();
                    MachineName = Environment.MachineName;
                }
            }
        }

        public static string MACAddress
        {
            get
            {
                return (from nic in NetworkInterface.GetAllNetworkInterfaces()
                        where nic.OperationalStatus == OperationalStatus.Up
                        select nic.GetPhysicalAddress().ToString()).FirstOrDefault();
            }
        }
        public static string OSName { get;  }
        public static string OSVersion { get; set; }
        public static string OSArchitecture { get; set; }
        public static string SystemArchitecture { get; set; }
        public static string MachineName { get; set; }
    }
}
