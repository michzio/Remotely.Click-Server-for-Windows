using Remotely.Click_Server_for_Windows.Model;
using Remotely.Click_Server_for_Windows.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Remotely.Click_Server_for_Windows.View
{
    public class ServerStatusMenuItem : MenuItem 
    {

        #region ServerStatusChange event 
        public delegate void ShouldServerStatusChangeHandler(ServerStatus from, ServerStatus to);

        public event ShouldServerStatusChangeHandler ShouldServerStatusChange; 

        protected void OnShouldServerStatusChange(ServerStatus from, ServerStatus to)
        {
            if(ShouldServerStatusChange != null)
            {
                ShouldServerStatusChange(from, to); 
            }
        }

        #endregion

        // Server status menu item related controls
        StackPanel stackPanel; 
        Image statusImage;
        TextBlock statusTextBlock;
        Button statusButton;

        // Server settings model 
        public ServerStatus currentServerStatus; 
        public ServerSettings serverSettings; 
        
        public ServerStatusMenuItem() : base()
        {
            createServerStatusMenuItemView();
            serverSettings = ServerSettings.shared();
            serverSettings.ServerStatusChanged += ServerStatusChanged_ServerSettings;
            reloadServerStatus(); 
        }

        private void reloadServerStatus()
        {
            switch(serverSettings.ServerStatus)
            {
                case ServerStatus.Down:
                    setServerStatusDown();
                    break;
                case ServerStatus.Starting:
                    setServerStatusStarting();
                    break;
                case ServerStatus.Running:
                    setServerStatusRunning();
                    break;
                default:
                    Console.WriteLine("Incorrect server status value!");
                    break;
            }

        }

        public void setServerStatusDown()
        {
            this.statusTextBlock.Text = "Server down";
            this.statusImage.Source = ServerStatus.Down.IconBitmapImage(); // ServerStatusMethods.IconBitmap(ServerStatusMethods.RED_ICON_PATH);
            this.currentServerStatus = ServerStatus.Down;
            this.statusButton.Content = "Start";
        }

        public void setServerStatusStarting()
        {
            this.statusTextBlock.Text = "Server starting...";
            this.statusImage.Source = ServerStatus.Starting.IconBitmapImage(); // ServerStatusMethods.IconBitmap(ServerStatusMethods.ORANGE_ICON_PATH);
            this.currentServerStatus = ServerStatus.Starting;
            this.statusButton.Content = "Restart";
        }

        public void setServerStatusRunning()
        {
            this.statusTextBlock.Text = "Server running";
            this.statusImage.Source = ServerStatus.Running.IconBitmapImage(); // ServerStatusMethods.IconBitmap(ServerStatusMethods.GREEN_ICON_PATH);
            this.currentServerStatus = ServerStatus.Running;
            this.statusButton.Content = "Stop";
        }

        private void createServerStatusMenuItemView()
        {
            stackPanel = new StackPanel() { Orientation = Orientation.Horizontal };
            statusImage = new Image() { Source = ServerStatus.Down.IconBitmapImage() 
                /* ServerStatusMethods.IconBitmap(ServerStatusMethods.RED_ICON_PATH) */, Width = 16, Height = 16 };
            statusTextBlock = new TextBlock() { Text = "Server down" };
            var margin = statusTextBlock.Margin;
            margin.Left = 20;
            margin.Top = 20;
            margin.Bottom = 20;
            statusTextBlock.Margin = margin;
            statusButton = new Button() { Content = "Start", Width = 100 };
            margin = statusButton.Margin;
            margin.Left = 30;
            margin.Top = 20;
            margin.Bottom = 20;
            margin.Right = 20;
            statusButton.Margin = margin;
            statusButton.Click += ServerStatusButton_Click;
            stackPanel.Children.Add(statusImage);
            stackPanel.Children.Add(statusTextBlock);
            stackPanel.Children.Add(statusButton);

            this.Header = stackPanel;
        }

        private void ServerStatusButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Console.WriteLine("Server Status Button clicked.");

            switch (currentServerStatus)
            {
                case ServerStatus.Down:
                    // change to starting serverc
                    OnShouldServerStatusChange(ServerStatus.Down, ServerStatus.Starting); 
                    setServerStatusStarting();
                    break;
                case ServerStatus.Running:
                    // change to stopped server  
                    OnShouldServerStatusChange(ServerStatus.Running, ServerStatus.Down);
                    setServerStatusDown();
                    break;
                case ServerStatus.Starting:
                    // try to restart server 
                    OnShouldServerStatusChange(ServerStatus.Running, ServerStatus.Running);
                    restartServer();
                    break;

            }
        }


        private async void restartServer()
        {
            setServerStatusDown();
            await Task.Delay(1000);
            setServerStatusStarting(); 
        }

        public void ServerStatusChanged_ServerSettings(ServerStatus from, ServerStatus to)
        {
            Console.WriteLine("ServerStatusMenuItem has detected change of ServerStatus in ServerSettings"); 

            switch(to)
            {
                case ServerStatus.Down:
                    setServerStatusDown();
                    break;
                case ServerStatus.Running:
                    setServerStatusRunning();
                    break;
                case ServerStatus.Starting:
                    setServerStatusStarting();
                    break;
            }

        }
    }
}
