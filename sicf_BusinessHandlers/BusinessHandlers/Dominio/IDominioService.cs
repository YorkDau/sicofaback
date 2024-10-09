using sicf_Models.Dto.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_BusinessHandlers.BusinessHandlers.Dominio
{
    public interface IDominioService
    {
        public List<DominioAgrupadoDTO> ListaDominio();

        public Task<List<DominioAsociadoDTO>> DominioPorGrupo(string data);

        public Task AgregarDominio(EntradaDominioDTO data);

        public Task EditarDominio(DominioActualizarDTO data);

        public  Task<DominioActualizarDTO> DominioDetalles(int id);
    }
}
