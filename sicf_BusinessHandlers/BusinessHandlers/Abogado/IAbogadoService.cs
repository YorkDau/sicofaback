using sicf_Models.Core;
using sicf_Models.Dto.Abogado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sicf_BusinessHandlers.BusinessHandlers.Abogado
{
    public interface IAbogadoService
    {
        public InvolucradosDTO ObtenerInvolucrados(long idSolicitudServicio);

        public  Task RegistrarMedidaProteccion(RequestMedidaProteccionDTO data);

        public RequestMedidaProteccionDTO ObtenerInformacionMedidasProteccion(long idSolicitudServicio);

        public List<TestProcedure> Testprocedure();

        public  Task<List<TipoRemisionDTO>> ObtenerTiposRemision();

        public  Task<DocumentoRemisionDTO> ReporteRemision(long idSolicitudServicio, string reporte, long idVictima);

        public  Task<List<InvolucradoSelectDTO>> ObtenerListaInvolucrados(long idSolicitudServicio);

        public  Task<List<RemisionDisponiblesDTO>> RemisionesDisponiblesPorInvolucrado(long idInvolucrado,long? idSolicitud = null);

        public  Task<List<RemisionesAsociada>> RemisionesAsociadasPorSolicitud(long idSolicitud);

    }
}
