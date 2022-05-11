using System;
using System.Collections.Generic;
using System.Text;

namespace Greenhouseproj
{
    interface IMonitor
    {
        public SensorData getSensorData(string ghId);
    }
}
