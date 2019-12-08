using System.Threading.Tasks;

namespace DictationProcessorLib.ServiceContracts
{
    public interface IDictationProcessingService
    {
        Task ProcessFolder(string inputFolderPath, string outputFolderPath);
    }
}