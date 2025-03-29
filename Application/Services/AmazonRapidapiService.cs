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
    public class AmazonRapidapiService
    {
        private readonly HttpClient httpClient;
        private readonly IConfiguration configuration;

        public AmazonRapidapiService(HttpClient httpClient, IConfiguration configuration)
        {
            this.httpClient = httpClient;
            this.configuration = configuration;
        }

        public async Task<List<Book>> GetBooksByYear(string topic)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://gutendex.com/books?topic=Science")
            };
                
            try
            {   
                using (var response = await httpClient.SendAsync(request))
                {
                    response.EnsureSuccessStatusCode();
                    var body = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(body);

                    var result = JsonConvert.DeserializeObject<>(body);
                    result.results;
                }

                return [];


            }
            catch(Exception e)
            {
                throw new Exception("Error While Calling External API", e);
            }
        }
    }
}