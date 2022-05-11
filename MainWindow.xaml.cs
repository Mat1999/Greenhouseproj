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

namespace Greenhouseproj
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Controller controllerModule;
        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void btnConnect_Click(object sender, RoutedEventArgs e)
        {
            controllerModule = new Controller();
            txbTeszt.Text = controllerModule.TesztReturn();
        }
    }
}
