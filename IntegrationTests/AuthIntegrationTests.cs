using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTests
{
    public class AuthIntegrationTests : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client;

        public AuthIntegrationTests(CustomWebApplicationFactory factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Register_ReturnsSuccess()
        {
            var payload = new StringContent(JsonConvert.SerializeObject(new
            {
                Name = "test",
                Password = "123"
            }), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/api/auth/register", payload);
            response.EnsureSuccessStatusCode();
        }
    }

}
