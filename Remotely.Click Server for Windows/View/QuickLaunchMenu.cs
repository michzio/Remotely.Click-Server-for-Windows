using Remotely.Click_Server_for_Windows.Interop;
using Remotely.Click_Server_for_Windows.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Remotely.Click_Server_for_Windows.View
{
    class QuickLaunchMenu : ContextMenu
    {

        ServerStatusMenuItem serverStatusMenuItem; 

        public QuickLaunchMenu(Action aboutCallback, Action exitApplicationCallback)
        {
            try {

                    List<object> quickLaunchMenuItems = new List<object>();
                    ItemsSource = quickLaunchMenuItems;

                    MenuItem menuItem = null;

                    // Server Status Menu Item 
                    serverStatusMenuItem = new ServerStatusMenuItem();
                    serverStatusMenuItem.ShouldServerStatusChange += OnShouldServerStatusChange_ServerStatusMenuItem; 
                    quickLaunchMenuItems.Add(serverStatusMenuItem);

                    quickLaunchMenuItems.Add(new Separator());

                    // About Menu Item
                    menuItem = new MenuItem(); 
                    menuItem.Header = "About Remotely.Click";
                    menuItem.Click += (sender, e) =>
                    {
                       aboutCallback();
                    };
                    //menuItem.Icon = ;
                    quickLaunchMenuItems.Add(menuItem);

                    // Preferences Menu Item 
                    menuItem = new MenuItem();
                    menuItem.Header = "Preferences";
                    menuItem.Click += (sender, e) =>
                    {
                        openPreferencesWindow(); 
                    };
                    quickLaunchMenuItems.Add(menuItem);

                    quickLaunchMenuItems.Add(new Separator());

                    // Hide Menu Item 
                    menuItem = new MenuItem();
                    menuItem.Header = "Hide";
                    menuItem.Click += (sender, e) =>
                    {
                        this.IsOpen = false;
                    };
                    quickLaunchMenuItems.Add(menuItem); 

                    // CLose Menu Item
                    menuItem = new MenuItem();
                    menuItem.Header = "Close";
                    menuItem.Click += (sender, e) =>
                    {
                        exitApplicationCallback();
                        foreach (System.Windows.Window window in System.Windows.Application.Current.Windows)
                        {
                            try
                            {
                                window.Close();
                            }
                            catch (Exception ex)
                            {
                                Debug.WriteLine(ex.Message);
                                Debug.WriteLine(ex.StackTrace);
                            }
                        }
                        exitApplicationCallback();
                    };
                    quickLaunchMenuItems.Add(menuItem);

                    replaceMenuAtCursorPosition();

                    // increase quick launch menu width
                    this.Width = 300;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Debug.WriteLine(ex.StackTrace);
            }
        }

        public void replaceMenuAtCursorPosition()
        {
            System.Drawing.Point mousePosition = new System.Drawing.Point(System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Right - 16, System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Bottom - 24);
            Win32.Cursor.GetCursorPos(out mousePosition);

            Placement = System.Windows.Controls.Primitives.PlacementMode.AbsolutePoint;

            VerticalOffset = mousePosition.Y - 2;
            HorizontalOffset = mousePosition.X - 8;
        }

        private void openPreferencesWindow()
        {
            var prefsWindow = new PreferencesWindow();
            prefsWindow.ShouldServerStatusChange += OnShouldServerStatusChange_PreferencesWindow; 
            prefsWindow.Show();
        }


        private void OnShouldServerStatusChange_ServerStatusMenuItem(ServerStatus from, ServerStatus to)
        {
            Console.WriteLine("QuickLaunchMenu ServerStatusMenuItem fired new event ShouldServerStatusChange");
            Console.WriteLine("Server Status should change from " + from + " to " + to);

            ServerSettings.shared().ServerStatus = to;
        }

        private void OnShouldServerStatusChange_PreferencesWindow(ServerStatus from, ServerStatus to)
        {
            Console.WriteLine("PreferencesWindow fired new event ShouldServerStatusChange");
            Console.WriteLine("Server Status should change from " + from + " to " + to);

            ServerSettings.shared().ServerStatus = to; 
        }
    }
}
