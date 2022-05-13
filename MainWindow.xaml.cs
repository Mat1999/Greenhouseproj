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
        Dictionary<int, string> dictErrorCode;
        public MainWindow()
        {
            InitializeComponent();
            dictErrorCode = new Dictionary<int, string>();
            dictErrorCode[100] = "A parancs végrehajtásra került!";
            dictErrorCode[101] = "Hibás kalkuláció!";
            dictErrorCode[102] = "Parancs került kiküldésre egy éppen parancsot végrehajtó eszköznek!";
            dictErrorCode[103] = "Hibás parancs került kiküldésre a kazánnak!";
            dictErrorCode[104] = "Hibás parancs került kiküldésre a locsolónak!";
            dictErrorCode[105] = "Az üzenetben lévő token nem érvényes!";
            dictErrorCode[106] = "Az üzenetben szereplő üvegház nem található!";
            dictErrorCode[107] = "Általános üzenet feldolgozási hiba!";
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
                        monitorThread.Start(gHouse);
                    }
                }
            }
            else
            {
                MessageBox.Show("Adjon meg egy szabályos frissítési rátát!", "Érvénytelen frissítési ráta", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void LiveServiceForHouse(object greenHouseParam)
        {
            Greenhouse liveHouse = (Greenhouse)greenHouseParam;
            string greenHouseID = liveHouse.ghId;
            SensorData actualData = new SensorData();
            int serviceOn = 0;
            while (serviceOn == 0)
            {
                int result = controllerModule.MonitorAndControlHouse(liveHouse, out actualData);
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
                                        + "Bojler bekapcsolva: " + actualData.boiler_on.ToString() + "\n";
                                    try
                                    {
                                        refreshText.Text += "Válasz a szervertől: " + dictErrorCode[result];
                                    }
                                    catch
                                    {
                                        refreshText.Text += "Válasz a szervertől: " + result.ToString();
                                    }
                                }
                            }
                        }
                                                                        
                    });
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
