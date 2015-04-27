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
    /// This Window is deigned to allow some control for the developer to manually send Packets from the example.
    /// This is perhaps more intuitive than having a seperate thread that simulates a simple-minded individual 
    /// who delights in flicking switches all day!
    /// </summary>
    public partial class LightSwitchWindow : Window
    {
        internal event EventHandler<SwitchFlickedEventArgs> SwitchFlicked;
        
        public LightSwitchWindow()
        {
            InitializeComponent();
        }

        private void BtnLightSwitch_Checked(object sender, RoutedEventArgs e)
        {
            if (SwitchFlicked != null)
            {
                SwitchFlicked(this, new SwitchFlickedEventArgs(true));
                BtnLightSwitch.Content = "On";
            }
        }

        private void BtnLightSwitch_Unchecked(object sender, RoutedEventArgs e)
        {
            if (SwitchFlicked != null)
            {
                SwitchFlicked(this, new SwitchFlickedEventArgs(false));
                BtnLightSwitch.Content = "Off";
            }
        }
      
    }

    internal class SwitchFlickedEventArgs :EventArgs
    {
        public readonly bool SwitchState;
        internal SwitchFlickedEventArgs(bool State)
        {
            this.SwitchState = State;
        }
    }
}
