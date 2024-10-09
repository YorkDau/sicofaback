using sicf_BusinessHandlers.AzureBlogStorage.AzureBlogStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_BusinessHandlers.BusinessHandlers.AzureBlogStorage
{
    public class LocalStorageService : IFileManagerLogic
    {
        private readonly string _storagePath;

        public LocalStorageService(string storagePath)
        {
            _storagePath = storagePath;
        }

        public async Task Upload(FileModel model, string carpeta)
        {
            var directoryPath = Path.Combine(_storagePath, carpeta);
            Directory.CreateDirectory(directoryPath);
            var filePath = Path.Combine(directoryPath, model.PdfFile.FileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                await model.PdfFile.OpenReadStream().CopyToAsync(fileStream);
            }
        }

        public async Task<byte[]> GetPdfFile(string pdfFileName, string carpeta)
        {
            var filePath = Path.Combine(_storagePath, carpeta, pdfFileName);
            if (File.Exists(filePath))
            {
                return await File.ReadAllBytesAsync(filePath);
            }
            return new byte[0];
        }

        public Task DeleteBLOBFile(string filename)
        {
            var filePath = Path.Combine(_storagePath, filename);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            return Task.CompletedTask;
        }

        public Task<bool> Consultarfile(string carpeta, string archivo)
        {
            var filePath = Path.Combine(_storagePath, carpeta, archivo);
            return Task.FromResult(File.Exists(filePath));
        }
    }

}
