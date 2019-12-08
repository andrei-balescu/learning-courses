using System;
using System.IO;
using System.IO.Compression;
using System.Security.Cryptography;
using System.Threading.Tasks;
using DictationProcessorLib.RepositoryContracts;

namespace DictationProcessorLib.Repositories
{
    public class FileRepository : IFileRepository
    {
        public async Task CreateCompressedFile(string inputFilePath, string outputFilePath)
        {
            using (FileStream inputFileStream = File.OpenRead(inputFilePath))
            {
                using (FileStream outputFileStream = File.Create(outputFilePath))
                {
                    using (var gzipStream = new GZipStream(outputFileStream, CompressionLevel.Optimal))
                    {
                        await inputFileStream.CopyToAsync(gzipStream);
                    }
                }
            }
        }

        public void DeleteAllFilesFromFolder(string folderPath)
        {
            string[] filesToDelete = Directory.GetFiles(folderPath);

            foreach (string filePath in filesToDelete)
            {
                File.Delete(filePath);
            }
        }

        public string GetChecksum(string filePath)
        {
            using (FileStream fileStream = File.OpenRead(filePath))
            {
                using (MD5 md5 = MD5.Create())
                {
                    byte[] md5bytes = md5.ComputeHash(fileStream);
                    string md5Checksum = BitConverter.ToString(md5bytes);

                    return md5Checksum;
                }
            }
        }
    }
}