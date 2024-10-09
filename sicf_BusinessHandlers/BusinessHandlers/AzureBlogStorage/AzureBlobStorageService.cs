using Azure.Storage.Blobs;
using Microsoft.Extensions.Configuration;
using sicf_BusinessHandlers.AzureBlogStorage.AzureBlogStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_BusinessHandlers.BusinessHandlers.AzureBlogStorage
{
    public class AzureBlobStorageService : IFileManagerLogic
    {
        private readonly BlobServiceClient _blobServiceClient;
        private readonly string _blobStorage;

        public AzureBlobStorageService(BlobServiceClient blobServiceClient, IConfiguration configuration)
        {
            _blobServiceClient = blobServiceClient;
            _blobStorage = configuration.GetSection("ConnectionStrings").GetSection("Storage").Value;
        }

        public async Task Upload(FileModel model, string carpeta)
        {
            var blobContainer = _blobServiceClient.GetBlobContainerClient(_blobStorage);
            var blobClient = blobContainer.GetBlobClient($"/{carpeta}/" + model.PdfFile.FileName);
            await blobClient.UploadAsync(model.PdfFile.OpenReadStream(), overwrite: true);
        }

        public async Task<byte[]> GetPdfFile(string pdfFileName, string carpeta)
        {
            var response = new byte[0];
            var blobContainer = _blobServiceClient.GetBlobContainerClient(_blobStorage);
            var blobClient = blobContainer.GetBlobClient($"/{carpeta}/" + pdfFileName);
            var isExist = await blobClient.ExistsAsync();
            if (isExist)
            {
                var downloadContent = await blobClient.DownloadAsync();
                using (MemoryStream ms = new MemoryStream())
                {
                    await downloadContent.Value.Content.CopyToAsync(ms);
                    return ms.ToArray();
                }
            }
            return response.ToArray();
        }

        public async Task DeleteBLOBFile(string filename)
        {
            var blobContainer = _blobServiceClient.GetBlobContainerClient(_blobStorage);
            var blobClient = blobContainer.GetBlobClient(filename);
            await blobClient.DeleteIfExistsAsync();
        }

        public async Task<bool> Consultarfile(string carpeta, string archivo)
        {
            var blobContainer = _blobServiceClient.GetBlobContainerClient(_blobStorage);
            var blobClient = blobContainer.GetBlobClient($"/{carpeta}/" + archivo);
            return await blobClient.ExistsAsync();
        }
    }

}
