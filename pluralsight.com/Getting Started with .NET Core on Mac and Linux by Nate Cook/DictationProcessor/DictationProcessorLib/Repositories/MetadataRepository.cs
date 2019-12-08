using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using DictationProcessorLib.DataContracts;
using DictationProcessorLib.RepositoryContracts;

namespace DictationProcessorLib.Repositories
{
    public class MetadataRepository : IMetadataRepository
    {
        public async Task<UploadMetadata[]> GetMetadata(string metadataFilePath)
        {
            using (FileStream metadataFileStream = File.OpenRead(metadataFilePath))
            {
                var uploadMetadata = await JsonSerializer.DeserializeAsync<UploadMetadata[]>(metadataFileStream);
                return uploadMetadata;
            }
        }

        public async Task SaveMetadata(UploadMetadata metadata, string metadataFilePath)
        {
            using (FileStream metadataFileStream = File.Create(metadataFilePath))
            {
                var serializerOptions = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };

                await JsonSerializer.SerializeAsync<UploadMetadata>(metadataFileStream, metadata, serializerOptions);
            }
        }
    }
}