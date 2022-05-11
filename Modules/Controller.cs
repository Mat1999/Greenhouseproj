using System;
using System.Collections.Generic;
using System.Text;

namespace Greenhouseproj
{
    class Controller
    {
        GreenHouseList green1490;

        public Controller()
        {
            Loader greenLoader = new Loader();
            //green1490 = greenLoader.loadGreenHouses();
        }

        public string TesztReturn()
        {
            Loader greenLoader = new Loader();
            return greenLoader.Teszt();
        }
    }
}
