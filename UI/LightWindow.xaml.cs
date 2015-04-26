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
    /// This Window is deigned to graphically display what's going on in a nicer way than the console.
    /// It's not really the best separation of concerns to have the Model (Light) update the View though...
    /// </summary>
    public partial class LightWindow : Window
    {
        
        public LightWindow()
        {
            InitializeComponent();
        }

        internal void UpdateVisual(bool state)
        {
            if (state)
            {
                OnImg.Visibility = Visibility.Visible;
                OffImg.Visibility = Visibility.Collapsed;
            }
            else
            {
                OffImg.Visibility = Visibility.Visible;
                OnImg.Visibility = Visibility.Collapsed;
            }
        }
    }
}
