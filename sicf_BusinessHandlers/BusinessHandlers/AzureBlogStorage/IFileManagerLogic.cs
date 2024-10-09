namespace sicf_BusinessHandlers.AzureBlogStorage.AzureBlogStorage
{
    public interface IFileManagerLogic
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task Upload(FileModel model, string carpeta);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pdfFileName"></param>
        /// <returns></returns>
        Task<byte[]> GetPdfFile(string pdfFileName, string carpeta);

        Task DeleteBLOBFile(string Filename);

        Task<bool> Consultarfile(string carpeta, string archivo);
    }
}
