using System;
using System.Collections.Generic;
using System.Text;

namespace Greenhouseproj
{
    class Greenhouse
    {
        public string ghId { get; set; }
        public string description { get; set; }
        public int temperature_min { get; set; }
        public int temperature_opt { get; set; }
        public int humidity_min { get; set; }
        public int volume { get; set; }
    }
}
