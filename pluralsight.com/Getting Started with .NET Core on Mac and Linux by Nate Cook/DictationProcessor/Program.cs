using System.Threading.Tasks;
using DictationProcessor.Repositories;
using DictationProcessor.Services;

namespace DictationProcessor
{
    class Program
    {
        static void Main(string[] args)
        {
            var metadataRepository = new MetadataRepository();
            var fileRepository = new FileRepository();
            var dictationProcessingService = new DictationProcessingService(metadataRepository, fileRepository);

            string inputFolderPath = "../../../../pluralsight.com/Getting Started with .NET Core on Mac and Linux by Nate Cook/dotnet-core-mac-linux-getting-started/m2/uploads";
            string outputFolderPath = "../../../../pluralsight.com/Getting Started with .NET Core on Mac and Linux by Nate Cook/dotnet-core-mac-linux-getting-started/ready_for_transcription";

            Task asyncProgram = dictationProcessingService.ProcessUploadFolder(inputFolderPath, outputFolderPath);
            asyncProgram.Wait();
        }
    }
}
