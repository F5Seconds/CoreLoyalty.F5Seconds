using System.Net.Http;

namespace CoreLoyalty.F5Seconds.Gateway.HttpClients
{
    public class GotItClient : IGotItClient
    {
        private readonly HttpClient _client;
        public GotItClient(HttpClient client)
        {
            _client = client;
        }
    }

    public interface IGotItClient
    {

    }
}
