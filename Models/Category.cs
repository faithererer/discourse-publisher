using Newtonsoft.Json;

namespace DiscoursePublisher.Models
{
    public class Category
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; } = string.Empty;

        [JsonProperty("slug")]
        public string Slug { get; set; } = string.Empty;

        [JsonProperty("description")]
        public string Description { get; set; } = string.Empty;
    }

    public class CategoryList
    {
        [JsonProperty("categories")]
        public List<Category> Categories { get; set; } = new List<Category>();
    }

    public class CategoriesResponse
    {
        [JsonProperty("category_list")]
        public CategoryList CategoryList { get; set; } = new CategoryList();
    }
}