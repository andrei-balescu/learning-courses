using System.Threading.Tasks;
using DictationProcessorLib.DataContracts;

namespace DictationProcessorLib.RepositoryContracts
{
    public interface IMetadataRepository
    {
        Task<UploadMetadata[]> GetMetadata(string folderPath);

        Task SaveMetadata(UploadMetadata metadata, string filePath);
    }
}