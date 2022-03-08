using CoreLoyalty.F5Seconds.Application.DTOs.F5seconds;
using CoreLoyalty.F5Seconds.ChannelUpdateState.Interfaces;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CoreLoyalty.F5Seconds.ChannelUpdateState.Repository
{
    public class VietCapitalHttpClientRepository : IVietCapitalHttpClientService
    {
        private readonly HttpClient _client;
        private readonly ILogger<VietCapitalHttpClientRepository> _logger;
        public VietCapitalHttpClientRepository(HttpClient client, ILogger<VietCapitalHttpClientRepository> logger)
        {
            _client = client;
            _logger = logger;
        }
        public async Task PostVietCapitalStateUpdate(ChannelUpdateStateDto payload)
        {
            var content = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("/v1/transaction/update-state", content);
            _logger.LogInformation($"{response.IsSuccessStatusCode}");
        }
    }
}
