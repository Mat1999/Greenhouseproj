using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace Greenhouseproj
{
    class Controller
    {
        GreenHouseList green1490;

        public Controller()
        {
            Loader greenLoader = new Loader();
            green1490 = greenLoader.loadGreenHouses();
            MessageBox.Show(green1490.greenhouseList[0].ghId + ", " + green1490.greenhouseList[0].description, "Áh");
        }

        
    }
}
