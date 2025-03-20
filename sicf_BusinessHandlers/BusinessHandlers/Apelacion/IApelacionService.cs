using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using sicf_Models.Dto.Apelacion;
using sicf_Models.Core;

namespace sicf_BusinessHandlers.BusinessHandlers.Apelacion
{
    public interface IApelacionService
    {
        public Task<SicofaApelacion> ObtenerApelacion(ApelacionObtencionDTO apelacion);
        public Task<bool> ActualizarApelacion(ApelacionDTO apelacion);
        public Task<bool> CerrarActuacionApelacion(long idTarea);
        public List<ApelacionMedidasDTO> ConsultarMedidasApelacion(long idSolicitudServicio);
        public List<ApelacionTareasDTO> ConsultarTareasApelacion(long idSolicitudServicio);
    }

    public interface IApelacion_Service
    {
        public List<ApelacioneReponseDTO> ConsultarApelaciones(ConsultarApelacionObtencionDTO apelacion);
        public List<SicofaObservacionSolicitudApelacion> ConsultarObservacionesApelaciones(int id_solicitud_servicio);
        public SicofaObservacionSolicitudApelacion GuardarObservacionesApelaciones(ObservacionSolicitudApelacionRequest request);
    }
}
