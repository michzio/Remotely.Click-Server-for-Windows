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
        private System.Windows.Forms.NotifyIcon notifyIcon = null;
        private Dictionary<string, System.Drawing.Icon> iconHandles = null;

        public MainWindow()
        {
            InitializeComponent();
        }

        protected override void OnInitialized(EventArgs e)
        {
            iconHandles = new Dictionary<string, System.Drawing.Icon>();
            string bitmapPath = System.IO.Path.Combine(Environment.CurrentDirectory, @"Icons\ic_speaker_phone_3x.png");
            var bitmap = new Bitmap(bitmapPath);
            var iconHandle = bitmap.GetHicon();
            iconHandles.Add("QuickLaunch", System.Drawing.Icon.FromHandle(iconHandle) ); 

            notifyIcon = new System.Windows.Forms.NotifyIcon();
            notifyIcon.Click += new EventHandler(OnNotifyIconClick);
            notifyIcon.DoubleClick += new EventHandler(OnNotifyIconDoubleClick);
            notifyIcon.Icon = iconHandles["QuickLaunch"];

            Loaded += OnLoaded;
            StateChanged += OnStateChanged;
            Closing += OnClosing;
            Closed += OnClosed;

            base.OnInitialized(e);
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            notifyIcon.Visible = true;
            notifyIcon.ShowBalloonTip(1, "Remotely.Click", "Server is starting...", System.Windows.Forms.ToolTipIcon.Info);
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
            View.QuickLaunchMenu menu = new View.QuickLaunchMenu(
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
            menu.IsOpen = true;
        }

    }

}