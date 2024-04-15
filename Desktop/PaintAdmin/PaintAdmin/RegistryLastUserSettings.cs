using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaintAdmin
{
    class RegistryLastUserSettings
    {
        public static string GetLogin()
        {
            RegistryKey currentUserKey = Registry.CurrentUser;
            RegistryKey lastSession = currentUserKey.OpenSubKey("LastSession");
            string login = lastSession.GetValue("login").ToString();
            return login;
        }

        public static string GetPassword()
        {
            RegistryKey currentUserKey = Registry.CurrentUser;
            RegistryKey lastSession = currentUserKey.OpenSubKey("LastSession");
            string password = lastSession.GetValue("password").ToString();
            return password;
        }

        public static string GetRoleID()
        {
            RegistryKey currentUserKey = Registry.CurrentUser;
            RegistryKey lastSession = currentUserKey.OpenSubKey("LastSession");
            string RoleID = lastSession.GetValue("roleId").ToString();
            return RoleID;
        }

        public static string GetUserID()
        {
            RegistryKey currentUserKey = Registry.CurrentUser;
            RegistryKey lastSession = currentUserKey.OpenSubKey("LastSession");
            string UserID = lastSession.GetValue("userId").ToString();
            return UserID;
        }

        public static void AddUser(string Login, string Password, string RoleID, string UserID)
        {
            RegistryKey currentUserKey = Registry.CurrentUser;
            RegistryKey lastSession = currentUserKey.CreateSubKey("LastSession");

            lastSession.SetValue("login", Login);
            lastSession.SetValue("password", Password);
            lastSession.SetValue("roleId", RoleID);
            lastSession.SetValue("userId", UserID);
            lastSession.Close();
        }

        public static bool CheckUser()
        {
            RegistryKey currentUserKey = Registry.CurrentUser;
            foreach (string SubKey in currentUserKey.GetSubKeyNames())
            {
                if (SubKey == "LastSession")
                {
                    return true;
                }
            }
            return false;
        }

        public static void DeleteUser()
        {
            RegistryKey currentUserKey = Registry.CurrentUser;
            foreach (string SubKey in currentUserKey.GetSubKeyNames())
            {
                if (SubKey == "LastSession")
                {
                    currentUserKey.DeleteSubKey("LastSession");
                }
            }
        }
    }
}
