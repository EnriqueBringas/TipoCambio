using System;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TipoCambio.Api.Core.DTO;
using TipoCambio.Api.Core.Utility.Transfer.Request;
using TipoCambio.Api.Gateway.Interfaces.v1;

namespace TipoCambio.Api.Gateway.Services.v1
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly string _url;
        private readonly HttpClient _client;

        public AuthenticationService(string url)
        {
            _url = url;
            _client = new HttpClient();
        }

        public async Task<TokenRS> Generate(RequestWith<TokenRQ> request)
        {
            try
            {
                _client.DefaultRequestHeaders.Clear();

                var json = JsonSerializer.Serialize(request, new JsonSerializerOptions { IgnoreNullValues = true });

                var rs = await _client.SendAsync(new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri($"{_url}/v1/api/UsersJwt/Generate"),
                    Content = new StringContent(json, Encoding.UTF8, MediaTypeNames.Application.Json)
                });

                return JsonSerializer.Deserialize<TokenRS>(await rs.Content.ReadAsStringAsync());
            }
            catch (Exception ex)
            {
                return new TokenRS();
            }
        }
    }
}