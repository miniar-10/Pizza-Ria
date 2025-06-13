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
                name = "test1",
                hashedPassword = "123",
                address=""
            }), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/api/auth/register", payload);

            var responseBody = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Status: {(int)response.StatusCode}\nBody: {responseBody}");
            }

            response.EnsureSuccessStatusCode();
        }
    }

}
