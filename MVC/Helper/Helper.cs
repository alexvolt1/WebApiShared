using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using MVC.Utility;

namespace MVC.Helper
{
    public class ApiHelper
    {
        private IConfigurationRoot _configuration;
        public HttpClient Initial()
        {
            var Client = new HttpClient();
            Client.BaseAddress = new Uri(SD.ClientBaseAddress);
            return Client;
        }
    }
}
