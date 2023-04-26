﻿using Alura.Adopet.Console.Modelos;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace Alura.Adopet.Console.Servicos
{
    internal class HttpClientPet
    {
        HttpClient client;

        public HttpClientPet()
        {
            client = ConfiguraHttpClient("http://localhost:5057");
        }
        HttpClient ConfiguraHttpClient(string url)
        {
            HttpClient _client = new HttpClient();
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            _client.BaseAddress = new Uri(url);
            return _client;
        }

        public Task CreatePetAsync(Pet pet)
        {
            return client.PostAsJsonAsync("pet/add", pet);
        }
    }
}