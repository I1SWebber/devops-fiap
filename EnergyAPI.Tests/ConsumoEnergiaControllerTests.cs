using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace EnergyAPI.Tests
{
    public class ConsumoEnergiaControllerTests
    {
        private readonly HttpClient _client;

        public ConsumoEnergiaControllerTests()
        {
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
            };

            _client = new HttpClient(handler)
            {
                BaseAddress = new Uri("http://localhost:32787")
            };
        }

        [Fact]
        public async Task Get_ReturnsHttpStatusCode200()
        {
            var response = await _client.GetAsync("/api/ConsumoEnergia");

            response.EnsureSuccessStatusCode(); // Verifica se o status code é 200
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
