using DiscoursePublisher.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiscoursePublisher.Services
{
    public interface IImporter
    {
        Task<IEnumerable<Post>> ImportAsync(string filePath);
    }
}