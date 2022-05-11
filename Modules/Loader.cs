using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Greenhouseproj
{
    class Loader : ILoader
    {
        HttpClient loaderClient;
        public GreenHouseList loadGreenHouses()
        {
            string responseJson;
            var taskString = loaderClient.GetStringAsync("http://193.6.19.58:8181/greenhouse");
            responseJson = taskString.Result;
            return new GreenHouseList();
        }

        public string Teszt()
        {
            string responseJson;
            var taskString = loaderClient.GetStringAsync("http://193.6.19.58:8181/greenhouse");
            responseJson = taskString.Result;
            return responseJson;
        }


        public Loader()
        {
            loaderClient = new HttpClient();
        }
    }
}
