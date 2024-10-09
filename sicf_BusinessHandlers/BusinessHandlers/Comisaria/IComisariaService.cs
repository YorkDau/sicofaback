using sicf_Models.Dto.Comisaria;
using sicf_Models.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_BusinessHandlers.BusinessHandlers.Comisaria
{
    public interface IComisariaService
    {
        public  string IniciarComisaria(CreacionComisariaDTO data);
        public  Task<InformacionComisariaDTO> InformacionComisaria(int idComisaria);
        public  Task ActualizarComisaria(InformacionComisariaDTO data, int comisaria);
        public List<ComisariaDTO> ConsultarComisaria(RequestComisariaDTO data);
        public ComisarioDTO? ConsultarComisario(long idComisaria);
        public List<UsuarioComisariaDTO>? ConsutalUsuarioComisaria(long idComisaria);
        public Task<ControledResponseDTO> CrearMinisterio(CreacionComisariaDTO ministerio);
        public Task<List<InformacionComisariaDTO>> CargarComisarias(List<MComisariaDTO> comisarias);

        public Tuple<string, string> ObtenerNombreComisariayComisario(long id);
    }
}
