using Microsoft.AspNetCore.Mvc;
using sicf_BusinessHandlers.AzureBlogStorage.AzureBlogStorage;
using sicf_DataBase.Remision;
using sicf_Models.Dto.Remision;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static sicf_Models.Constants.Constants;

namespace sicf_BusinessHandlers.BusinessHandlers.Remision
{
    public class RemisionHandler : IRemisionHandler
    {
        private readonly IRemisionRepository _remisionRepository;
        private readonly IFileManagerLogic _fileManagerLogic;

        public RemisionHandler(IRemisionRepository remisionRepository, IFileManagerLogic fileManagerLogic)
        {

            this._remisionRepository = remisionRepository;
            this._fileManagerLogic = fileManagerLogic;
        }
        public List<string> ValidarRecomdSegRedApoyoEntExt(RequestRecomdSegRedApoyoEntExtDto recomdSegRedApoyoEnt)
        {

            try
            {
                List<string> errors = new List<string>();

                if (recomdSegRedApoyoEnt.SegCheckbox1 == false && recomdSegRedApoyoEnt.SegCheckbox2 == false && recomdSegRedApoyoEnt.SegCheckbox3 == false
                    && recomdSegRedApoyoEnt.SegCheckbox4 == false && recomdSegRedApoyoEnt.SegCheckbox5 == false && recomdSegRedApoyoEnt.SegCheckbox6 == false
                    && recomdSegRedApoyoEnt.SegCheckbox7 == false && recomdSegRedApoyoEnt.SegCheckbox8 == false && recomdSegRedApoyoEnt.SegCheckbox9 == false
                    && recomdSegRedApoyoEnt.RedApoyCheckbox1 == false && recomdSegRedApoyoEnt.RedApoyCheckbox2 == false)
                {
                    errors.Add(Message.RequeridoRequestRecomdSegRedApoyoEntExt);
                }

                return errors;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task<string> UploadPDFToBlogStorage(FileModel model)
        {
            try
            {
                if (model.PdfFile != null)
                {
                    await _fileManagerLogic.Upload(model,"carpeta");
                }
                return Message.Ok;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<byte[]?> GetPdfFileFromBlogStarage(string pdfFileName)
        {
            try
            {
                var imgBytes = await _fileManagerLogic.GetPdfFile(pdfFileName,"carpeta");
                return imgBytes;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
