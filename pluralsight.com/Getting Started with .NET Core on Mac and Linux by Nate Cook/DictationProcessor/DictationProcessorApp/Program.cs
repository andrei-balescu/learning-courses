using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using DictationProcessorLib.DataContracts;
using DictationProcessorLib.Repositories;
using DictationProcessorLib.RepositoryContracts;
using DictationProcessorLib.ServiceContracts;
using DictationProcessorLib.Services;

namespace DictationProcessorApp
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

            Task<string> asyncProgram = ProcessUploadFolder(appSettings);
            string completedMessage = asyncProgram.Result;
            
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                GtkHelper.DisplayAlert(completedMessage);
            }
        }

        private static async Task<string> ProcessUploadFolder(AppSettings appSettings)
        {
            // cleaning up output folder
            Console.WriteLine("Deleting files in output folder");
            _fileRepository.DeleteAllFilesFromFolder(appSettings.OutputFolderPath);

            IEnumerable<string> subfolders = Directory.EnumerateDirectories(appSettings.InputFolderPath);

            int numberOfProcessedFolders = 0;

            foreach (var subfolder in subfolders)
            {
                await _dictationProcessingService.ProcessFolder(subfolder, appSettings.OutputFolderPath);
                numberOfProcessedFolders++;
            }
            string completedMessage = ($"{numberOfProcessedFolders} folders were processed");
            return completedMessage;
        }
    }
}
