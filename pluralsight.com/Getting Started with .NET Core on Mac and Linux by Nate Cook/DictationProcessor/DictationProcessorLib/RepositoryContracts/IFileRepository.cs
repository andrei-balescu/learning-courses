using System.Threading.Tasks;

namespace DictationProcessorLib.RepositoryContracts
{
    public interface IFileRepository
    {
        string GetChecksum(string filePath);

        Task CreateCompressedFile(string inputFilePath, string outputFilePath);

        void DeleteAllFilesFromFolder(string folderPath);
    }
}