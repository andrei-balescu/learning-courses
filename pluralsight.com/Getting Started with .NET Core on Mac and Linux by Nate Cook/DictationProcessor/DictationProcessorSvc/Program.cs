using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using DictationProcessorLib.Repositories;
using DictationProcessorLib.RepositoryContracts;
using DictationProcessorLib.ServiceContracts;
using DictationProcessorLib.Services;

namespace DictationProcessorSvc
{
    class Program
    {
        private static IDictationProcessingService _dictationProcessingService;
        private static IFileRepository _fileRepository;

        static void Main(string[] args)
        {
            var metadataRepository = new MetadataRepository();
            _fileRepository = new FileRepository();
            _dictationProcessingService = new DictationProcessingService(metadataRepository, _fileRepository);

            string inputFolderPath = "../../../../../pluralsight.com/Getting Started with .NET Core on Mac and Linux by Nate Cook/dotnet-core-mac-linux-getting-started/m3/demos/uploads";
            string outputFolderPath = "../../../../../pluralsight.com/Getting Started with .NET Core on Mac and Linux by Nate Cook/dotnet-core-mac-linux-getting-started/ready_for_transcription";

            Task asyncProgram = ProcessNewUploads(inputFolderPath, outputFolderPath);
            asyncProgram.Wait();
        }

        private static async Task ProcessNewUploads(string inputFolderPath, string outputFolderPath)
        {
            var fileSystemWatcher = new FileSystemWatcher(inputFolderPath, "metadata.json")
            {
                IncludeSubdirectories = true
            };

            while (true)
            {
                var result = fileSystemWatcher.WaitForChanged(WatcherChangeTypes.Created);
                Console.WriteLine($"New metadata file {result.Name}");

                string fullMetadataFilePath = Path.Combine(inputFolderPath, result.Name);
                string inputDirectoryName = Path.GetDirectoryName(fullMetadataFilePath);

                await _dictationProcessingService.ProcessFolder(inputDirectoryName, outputFolderPath);

                const int c_sleepTimeMilliseconds = 1000;
                Thread.Sleep(c_sleepTimeMilliseconds);
            }
        }
    }
}
