using System;
using System.Collections.Generic;
using System.Text;

namespace Greenhouseproj
{
    class Loader : ILoader
    {

        public GreenHouseList loadGreenHouses()
        {
            return new GreenHouseList();
        }
    }
}
