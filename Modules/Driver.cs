﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Greenhouseproj
{
    class Driver : IDriver
    {
        HttpClient driverClient;

        public int sendCommand(Greenhouse gh, string token, double boilerValue, double sprinklerValue)
        {
            Command commandToSend = new Command();
            string destinationUrl = "http://193.6.19.58:8181/greenhouse/" + token;
            commandToSend.ghId = gh.ghId;
            if (boilerValue == 0.0)
            {
                commandToSend.boilerCommand = "";
            }
            else
            {
                commandToSend.boilerCommand = "bup" + Math.Round(boilerValue).ToString() + "c";
            }
            if (sprinklerValue == 0.0)
            {
                commandToSend.sprinklerCommand = "";
            }
            else
            {
                commandToSend.sprinklerCommand = "son" + Math.Round(sprinklerValue).ToString() + "l";
            }
            StringContent messageContent = new StringContent(commandToSend.ToString());
            messageContent.Headers.ContentType = new MediaTypeHeaderValue("text/plain");
            var response = driverClient.PostAsync(destinationUrl, messageContent).Result.Content.ReadAsStringAsync().Result;
            return int.Parse(response);
        }

        public Driver()
        {
            driverClient = new HttpClient();
        }
    }
}
