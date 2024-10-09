using Azure.Storage.Blobs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using System.Data.SqlClient;
using static sicf_Models.Constants.Constants;

namespace sicf_BusinessHandlers.AzureBlogStorage.AzureBlogStorage
{
    public class FileManagerLogic : IFileManagerLogic
    {
        private readonly BlobServiceClient _blobServiceClient;
        private IConfiguration configuration { get; set; }

        private string blobStorage { get; set; }
        public FileManagerLogic(BlobServiceClient blobServiceClient, IConfiguration configuration)
        {
            _blobServiceClient = blobServiceClient;
            blobStorage = configuration.GetSection("ConnectionStrings").GetSection("Storage").Value;
        }

        /// <summary>
        /// Metodo me permite guardar un documento en azure store o sobreescribir el mismo
        /// </summary>
        /// <param name="model"></param>
        /// <returns  name="response"></returns>
        public async Task Upload(FileModel model,string carpeta)
        {
            // var blobContainer = _blobServiceClient.GetBlobContainerClient(CargaDocumento.blobStorage);
            var blobContainer = _blobServiceClient.GetBlobContainerClient(blobStorage);
            var blobClient = blobContainer.GetBlobClient($"/{carpeta}/" + model.PdfFile.FileName);
            var response = await blobClient.UploadAsync(model.PdfFile.OpenReadStream(), overwrite: true);
            
        }

        public async Task<byte[]> GetPdfFile(string pdfFileName,string carpeta)
        {
            var response = new byte[0];
            // var blobContainer = _blobServiceClient.GetBlobContainerClient(CargaDocumento.blobStorage);

            var blobContainer = _blobServiceClient.GetBlobContainerClient(blobStorage);

            var blobClient = blobContainer.GetBlobClient($"/{carpeta}/"+pdfFileName);
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

        public async Task DeleteBLOBFile(string Filename)
        {
            var blobContainer = _blobServiceClient.GetBlobContainerClient("sicofadev");
            var blobClient = blobContainer.GetBlobClient(Filename);
            await blobClient.DeleteIfExistsAsync();
        }

        public async Task<bool> Consultarfile(string carpeta, string archivo) {

            
            var blobContainer = _blobServiceClient.GetBlobContainerClient("sicofadev");
            var blobClient = blobContainer.GetBlobClient($"/{carpeta}" + "/" + archivo);
            return   await blobClient.ExistsAsync();
        
        }


    }
}
