using Remotely.Click_Server_for_Windows.Interop;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Remotely.Click_Server_for_Windows.View
{
    class QuickLaunchMenu : ContextMenu
    {
        public QuickLaunchMenu(Action aboutCallback, Action exitApplicationCallback)
        {
            try {

                 System.Drawing.Point mousePosition = new System.Drawing.Point(System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Right - 16, System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Bottom - 24);
                 Win32.Cursor.GetCursorPos(out mousePosition);

                List<object> quickLaunchMenuItems = new List<object>();
                ItemsSource = quickLaunchMenuItems;

                MenuItem menuItem = null;

                // About Menu Item
                menuItem = new MenuItem(); 
                menuItem.Header = "About";
                menuItem.Click += (sender, e) =>
                {
                   aboutCallback();
                };
                //menuItem.Icon = ;
                quickLaunchMenuItems.Add(menuItem);

                    quickLaunchMenuItems.Add(new Separator());

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

                    Placement = System.Windows.Controls.Primitives.PlacementMode.AbsolutePoint;

                    VerticalOffset = mousePosition.Y - 2;
                    HorizontalOffset = mousePosition.X - 8;

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Debug.WriteLine(ex.StackTrace);
            }
        }

    }
}
