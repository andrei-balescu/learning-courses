using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using DictationProcessorLib.DataContracts;
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
            
            var configurationService = new ConfigurationService();
            AppSettings appSettings = configurationService.GetAppSettings();

            Task asyncProgram = ProcessNewUploads(appSettings);
            asyncProgram.Wait();
        }

        private static async Task ProcessNewUploads(AppSettings appSettings)
        {
            var fileSystemWatcher = new FileSystemWatcher(appSettings.InputFolderPath, "metadata.json")
            {
                IncludeSubdirectories = true
            };

            while (true)
            {
                var result = fileSystemWatcher.WaitForChanged(WatcherChangeTypes.Created);
                Console.WriteLine($"New metadata file {result.Name}");

                string fullMetadataFilePath = Path.Combine(appSettings.InputFolderPath, result.Name);
                string inputDirectoryName = Path.GetDirectoryName(fullMetadataFilePath);

                await _dictationProcessingService.ProcessFolder(inputDirectoryName, appSettings.OutputFolderPath);

                const int c_sleepTimeMilliseconds = 1000;
                Thread.Sleep(c_sleepTimeMilliseconds);
            }
        }
    }
}
