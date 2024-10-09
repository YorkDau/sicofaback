using sicf_BusinessHandlers.AzureBlogStorage.AzureBlogStorage;
using sicf_Models.Dto.Remision;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_BusinessHandlers.BusinessHandlers.Remision
{
    public interface IRemisionHandler
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="recomdSegRedApoyoEnt"></param>
        /// <returns></returns>
        public List<string> ValidarRecomdSegRedApoyoEntExt(RequestRecomdSegRedApoyoEntExtDto recomdSegRedApoyoEnt);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<string> UploadPDFToBlogStorage(FileModel model);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        Task<byte[]?> GetPdfFileFromBlogStarage(string pdfFileName);

    }
}
