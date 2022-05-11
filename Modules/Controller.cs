using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace Greenhouseproj
{
    class Controller
    {
        public GreenHouseList green1490 { get; set; }

        public Controller()
        {
            Loader greenLoader = new Loader();
            green1490 = greenLoader.loadGreenHouses();
        }

        public int MonitorAndControlHouse(string greenHouseId, out SensorData actData)
        {
            Monitor greenMonitor = new Monitor();
            SensorData actualData = greenMonitor.getSensorData(greenHouseId);
            actData = actualData;
            return 0;
        }
    }
}
