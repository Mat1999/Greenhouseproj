using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Newtonsoft.Json;

namespace Greenhouseproj
{
    class Loader : ILoader
    {
        HttpClient loaderClient;
        

        public GreenHouseList loadGreenHouses()
        {
            string responseJson;
            GreenHouseList result = new GreenHouseList();
            MessageBoxResult retryResult = MessageBoxResult.Yes;
            while (retryResult == MessageBoxResult.Yes) {
                try
                {
                    var taskString = loaderClient.GetStringAsync("http://193.6.19.58:8181/greenhouse");
                    responseJson = taskString.Result;
                    result = JsonConvert.DeserializeObject<GreenHouseList>(responseJson);
                    retryResult = MessageBoxResult.No;
                }
                catch
                {
                    retryResult = MessageBox.Show("Hiba történt a kapcsolódás közben. Újrapróbálkozik?","Kapcsolódási hiba",MessageBoxButton.YesNo,MessageBoxImage.Error);
                }
            }
            
            return result;
        }

        public Loader()
        {
            loaderClient = new HttpClient();
        }
    }
}
