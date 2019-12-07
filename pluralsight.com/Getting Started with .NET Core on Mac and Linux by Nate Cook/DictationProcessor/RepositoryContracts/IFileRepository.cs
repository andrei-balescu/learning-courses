using System.Threading.Tasks;

namespace DictationProcessor.RepositoryContracts
{
    public interface IFileRepository
    {
        string GetChecksum(string filePath);

        Task CreateCompressedFile(string inputFilePath, string outputFilePath);

        void DeleteAllFilesFromFolder(string folderPath);
    }
}