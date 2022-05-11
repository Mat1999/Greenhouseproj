using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using Newtonsoft.Json;

namespace Greenhouseproj
{
    class Monitor : IMonitor
    {
        HttpClient monitorClient;

        public SensorData getSensorData(string ghId)
        {
            string monitorURL = "http://193.6.19.58:8181/greenhouse/" + ghId;
            string responseJson;
            var taskString = monitorClient.GetStringAsync(monitorURL);
            responseJson = taskString.Result;
            SensorData result = JsonConvert.DeserializeObject<SensorData>(responseJson);
            return result;
        }

        public Monitor()
        {
            monitorClient = new HttpClient();
        }
    }
}
