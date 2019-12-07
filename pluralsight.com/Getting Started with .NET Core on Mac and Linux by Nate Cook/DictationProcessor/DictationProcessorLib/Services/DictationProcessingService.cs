using System;
using System.IO;
using System.Threading.Tasks;
using DictationProcessorLib.DataContracts;
using DictationProcessorLib.RepositoryContracts;
using DictationProcessorLib.ServiceContracts;

namespace DictationProcessorLib.Services
{
    public class DictationProcessingService : IDictationProcessingService
    {
        private IMetadataRepository _metadataRepository;

        private IFileRepository _fileRepository;

        public DictationProcessingService(IMetadataRepository metadataRepository, IFileRepository fileRepository)
        {
            _metadataRepository = metadataRepository;
            _fileRepository = fileRepository;
        }

        public async Task ProcessFolder(string inputFolderPath, string outputFolderPath)
        {
            const string c_metadataFileName = "metadata.json";
            var metadataFilePath = Path.Combine(inputFolderPath, c_metadataFileName);

            Console.WriteLine($"Reading {metadataFilePath}");

            UploadMetadata[] metadataCollection = await _metadataRepository.GetMetadata(metadataFilePath);

            foreach (UploadMetadata fileMetadata in metadataCollection)
            {
                var audioFilePath = Path.Combine(inputFolderPath, fileMetadata.File.FileName);

                await ProcessFile(audioFilePath, outputFolderPath, fileMetadata);
            }
        }

        private async Task ProcessFile(string inputFilePath, string outputFolderPath, UploadMetadata fileMetadata)
        {
            // verify checksum
            string md5Checksum = _fileRepository.GetChecksum(inputFilePath);
            string formattedChecksum = md5Checksum.Replace("-", string.Empty).ToLower();

            if (formattedChecksum != fileMetadata.File.Md5Checksum)
            {
                var errorMessage = "Checksum verification failed. File corrupted?";
                throw new Exception(errorMessage);
            }

            // create unique ID
            string fileExtension = Path.GetExtension(inputFilePath);
            Guid uniqueId = Guid.NewGuid();
            string outputFileName = $"{uniqueId}{fileExtension}";

            // compress file
            string outputCompressedFileName = $"{outputFileName}.gz";
            var outputCompressedFilePath = Path.Combine(outputFolderPath, outputCompressedFileName);
            fileMetadata.File.FileName = outputCompressedFilePath;

            Console.WriteLine($"Creating output file {outputCompressedFileName}");
            await _fileRepository.CreateCompressedFile(inputFilePath, outputCompressedFilePath);

            // create file metadata
            string ouptutMetadataFileName = $"{outputFileName}.json";
            string outputMetadataFilePath = Path.Combine(outputFolderPath, ouptutMetadataFileName);

            Console.WriteLine($"Creating output metadata {ouptutMetadataFileName}");
            await _metadataRepository.SaveMetadata(fileMetadata, outputMetadataFilePath);
        }
    }
}