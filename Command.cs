using System;
using System.Collections.Generic;
using System.Text;

namespace Greenhouseproj
{
    class Command
    {
        public string ghId { get; set; }
        public string boilerCommand { get; set; }
        public string sprinklerCommand { get; set; }

        /*
         * {
                "ghId": "KFI3EW45RD",
                "boilerCommand":"bup5c",
                "sprinklerCommand":"son224l"
           }
         * */
        public override string ToString()
        {
            string result = "";
            //result += "content-type:text/plain;\n";
            result += "{\n";
            result += "\t" + '"' + "ghId" + '"' + ": " + '"' + "" + ghId + '"' + "," + "\n";
            result += "\t" + '"' + "boilerCommand" + '"' + ":" + '"' + "" + boilerCommand + '"' + "," + "\n";
            result += "\t" + '"' + "sprinklerCommand" + '"' + ":" + '"' + "" + sprinklerCommand + '"' + "\n";
            result += "}";
            return result;
        }
    }
}
