using Remotely.Click_Server_for_Windows.Model;
using Remotely.Click_Server_for_Windows.View;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Remotely.Click_Server_for_Windows
{


    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private NotifyIcon notifyIcon = null;
        private Dictionary<string, Icon> iconHandles = null;

        private QuickLaunchMenu quickLaunchMenu; 

        public MainWindow()
        {
            InitializeComponent();
        }

        protected override void OnInitialized(EventArgs e)
        {
            Loaded += OnLoaded;
            StateChanged += OnStateChanged;
            Closing += OnClosing;
            Closed += OnClosed;

            base.OnInitialized(e);
        }

        private void loadIconHandles()
        {
            iconHandles = new Dictionary<string, System.Drawing.Icon>();
            //string bitmapPath = System.IO.Path.Combine(Environment.CurrentDirectory, @"Icons\ic_speaker_phone_3x.png");
            //var bitmap = new Bitmap(bitmapPath);
            //var iconHandle = bitmap.GetHicon();
            var iconHandle = Remotely.Click_Server_for_Windows.Properties.Resources.NotifyIcon.GetHicon();
            iconHandles.Add("QuickLaunch", System.Drawing.Icon.FromHandle(iconHandle));
        }

        private NotifyIcon createNotifyIcon()
        {
            var notifyIcon = new NotifyIcon();
            notifyIcon.Click += new EventHandler(OnNotifyIconClick);
            notifyIcon.DoubleClick += new EventHandler(OnNotifyIconDoubleClick);
            notifyIcon.Icon = iconHandles["QuickLaunch"];

            return notifyIcon;
        }

        private QuickLaunchMenu createQuickLaunchMenu()
        {
            var quickLaunchMenu = new View.QuickLaunchMenu(
              () =>
              {
                  if (this.WindowState == WindowState.Minimized)
                  {
                      this.WindowState = WindowState.Normal;
                  }
                  this.Show();
              },
              () =>
              {
                  WindowState = WindowState.Minimized;
                  Close();
              });
            return quickLaunchMenu; 
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            // create quick launch menu in notification try
            loadIconHandles();
            notifyIcon = createNotifyIcon();
            quickLaunchMenu = createQuickLaunchMenu();

            if (notifyIcon == null) return; 
            notifyIcon.Visible = true;
            if (ServerSettings.shared().ShouldServerAutostartWithApp)
            {
                notifyIcon.ShowBalloonTip(1, "Remotely.Click", "Server is starting...", System.Windows.Forms.ToolTipIcon.Info);
            }
        }

        private void OnStateChanged(object sender, EventArgs e)
        {
            if (this.WindowState == WindowState.Minimized)
            {
                this.Topmost = false;
                this.ShowInTaskbar = false;
                notifyIcon.Visible = true;
            }
            else
            {
                notifyIcon.Visible = true;
                this.ShowInTaskbar = true;
                this.Topmost = true;
            }
        }

        private void OnClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (this.WindowState != WindowState.Minimized)
            {
                e.Cancel = true;
                
                this.WindowState = WindowState.Minimized;
            }
        }

        private void OnClosed(object sender, System.EventArgs e)
        {
            try
            {
                if (notifyIcon != null)
                {
                    notifyIcon.Visible = false;
                    notifyIcon.Dispose();
                    notifyIcon = null;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Debug.WriteLine(ex.StackTrace);
            }
        }

        void OnNotifyIconClick(object sender, EventArgs e)
        {
            ShowQuickLaunchMenu();
        }

        void OnNotifyIconDoubleClick(object sender, EventArgs e)
        {

        }


        private void ShowQuickLaunchMenu()
        {
            quickLaunchMenu.IsOpen = true;
            quickLaunchMenu.replaceMenuAtCursorPosition();
        }

        private void RemotelyClickWebPage_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.remotely.click");
        }
    }

}