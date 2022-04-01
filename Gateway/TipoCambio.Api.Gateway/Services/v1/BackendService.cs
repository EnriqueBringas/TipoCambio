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
    public class BackendService : IBackendService
    {
        private readonly string _url;
        private readonly HttpClient _client;

        public BackendService(string url)
        {
            _url = url;
            _client = new HttpClient();
        }

        public async Task<CurrencyRS> Currency(Request request)
        {
            try
            {
                _client.DefaultRequestHeaders.Clear();

                var json = JsonSerializer.Serialize(request, new JsonSerializerOptions { IgnoreNullValues = true });

                var rs = await _client.SendAsync(new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri($"{_url}/v1/api/Currency/Select"),
                    Content = new StringContent(json, Encoding.UTF8, MediaTypeNames.Application.Json)
                });

                return JsonSerializer.Deserialize<CurrencyRS>(await rs.Content.ReadAsStringAsync());
            }
            catch (Exception ex)
            {
                return new CurrencyRS();
            }
        }

        public async Task<ChangeMoneyRS> ChangeMoney(RequestWith<ChangeMoneyRQ> request)
        {
            try
            {
                _client.DefaultRequestHeaders.Clear();

                var json = JsonSerializer.Serialize(request, new JsonSerializerOptions { IgnoreNullValues = true });

                var rs = await _client.SendAsync(new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri($"{_url}/v1/api/ExchangeRate/ChangeMoney"),
                    Content = new StringContent(json, Encoding.UTF8, MediaTypeNames.Application.Json)
                });

                return JsonSerializer.Deserialize<ChangeMoneyRS>(await rs.Content.ReadAsStringAsync());
            }
            catch(Exception ex)
            {
                return new ChangeMoneyRS();
            }
        }

        public async Task<ExchangeRS> InsertExchange(RequestWith<ExchangeRQ> request)
        {
            try
            {
                _client.DefaultRequestHeaders.Clear();

                var json = JsonSerializer.Serialize(request, new JsonSerializerOptions { IgnoreNullValues = true });

                var rs = await _client.SendAsync(new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri($"{_url}/v1/api/ExchangeRate/Insert"),
                    Content = new StringContent(json, Encoding.UTF8, MediaTypeNames.Application.Json)
                });

                return JsonSerializer.Deserialize<ExchangeRS>(await rs.Content.ReadAsStringAsync());
            }
            catch (Exception ex)
            {
                return new ExchangeRS();
            }
        }

        public async Task<ExchangeRS> UpdateExchange(RequestWith<ExchangeRQ> request)
        {
            try
            {
                _client.DefaultRequestHeaders.Clear();

                var json = JsonSerializer.Serialize(request, new JsonSerializerOptions { IgnoreNullValues = true });

                var rs = await _client.SendAsync(new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri($"{_url}/v1/api/ExchangeRate/Update"),
                    Content = new StringContent(json, Encoding.UTF8, MediaTypeNames.Application.Json)
                });

                return JsonSerializer.Deserialize<ExchangeRS>(await rs.Content.ReadAsStringAsync());
            }
            catch (Exception ex)
            {
                return new ExchangeRS();
            }
        }

        public async Task<ExchangeDeleteRS> DeleteExchange(RequestWith<ExchangeRQ> request)
        {
            try
            {
                _client.DefaultRequestHeaders.Clear();

                var json = JsonSerializer.Serialize(request, new JsonSerializerOptions { IgnoreNullValues = true });

                var rs = await _client.SendAsync(new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri($"{_url}/v1/api/ExchangeRate/Delete"),
                    Content = new StringContent(json, Encoding.UTF8, MediaTypeNames.Application.Json)
                });

                return JsonSerializer.Deserialize<ExchangeDeleteRS>(await rs.Content.ReadAsStringAsync());
            }
            catch (Exception ex)
            {
                return new ExchangeDeleteRS();
            }
        }

        public async Task<ExchangeListRS> SelectExchange(Request request)
        {
            try
            {
                _client.DefaultRequestHeaders.Clear();

                var json = JsonSerializer.Serialize(request, new JsonSerializerOptions { IgnoreNullValues = true });

                var rs = await _client.SendAsync(new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri($"{_url}/v1/api/ExchangeRate/Select"),
                    Content = new StringContent(json, Encoding.UTF8, MediaTypeNames.Application.Json)
                });

                return JsonSerializer.Deserialize<ExchangeListRS>(await rs.Content.ReadAsStringAsync());
            }
            catch (Exception ex)
            {
                return new ExchangeListRS();
            }
        }

        public async Task<ExchangeRS> SelectByIdExchange(RequestWith<ExchangeRQ> request)
        {
            try
            {
                _client.DefaultRequestHeaders.Clear();

                var json = JsonSerializer.Serialize(request, new JsonSerializerOptions { IgnoreNullValues = true });

                var rs = await _client.SendAsync(new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri($"{_url}/v1/api/ExchangeRate/SelectBy"),
                    Content = new StringContent(json, Encoding.UTF8, MediaTypeNames.Application.Json)
                });

                return JsonSerializer.Deserialize<ExchangeRS>(await rs.Content.ReadAsStringAsync());
            }
            catch (Exception ex)
            {
                return new ExchangeRS();
            }
        }
    }
}