using sicf_DataBase.Repositories.Dominio;
using sicf_Models.Dto.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_BusinessHandlers.BusinessHandlers.Dominio
{
    public class DominioService : IDominioService
    {

        private IDominioRepository dominioRepository;

        public DominioService(IDominioRepository dominioRepository  )
        {
            this.dominioRepository = dominioRepository;
        }

        public List<DominioAgrupadoDTO> ListaDominio()
        {
            try
            {

               return   dominioRepository.ListaDominio();

            }
            catch (Exception ex) {


                throw new Exception(ex.Message);
            
            }
        }

        public async Task<List<DominioAsociadoDTO>> DominioPorGrupo(string data)
        {
            try
            {
                return await  dominioRepository.DominioPorGrupo(data);
            }
            catch (Exception ex) {

                throw new Exception(ex.Message);
            
            }
        
        }

        public async Task AgregarDominio(EntradaDominioDTO data)
        {
            try
            {
                 await dominioRepository.AgregarDominio(data);
            }
            catch (Exception ex) {

                throw new Exception(ex.Message);
            }
        }

        public async Task EditarDominio(DominioActualizarDTO data)
        {
            try
            {

              await  dominioRepository.EditarDominio(data);
            }
            catch (Exception ex) {

                throw new Exception(ex.Message);
            }
        
        }

        public async Task<DominioActualizarDTO> DominioDetalles(int id) {

            try
            {
                return await  dominioRepository.DominioDetalles(id);
            }
            catch (Exception ex) {

                throw new Exception(ex.Message);

            }

        }
    }
}
