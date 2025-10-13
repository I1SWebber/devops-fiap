using System;
using System.Net;
using System.Net.Http;
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
                ServerCertificateCustomValidationCallback =
                    (message, cert, chain, errors) => true
            };

            _client = new HttpClient(handler)
            {
                BaseAddress = new Uri("http://localhost:32787")
            };
        }

        // ✅ O [Fact] fica no MÉTODO de teste (e aqui está pulado no CI)
        [Fact(Skip = "Ignorado no CI: não sobe API local durante o pipeline")]
        public async Task Get_ReturnsHttpStatusCode200()
        {
            var response = await _client.GetAsync("/api/ConsumoEnergia");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
