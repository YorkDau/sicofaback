using sicf_Models.Dto.Comisaria;
using sicf_Models.Dto.Token;
using sicf_Models.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_DataBase.Repositories.Comisaria
{
    public interface IComisariaRepository
    {
        public  long IniciarComisaria(CreacionComisariaDTO data);
        public  Task<InformacionComisariaDTO> InformacionComisaria(int idComisaria);
        public  Task ActualizarComisaria(InformacionComisariaDTO data, int comisaria);
        public long ActualizarComisaria(ComisariaDTO data);
        public long? ValidarCodigoComisaria(string codigoComisaria);
        public long? ValidarnombreComisaria(string nombreComisaria);
        public long? ValidarCorreoComisario(string correoElectronico);
        public long? ValidarIdentificacionComisario(string numeroDocumento);
        public List<ComisariaDTO> ConsultarComisaria(RequestComisariaDTO data);
        public ComisarioDTO? ConsultarComisario(long idComisaria);
        public List<UsuarioComisariaDTO>? ConsutalUsuarioComisaria(long idComisaria);
        public List<ComisariaUsuario> ConsultaComisariasUsuario(int idUsuario);
        public Task<ControledResponseDTO> CrearMinisterio(CreacionComisariaDTO ministerio);
        public Task<List<InformacionComisariaDTO>> CargarComisarias(List<MComisariaDTO> comisarias);

        public Tuple<string, string> ObtenerNombreComisariayComisario(long id);
    }
}
