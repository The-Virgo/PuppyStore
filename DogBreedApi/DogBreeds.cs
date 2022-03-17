using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DogBreedApi
{
    public static class DogBreed
    {
        private static readonly HttpClient _httpClient;

        static DogBreed()
        {
            _httpClient = new HttpClient();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="HttpRequestException">Thrown is call is unsuccesful</exception>
        /// <returns></returns>
        public static async Task<List<Root>> GetBreedsAsync(string apiKey)
        {
            HttpResponseMessage response = await _httpClient.GetAsync("https://api.thedogapi.com/v1/breeds?attach_breed=0");
            response.Headers.Add("x-api-key", apiKey);
            response.EnsureSuccessStatusCode();
            string responseText = await response.Content.ReadAsStringAsync();
            List<Root> responseList = JsonConvert.DeserializeObject<List<Root>>(responseText);
            return responseList;
        }

        public static async Task<Root> GetBreedAsync(string apiKey, string id)
        {
            HttpResponseMessage response = await _httpClient.GetAsync("https://api.thedogapi.com/v1/breeds/" + id);
            response.Headers.Add("x-api-key", apiKey);
            response.EnsureSuccessStatusCode();
            string responseText = await response.Content.ReadAsStringAsync();
            Root breed = JsonConvert.DeserializeObject<Root>(responseText);
            return breed;
        }
    }
 
    public class Weight
    {
        [JsonProperty("imperial")]
        public string Imperial { get; set; }

        [JsonProperty("metric")]
        public string Metric { get; set; }
    }

    public class Height
    {
        [JsonProperty("imperial")]
        public string Imperial { get; set; }

        [JsonProperty("metric")]
        public string Metric { get; set; }
    }

    public class Image
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("width")]
        public int Width { get; set; }

        [JsonProperty("height")]
        public int Height { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }
    }

    public class Root
    {
        [JsonProperty("weight")]
        public Weight Weight { get; set; }

        [JsonProperty("height")]
        public Height Height { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("bred_for")]
        public string BredFor { get; set; }

        [JsonProperty("breed_group")]
        public string BreedGroup { get; set; }

        [JsonProperty("life_span")]
        public string LifeSpan { get; set; }

        [JsonProperty("temperament")]
        public string Temperament { get; set; }

        [JsonProperty("origin")]
        public string Origin { get; set; }

        [JsonProperty("reference_image_id")]
        public string ReferenceImageId { get; set; }

        [JsonProperty("image")]
        public Image Image { get; set; }
    }
}
