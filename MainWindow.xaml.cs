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
        int refreshSecs;
        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void btnConnect_Click(object sender, RoutedEventArgs e)
        {
            bool refreshValid = int.TryParse(txbRefreshRate.Text,out refreshSecs);
            if (refreshValid)
            {
                controllerModule = new Controller();
                if (controllerModule.green1490.greenhouseList != null)
                {
                    foreach (Greenhouse gHouse in controllerModule.green1490.greenhouseList)
                    {
                        AddTabItemToHouse(gHouse);
                        Thread monitorThread = new Thread(new ParameterizedThreadStart(LiveServiceForHouse));
                        monitorThread.IsBackground = true;
                        monitorThread.Start(gHouse.ghId);
                    }
                }
            }
            else
            {
                MessageBox.Show("Adjon meg egy szabályos frissítési rátát!", "Érvénytelen frissítési ráta", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void LiveServiceForHouse(object greenHouseIDparam)
        {
            string greenHouseID = (string)greenHouseIDparam;
            SensorData actualData = new SensorData() ;
            int result = 0;
            while (result == 0)
            {
                result = controllerModule.MonitorAndControlHouse(greenHouseID, out actualData);
                if (result == 0)
                {
                    this.Dispatcher.Invoke(() =>
                    {
                        
                        object refreshTab = tbcDataDisplay.FindName("tab" + greenHouseID);
                        if (refreshTab is TabItem)
                        {
                            object refreshGrid = ((TabItem)refreshTab).FindName("grid" + greenHouseID);
                            if (refreshGrid is Grid)
                            {
                                object refreshObject = ((Grid)refreshGrid).FindName("txbAct" + greenHouseID);
                                if (refreshObject is TextBlock)
                                {
                                    TextBlock refreshText = refreshObject as TextBlock;
                                    refreshText.Text = "Token: " + actualData.token + "\n"
                                        + "Aktuális hőmérséklet: " + actualData.temperature_act.ToString() + "\n"
                                        + "Aktuális páratartalom: " + actualData.humidity_act.ToString() + "\n"
                                        + "Locsoló bekapcsolva: " + actualData.sprinkler_on.ToString() + "\n"
                                        + "Bojler bekapcsolva: " + actualData.boiler_on.ToString();
                                }
                            }
                        }
                                                                        
                    });
                }
                Thread.Sleep(TimeSpan.FromSeconds(refreshSecs));
            }
        }

        private void AddTabItemToHouse(Greenhouse house)
        {
            TabItem newTabItem = new TabItem
            {
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
            TextBlock actualDataField = new TextBlock
            {
                Name = "txbAct" + house.ghId
            };
            actualDataField.Text = "-----";
            Grid tabGrid = new Grid {
                Name = "grid" + house.ghId
            };
            RowDefinition rowDefinition = new RowDefinition();
            rowDefinition.Height = new GridLength(1.0, GridUnitType.Star);
            RowDefinition rowDefinition2 = new RowDefinition();
            rowDefinition2.Height = new GridLength(1.0, GridUnitType.Star);
            tabGrid.RowDefinitions.Add(rowDefinition);
            tabGrid.RowDefinitions.Add(rowDefinition2);
            tabGrid.Children.Add(basicData);
            tabGrid.Children.Add(actualDataField);
            Grid.SetRow(basicData, 0);
            Grid.SetRow(actualDataField, 1);
            newTabItem.Content = tabGrid;
            tbcDataDisplay.Items.Add(newTabItem);
            tbcDataDisplay.RegisterName(newTabItem.Name, newTabItem);
            newTabItem.RegisterName(tabGrid.Name, tabGrid);
            tabGrid.RegisterName(actualDataField.Name, actualDataField);

        }
    }
}
