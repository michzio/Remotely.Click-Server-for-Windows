using Remotely.Click_Server_for_Windows.Support;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Remotely.Click_Server_for_Windows.Model
{
    public enum ServerStatus
    {
        Down,
        Running,
        Starting,
    }

    public static class ServerStatusMethods
    {
        public const string RED_ICON_PATH = "Icons/red-icon.png";
        public const string ORANGE_ICON_PATH = "Icons/orange-icon.png";
        public const string GREEN_ICON_PATH = "Icons/green-icon.png";

        public static BitmapImage ImageSource(this ServerStatus status)
        {
            switch (status)
            {
                case ServerStatus.Down:
                    return ImageHelpers.BitmapToImageSource(Properties.Resources.RedIcon);
                case ServerStatus.Starting:
                    return ImageHelpers.BitmapToImageSource(Properties.Resources.OrangeIcon);
                case ServerStatus.Running:
                    return ImageHelpers.BitmapToImageSource(Properties.Resources.GreenIcon);
            }

            return null;
        }

        public static BitmapImage IconBitmapImage(this ServerStatus status)
        {
            switch(status) {
                case ServerStatus.Down:
                    return IconBitmapImage(RED_ICON_PATH); 
                case ServerStatus.Starting:
                    return IconBitmapImage(ORANGE_ICON_PATH);
                case ServerStatus.Running:
                    return IconBitmapImage(GREEN_ICON_PATH); 
            }

            return null; 
        }

        public static BitmapImage IconBitmapImage(string iconPath)
        {
            var absoluteIconPath = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, iconPath);
            Uri statusIconUri = new Uri(absoluteIconPath, UriKind.Absolute);
            BitmapImage statusIconBitmap = new BitmapImage(statusIconUri);
            return statusIconBitmap;
        }
    }
}
