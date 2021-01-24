using BlazorCRUDApp.Client.Features;
using BlazorCRUDApp.Shared.Entities.RequestFeatures;
using BlazorCRUDApp.Shared.Models;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace BlazorCRUDApp.Client.HttpRepository
{
    public class PersonHttpRepository : IPersonHttpRepository
    {
        private readonly HttpClient _client;

        public PersonHttpRepository(HttpClient client)
        {
            _client = client;
        }

        public async Task<PagingResponse<Person>> GetPeople(PersonParameters personParameters)
        {
            var queryStringParam = new Dictionary<string, string>
            {
                ["pageNumber"] = personParameters.PageNumber.ToString()
            };

            var response = await _client.GetAsync(QueryHelpers.AddQueryString("https://localhost:5001/api/people", queryStringParam));
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }

            var pagingResponse = new PagingResponse<Person>
            {
                Items = JsonSerializer.Deserialize<List<Person>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }),
                MetaData = JsonSerializer.Deserialize<MetaData>(response.Headers.GetValues("X-Pagination").First(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
            };

            return pagingResponse;
        }
    }
}
