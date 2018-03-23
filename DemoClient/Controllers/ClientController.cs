using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Pivotal.Discovery.Client;
//using Steeltoe.Common.Discovery;

namespace DemoClient.Controllers
{
    [Produces("application/json")]
    [Route("api/Client")]
    public class ClientController : Controller
    {
        private readonly IDiscoveryClient _discoveryClient;
        private readonly HttpClient _httpClient;
        private string ServiceURL;

        public ClientController(IDiscoveryClient client, IConfiguration config)
        {
            _discoveryClient = client;
            _httpClient = new HttpClient(new DiscoveryHttpClientHandler(_discoveryClient));
            ServiceURL = config.GetSection("ServiceConfig:URI").Value;
        }

        public async Task<string> Get()
        {
            var response = await _httpClient.GetAsync($"{ServiceURL}/api/Values");
            return await response.Content.ReadAsStringAsync();
        }
    }
}