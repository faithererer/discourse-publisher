using System;
using System.IO;

namespace DiscoursePublisher.Services
{
    public static class ImporterFactory
    {
        public static IImporter? GetImporter(string filePath)
        {
            string extension = Path.GetExtension(filePath).ToLowerInvariant();
            switch (extension)
            {
                case ".json":
                    return new JsonImporter();
                // Future importers can be added here
                // case ".md":
                //     return new MarkdownImporter();
                // case ".csv":
                //     return new CsvImporter();
                default:
                    return null;
            }
        }
    }
}