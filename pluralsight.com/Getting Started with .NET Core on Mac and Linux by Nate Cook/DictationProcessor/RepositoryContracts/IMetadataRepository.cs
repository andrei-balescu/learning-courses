using System.Threading.Tasks;
using DictationProcessor.DataContracts;

namespace DictationProcessor.RepositoryContracts
{
    public interface IMetadataRepository
    {
        Task<UploadMetadata[]> GetMetadata(string folderPath);

        Task SaveMetadata(UploadMetadata metadata, string filePath);
    }
}