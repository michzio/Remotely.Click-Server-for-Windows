using Remotely.Click_Server_for_Windows.Model;
using Remotely.Click_Server_for_Windows.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Remotely.Click_Server_for_Windows
{
    /// <summary>
    /// Interaction logic for PreferencesWindow.xaml
    /// </summary>
    public partial class PreferencesWindow : Window
    {

        #region ShouldServerStatusChange event 
        public delegate void ShouldServerStatusChangeHandler(ServerStatus from, ServerStatus to);

        public event ShouldServerStatusChangeHandler ShouldServerStatusChange;

        protected void OnShouldServerStatusChange(ServerStatus from, ServerStatus to)
        {
            // check if there are any subscribers 
            if (ShouldServerStatusChange != null)
            {
                // call the event 
                ShouldServerStatusChange(from, to);
            }
           
        }
        #endregion

        #region ShouldAutostartApp event 
        public delegate void ShouldAutostartAppHandler(bool flag);

        public event ShouldAutostartAppHandler ShouldAutostartApp;

        protected void OnShouldAutostartApp(bool flag)
        {
            // check if there are any subscribers 
            if(ShouldAutostartApp != null)
            {
                // call the event 
                ShouldAutostartApp(flag); 
            }
        }
        #endregion

        #region ShouldServerAutostartWithApp event
        public delegate void ShouldServerAutostartWithAppHandler(bool flag);

        public event ShouldServerAutostartWithAppHandler ShouldServerAutostartWithApp;

        protected void OnShouldServerAutostartWithApp(bool flag)
        {
            // check if there are any subscribers 
            if(ShouldServerAutostartWithApp != null)
            {
                // call the event 
                ShouldServerAutostartWithApp(flag); 
            }
        }
        #endregion

        #region ShouldStartServiceDiscovery event
        public delegate void ShouldStartServiceDiscoveryHandler(string discoverableName);

        public event ShouldStartServiceDiscoveryHandler ShouldStartServiceDiscovery; 

        protected void OnShouldStartServiceDiscovery(string discoverableName)
        {
            // check if there are any subscribers 
            if(ShouldStartServiceDiscovery != null)
            {
                // call the event 
                ShouldStartServiceDiscovery(discoverableName);
            }
        }
        #endregion

        #region ShouldStopServiceDiscovery event 
        public delegate void ShouldStopServiceDiscoveryHandler();

        public event ShouldStopServiceDiscoveryHandler ShouldStopServiceDiscovery; 

        protected void OnShouldStopServiceDiscovery()
        {
            // check if there are any subscribers 
            if(ShouldStopServiceDiscovery != null)
            {
                // call the event 
                ShouldStopServiceDiscovery();
            }
        }
        #endregion

        #region ShouldUseSecurityPassword event 
        public delegate void ShouldUseSecurityPasswordHandler(bool flag, string password);

        public event ShouldUseSecurityPasswordHandler ShouldUseSecurityPassword; 

        protected void OnShouldUseSecurityPassword(bool flag, string password)
        {
            // check if there are any subscribers 
            if(ShouldUseSecurityPassword != null)
            {
                // call the event
                ShouldUseSecurityPassword(flag, password);
            }
        }
        #endregion

        private ServerSettings ServerSettings { get; set; }

        public PreferencesWindow()
        {
            InitializeComponent();
            ServerSettings = ServerSettings.shared();

            // load current settings 
            this.ShouldAutostart.IsChecked = ServerSettings.ShouldApplicationAutostart;
            this.ShouldServerAutostart.IsChecked = ServerSettings.ShouldServerAutostartWithApp;
            this.ShouldAutoDiscoverDevices.IsChecked = ServerSettings.ShouldAutoDiscoverDevices;
            this.DeviceDiscoverableName.Text = ServerSettings.DeviceDiscoverableName;
            this.ServerIpAddress.Text = ServerSettings.ServerIpAddress;
            this.ServerPortNumber.Text = ServerSettings.ServerPortNumber;
            this.ShouldUseCustomPortNumber.IsChecked = ServerSettings.ShouldUseCustomPortNumber;
            this.PasswordBox.Password = ServerSettings.SecurityPassword;
            this.ConfirmPasswordBox.Password = ServerSettings.SecurityPassword;

            // additional configuration based on current settings 
            updateServerPortNumberTextBoxState();

            loadServerStatusInfo(); 
        }

        private void loadServerStatusInfo()
        {
            switch (ServerSettings.ServerStatus) {
                case ServerStatus.Down:
                    this.ListenButton.Content = "Start listening";
                    this.ServerStatusIcon.Source = ServerStatus.Down.IconBitmapImage(); 
                    break;
                case ServerStatus.Starting:
                    this.ListenButton.Content = "Restart";
                    this.ServerStatusIcon.Source = ServerStatus.Starting.IconBitmapImage();
                    break;
                case ServerStatus.Running:
                    this.ListenButton.Content = "Stop listening";
                    this.ServerStatusIcon.Source = ServerStatus.Running.IconBitmapImage(); 
                    break;
                default:
                    Console.WriteLine("Incorrect server status!");
                    break;
            } 
        }

        private void ShouldAutostart_CheckBoxClicked(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Should start automatically at login: " + this.ShouldAutostart.IsChecked);

            ServerSettings.ShouldApplicationAutostart = this.ShouldAutostart.IsChecked ?? false;
            ServerSettings.Save();
            // should autostart app delegate call
            OnShouldAutostartApp(this.ShouldAutostart.IsChecked ?? false);

            
            if (ServerSettings.ShouldApplicationAutostart)
            {
                SystemStartupService.AddThisAppToCurrentUserStartup(); 
            } else
            {
                SystemStartupService.DeleteThisAppFromCurrentUserStartup(); 
            }
        }

        private void ShouldServerAutostart_CheckBoxClicked(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Should start server at launch of application checkbox value: " 
                            + this.ShouldServerAutostart.IsChecked);

            ServerSettings.ShouldServerAutostartWithApp = this.ShouldServerAutostart.IsChecked ?? false;
            ServerSettings.Save();
            // should server autostart with app delegate call
            OnShouldServerAutostartWithApp(this.ShouldServerAutostart.IsChecked ?? false); 
        }

        private void ShouldAutoDiscoverDevices_CheckBoxClicked(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Should auto discover network devices checkbox value: " + this.ShouldAutoDiscoverDevices.IsChecked);

            ServerSettings.ShouldAutoDiscoverDevices = this.ShouldAutoDiscoverDevices.IsChecked ?? false;
            ServerSettings.Save(); 

            if(ServerSettings.ServerStatus ==  ServerStatus.Running)
            {
                if(ServerSettings.ShouldAutoDiscoverDevices)
                {
                    Console.WriteLine("Should start Bonjour service discovery");
                    // should start service discovery delegate call
                    OnShouldStartServiceDiscovery(this.DeviceDiscoverableName.Text);
                } else
                {
                    Console.WriteLine("Should stop Bonjour service discovery");
                    // should stop service discovery delegate call
                    OnShouldStopServiceDiscovery(); 
                }
            }
        }



        private void ShouldUseCustomPortNumber_CheckBoxClicked(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Should use custom port number checkbox value: " + this.ShouldUseCustomPortNumber.IsChecked); 
           
            ServerSettings.ShouldUseCustomPortNumber = this.ShouldUseCustomPortNumber.IsChecked ?? false;
            ServerSettings.Save();

            updateServerPortNumberTextBoxState(); 

            if(!ServerSettings.ShouldUseCustomPortNumber && ServerSettings.IsServerActive())
            {
                askToRestartServer(); 
            }
        }

        private void updateServerPortNumberTextBoxState()
        {
            if (ServerSettings.ShouldUseCustomPortNumber)
            {
                this.ServerPortNumber.IsEnabled = true;
            }
            else
            {
                this.ServerPortNumber.IsEnabled = false;
                this.ServerPortNumber.Text = "0";
            }
        }

        private void askToRestartServer()
        {
            // display modal asking user to restart server
            string messageBoxText = "Should be restarted to apply changes?";
            string messageBoxTitle = "Server is running...";
            MessageBoxButton messageBoxButton = MessageBoxButton.YesNo;
            MessageBoxImage messageBoxImage = MessageBoxImage.Question;
            MessageBoxResult result = MessageBox.Show(messageBoxText, messageBoxTitle, messageBoxButton, messageBoxImage);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    Console.WriteLine("Should restart server with new configuration.");
                    this.ListenButton.Content = "Restart";
                    this.ServerStatusIcon.Source = ServerStatus.Starting.IconBitmapImage(); 
                    // should Server Status change from Running/Starting to Starting delegate call
                    OnShouldServerStatusChange(ServerSettings.ServerStatus, ServerStatus.Starting);
                    break;
                case MessageBoxResult.No:
                    
                    break;
            }
        }

        private void ServerIpAddress_TextChanged(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Server IP address value has changed to: " + this.ServerIpAddress.Text); 
        }

        private void ServerPortNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            Console.WriteLine("Server port number value has changed to: " + this.ServerPortNumber.Text); 
        }

        private void DeviceDiscoverableName_TextChanged(object sender, TextChangedEventArgs e)
        {
            Console.WriteLine("Device discoverable name value has changed to: " + this.DeviceDiscoverableName.Text); 
        }

        private void ListenButton_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Listen button clicked with label: " + this.ListenButton.Content); 

            switch(ServerSettings.ServerStatus)
            {
                case ServerStatus.Running:
                    this.ListenButton.Content = "Start Listening";
                    this.ServerStatusIcon.Source = ServerStatus.Down.IconBitmapImage();
                    // should Server Status change from .Running to .Down delegate call
                    OnShouldServerStatusChange(ServerStatus.Running, ServerStatus.Down); ; 
                    break;
                case ServerStatus.Starting:
                    // keep starting again
                    // should Server Status change from .Starting to .Starting delegate call
                    OnShouldServerStatusChange(ServerStatus.Starting, ServerStatus.Starting);
                    break;
                case ServerStatus.Down:
                    this.ListenButton.Content = "Restart";
                    this.ServerStatusIcon.Source = ServerStatus.Starting.IconBitmapImage();
                    // should Server Status change from .Down to .Starting delegate call
                    OnShouldServerStatusChange(ServerStatus.Down, ServerStatus.Starting);
                    break;
                default:
                    Console.WriteLine("Incorrect server status!"); 
                    break;
            }
        }

        private void DeviceDiscoverableName_LostFocus(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Device discoverable name value entered: " + this.DeviceDiscoverableName.Text);

            ServerSettings.DeviceDiscoverableName = this.DeviceDiscoverableName.Text;
            ServerSettings.Save(); 

            if(ServerSettings.IsDevicesAutoDiscoverActive)
            {
                Console.WriteLine("Should restart Bonjour service discovery with new name");
                // should stop service discovery delegate call 
                OnShouldStopServiceDiscovery();
                // should start service discovery with name delegate call
                OnShouldStartServiceDiscovery(this.DeviceDiscoverableName.Text); 
            }
        }

        private void ServerIpAddress_LostFocus(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Server IP address entered: " + this.ServerIpAddress.Text); 
        }

        private void ServerPortNumber_LostFocus(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Server port number entered: " + this.ServerPortNumber.Text);

            ServerSettings.ServerPortNumber = this.ServerPortNumber.Text;
            ServerSettings.Save();

            if(ServerSettings.IsServerActive())
            {
                askToRestartServer();
            }
        }

        private void PasswordBox_LostFocus(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Password entered: " + this.PasswordBox.Password);
        }

        private void ConfirmPasswordBox_LostFocus(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Confirm Password entered: " + this.ConfirmPasswordBox.Password);
        }

        private void ApplyButton_Click(object sender, RoutedEventArgs e)
        {
            if(PasswordBox.Password != ConfirmPasswordBox.Password)
            {
                // display modal with information that passwords does not match
                string messageBoxText = "Both password field and confirm password filed should contain the same value.";
                string messageBoxTitle = "Passwords doesn't match";
                MessageBoxButton messageBoxButton = MessageBoxButton.OK;
                MessageBoxImage messageBoxImage = MessageBoxImage.Warning;
                MessageBox.Show(messageBoxText, messageBoxTitle, messageBoxButton, messageBoxImage);
                return;
            }

            if(PasswordBox.Password == "")
            {
                // display modal with information that password box left empty 
                string messageBoxText = "Password fields left empty. Please enter correct password value.";
                string messageBoxTitle = "Password empty";
                MessageBoxButton messageBoxButton = MessageBoxButton.OK;
                MessageBoxImage messageBoxImage = MessageBoxImage.Warning;
                MessageBox.Show(messageBoxText, messageBoxTitle, messageBoxButton, messageBoxImage);
                return;
            }

            Console.WriteLine("Applying new password: " + this.PasswordBox.Password);

            // if password & confirm password matches then apply it 
            ServerSettings.SecurityPassword = this.PasswordBox.Password;
            ServerSettings.ShouldUseSecurityPassword = true;
            ServerSettings.Save();

            // should use security password delegate call
            OnShouldUseSecurityPassword(true, ServerSettings.SecurityPassword);

            Console.WriteLine("Applied password: " + ServerSettings.SecurityPassword);
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            ServerSettings.SecurityPassword = "";
            ServerSettings.ShouldUseSecurityPassword = false;

            // should use security password (false) delegate call
            OnShouldUseSecurityPassword(false, ""); 

            // clear form controls 
            this.PasswordBox.Password = "";
            this.ConfirmPasswordBox.Password = "";

            Console.WriteLine("Password cleared!"); 
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Password Box value has changed to: " + this.PasswordBox.Password);
        }

        private void ConfirmPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Confirm Password Box value has changed to: " + this.ConfirmPasswordBox.Password);
        }
    }
}
