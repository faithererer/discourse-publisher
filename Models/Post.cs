using System.Collections.Generic;

using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace DiscoursePublisher.Models
{
    public partial class Post : ObservableObject
    {
        [ObservableProperty]
        [JsonProperty("title")]
        private string _title = string.Empty;

        [ObservableProperty]
        [JsonProperty("content")]
        private string _content = string.Empty;

        [ObservableProperty]
        [JsonProperty("categoryId")]
        private int _categoryId;

        [ObservableProperty]
        [JsonProperty("tags")]
        private List<string> _tags = new();

        [ObservableProperty]
        private string _status = "Pending";
    }
}