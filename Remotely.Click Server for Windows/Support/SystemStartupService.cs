using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using System.Reflection;

namespace Remotely.Click_Server_for_Windows.Support
{
    class SystemStartupService
    {

        public static void AddThisAppToCurrentUserStartup()
        {
            AddAppToCurrentUserStartup(Assembly.GetExecutingAssembly().GetName().Name,
                                       Assembly.GetExecutingAssembly().Location, false); 
        }

        public static void AddAppToCurrentUserStartup(string appName, string appPath, bool asBackground) { 
            using (RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true))
            {
                registryKey.SetValue(appName, "\"" + appPath + "\"" + (asBackground ? " /background" : "") ); 
            }
        }

        public static void DeleteThisAppFromCurrentUserStartup()
        {
            DeleteAppFromCurrentUserStartup(Assembly.GetExecutingAssembly().GetName().Name);
        }

        public static void DeleteAppFromCurrentUserStartup(string appName)
        {
            using (RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true))
            {
                registryKey.DeleteValue(appName, false);
            }
        }

        public static void AddThisAppToLocalMachineStartup()
        {
            AddAppToLocalMachineStartup(Assembly.GetExecutingAssembly().GetName().Name,
                                       Assembly.GetExecutingAssembly().Location, false);
        }

        public static void AddAppToLocalMachineStartup(string appName, string appPath, bool asBackground)
        {
            using(RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true))
            {
                registryKey.SetValue(appName, "\"" + appPath + "\"" + (asBackground ? " /background" : "") ); 
            }
        }

        public static void DeleteThisAppFromLocalMachineStartup()
        {
            DeleteAppFromLocalMachineStartup(Assembly.GetExecutingAssembly().GetName().Name);
        }

        public static void DeleteAppFromLocalMachineStartup(string appName)
        {
            using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true))
            {
                registryKey.DeleteValue(appName, false);
            }
        }
    }
}
