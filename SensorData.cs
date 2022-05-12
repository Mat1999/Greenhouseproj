using System;
using System.Collections.Generic;
using System.Text;

namespace Greenhouseproj
{
    class SensorData
    {
        public string ghId { get; set; }
        public string token { get; set; }
        public double temperature_act { get; set; }
        public double humidity_act { get; set; }
        public bool boiler_on { get; set; }
        public bool sprinkler_on { get; set; }
    }
}
