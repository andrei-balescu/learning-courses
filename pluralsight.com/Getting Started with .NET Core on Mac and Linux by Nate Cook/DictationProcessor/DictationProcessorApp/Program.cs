using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
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

            string inputFolderPath = "../../../../../pluralsight.com/Getting Started with .NET Core on Mac and Linux by Nate Cook/dotnet-core-mac-linux-getting-started/m2/uploads";
            string outputFolderPath = "../../../../../pluralsight.com/Getting Started with .NET Core on Mac and Linux by Nate Cook/dotnet-core-mac-linux-getting-started/ready_for_transcription";

            Task asyncProgram = ProcessUploadFolder(inputFolderPath, outputFolderPath);
            asyncProgram.Wait();
        }

        private static async Task ProcessUploadFolder(string inputFolderPath, string outputFolderPath)
        {
            // cleaning up output folder
            Console.WriteLine("Deleting files in output folder");
            _fileRepository.DeleteAllFilesFromFolder(outputFolderPath);

            IEnumerable<string> subfolders = Directory.EnumerateDirectories(inputFolderPath);

            foreach (var subfolder in subfolders)
            {
                await _dictationProcessingService.ProcessFolder(subfolder, outputFolderPath);
            }
        }
    }
}
