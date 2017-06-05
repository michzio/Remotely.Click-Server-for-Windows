using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Remotely.Click_Server_for_Windows
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            Console.WriteLine("Remotely.Click Server app starting...");
            Console.WriteLine(System.Reflection.Assembly.GetExecutingAssembly().GetName().Name);
           
        }

    }
    
}
