using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using DictationProcessor.DataContracts;
using DictationProcessor.RepositoryContracts;

namespace DictationProcessor.Services
{
    public class DictationProcessingService
    {
        private IMetadataRepository _metadataRepository;

        private IFileRepository _fileRepository;

        public DictationProcessingService (IMetadataRepository metadataRepository, IFileRepository fileRepository)
        {
            _metadataRepository = metadataRepository;
            _fileRepository = fileRepository;
        }

        public async Task ProcessUploadFolder(string inputFolderPath, string outputFolderPath)
        {
            // cleaning up output folder
            Console.WriteLine("Deleting files in output folder");
            _fileRepository.DeleteAllFilesFromFolder(outputFolderPath);

            IEnumerable<string> subfolders = Directory.EnumerateDirectories(inputFolderPath);

            foreach (var subfolder in subfolders)
            {
                const string c_metadataFileName = "metadata.json";
                var metadataFilePath = Path.Combine(subfolder, c_metadataFileName);

                Console.WriteLine($"Reading {metadataFilePath}");

                UploadMetadata[] metadataCollection = await _metadataRepository.GetMetadata(metadataFilePath);

                foreach(UploadMetadata fileMetadata in metadataCollection)
                {
                    var audioFilePath = Path.Combine(subfolder, fileMetadata.File.FileName);

                    await ProcessFile(audioFilePath, outputFolderPath, fileMetadata);
                }
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