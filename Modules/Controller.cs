using System;
using System.Collections.Generic;
using System.IO;
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

        public int MonitorAndControlHouse(Greenhouse greenHouse, out SensorData actData)
        {
            Monitor greenMonitor = new Monitor();
            SensorData actualData = greenMonitor.getSensorData(greenHouse.ghId);
            Driver greenDriver = new Driver(); 
            actData = actualData;
            double boilerNumber = 0.0;
            double sprinklerNumber = 0.0;
            bool boilerError = false;
            bool sprinklerError = false;
            if (!actualData.boiler_on && !actualData.sprinkler_on)
            {
                boilerNumber = CalculateBoiler(actualData.temperature_act, greenHouse.temperature_min, greenHouse.temperature_opt, out boilerError);
                sprinklerNumber = CalculateSprinkler(actualData.humidity_act, greenHouse.humidity_min, greenHouse.volume, out sprinklerError);
            }
            if (boilerError || sprinklerError)
            {
                string path = System.AppContext.BaseDirectory + "\\log_" + greenHouse.ghId + "_" + DateTime.Now.Year.ToString() + "_" + DateTime.Now.Month.ToString() + "_" + DateTime.Now.Day.ToString() + "_" + DateTime.Now.Hour.ToString() + "_" + DateTime.Now.Minute.ToString() + "_" + DateTime.Now.Second.ToString() + "_" + DateTime.Now.Millisecond.ToString() + ".txt";
                FileStream stream = new FileStream(path, FileMode.Create);
                StreamWriter writer = new StreamWriter(stream);

                writer.Close();
                stream.Close();
            }
            int responseFromServer = greenDriver.sendCommand(greenHouse, actualData.token, boilerNumber, sprinklerNumber);
            return responseFromServer;
        }

        private double CalculateBoiler(double actualTemp, int minTemp, int optTemp, out bool error)
        {
            error = false;
            if (actualTemp < minTemp)
            {
                if (actualTemp > minTemp - 5)
                {
                    double rightTemp = optTemp - actualTemp;
                    return rightTemp;
                }
                else
                {
                    error = true;
                }
            }
            return 0.0;
        }

        private double CalculateSprinkler(double actualHum, int minHum, int houseVolume, out bool error)
        {
            error = false;
            return 0.0;
        }
    }
}
