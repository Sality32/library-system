using Application.DTOs;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Application.Services
{
    public class GutendexApiService
    {
        private readonly HttpClient httpClient;
        private readonly IConfiguration configuration;

        public GutendexApiService(HttpClient httpClient, IConfiguration configuration)
        {
            this.httpClient = httpClient;
            this.configuration = configuration;
        }

        public async Task<ApiExternalResponseDto> GetBooksByYear(int page)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://gutendex.com/books?topic=Science")
            };
                
            try
            {
                var response = await this.httpClient.GetAsync($"https://gutendex.com/books?sort=ascending&page={page}");
                if (!response.IsSuccessStatusCode)
                {
                    // Handle non-success responses here
                    throw new Exception($"Error: {response.StatusCode}");
                }
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<ApiExternalResponseDto>(content);


            }
            catch(Exception e)
            {
                throw new Exception("Error While Calling External API", e);
            }
        }
    }
}