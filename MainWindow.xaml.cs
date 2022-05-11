using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
            foreach (Greenhouse house in controllerModule.green1490.greenhouseList)
            {
                TabItem newTabItem = new TabItem {
                    Header = house.description,
                    Name = "tab" + house.ghId
                };
                TextBlock basicData = new TextBlock();
                basicData.Text = "ÜVEGHÁZ ADATAI" + "\n"
                    + "ID: " + house.ghId.ToString() + "\n"
                    + "Minimális hőmérséklet: " + house.temperature_min.ToString() + "\n"
                    + "Optimális hőmérséklet: " + house.temperature_opt + "\n"
                    + "Minimális páratartalom: " + house.humidity_min + "\n"
                    + "Térfogat: " + house.volume.ToString();

                Grid tabGrid = new Grid();
                RowDefinition rowDefinition = new RowDefinition();
                rowDefinition.Height = new GridLength(1.0, GridUnitType.Star);
                tabGrid.RowDefinitions.Add(rowDefinition);
                tabGrid.Children.Add(basicData);
                newTabItem.Content = tabGrid;
                tbcDataDisplay.Items.Add(newTabItem);
                Thread monitorThread = new Thread(new ParameterizedThreadStart(LiveServiceForHouse));
                monitorThread.Start(house.ghId);
            }
        }

        private void LiveServiceForHouse(object greenHouseIDparam)
        {
            string greenHouseID = (string)greenHouseIDparam;
            SensorData actualData;
            int result = controllerModule.MonitorAndControlHouse(greenHouseID, out actualData);
        }
    }
}
