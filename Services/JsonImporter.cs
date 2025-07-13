using DiscoursePublisher.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace DiscoursePublisher.Services
{
    public class JsonImporter : IImporter
    {
        public async Task<IEnumerable<Post>> ImportAsync(string filePath)
        {
            string jsonContent = await File.ReadAllTextAsync(filePath);
            var posts = JsonConvert.DeserializeObject<List<Post>>(jsonContent);
            return posts ?? new List<Post>();
        }
    }
}