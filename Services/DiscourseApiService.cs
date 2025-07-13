using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using DotNetEnv;
using Newtonsoft.Json;
using DiscoursePublisher.Models;
using System.Collections.Generic;

namespace DiscoursePublisher.Services
{
    public class DiscourseApiService
    {
        private const string PostsEndpoint = "/posts.json";
        private const string CategoriesEndpoint = "/categories.json";
        private const string TagsEndpoint = "/tags.json";

        private readonly HttpClient _httpClient;

        public DiscourseApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<HttpResponseMessage> CreatePostAsync(string title, string content, int categoryId, string[] tags)
        {
            var postData = new
            {
                title = title,
                raw = content,
                category = categoryId,
                tags = tags
            };

            string jsonPayload = JsonConvert.SerializeObject(postData);
            var requestContent = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

            // Log the request payload for debugging
            System.Diagnostics.Debug.WriteLine($"Discourse API Request: {jsonPayload}");

            HttpResponseMessage response = await _httpClient.PostAsync(PostsEndpoint, requestContent);
            return response;
        }

        public async Task<List<Category>> GetCategoriesAsync()
        {
            HttpResponseMessage response = await _httpClient.GetAsync(CategoriesEndpoint);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            var categoriesResponse = JsonConvert.DeserializeObject<CategoriesResponse>(responseBody);
            return categoriesResponse?.CategoryList?.Categories ?? new List<Category>();
        }

        public async Task<List<Tag>> GetTagsAsync()
        {
            HttpResponseMessage response = await _httpClient.GetAsync(TagsEndpoint);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            var tagsResponse = JsonConvert.DeserializeObject<TagsResponse>(responseBody);
            return tagsResponse?.Tags ?? new List<Tag>();
        }
    }
}