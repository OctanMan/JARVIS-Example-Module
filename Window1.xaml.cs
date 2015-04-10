using ExampleJARVIS.Objects;
using System;
using System.Collections.Generic;
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

namespace ExampleJARVIS
{
    /// <summary>
    /// This Window is deigned to allow some control for the developer to manually send
    /// Packets from the example. This is better than having a seperate thread that simulates
    /// a simple individual who delights in flicking switches all day
    /// </summary>
    public partial class Window1 : Window
    {
        private LightSwitch demoSwitch;
        
        public Window1()
        {
            InitializeComponent();
        }
 
        internal void addDemoSwitch(LightSwitch switchForDemo)
        {
            //A quick and dumb way to get a LightSwitch Reference
            demoSwitch = switchForDemo;
        }

        private void BtnLightSwitch_Checked(object sender, RoutedEventArgs e)
        {
           //TODO: Get a PacketSender to successfully send to the Router!
            demoSwitch.SendPacket();
        }

        private void BtnLightSwitch_Unchecked(object sender, RoutedEventArgs e)
        {

        }


    }
}
