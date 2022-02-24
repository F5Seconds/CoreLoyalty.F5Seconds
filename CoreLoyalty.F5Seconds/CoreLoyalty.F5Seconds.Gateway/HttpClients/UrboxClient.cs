using System.Net.Http;
using System.Threading.Tasks;

namespace CoreLoyalty.F5Seconds.Gateway.HttpClients
{
    public class UrboxClient: IUrboxClient
    {
        private readonly HttpClient _client;
        public UrboxClient(HttpClient client)
        {
            _client = client;
        }

        public async Task<HttpResponseMessage> VoucherListAsync()
        {
            var response = await _client.GetAsync("/api/gift/lists");
            return response;
        }
    }

    public interface IUrboxClient
    {
        Task<HttpResponseMessage> VoucherListAsync();
    }
}
