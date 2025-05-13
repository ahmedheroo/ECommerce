using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace BaytonicECommerce.Helper
{
    public class Helpers
    {
        public HttpClient Initiate()
        {
            var Client = new HttpClient();
            Client.BaseAddress = new Uri("http://localhost:22908/");
            var contentType = new MediaTypeWithQualityHeaderValue
("application/json");
            Client.DefaultRequestHeaders.Accept.Add(contentType);
            return Client;
            //var client = new HttpClient();
            //client.BaseAddress = new Uri("http://dnp8282-001-site1.htempurl.com/");
            //return client;
        }
    }
}
