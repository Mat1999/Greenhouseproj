using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

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
            GreenHouseList result = JsonConvert.DeserializeObject<GreenHouseList>(responseJson);
            return result;
        }

        public Loader()
        {
            loaderClient = new HttpClient();
        }
    }
}
