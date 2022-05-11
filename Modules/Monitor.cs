using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using System.Text;

namespace Greenhouseproj
{
    class Monitor : IMonitor
    {
        public SensorData getSensorData(string ghId)
        {
            return new SensorData();
        }
    }
}
