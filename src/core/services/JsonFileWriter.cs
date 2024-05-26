using System.Text.Json;
using BLBConcordance.Core.Interfaces;

namespace BLBConcordance.Core.Services
{
    public sealed class JsonFileWriter : IFileWriter
    {
        public void WriteToFile(string path, object data)
        {
            var jsonString = JsonSerializer.Serialize(data);
            File.WriteAllText(path, jsonString);
        }
    }
}