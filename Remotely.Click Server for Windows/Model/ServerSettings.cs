using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Remotely.Click_Server_for_Windows.Model
{
    [Serializable]
    public class ServerSettings
    {
        private static readonly string DEFAULT_FILE_NAME = "server_settings.xml";
        private static readonly string DEFAULT_DIR_PATH = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Settings");
        private static readonly string DEFAULT_FILE_PATH = Path.Combine(DEFAULT_DIR_PATH, DEFAULT_FILE_NAME);

        private ServerStatus _serverStatus;
        private bool _isDevicesAutoDiscoverActive; 

        // runtime settings, do not serialize to XML
        [XmlIgnore]
        public ServerStatus ServerStatus
        {
            get { return this._serverStatus;  }
            set {
                OnServerStatusChanged(_serverStatus, value); 
                _serverStatus = value;
            }
        }
        [XmlIgnore]
        public bool IsDevicesAutoDiscoverActive
        {
            get { return this._isDevicesAutoDiscoverActive;  }
            set {
                _isDevicesAutoDiscoverActive = value;
                OnDevicesAutoDiscoveryChanged(value);
            }
        }

        // persistent settings, serialize to XML
        public bool ShouldApplicationAutostart { get; set; } 
        public bool ShouldServerAutostartWithApp { get; set; }

        public bool ShouldAutoDiscoverDevices { get; set; }
        public string DeviceDiscoverableName { get; set; }

        public string ServerIpAddress { get; set; }
        public string ServerPortNumber { get; set; }
        public bool ShouldUseCustomPortNumber { get; set; }

        public bool ShouldUseSecurityPassword { get; set; }
        public string SecurityPassword { get; set; }

        private ServerSettings()
        {
            // set default values 
            ServerStatus = ServerStatus.Down;
            IsDevicesAutoDiscoverActive = false;

            ShouldApplicationAutostart = true;
            ShouldServerAutostartWithApp = true;

            ShouldAutoDiscoverDevices = true;
            DeviceDiscoverableName = "";

            ServerIpAddress = "0.0.0.0";
            ServerPortNumber = "0";
            ShouldUseCustomPortNumber = false;

            ShouldUseSecurityPassword = false;
            SecurityPassword = ""; 

        }

        private static ServerSettings sharedServerSettings; 

        public static ServerSettings shared()
        {
            if(sharedServerSettings == null)
            {
                sharedServerSettings = new ServerSettings();
                sharedServerSettings.Load();
            }
            
            return sharedServerSettings; 
        }

        public bool IsServerActive()
        {
            if(ServerStatus == ServerStatus.Running || ServerStatus == ServerStatus.Starting)
            {
                return true; 
            }

            return false;
        }

        public bool IsServerRunning()
        {
            if(ServerStatus == ServerStatus.Running)
            {
                return true; 
            }
            return false;
        }

        public void Save()
        {
            Save(DEFAULT_FILE_PATH);
        }

        public void Save(string filePath)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(filePath));

            using (StreamWriter writer = new StreamWriter(filePath))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(ServerSettings));
                serializer.Serialize(writer, this);

                OnServerSettingsChanged(this); 
            }
        }

        public ServerSettings Load()
        {
            return Load(DEFAULT_FILE_PATH);
        }

        public ServerSettings Load(string filePath)
        {
            if (!File.Exists(filePath))
            {
                Save(); 
            }
            using (StreamReader reader = new StreamReader(filePath))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(ServerSettings));
                var newSettings = serializer.Deserialize(reader) as ServerSettings;
                updateWith(newSettings); 
                return this; 
            }
        }

        private void updateWith(ServerSettings newSettings)
        {
            var propInfo = newSettings.GetType().GetProperties();
            foreach (var item in propInfo)
            {
                if (this.GetType().GetProperty(item.Name).GetCustomAttributes(false).Any(a => a is XmlIgnoreAttribute))
                    continue; 
                this.GetType().GetProperty(item.Name).SetValue(this, item.GetValue(newSettings, null), null);
            }
        }


        // Events informing subscribers about server settings changed
        #region ServerStatusChanged event
        public delegate void ServerStatusChangedHandler(ServerStatus from, ServerStatus to);

        public event ServerStatusChangedHandler ServerStatusChanged;

        protected void OnServerStatusChanged(ServerStatus from, ServerStatus to)
        {
            if(ServerStatusChanged != null)
            {
                ServerStatusChanged(from, to); 
            }
        }
        #endregion

        #region DevicesAutoDiscoveryChanged event
        public delegate void DevicesAutoDiscoveryChangedHandler(bool isActive);

        public event DevicesAutoDiscoveryChangedHandler DevicesAutoDiscoveryChanged;

        protected void OnDevicesAutoDiscoveryChanged(bool isActive)
        {
            if(DevicesAutoDiscoveryChanged != null)
            {
                DevicesAutoDiscoveryChanged(isActive); 
            }
        }
        #endregion

        #region ServerSettingsChanged event 
        public delegate void ServerSettingsChangedHandler(ServerSettings settings);

        public event ServerSettingsChangedHandler ServerSettingsChanged; 

        protected void OnServerSettingsChanged(ServerSettings settings)
        {
            if(ServerSettingsChanged != null)
            {
                ServerSettingsChanged(settings); 
            }
        }
        #endregion 
    }
}
