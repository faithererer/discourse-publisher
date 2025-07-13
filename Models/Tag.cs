using Newtonsoft.Json;
using System.Collections.Generic;

namespace DiscoursePublisher.Models
{
    public class Tag
    {
        [JsonProperty("id")]
        public string Id { get; set; } = string.Empty; // Tag ID is a string (the name)

        [JsonProperty("text")]
        public string Text { get; set; } = string.Empty;

        [JsonProperty("count")]
        public int Count { get; set; }
    }

    public class TagsResponse
    {
        [JsonProperty("tags")]
        public List<Tag> Tags { get; set; } = new List<Tag>();
    }
}